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

public partial class SystemManage_TreeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview("");

            //�趨��ťȨ��            
            btn_Add.Visible = ZWL.Common.PublicMethod.StrIFIn("|099A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Edit.Visible = ZWL.Common.PublicMethod.StrIFIn("|099M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Del.Visible = ZWL.Common.PublicMethod.StrIFIn("|099D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|099E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
    }
   
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }

	public void DataBindToGridview(string IDList)
	{
		ZWL.BLL.ERPTreeList MyModel = new ZWL.BLL.ERPTreeList();
		if (IDList.Trim().Length > 0)
		{
            GVData.DataSource = MyModel.GetList(" " + DropDownList2.SelectedItem.Value.ToString() + " like '%" + this.TextBox3.Text.Trim() + "%' and ID in(" + IDList + ") order by ParentID asc,PaiXuStr asc,ID asc");
		}
		else
		{
            GVData.DataSource = MyModel.GetList(" " + DropDownList2.SelectedItem.Value.ToString() + " like '%" + this.TextBox3.Text.Trim() + "%' order by ParentID asc,PaiXuStr asc,ID asc");
		}
		GVData.DataBind();
		LabPageSum.Text = Convert.ToString(GVData.PageCount);
		LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
		this.GoPage.Text = LabCurrentPage.Text.ToString();
	}
	
    protected void btn_Search_Click(object sender, EventArgs e) {
        this.tb3_value.Value = this.TextBox3.Text.Trim();
        this.Dr2_value.Value = this.DropDownList2.SelectedItem.Value;
        DataBindToGridview("");
    }

    protected void btn_SearchInResult_Click(object sender, EventArgs e) {
        //������һ�β�ѯ���
        string JJ = "0";
        for(int i = 0; i < this.GVData.Rows.Count; i++) {
            Label LabV = (Label)GVData.Rows[i].FindControl("LabVisible");
            JJ = JJ + "," + LabV.Text.Trim();
        }
        DataBindToGridview(JJ);
    }
    #region  ��ҳ����
   
    protected void ButtonGo_Click(object sender, EventArgs e) {
        try {
            if(GoPage.Text.Trim().ToString() == "") {
                Response.Write("<script language='javascript'>alert('ҳ�벻����Ϊ��!');</script>");
            }
            else if(GoPage.Text.Trim().ToString() == "0" || Convert.ToInt32(GoPage.Text.Trim().ToString()) > GVData.PageCount) {
                Response.Write("<script language='javascript'>alert('ҳ�벻��һ����Чֵ!');</script>");
            }
            else if(GoPage.Text.Trim() != "") {
                int PageI = Int32.Parse(GoPage.Text.Trim()) - 1;
                if(PageI >= 0 && PageI < (GVData.PageCount)) {
                    GVData.PageIndex = PageI;
                }
            }

            if(TxtPageSize.Text.Trim().ToString() == "") {
                Response.Write("<script language='javascript'>alert('ÿҳ��ʾ����������Ϊ��!');</script>");
            }
            else if(TxtPageSize.Text.Trim().ToString() == "0") {
                Response.Write("<script language='javascript'>alert('ÿҳ��ʾ��������һ����Чֵ!');</script>");
            }
            else if(TxtPageSize.Text.Trim() != "") {
                try {
                    int MyPageSize = int.Parse(TxtPageSize.Text.ToString().Trim());
                    this.GVData.PageSize = MyPageSize;
                }
                catch {
                    Response.Write("<script language='javascript'>alert('ÿҳ��ʾ��������һ����Чֵ!');</script>");
                }
            }

            DataBindToGridview("");
        }
        catch {
            DataBindToGridview("");
            Response.Write("<script language='javascript'>alert('��������Ч���֣�');</script>");
        }
    }
    protected void PagerButtonClick(object sender, EventArgs e) {
        //���Button�Ĳ���ֵ
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
                //��ҳֵ
                GVData.PageIndex = 0;
                break;
        }
        DataBindToGridview("");
    }
    #endregion

    protected void btn_Add_Click(object sender, EventArgs e) {
        Response.Redirect("TreeListAdd.aspx");
    }
    protected void btn_Edit_Click(object sender, EventArgs e) {
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("TreeListModify.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void btn_Del_Click(object sender, EventArgs e) {
        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if(ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPTreeList where ID in (" + IDlist + ")") == -1) {
            Response.Write("<script>alert('ɾ��ѡ�м�¼ʱ�������������µ�½�����ԣ�');</script>");
        }
        else {
            DataBindToGridview("");
            //дϵͳ��־
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "�û�ɾ���˵�������Ϣ";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('ɾ���ɹ���');window.location.href='" + Request.RawUrl + "'</script>");
        }
    }
    protected void btn_Report_Click(object sender, EventArgs e) {

        #region  //�����ѯ����
        string IDList = "";
        for(int i = 0; i < this.GVData.Rows.Count; i++) {
            Label LabV = (Label)GVData.Rows[i].FindControl("LabVisible");
            if(i == 0) {
                IDList = IDList + LabV.Text.Trim();
            }
            IDList = IDList + "," + LabV.Text.Trim();
        }
        string Condition="";
        if (!string.IsNullOrEmpty(IDList))
        {
            Condition+="and ID in(" + IDList + ")";
        }
        #endregion

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select  TextStr,ImageUrlStr,ValueStr,NavigateUrlStr,Target,ParentID,QuanXianList,PaiXuStr  from ERPTreeList where  " + this.Dr2_value.Value.Trim() + " like '%" + this.tb3_value.Value.Trim() + "%' " + Condition + " order by ParentID,PaiXuStr ");
        string pHeader = "��ʾ����|����ͼƬ|��̨��ֵ|���ӵ�ַ|Ŀ����|���ڵ�|Ȩ��|����";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("TextStr", "��ʾ����");
        //MyTable.Add("ImageUrlStr", "����ͼƬ");
        //MyTable.Add("ValueStr", "��̨��ֵ");
        //MyTable.Add("NavigateUrlStr", "���ӵ�ַ");
        //MyTable.Add("Target", "Ŀ����");
        //MyTable.Add("ParentID", "���ڵ�");
        //MyTable.Add("QuanXianList", "Ȩ��");
        //MyTable.Add("PaiXuStr", "����");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select  TextStr,ImageUrlStr,ValueStr,NavigateUrlStr,Target,ParentID,QuanXianList,PaiXuStr  from ERPTreeList where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel����");
	
    }
   
}
