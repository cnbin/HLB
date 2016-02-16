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

public partial class SystemManage_TreeListModify : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			ZWL.Common.PublicMethod.CheckSession();
			ZWL.BLL.ERPTreeList Model = new ZWL.BLL.ERPTreeList();
			Model.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
			this.txtTextStr.Text=Model.TextStr.ToString();
			this.txtImageUrlStr.Text=Model.ImageUrlStr.ToString();
			this.txtValueStr.Text=Model.ValueStr.ToString();
			this.txtNavigateUrlStr.Text=Model.NavigateUrlStr.ToString();
			this.txtTarget.Text=Model.Target.ToString();
			this.txtParentID.Text=Model.ParentID.ToString();
			this.txtQuanXianList.Text=Model.QuanXianList.ToString();
			this.txtPaiXuStr.Text=Model.PaiXuStr.ToString();
            this.SelClass.SelectedValue = Model.ParentClass;
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/TreeList.aspx" : Request.UrlReferrer.ToString();
		}
	}
	
    protected void btn_Sub_Click(object sender, EventArgs e) {

        if(ZWL.Common.PublicMethod.IFExists("ValueStr", "ERPTreeList", int.Parse(Request.QueryString["ID"].ToString()), this.txtValueStr.Text) == true) {
            ZWL.BLL.ERPTreeList Model = new ZWL.BLL.ERPTreeList();

            Model.ID = int.Parse(Request.QueryString["ID"].ToString());
            Model.TextStr = this.txtTextStr.Text.ToString();
            Model.ImageUrlStr = this.txtImageUrlStr.Text.ToString();
            Model.ValueStr = this.txtValueStr.Text.ToString();
            Model.NavigateUrlStr = this.txtNavigateUrlStr.Text.ToString();
            Model.Target = this.txtTarget.Text.ToString();
            Model.ParentID = int.Parse(this.txtParentID.Text);
            Model.QuanXianList = this.txtQuanXianList.Text.ToString();
            Model.PaiXuStr = int.Parse(this.txtPaiXuStr.Text);
            Model.ParentClass = this.SelClass.SelectedItem.Value;
            Model.Update();

            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户修改菜单管理信息(" + this.txtTextStr.Text + ")";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('菜单管理信息修改成功！');window.location.href='TreeList.aspx';</script>");
            //ZWL.Common.MessageBox.ShowAndRedirect(this, "菜单管理信息修改成功！", "TreeList.aspx");
        }
        else {
            Response.Write("<script>alert('当前指定的后台数值已经存在，为了保持唯一性，请重新指定');</script>");
            //ZWL.Common.MessageBox.Show(this, "当前指定的后台数值已经存在，为了保持唯一性，请重新指定！");
        }
    }
}
