using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SystemManage_CommonModyfy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //绑定页面数据
            ZWL.BLL.ERPCommon MyModel = new ZWL.BLL.ERPCommon();
            MyModel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            if (MyModel != null)
            {
                this.TextBox1.Text = MyModel.Code;
                this.TextBox3.Text = MyModel.CName;
                this.TextBox4.Text = MyModel.CType;
                this.TextBox5.Text = MyModel.CSort.ToString();
                this.TextBox6.Text = MyModel.CDescription;
            }
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/Common.aspx" : Request.UrlReferrer.ToString();

            //ZWL.Common.PublicMethod.SetSessionValue("WenJianList", MyModel.FuJianStr);
            //ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
        }
    }
 
    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{
    //    string FileNameStr = ZWL.Common.PublicMethod.UploadFileIntoDir(this.FileUpload1, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName));
    //    if (ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Trim() == "")
    //    {
    //        ZWL.Common.PublicMethod.SetSessionValue("WenJianList", FileNameStr);
    //    }
    //    else
    //    {
    //        ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList") + "|" + FileNameStr);
    //    }
    //    ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
    //}
    //protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
    //        {
    //            if (this.CheckBoxList1.Items[i].Selected == true)
    //            {
    //                ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Replace(this.CheckBoxList1.Items[i].Text, "").Replace("||", "|"));
    //            }
    //        }
    //        ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
    //    }
    //    catch
    //    { }
    //}

    //protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
    //        {
    //            Response.Write("<script>window.open('../DsoFramer/ReadFile.aspx?FilePath=" + this.CheckBoxList1.SelectedItem.Text + "');</script>");
    //        }
    //    }
    //    catch
    //    { }
    //}
    //protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
    //        {
    //            Response.Write("<script>window.open('../DsoFramer/EditFile.aspx?FilePath=" + this.CheckBoxList1.SelectedItem.Text + "');</script>");
    //        }
    //    }
    //    catch
    //    { }
    //}
    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.BLL.ERPCommon MyModel = new ZWL.BLL.ERPCommon();
        MyModel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));

        ZWL.BLL.ERPCommon Model = new ZWL.BLL.ERPCommon();
        Model.ID = int.Parse(Request.QueryString["ID"].ToString());
        Model.Code = this.TextBox1.Text;
        Model.CName = this.TextBox3.Text;
        Model.CType = this.TextBox4.Text;
        Model.CSort = int.Parse(this.TextBox5.Text);     //
        Model.CDescription = this.TextBox6.Text;
        Model.UpdateTime = MyModel.UpdateTime;
        Model.Update();

        ////写系统日志
        //ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        //MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        //MyRiZhi.DoSomething = "用户修改工作计划信息(" + this.TextBox1.Text + ")";
        //MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        //MyRiZhi.Add();
         Response.Write("<script>alert('共有参数修改成功！');window.location.href='Common.aspx';</script>");
        //ZWL.Common.MessageBox.ShowAndRedirect(this, "共有参数修改成功！", "Common.aspx");
    }
}