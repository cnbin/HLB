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
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|016E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            DataBindToGridview();
        }
    }
    //protected FenceXX Model;
    public void DataBindToGridview()
    {
        //DataEntityDataContext context = new DataEntityDataContext();
        //var T = context.WZXX.Where(p => p.ID > 0);
        //if(!string.IsNullOrEmpty(this.TextBox1.Text))
        //{
        //    T = T.Where(p=>p.SSBM.Contains(this.TextBox1.Text));
        //}
        //if (!string.IsNullOrEmpty(this.TextBox2.Text))
        //{
        //    T = T.Where(p => p.XM.Contains(this.TextBox2.Text));
        //}
        //if (!string.IsNullOrEmpty(this.txtYuJiTiXing.Text))
        //{
        //    T = T.Where(p => p.RQ>=DateTime.Parse(this.txtYuJiTiXing.Text));
        //}
        //if (!string.IsNullOrEmpty(this.TextBox3.Text))
        //{
        //    T = T.Where(p => p.RQ <= DateTime.Parse(this.TextBox3.Text));
        //}
      //  T=T.Where(p => )
      //  context.ExecuteQuery<WZXX>(" CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0");
        
        //T = T.OrderByDescending(p => p.RQ);
        string Condition = "";

        if(!string.IsNullOrEmpty(this.TextBox1.Text)) {
            Condition += Condition == "" ? "[SSBM] like '%" + this.TextBox1.Text + "%'" : "[SSBM] like '%" + this.TextBox1.Text + "%'";
        }
        if(!string.IsNullOrEmpty(this.TextBox2.Text)) {
            Condition += Condition == "" ? "[XM] like '%" + this.TextBox2.Text + "%'" : "and [XM] like '%" + this.TextBox2.Text + "%'";
        }
        if(!string.IsNullOrEmpty(this.txtYuJiTiXing.Text)) {
            Condition += Condition == "" ? " [RQ] >= '" + DateTime.Parse(this.txtYuJiTiXing.Text) + "'" : "and  [RQ] >= '" + DateTime.Parse(this.txtYuJiTiXing.Text) + "'";
        }
        if(!string.IsNullOrEmpty(this.TextBox3.Text)) {
            Condition += Condition == "" ? " [RQ] <= '" + DateTime.Parse(this.TextBox3.Text) + "'" : "and  [RQ] <= '" + DateTime.Parse(this.TextBox3.Text) + "'";
        }
        Condition = Condition == "" ? "" : " Where " + Condition;

        DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("Select W.ID,W.SSBM,W.XM,W.RQ,W.X,W.Y,W.BZ1,E.TrueName "
                                                                +"from WZXX as W left join [ERPUser] as E on W.XM=E.UserName" + Condition + " order by ID desc ");
        
        foreach(DataRow R in dt.Rows)
        {
            if(string.IsNullOrEmpty(R["TrueName"].ToString())) {
                R["TrueName"] = R["XM"];
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
 
    protected void btn_Search_Click(object sender, EventArgs e) {

        tb1_value.Value = TextBox1.Text;
        tb2_value.Value = TextBox2.Text;
        tbyjt_value.Value = txtYuJiTiXing.Text;
        tb3_value.Value = TextBox3.Text;
        DataBindToGridview();

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

        string Condition = "";
        if(!string.IsNullOrEmpty(tbyjt_value.Value)) {
            Condition += "and RQ>= '" + this.tbyjt_value.Value + "'";
        }
        if(!string.IsNullOrEmpty(tb3_value.Value)) {
            Condition += " and RQ <= '" + this.tb3_value.Value + "'";
        }

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select ID,SSBM,XM,RQ,X,Y,BZ1 from WZXX where SSBM like '%" + this.tb1_value.Value.Trim() + "%' and XM like '%" + this.tb2_value.Value.Trim() + "%'"+Condition+"  order by ID desc");
        string pHeader = "编号|所属部门|姓名|定位时间|X坐标|Y坐标|定位状态";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("ID", "编号");
        //MyTable.Add("SSBM", "所属部门");
        //MyTable.Add("XM", "姓名");
        //MyTable.Add("RQ", "定位时间");
        //MyTable.Add("X", "X坐标");
        //MyTable.Add("Y", "Y坐标");
        //MyTable.Add("BZ1", "定位状态");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select ID,SSBM,XM,RQ,X,Y,BZ1 from WZXX where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");

       // ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select ID,SSBM,XM,RQ,X,Y,BZ1 from hx_vWZXX where ID in (" + IDList + ") and CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0 order by ID desc"), MyTable, "Excel报表");
    }
}