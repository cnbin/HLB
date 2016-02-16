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

public partial class SystemManage_SystemJiaoSeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            //加载节点进入CheCkBoxList中
            ZWL.Common.PublicMethod.AddItmesInCheCKList(this.CheckBoxList1);


            for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
            {
                if (this.CheckBoxList1.Items[i].Text.Trim() == "")
                {
                    this.CheckBoxList1.Items[i].Enabled = false;
                    this.CheckBoxList1.Items[i].Attributes.CssStyle.Add("DISPLAY", "none");
                }
            }
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/SystemJiaoSe.aspx" : Request.UrlReferrer.ToString();
        }
    }
   
    protected void btn_Sub_Click(object sender, EventArgs e) {
        if(ZWL.Common.PublicMethod.IFExists("JiaoSeName", "ERPJiaoSe", 0, this.TextBox1.Text) == true) {
            ZWL.BLL.ERPJiaoSe MyModel = new ZWL.BLL.ERPJiaoSe();
            MyModel.JiaoSeName = this.TextBox1.Text;
            MyModel.BackInfo = this.TextBox2.Text;
            MyModel.QuanXian = ZWL.Common.PublicMethod.GetStringFromCheckList(this.CheckBoxList1);
            MyModel.Add();

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户添加角色信息(" + this.TextBox1.Text + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('角色信息添加成功！');window.location.href='SystemJiaoSe.aspx';</script>");
            //ZWL.Common.MessageBox.ShowAndRedirect(this, "角色信息添加成功！", "SystemJiaoSe.aspx");
        }
        else {
            Response.Write("<script>alert('该角色名已经存在，请更换其他名称！');</script>");
            //ZWL.Common.MessageBox.Show(this, "该角色名已经存在，请更换其他名称！");
        }
    }
}
