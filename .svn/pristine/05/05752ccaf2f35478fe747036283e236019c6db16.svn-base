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
using ZWL.Common;

public partial class GongGao_GongGaoAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //设置上传的附件为空
            DataEntityDataContext context = new DataEntityDataContext();
            var T = context.ERPCommon.Where(p => p.Code == "QDDLX").OrderBy(p=>p.CSort);

            foreach(var item in T)
            {
                this.DropDownList1.Items.Add(new ListItem(item.CName, item.CName));
            }
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../QDGL/POI.aspx" : Request.UrlReferrer.ToString();

        }
    }
   
    protected void btn_Sub_Click(object sender, EventArgs e) {
        DataEntityDataContext context = new DataEntityDataContext();
        POIXX Model = new POIXX();
        Model.MC = this.TextBox1.Text;
        Model.WHRID = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        Model.X = this.TextBox3.Text;
        Model.Y = this.TextBox4.Text;
        Model.BZ2 = this.TextBox5.Text;
        Model.LX = this.DropDownList1.SelectedValue;
        Model.DZ = this.TxtContent.Text;
        Model.BZ1 = this.ZT.SelectedValue;
        Model.WHSJ = DateTime.Now;
        Model.WHR = ZWL.Common.PublicMethod.GetSessionValue("TrueName");
        context.POIXX.InsertOnSubmit(Model);
        context.SubmitChanges();

        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户添加POI信息(" + this.TextBox1.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('POI信息添加成功！');window.location.href='POI.aspx'</script>");
    }
}