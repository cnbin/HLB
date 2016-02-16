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

public partial class QDGL_ZXWZAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            /*
            ZWL.Common.PublicMethod.CheckSession();
            //设置上传的附件为空
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", "");
             */
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../QDGL/ZXWZ.aspx" : Request.UrlReferrer.ToString();
        }
    }
  
    protected void btn_Sub_Click(object sender, EventArgs e) {
        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.system_info.Where(p => p.LX == this.LX.SelectedValue);
        if(T.Count() > 0) {
            Response.Write("<script>alert('不能重复添加定位参！');</script>");
            //ZWL.Common.MessageBox.Show(this, "不能重复添加定位参数！");
        }
        else {
            system_info model = new system_info();
            model.LX = this.LX.SelectedValue;
            model.CJJGSJ = int.Parse(this.TextBoxname.Text);
            model.WHSJ = DateTime.Now;
            model.WHRID = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            model.WHR = ZWL.Common.PublicMethod.GetSessionValue("TrueName");
            context.system_info.InsertOnSubmit(model);
            context.SubmitChanges();

            ////写系统日志
            //ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            //MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            //MyRiZhi.DoSomething = "用户添加公共信息(" + this.TextBoxname.Text + ")";
            //MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            //MyRiZhi.Add();
            
            Response.Write("<script>alert('定位参数添加成功！');window.location.href='ZXWZ.aspx';</script>");
            //ZWL.Common.MessageBox.ShowAndRedirect(this, " 定位参数添加成功  ！", "ZXWZ.aspx");
        }
    }
}