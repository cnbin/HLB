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

public partial class SystemManage_BuMenInfoModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            ZWL.BLL.ERPBuMen MyBuMen = new ZWL.BLL.ERPBuMen();
            MyBuMen.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            if (MyBuMen!=null)
            {
                this.TextBox1.Text = MyBuMen.BuMenName;
                string ChargeMan = "'" + MyBuMen.ChargeMan.Replace(",", "','") + "'";
                DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [TrueName] from ERPUser Where [UserName] in (" + ChargeMan + ")");
                string TrueName = ""; 
                foreach(DataRow R in dt.Rows) {
                    TrueName += TrueName == "" ? R["TrueName"] : "," + R["TrueName"];
                }
                this.TextBox2.Text = TrueName;

                this.TextBox3.Text = MyBuMen.TelStr;
                this.TextBox4.Text = MyBuMen.ChuanZhen;
                this.TextBox5.Text = MyBuMen.BackInfo;
            }
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/BuMenInfo.aspx" : Request.UrlReferrer.ToString();
            
        }
    }
  
    protected void btn_Sub_Click(object sender, EventArgs e) {
        if(ZWL.Common.PublicMethod.IFExists("BuMenName", "ERPBuMen", int.Parse(Request.QueryString["ID"].ToString()), this.TextBox1.Text) == true) {
            ZWL.BLL.ERPBuMen MyBuMen = new ZWL.BLL.ERPBuMen();
            MyBuMen.ID = int.Parse(Request.QueryString["ID"].ToString());
            MyBuMen.BuMenName = this.TextBox1.Text;
            MyBuMen.ChargeMan = this.TextBox2.Text;
            MyBuMen.TelStr = this.TextBox3.Text;
            MyBuMen.ChuanZhen = this.TextBox4.Text;
            MyBuMen.BackInfo = this.TextBox5.Text;
            MyBuMen.DirID = int.Parse(Request.QueryString["DirID"].ToString());
            MyBuMen.Update();

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户修改部门信息(" + this.TextBox1.Text + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('部门信息修改成功！');window.location.href='BuMenInfo.aspx?View=" + Request.QueryString["View"].ToString() + "&Type=" + Request.QueryString["Type"].ToString() + "&DirID=" + Request.QueryString["DirID"].ToString() + "'</script>");
            //ZWL.Common.MessageBox.ShowAndRedirect(this, "部门信息修改成功！", "BuMenInfo.aspx?View=" + Request.QueryString["View"].ToString() + "&Type=" + Request.QueryString["Type"].ToString() + "&DirID=" + Request.QueryString["DirID"].ToString());
        }
        else {
            Response.Write("<script>alert('该部门已经存在，请更换名称！！');</script>");
            //ZWL.Common.MessageBox.Show(this, "该部门已经存在，请更换名称！");
        }
    }
}
