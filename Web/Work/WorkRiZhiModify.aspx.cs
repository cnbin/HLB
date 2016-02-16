using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Work_WorkRiZhiModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //绑定页面数据
            ZWL.BLL.ERPWorkRiZhi Model = new ZWL.BLL.ERPWorkRiZhi();
            Model.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            this.TextBox1.Text = Model.TitleStr;
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../Work/WorkRiZhi.aspx" : Request.UrlReferrer.ToString();
        }
    }
  
    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.BLL.ERPWorkRiZhi Model = new ZWL.BLL.ERPWorkRiZhi();
        Model.ID = int.Parse(Request.QueryString["ID"].ToString());
        Model.TitleStr = this.TextBox1.Text;
        Model.ContentStr = "";
        Model.TypeStr = ZWL.Common.PublicMethod.GetSessionValue("Department");
        Model.TimeStr = DateTime.Now;
        Model.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        Model.Update();

        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户修改分享信息(" + this.TextBox1.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();

        ZWL.Common.MessageBox.ShowAndRedirect(this, "分享信息修改成功！", "WorkRiZhi.aspx");
    }
}