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

public partial class WorkPlan_HuiBaoD : System.Web.UI.Page
{
    //string Department_ID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.BLL.ERPBuMen BuMenModel = new ZWL.BLL.ERPBuMen();
            //Department_ID = BuMenModel.GetList("BuMenName='" + ZWL.Common.PublicMethod.GetSessionValue("Department") + "'").Tables[0].Rows[0]["ID"].ToString();
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();

            //设定按钮权限            
            btn_Add.Visible = ZWL.Common.PublicMethod.StrIFIn("|112A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Change.Visible = ZWL.Common.PublicMethod.StrIFIn("|112M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Del.Visible = ZWL.Common.PublicMethod.StrIFIn("|112D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|112E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            string uname = System.Web.HttpContext.Current.Session["UserName"].ToString();
           

            if ((Request["token"] ?? "") == "ajax" && !string.IsNullOrEmpty(uname))
            {
                string res = "";
                ZWL.BLL.ERPHuiBao MyModel = new ZWL.BLL.ERPHuiBao();

                res = MyModel.GetListByZT("预警取消");
                Response.Clear();
                Response.Write(res);
                Response.End();
            }
        }
    }
    public void DataBindToGridview()
    {
        ZWL.BLL.ERPHuiBao MyModel = new ZWL.BLL.ERPHuiBao();
        //DataSet ds = MyModel.GetList("TitleStr Like '%" + this.TextBox1.Text + "%' and UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' order by ID desc");
        DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select h.ID,h.TitleStr,h.SSBM,h.UserName,h.TimeStr,h.ZT,h.DDWL,u.TrueName as TrueName from [hx_vERPHuiBao] as h left join ERPUser as u on h.UserName=u.UserName where TitleStr Like '%" + this.TextBox1.Text + "%' and CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0  order by ID desc");
            //MyModel.GetList("TitleStr Like '%" + this.TextBox1.Text + "%' and CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0 ","ID",true);
        foreach(DataRow R in dt.Rows) {
            if(R["TrueName"] == null) {
                R["TrueName"] = R["UserName"];
            }
        }

        if(dt != null)
        {
            GVData.DataSource = dt;
            GVData.DataBind();
            LabPageSum.Text = Convert.ToString(GVData.PageCount);
            LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
            this.GoPage.Text = LabCurrentPage.Text.ToString();
        }

    }

    [System.Web.Services.WebMethod()]
    public static string GetWarning() {
        string res = "";
        ZWL.BLL.ERPHuiBao MyModel = new ZWL.BLL.ERPHuiBao();
        res = MyModel.GetListByDeparment(int.Parse(ZWL.Common.PublicMethod.GetSessionValue("DepartmentID")));
        return res;
    }
   
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }
   
  
    protected void btn_Add_Click(object sender, EventArgs e) {
        Response.Redirect("HuiBaoAdd.aspx");
    }
    protected void btn_Change_Click(object sender, EventArgs e) {
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("HuiBaoModify.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void btn_Del_Click(object sender, EventArgs e) {
        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if(ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPHuiBao where ID in (" + IDlist + ")") == -1) {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else {
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户删除工作报告";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('删除成功！');window.location.href='" + Request.RawUrl + "'</script>");
        }
    }
    protected void btn_Report_Click(object sender, EventArgs e) {

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select TitleStr,SSBM,UserName,TimeStr,ZT from hx_vERPHuiBao where TitleStr Like '%" + this.tb1_value.Value.Trim() + "%' and CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0 order by ID desc");
        string pHeader = "报告主题|所属部门|姓名|发送时间|状态";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("TitleStr", "报告主题");
        //MyTable.Add("CanLookUser", "允许查看人");
        //MyTable.Add("TimeStr", "更新时间");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select TitleStr,CanLookUser,TimeStr from hx_vERPHuiBao where ID in (" + IDList + ") and CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0 order by ID desc"), MyTable, "Excel报表");
  
    }
    protected void btn_Search_Click(object sender, EventArgs e) {
        tb1_value.Value = TextBox1.Text;
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