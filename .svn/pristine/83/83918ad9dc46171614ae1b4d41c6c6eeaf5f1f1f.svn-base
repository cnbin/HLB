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

public partial class WorkPlan_MyWorkPlanModify : System.Web.UI.Page
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
                this.TextBox1.Text = MyModel.TitleStr;

                string CanLookUser = "";
                foreach(string C in MyModel.CanLookUser.Split(',')) {
                    string CTrueName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select [TrueName] from ERPUser where [UserName]='" + C + "'");
                    if(!string.IsNullOrEmpty(CTrueName)) {
                        CanLookUser += CanLookUser == "" ? CTrueName : "," + CTrueName;
                    }
                    else {
                        CanLookUser += CanLookUser == "" ? C : "," + C;
                    }
                }

                this.TextBox2.Text = CanLookUser; //显示真实姓名
                this.TxtContent.Text = MyModel.ContentStr;
                this.TextBox3.Text = MyModel.DDWL;
                //Label1.Text = MyModel.ZT;
            }
           

            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", MyModel.FuJianStr);
            ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../WorkPlan/HuiBaoM.aspx" : Request.UrlReferrer.ToString();
        }
    }
   
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
    #region 状态
    //ZWL.BLL.ERPHuiBao Model2 = new ZWL.BLL.ERPHuiBao();    
    //protected void ZT1_Click(object sender, EventArgs e)
    //{
    //    Model2.ID = int.Parse(Request.QueryString["ID"].ToString());
    //    Model2.ZT = "预警保存";
    //    Model2.UpdateZT();
    //    ZWL.Common.MessageBox.ShowAndRedirect(this, "状态修改成功！", "HuiBao.aspx");
    //}
    //protected void ZT2_Click(object sender, EventArgs e)
    //{
    //    Model2.ID = int.Parse(Request.QueryString["ID"].ToString());
    //    Model2.ZT = "预警转发";
    //    Model2.UpdateZT();
    //    ZWL.Common.MessageBox.ShowAndRedirect(this, "状态修改成功！", "HuiBao.aspx");
    //}
    //protected void ZT3_Click(object sender, EventArgs e)
    //{
    //    Model2.ID = int.Parse(Request.QueryString["ID"].ToString());
    //    Model2.ZT = "预警取消";
    //    Model2.UpdateZT();
    //    ZWL.Common.MessageBox.ShowAndRedirect(this, "状态修改成功！", "HuiBao.aspx");
    //}
    #endregion

    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.BLL.ERPHuiBao Model = new ZWL.BLL.ERPHuiBao();
        Model.ID = int.Parse(Request.QueryString["ID"].ToString());
        Model.TitleStr = this.TextBox1.Text;
        Model.ContentStr = this.TxtContent.Text;
        //Model.CanLookUser = this.TextBox2.Text; 
        Model.DDWL = this.ddl_WarningType.SelectedValue;
        Model.CanLookUser = this.UserName_Input.Value;
        Model.TimeStr = DateTime.Now;
        Model.FuJianStr = ZWL.Common.PublicMethod.GetSessionValue("WenJianList");
        Model.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        Model.DDWL = this.TextBox3.Text;
        Model.SSBM = ZWL.Common.PublicMethod.GetSessionValue("Department");
        if(string.IsNullOrEmpty(this.TextBox2.Text)) { //此处只是判断，无需修改成真实姓名
            Model.ZT = "信息保存";
        }
        else {
            Model.ZT = "信息转发";
        }
        Model.Update();

        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户修改工作报告信息(" + this.TextBox1.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('工作报告修改成功！');window.location.href='"+ReturnInput.Value+"'</script>");
        
    }
  
}