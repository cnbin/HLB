using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class QDGL_FenceModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            DataEntityDataContext context = new DataEntityDataContext();
            FenceXX Model = context.FenceXX.SingleOrDefault(f => f.ID == int.Parse(Request.QueryString["ID"].ToString()));
            var T = context.ERPCommon.Where(p => p.Code == "QDDLX").OrderBy(p => p.CSort);

            foreach (var item in T)
            {
                this.ddl_LX.Items.Add(new ListItem(item.CName, item.CName));
            }
            this.txt_MC.Text = Model.MC;

            string Coords = "";
            txt_Coords.Height=20;
            string[] CoordsList= Model.Coords.Split(';');
            for(int i = 0,k = 0; i < CoordsList.Length; i++) {
                if((i) % 2 == 0) {
                    k++;
                    Coords += "\r\n";
                    txt_Coords.Height = 20*k;
                }
                Coords += CoordsList[i]+";";
            }
            
            this.txt_Coords.Text = Coords;
            string FenceUsers ="'"+ Model.FenceUser.Replace(",","','")+"'";
            DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [TrueName] from ERPUser Where UserName  in (" + FenceUsers + ")");
            string TrueName = ""; 
            foreach(DataRow R in dt.Rows) {
                TrueName += TrueName == "" ? R["TrueName"] : "," + R["TrueName"];
            }
            this.txt_FenceUser.Text = TrueName;
            this.txt_BZ2.Text = Model.BZ2;
            this.ddl_LX.SelectedValue = Model.LX;
            this.txt_DZ.Text = Model.DZ;
            this.ZT.SelectedValue = Model.BZ1;
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../QDGL/Fence.aspx" : Request.UrlReferrer.ToString();
        }
    }
    
    protected void btn_Sub_Click(object sender, EventArgs e) {
        DataEntityDataContext context = new DataEntityDataContext();
        FenceXX Model = context.FenceXX.SingleOrDefault(f => f.ID == int.Parse(Request.QueryString["ID"].ToString()));
        Model.MC = this.txt_MC.Text;
        Model.WHRID = ZWL.Common.PublicMethod.GetSessionValue("UserName");

        string Coords = this.txt_Coords.Text.Replace("\r", "").Replace("\n", "");
        Model.Coords = Coords.Remove(Coords.Length - 1);

        Model.FenceUser = this.UserName_Input.Value;
        Model.BZ2 = this.txt_BZ2.Text;
        Model.LX = this.ddl_LX.SelectedValue;
        Model.DZ = this.txt_DZ.Text;
        Model.BZ1 = this.ZT.SelectedValue;
        Model.WHSJ = DateTime.Now;
        Model.WHR = ZWL.Common.PublicMethod.GetSessionValue("TrueName");
        context.SubmitChanges();


        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户修改电子围栏信息(" + this.txt_MC.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('电子围栏信息修改成功！');window.location.href='Fence.aspx'</script>");
        //ZWL.Common.MessageBox.ShowAndRedirect(this, "电子围栏信息修改成功！", "Fence.aspx");
    }
}