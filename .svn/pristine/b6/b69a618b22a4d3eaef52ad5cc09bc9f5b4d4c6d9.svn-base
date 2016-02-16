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

public partial class LanEmail_LanEmailAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack) {
            ZWL.Common.PublicMethod.CheckSession();
            //设置上传的附件为空
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", "");
            try {
                
                string UserNames = "'"+Request.QueryString["UserName"].ToString().Replace(",","','")+"'";
                this.UserName_Input.Value = Request.QueryString["UserName"].ToString();
                DataTable dt= ZWL.DBUtility.DbHelperSQL.GetDataTable("select [TrueName] from ERPUser where [UserName] in (" +UserNames + ")");
                string UserName ="";
                foreach(DataRow R in dt.Rows)
                {
                    UserName += UserName == "" ? R["TrueName"] : "," + R["TrueName"];
                }
                this.TextBox2.Text = UserName;
                    


            }
            catch {
            }            //检测是回复或者转发
            try {
                if(Request.QueryString["Type"].ToString().Trim() == "HuiFu") {
                    ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
                    MyModel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
                    //设置页面数据
                    this.TextBox1.Text = "Re：" + MyModel.EmailTitle;
                    this.UserName_Input.Value = MyModel.FromUser;

                    string FromUser = "";
                    foreach(string C in MyModel.FromUser.Split(',')) {
                        string CTrueName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select [TrueName] from ERPUser where [UserName]='" + C + "'");
                        if(!string.IsNullOrEmpty(CTrueName)) {
                            FromUser += FromUser == "" ? CTrueName : "," + CTrueName;
                        }
                        else {
                            FromUser += FromUser == "" ? C : "," + C;
                        }
                    }
                    this.TextBox2.Text = FromUser;

                }
            }
            catch {
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            try {
                if(Request.QueryString["Type"].ToString().Trim() == "ZhuanFa") {
                    ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
                    MyModel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
                    //设置页面数据
                    this.TextBox1.Text = "RW：" + MyModel.EmailTitle;
                    this.TxtContent.Text = MyModel.EmailContent;

                    ZWL.Common.PublicMethod.SetSessionValue("WenJianList", MyModel.FuJian);
                    ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, MyModel.FuJian);
                }
            }
            catch {
            }
            if(Request.UrlReferrer.ToString().IndexOf("Main.aspx") == -1) {
                ReturnInput.Value = Request.UrlReferrer == null ? "../LanEmail/LanEmailShou.aspx" : Request.UrlReferrer.ToString();
            }
            else {
                ReturnInput.Value = "../LanEmail/LanEmailShou.aspx";
            }

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
                    ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Replace(this.CheckBoxList1.Items[i].Value, "").Replace("||", "|"));
                }
            }
            ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
        }
        catch
        { }
    }
   
    protected void btn_Draft_Click(object sender, EventArgs e) {
        //草稿
        ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();

        MyModel.EmailTitle = this.TextBox1.Text;
        MyModel.EmailContent = this.TxtContent.Text;
        MyModel.FuJian = ZWL.Common.PublicMethod.GetSessionValue("WenJianList");
        MyModel.FromUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyModel.EmailState = "草稿";
        string[] ToWhoList = this.UserName_Input.Value.Trim().Split(',');
        for(int i = 0; i < ToWhoList.Length; i++) {
            if(ToWhoList[i].Trim().Length > 0) {
                MyModel.ToUser = ToWhoList[i].Trim();
                MyModel.Add();
            }
        }

        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户添加新记录(" + this.TextBox1.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('新增记录成功！');window.location.href='" + ReturnInput.Value + "'</script>");
    }
    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();

        MyModel.EmailTitle = this.TextBox1.Text;
        MyModel.EmailContent = this.TxtContent.Text;
        MyModel.FuJian = ZWL.Common.PublicMethod.GetSessionValue("WenJianList");
        MyModel.FromUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyModel.EmailState = "未读";
        string[] ToWhoList = this.UserName_Input.Value.Trim().Split(',');
        for(int i = 0; i < ToWhoList.Length; i++) {
            if(ToWhoList[i].Trim().Length > 0) {
                MyModel.ToUser = ToWhoList[i].Trim();
                MyModel.Add();
            }
        }
        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户添加新记录(" + this.TextBox1.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('新增记录成功！');window.location.href='" + ReturnInput.Value + "'</script>");
        
    }
}