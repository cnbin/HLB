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

public partial class SystemManage_CommonAdd : System.Web.UI.Page
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
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/Common.aspx" : Request.UrlReferrer.ToString();
        }
    }
  
    /*
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string FileNameStr = ZWL.Common.PublicMethod.UploadFileIntoDir(this.FileUpload1, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName));
        if (ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Trim() == "")
        {
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", FileNameStr);
        }
        else
        {
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList") + "|" + FileNameStr);
        }
        ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
    }


    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
            {
                if (this.CheckBoxList1.Items[i].Selected == true)
                {
                    ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Replace(this.CheckBoxList1.Items[i].Text, "").Replace("||", "|"));
                }
            }
            ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
        }
        catch
        { }
    }

    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
            {
                Response.Write("<script>window.open('../DsoFramer/ReadFile.aspx?FilePath=" + this.CheckBoxList1.SelectedItem.Text + "');</script>");
            }
        }
        catch
        { }
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
            {
                Response.Write("<script>window.open('../DsoFramer/EditFile.aspx?FilePath=" + this.CheckBoxList1.SelectedItem.Text + "');</script>");
            }
        }
        catch
        { }
    }    

        */


    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.BLL.ERPCommon Model = new ZWL.BLL.ERPCommon();

        Model.Code = this.TextBoxcode.Text;
        Model.CName = this.TextBoxname.Text;
        Model.CType = this.TextBoxtype.Text;
        Model.CSort = int.Parse(this.TextBoxsort.Text); //没有验证
        Model.CDescription = this.TextBoxdes.Text;
        Model.UpdateTime = DateTime.Now;
        Model.Add();


        ////写系统日志
        //ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        //MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        //MyRiZhi.DoSomething = "用户添加公共信息(" + this.TextBoxname.Text + ")";
        //MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        //MyRiZhi.Add();
         Response.Write("<script>alert('共有参数添加成功！');window.location.href='Common.aspx';</script>");
        //ZWL.Common.MessageBox.ShowAndRedirect(this, "共有参数添加成功！", "Common.aspx");
    }
}