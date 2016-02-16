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

public partial class Personal_ChangPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            this.Label1.Text = ZWL.Common.PublicMethod.GetSessionValue("UserName");

            //设定按钮权限
            btn_Sub.Visible = ZWL.Common.PublicMethod.StrIFIn("|012M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
    }
   
    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.BLL.ERPUser MyModel = new ZWL.BLL.ERPUser();
        if(ZWL.Common.DEncrypt.DESEncrypt.Encrypt(this.txt_pwd.Text) == ZWL.Common.PublicMethod.GetSessionValue("Password")) {
            MyModel.ID = int.Parse(ZWL.Common.PublicMethod.GetSessionValue("UserID"));
            MyModel.UserPwd = ZWL.Common.DEncrypt.DESEncrypt.Encrypt(this.TextBox1.Text);
            MyModel.UpdatePwd();
            ZWL.Common.MessageBox.Show(this, "修改用户密码成功！");


            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户修改密码";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
        }
        else {
            ZWL.Common.MessageBox.Show(this, "旧密码错误！");
        }
    }
}