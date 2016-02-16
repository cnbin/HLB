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

public partial class SystemManage_SystemUserSetDep : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            this.Label1.Text = Request.QueryString["UserName"].ToString()+"("+ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 [TrueName] from ERPUser where UserName='" + Request.QueryString["UserName"].ToString() + "'")+")";
            this.UserName_Input.Value = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 XiaShuUser from ERPUser where UserName='" + Request.QueryString["UserName"].ToString() + "'");
            string UserNames = "'" + UserName_Input.Value.Replace(",","','")+"'";
            DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select TrueName from  ERPUser where UserName in (" + UserNames+")");
            string TrueName = "";
            foreach(DataRow R in dt.Rows)
            {
                TrueName += TrueName == "" ? R["TrueName"].ToString() : "," + R["TrueName"].ToString();
            }
            this.TextBox1.Text = TrueName;

            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/SystemUser.aspx" : Request.UrlReferrer.ToString();
            lb_input.Value = Request.QueryString["UserName"].ToString();
        }
    }
  
    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.DBUtility.DbHelperSQL.ExecuteSQL("update ERPUser set XiaShuUser='" + this.UserName_Input.Value.Trim() + "' where UserName='" + Request.QueryString["UserName"].ToString() + "'");


        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户设置下属员工(" + this.UserName_Input.Value + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('下属员工设置成功！');window.location.href='SystemUser.aspx'</script>");
    }
}
