using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class QDGL_QDXX : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            try {
                this.TextBox1.Text = Request.QueryString["UserName"].ToString();
                string UserNameSQL = "'" + Request.QueryString["UserName"].ToString().Replace(",", "','") + "'";
                DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("Select * From ERPUser Where [UserName] in ( " + UserNameSQL + " )");
                foreach(DataRow R in dt.Rows){
                    this.TextBox2.Text += this.TextBox2.Text == "" ? R["TrueName"].ToString() : "," + R["TrueName"].ToString();
                }
            }
            catch {
            }
            DataBindToGridview();
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|113E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));

            
        }
    }

    public void DataBindToGridview()
    {
        //DataEntityDataContext context = new DataEntityDataContext();

        //var T = context.QDXX.Where(p => p.ID > 0).OrderByDescending(p => p.ID);
        //T = (System.Linq.IOrderedQueryable<QDXX>)T.Where(p => p.XM.Contains(R[0].ToString()));
               



        string UserCondition = "";
        if(!string.IsNullOrEmpty(this.TextBox1.Text)) {
            string[] UserNameList = this.TextBox1.Text.Split(',');
            foreach(string Name in UserNameList) {
                UserCondition += UserCondition == "" ? "([UserName]  like '%" + Name + "%')" : "or ([UserName]  like '%" + Name + "%')";
            }
        }
        if(!string.IsNullOrEmpty(this.TextBox2.Text)) {
            string[] TrueNameList = this.TextBox2.Text.Split(',');
            foreach(string Name in TrueNameList) {
                UserCondition += UserCondition == "" ? "([TrueName]  like '%" + Name + "%')" : "or ([TrueName]  like '%" + Name + "%')";
            }
        }
        UserCondition = UserCondition == "" ? UserCondition : " Where " + UserCondition;
        DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [UserName] from [ERPUser] " + UserCondition);

        string Condition = "";
        string UserNameSQL = "";
        foreach(DataRow R in dt.Rows) {
            UserNameSQL += UserNameSQL == "" ? "'"+R["UserName"].ToString()+"'" : ",'" + R["UserName"].ToString()+"'";
        }
        Condition += UserNameSQL == "" ? "" : "[XM] in (" + UserNameSQL + ")"; 

        if(Dr_Type.SelectedValue != "全部") {
            Condition += Condition == "" ? " ([BZ1] " + Dr_Type.SelectedValue + ")" : "and ( [BZ1] " + Dr_Type.SelectedValue + ")";
        }

        if(!string.IsNullOrEmpty(Condition))
        {
            Condition = " Where " + Condition;
        }

        DataTable T = ZWL.DBUtility.DbHelperSQL.GetDataTable("Select * from  [QDXX] " + Condition + "order by [ID] desc");

        #region //处理X，Y坐标过长
        foreach(DataRow R in T.Rows) {
            if(!string.IsNullOrEmpty(R["x"].ToString())) {
                int x = R["x"].ToString().IndexOf(".") + 7; //x坐标的小数点后六位数
                if(R["x"].ToString().Length > x) {
                    R["x"] = R["x"].ToString().Substring(0, x) + "...";
                }
            }
            if(!string.IsNullOrEmpty(R["y"].ToString())) {
                int y = R["y"].ToString().IndexOf(".") + 7; //y坐标的小数点后六位数
                if(R["y"].ToString().Length > y) {
                    R["y"] = R["y"].ToString().Substring(0, y) + "...";
                }
            }
            R["XM"] = R["XM"].ToString() + "(" + ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 [TrueName] from [ERPUser] where [UserName]='" + R["XM"].ToString() + "'") + ")";
        }
        #endregion

        GVData.DataSource = T;
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
    #endregion
    protected void btn_Report_Click(object sender, EventArgs e) {

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select SSBM,XM,RQ,POI,QDSJ,BZ1,BZ2,X,Y from QDXX where XM like '%" + this.tb1_value.Value.Trim() + "%' order by ID desc");
        string pHeader = "所属部门|姓名|日期|POI点|签到时间|类型|签到地点|X|Y";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("ID", "编号");
        //MyTable.Add("SSBM", "所属部门");
        //MyTable.Add("XM", "姓名");
        //MyTable.Add("RQ", "日期");
        //MyTable.Add("POI", "POI点");
        //MyTable.Add("QDSJ", "签到时间");
        //MyTable.Add("QTSJ", "签退时间");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select ID,SSBM,XM,RQ,POI,QDSJ,QTSJ from QDXX where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");
    
    }
    protected void btn_Search_Click(object sender, EventArgs e) {
        tb1_value.Value = TextBox1.Text;
        DataBindToGridview();
    }
}