using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WorkPlan_WorkPlanView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //绑定页面数据
            ZWL.BLL.ERPHuiBao MyModel = new ZWL.BLL.ERPHuiBao();
            MyModel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            if (MyModel!=null)
            {
                this.Label1.Text = MyModel.TitleStr;
                string CanLookUser="";
                foreach (string C in MyModel.CanLookUser.Split(','))
                {
                    string CTrueName=ZWL.DBUtility.DbHelperSQL.GetSHSL("select [TrueName] from ERPUser where [UserName]='"+C+"'");
                    if(!string.IsNullOrEmpty(CTrueName)) {
                        CanLookUser += CanLookUser == "" ? CTrueName : "," + CTrueName;
                    }
                    else {
                        CanLookUser += CanLookUser == "" ? C : "," + C;
                    }
                }
                this.Label2.Text = CanLookUser;
                   
                this.Label6.Text = MyModel.ContentStr;
                string TrueName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 [TrueName] from ERPUser where [UserName]='" + MyModel.UserName + "'");

                this.Label4.Text = TrueName;
                this.Label5.Text = MyModel.TimeStr.ToString();
                this.Label8.Text = MyModel.DDWL;
                this.Label7.Text = MyModel.ZT;
                this.Label3.Text = ZWL.Common.PublicMethod.GetWenJian(MyModel.FuJianStr, "../UploadFile/");
            }
           

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "查看工作报告信息(" + this.Label1.Text + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();

            QD.Visible = ZWL.Common.PublicMethod.StrIFIn("|111M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ZT1.Visible = ZWL.Common.PublicMethod.StrIFIn("|111M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            ZT3.Visible = ZWL.Common.PublicMethod.StrIFIn("|111M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));

            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../WorkPlan/HuiBaoM.aspx" : Request.UrlReferrer.ToString();
        }
    }
    protected void QD_Click(object sender, EventArgs e)
    {
       
        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.ERPHuiBao.SingleOrDefault(p => p.ID == int.Parse(Request.QueryString["ID"].ToString()));
        //T.CanLookUser = this.TextBox2.Text;
        T.CanLookUser = this.UserName_Input.Value;
        context.SubmitChanges();
        Response.Write("<script>alert('工作报告转发成功！');window.location.href='" + ReturnInput.Value + "'</script>");
        //ZWL.Common.MessageBox.ShowAndRedirect(this, "工作报告转发成功！", "HuiBao.aspx");
    }
    ZWL.BLL.ERPHuiBao Model2 = new ZWL.BLL.ERPHuiBao(); 
    protected void ZT1_Click(object sender, EventArgs e)
    {
        Model2.ID = int.Parse(Request.QueryString["ID"].ToString());
        Model2.ZT = "预警保存";
        Model2.UpdateZT();
        Response.Write("<script>alert('状态修改成功！');window.location.href='" + ReturnInput.Value + "'</script>");
       // ZWL.Common.MessageBox.ShowAndRedirect(this, "状态修改成功！", "HuiBao.aspx");
    }
    protected void ZT2_Click(object sender, EventArgs e)
    {
        Model2.ID = int.Parse(Request.QueryString["ID"].ToString());
        Model2.ZT = "预警取消";
        Model2.UpdateZT();
        Response.Write("<script>alert('状态修改成功！');window.location.href='" + ReturnInput.Value + "'</script>");
        //ZWL.Common.MessageBox.ShowAndRedirect(this, "状态修改成功！", "HuiBao.aspx");
    }
}
