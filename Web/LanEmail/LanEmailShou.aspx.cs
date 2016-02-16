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

public partial class LanEmail_LanEmailShou : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();

            //设定按钮权限
            btn_Add.Visible = ZWL.Common.PublicMethod.StrIFIn("|001A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Del.Visible = ZWL.Common.PublicMethod.StrIFIn("|001D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|001E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
    }
    public void DataBindToGridview()
    {
        //ZWL.BLL.ERPLanEmail MyLanEmail = new ZWL.BLL.ERPLanEmail();
        //GVData.DataSource = MyLanEmail.GetList("EmailTitle like '%" + this.TextBox1.Text.Trim() + "%' and FromUser like '%" + this.TextBox2.Text.Trim() + "%'  and EmailState like '%" + this.TextBox3.Text.Trim() + "%' and ToUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and (EmailState='未读' or EmailState='已读')  order by ID desc");
      
        DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select L.[ID],L.[EmailTitle],L.[TimeStr],L.[EmailContent],L.[FuJian],L.[FromUser],L.[ToUser],L.[EmailState],U.TrueName " +
                                                             "from ERPLanEmail as L left join ERPUser as U on L.FromUser=U.UserName " +
                                                             "where L.EmailTitle like '%" + this.TextBox1.Text.Trim() + "%' and L.FromUser like '%" + this.TextBox2.Text.Trim() + "%'  and L.EmailState like '%" + this.TextBox3.Text.Trim() + "%' and L.ToUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and ( L.EmailState='未读' or L.EmailState='已读')  order by ID desc");
        foreach(DataRow R in dt.Rows) {
            if(string.IsNullOrEmpty(R["TrueName"].ToString())) {
                R["TrueName"] = R["FromUser"];
            }
            else {
            }
        }
       
        GVData.DataSource = dt;
        GVData.DataBind();
        LabPageSum.Text = Convert.ToString(GVData.PageCount);
        LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
        this.GoPage.Text = LabCurrentPage.Text.ToString();
    }

   

    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }
    #region  分页方法

    
    protected void PagerButtonClick(object sender, EventArgs e) {
        //获得Button的参数值
        string arg = ((Button)sender).CommandName.ToString();
        switch(arg) {
            case ("Next"):
                if(this.GVData.PageIndex < (GVData.PageCount - 1))
                    GVData.PageIndex++;
                break;
            case ("Pre"):
                if(GVData.PageIndex > 0)
                    GVData.PageIndex--;
                break;
            case ("Last"):
                try {
                    GVData.PageIndex = (GVData.PageCount - 1);
                }
                catch {
                    GVData.PageIndex = 0;
                }

                break;
            default:
                //本页值
                GVData.PageIndex = 0;
                break;
        }
        DataBindToGridview();
    }
    
    #endregion

    protected void ButtonGo_Click(object sender, EventArgs e) {
        try {
            if(GoPage.Text.Trim().ToString() == "") {
                Response.Write("<script language='javascript'>alert('页码不可以为空!');</script>");
            }
            else if(GoPage.Text.Trim().ToString() == "0" || Convert.ToInt32(GoPage.Text.Trim().ToString()) > GVData.PageCount) {
                Response.Write("<script language='javascript'>alert('页码不是一个有效值!');</script>");
            }
            else if(GoPage.Text.Trim() != "") {
                int PageI = Int32.Parse(GoPage.Text.Trim()) - 1;
                if(PageI >= 0 && PageI < (GVData.PageCount)) {
                    GVData.PageIndex = PageI;
                }
            }

            if(TxtPageSize.Text.Trim().ToString() == "") {
                Response.Write("<script language='javascript'>alert('每页显示行数不可以为空!');</script>");
            }
            else if(TxtPageSize.Text.Trim().ToString() == "0") {
                Response.Write("<script language='javascript'>alert('每页显示行数不是一个有效值!');</script>");
            }
            else if(TxtPageSize.Text.Trim() != "") {
                try {
                    int MyPageSize = int.Parse(TxtPageSize.Text.ToString().Trim());
                    this.GVData.PageSize = MyPageSize;
                }
                catch {
                    Response.Write("<script language='javascript'>alert('每页显示行数不是一个有效值!');</script>");
                }
            }

            DataBindToGridview();
        }
        catch {
            DataBindToGridview();
            Response.Write("<script language='javascript'>alert('请输入有效数字！');</script>");
        }
    }
    protected void btn_Add_Click(object sender, EventArgs e) {
        Response.Redirect("LanEmailAdd.aspx");
    }
    protected void btn_Change_Click(object sender, EventArgs e) {

    }
    protected void btn_Del_Click(object sender, EventArgs e) {

        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if(ZWL.DBUtility.DbHelperSQL.ExecuteSQL("update ERPLanEmail set EmailState='删除' where ID in (" + IDlist + ")") == -1) {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else {
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户删除收件箱中的调度";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('删除成功！');window.location.href='" + Request.RawUrl + "'</script>");
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e) {

        tb1_value.Value = TextBox1.Text;
        tb2_value.Value = TextBox2.Text;
        tb3_value.Value = TextBox3.Text;
        DataBindToGridview();
    }
    protected void btn_Report_Click(object sender, EventArgs e) {


        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select EmailTitle,FromUser,TimeStr,EmailState from ERPLanEmail where EmailTitle like '%" + this.tb1_value.Value.Trim() + "%' and FromUser like '%" + this.tb2_value.Value.Trim() + "%'  and EmailState like '%" + this.tb3_value.Value.Trim() + "%' and ToUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and (EmailState='未读' or EmailState='已读')  order by ID desc");
        string pHeader="调度主题|发送人|发送时间|调度状态";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("EmailTitle", "调度标题");
        //MyTable.Add("FromUser", "发送人");
        //MyTable.Add("TimeStr", "发送时间");
        //MyTable.Add("EmailState", "调度状态");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select EmailTitle,FromUser,TimeStr,EmailState from ERPLanEmail where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");
  
    }
}
