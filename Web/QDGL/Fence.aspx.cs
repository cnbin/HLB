using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;


public partial class QDGL_Fence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();
            btn_Add.Visible = ZWL.Common.PublicMethod.StrIFIn("|116A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Change.Visible = ZWL.Common.PublicMethod.StrIFIn("|116M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Del.Visible = ZWL.Common.PublicMethod.StrIFIn("|116D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|116E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
    }
    public void DataBindToGridview()
    {
        DataEntityDataContext context = new DataEntityDataContext();
        foreach(var Fence in context.FenceXX.ToList()) {
            string Coords = "";
            string[] CoordsList = Fence.Coords.Split(';');
            for(int i = 0; i < CoordsList.Length; i++) {
                if((i + 1) % 3 == 0) { //换行
                    Coords += CoordsList[i] + ";" + "<br />";
                }
                else {
                    Coords += CoordsList[i] + ";" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
            }
            Fence.Coords = Coords;
        }
        var T = context.FenceXX.Where(f => f.ID > 0);
        if(!string.IsNullOrEmpty(this.TextBox1.Text)) {
            T = T.Where(f => f.MC.Contains(this.TextBox1.Text));
        }
        GVData.DataSource = T.ToList();

        GVData.DataBind();
        LabPageSum.Text = Convert.ToString(GVData.PageCount);
        LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
        this.GoPage.Text = LabCurrentPage.Text.ToString();
    }
    #region  分页方法
    
    #endregion
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow) {
            TableCellCollection cells = e.Row.Cells;
            cells[3].Text=Server.HtmlDecode(cells[3].Text);
        }
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }
   
    protected void btn_Add_Click(object sender, EventArgs e) {
        Response.Redirect("FenceAdd.aspx");
    }
    protected void btn_Change_Click(object sender, EventArgs e) {
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("FenceModify.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void btn_Del_Click(object sender, EventArgs e) {
        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if(ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from FenceXX where ID in (" + IDlist + ")") == -1) {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else {
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户删除电子围栏信息";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('删除成功！');window.location.href='" + Request.RawUrl + "'</script>");
        }
    }
    protected void btn_Report_Click(object sender, EventArgs e) {

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select MC,DZ,Coords,BZ2,WHR from FenceXX where MC like '%"+this.tb1_value.Value.Trim() +"%' order by ID desc");
        string pHeader = "电子围栏名称|县区|坐标集|描述|管理人员";
        for(int i = 0; i < ds.Tables[0].Rows.Count; i++) {
            string Coords = "";
            string[] CoordsList = ds.Tables[0].Rows[i]["Coords"].ToString().Split(';');
            for(int j = 0; j < CoordsList.Length; j++) {
                Coords += CoordsList[j] + ";" + "<br />";
            }
            ds.Tables[0].Rows[i]["Coords"] = Coords;

        }

       ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
       //Hashtable MyTable = new Hashtable();
       //MyTable.Add("ID", "编号");
       //MyTable.Add("MC", "电子围栏名称");
       //MyTable.Add("LX", "类型");
       //MyTable.Add("DZ", "县区");
       ////MyTable.Add("X", "X坐标");
       ////MyTable.Add("Y", "Y坐标");
       //MyTable.Add("BZ1", "状态");
       //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select ID,MC,LX,DZ,BZ1 from FenceXX where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");
  
  
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
    protected void btn_Search_Click(object sender, EventArgs e) {
        tb1_value.Value = TextBox1.Text;
        DataBindToGridview();
    }
}