using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class QDGL_WZCJ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            btn_Add.Visible = ZWL.Common.PublicMethod.StrIFIn("|114A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Change.Visible = ZWL.Common.PublicMethod.StrIFIn("|114M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Del.Visible = ZWL.Common.PublicMethod.StrIFIn("|114D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|114E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
    
            DataBindToGridview();
        }
    }

    public void DataBindToGridview()
    {
        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.POIXX.Where(p => p.ID > 0);
        if (!string.IsNullOrEmpty(this.TextBox1.Text))
        {
            T = T.Where(p => p.MC.Contains(this.TextBox1.Text));
        }
        GVData.DataSource = T.ToList();
        GVData.DataBind();
        LabPageSum.Text = Convert.ToString(GVData.PageCount);
        LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
        this.GoPage.Text = LabCurrentPage.Text.ToString();
    }
   
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
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

    protected void btn_Search_Click(object sender, EventArgs e) {
        tb1_value.Value = TextBox1.Text;
        DataBindToGridview();
    }
    protected void btn_Report_Click(object sender, EventArgs e) {

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select MC,LX,DZ,X,Y,BZ1 from POIXX where MC like '%" + this.tb1_value.Value.Trim() + "%' order by ID desc");
        string pHeader = "POI名称|类型|地址|X坐标|Y坐标|状态";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("ID", "编号");
        //MyTable.Add("MC", "POI名称");
        //MyTable.Add("LX", "类型");
        //MyTable.Add("DZ", "地址");
        //MyTable.Add("X", "X坐标");
        //MyTable.Add("Y", "Y坐标");
        //MyTable.Add("BZ1", "状态");

        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select ID,MC,LX,DZ,X,Y,BZ1 from POIXX where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");
   
    }
    protected void btn_Add_Click(object sender, EventArgs e) {
        Response.Redirect("POIAdd.aspx");
    }
    protected void btn_Change_Click(object sender, EventArgs e) {
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("POIModify.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void btn_Del_Click(object sender, EventArgs e) {
        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if(ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from POIXX where ID in (" + IDlist + ")") == -1) {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else {
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户删除POI信息";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('删除成功！');window.location.href='" + Request.RawUrl + "'</script>");
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