﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class SystemManage_SystemUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();

            //设定按钮权限
            btn_Add.Visible = ZWL.Common.PublicMethod.StrIFIn("|086A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Edit.Visible = ZWL.Common.PublicMethod.StrIFIn("|086M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Del.Visible = ZWL.Common.PublicMethod.StrIFIn("|086D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|086E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));

            //判断是否属于查询模块
            try
            {
                string SerchTypeStr = Request.QueryString["Type"].ToString();
                if (SerchTypeStr.Trim().Length > 0)
                {
                    this.btn_Add.Visible = false;
                    this.btn_Edit.Visible = false;
                    this.btn_Del.Visible = false;
                    this.btn_Report.Visible = false;
                }
            }
            catch
            { }
        }
    }
    public void DataBindToGridview()
    {
        //string xKeGuanDep = "'"+ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 xKeGuanDep from ERPUser where UserName='"+ZWL.Common.PublicMethod.GetSessionValue("UserName").Replace(",","','")+"'")+"'";
        //string ConditinonStr = " 1=1 ";
        //if (xKeGuanDep == "'所有部门'" || xKeGuanDep == "''")
        //{
        //}
        //else
        //{ ConditinonStr = " Department in (" + xKeGuanDep + ") "; }

        ZWL.BLL.ERPUser MyModel = new ZWL.BLL.ERPUser();
        //GVData.DataSource = MyModel.GetList("UserName Like '%" + this.TextBox1.Text + "%' and Department Like '%" + this.TextBox2.Text + "%' and  " + ConditinonStr + "  order by ID desc");
        DataSet ds = MyModel.GetList("UserName Like '%" + this.TextBox1.Text + "%' and Department Like '%" + this.TextBox2.Text + "%'  order by ID desc");
        if (ds != null)
        {
            GVData.DataSource = ds;
            GVData.DataBind();


            LabPageSum.Text = Convert.ToString(GVData.PageCount);
            LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
            this.GoPage.Text = LabCurrentPage.Text.ToString();
        }
    }
    #region  分页方法
    protected void ButtonGo_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void PagerButtonClick(object sender, ImageClickEventArgs e)
    {
       
    }
    #endregion
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);

        //判定是否有权限
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink MyHyp1 = (HyperLink)e.Row.FindControl("HyperLink3");
            MyHyp1.Visible = ZWL.Common.PublicMethod.StrIFIn("|086C|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
    }
   
    protected void btn_Add_Click(object sender, EventArgs e) {
        Response.Redirect("SystemUserAdd.aspx");
    }
    protected void btn_Edit_Click(object sender, EventArgs e) {

        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("SystemUserModify.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void btn_Del_Click(object sender, EventArgs e) {
        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if(ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPUser where ID in (" + IDlist + ")") == -1) {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else {
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户删除用户信息";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('删除成功！');window.location.href='" + Request.RawUrl + "'</script>");
        }
    }
    protected void btn_Report_Click(object sender, EventArgs e) {


        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select UserName,TrueName,Serils,Department,JiaoSe from ERPUser where UserName Like '%" + this.tb1_value.Value.Trim() + "%' and Department Like '%" + this.tb2_value.Value.Trim() + "%'  order by ID desc");
        string pHeader = "用户名|姓名|员工编号|所属部门|所属角色";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("UserName", "用户名");
        //MyTable.Add("TrueName", "姓名");
        //MyTable.Add("Serils", "员工编号");
        //MyTable.Add("Department", "所属部门");
        //MyTable.Add("JiaoSe", "角色");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select UserName,TrueName,Serils,Department,JiaoSe from ERPUser where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");
    
    }
    protected void btn_Search_Click(object sender, EventArgs e) {
        tb1_value.Value = TextBox1.Text;
        tb2_value.Value = TextBox2.Text;
        DataBindToGridview();
    }
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
}