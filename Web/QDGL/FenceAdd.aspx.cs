using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZWL.Common;

public partial class QDGL_FenceAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //设置上传的附件为空
            DataEntityDataContext context = new DataEntityDataContext();
            var T = context.ERPCommon.Where(p => p.Code == "QDDLX").OrderBy(p => p.CSort);

            foreach (var item in T)
            {
                this.ddl_LX.Items.Add(new ListItem(item.CName, item.CName));
            }
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../QDGL/Fence.aspx" : Request.UrlReferrer.ToString();
        }
    }
   
    protected void btn_Sub_Click(object sender, EventArgs e) {
        DataEntityDataContext context = new DataEntityDataContext();
        FenceXX Model = new FenceXX();
        Model.MC = this.txt_MC.Text;
        Model.WHRID = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        Model.Coords = this.txt_Coords.Text;
        Model.FenceUser = this.UserName_Input.Value;
        Model.BZ2 = this.txt_BZ2.Text;
        Model.LX = this.ddl_LX.SelectedValue;
        Model.DZ = this.txt_DZ.Text;
        Model.BZ1 = this.ZT.SelectedValue;
        Model.WHSJ = DateTime.Now;
        Model.WHR = ZWL.Common.PublicMethod.GetSessionValue("TrueName");
        context.FenceXX.InsertOnSubmit(Model);
        context.SubmitChanges();

        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户添加电子围栏(" + this.txt_MC.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('电子围栏添加成功！');window.location.href='Fence.aspx'</script>");
        //ZWL.Common.MessageBox.ShowAndRedirect(this, "电子围栏添加成功！", "Fence.aspx");
    }
}