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

public partial class WorkPlan_ManageWorkPlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();
            //设定按钮权限 
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|110E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
    }
    public void DataBindToGridview()
    {
        //ZWL.BLL.ERPHuiBao MyModel = new ZWL.BLL.ERPHuiBao();
        //DataSet ds = MyModel.GetList("(TitleStr Like '%" + this.TextBox1.Text + "%' and ','+CanLookUser+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' or UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "') and ZT='信息转发' order by ID desc");
        DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select H.[ID],H.[TitleStr],H.[ContentStr],H.[FuJianStr],H.[UserName],H.[CanLookUser],H.[TimeStr],H.SSBM,H.DDWL,H.ZT,H.BZ1,H.BZ2,U.TrueName " +
                                                              "FROM hx_vERPHuiBao as H left join ERPUser as U on H.UserName=U.UserName "+
                                                               "where (H.TitleStr Like '%" + this.TextBox1.Text + "%' and ','+H.CanLookUser+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' or H.UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "')" +
                                                               "order by H.ID desc");
        //DataSet ds = MyModel.GetList("(TitleStr Like '%" + this.TextBox1.Text + "%' and ','+CanLookUser+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' or UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "')","ID",true);
        if(dt!= null)
        {
            foreach(DataRow R in dt.Rows) {
                if(string.IsNullOrEmpty(R["TrueName"].ToString())) {
                    R["TrueName"] = R["UserName"];
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
       
    }
    
    
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }
   
    #region  分页方法
   
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
    protected void btn_Report_Click(object sender, EventArgs e) {

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select TitleStr,SSBM,UserName,TimeStr,ZT from ERPHuiBao where (TitleStr Like '%" + this.tb1_value.Value.Trim() + "%' and ','+CanLookUser+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' or UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "')  order by ID desc");
        string pHeader = "报告主题|所属部门|姓名|发送时间|状态";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("TitleStr", "报告主题");
        //MyTable.Add("UserName", "撰写人");
        //MyTable.Add("CanLookUser", "允许查看人");
        //MyTable.Add("TimeStr", "更新时间");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select TitleStr,UserName,CanLookUser,TimeStr from ERPHuiBao where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");

    }
    #endregion
    protected void btn_Search_Click(object sender, EventArgs e) {
        tb1_value.Value = TextBox1.Text;
        DataBindToGridview();
    }
}