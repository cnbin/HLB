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
using System.Data.SqlClient;

public partial class GongGao_GongGao : System.Web.UI.Page {
    public string HaveChild = "";
   // string IDS = ""; //用来保存有存在上级的ID
   // string QIDS = "";//部门权限ID
    string UserName = ""; //用户名，由URL传入
    protected void Page_Load(object sender, EventArgs e) {
        if(!Page.IsPostBack) {
            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();

            //设定按钮权限

            btn_Add.Visible = ZWL.Common.PublicMethod.StrIFIn("|004A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Change.Visible = ZWL.Common.PublicMethod.StrIFIn("|004M|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Del.Visible = ZWL.Common.PublicMethod.StrIFIn("|004D|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            btn_Report.Visible = ZWL.Common.PublicMethod.StrIFIn("|004E|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));

        }

        #region  //树状图
        UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        string Condition = "";
        if(UserName != "") {
            Condition = " where [UserName]='" + UserName + "'";
        }
        string Departments = "";
        DataTable Udt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [Department] from [ERPUser] " + Condition);
        if(Udt.Rows.Count != 0) {
            Departments = Udt.Rows[0][0].ToString();
        }
        else {
            Departments = "";
        }
        Departments = "'" + Departments.Replace(",", "','") + "'";//拼装IDS，用于SQL语句
        //获取角色的部门
        DataTable BMdt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [ID],[DirID] from ERPBuMen where [BuMenName] in (" + Departments + ")");

        //foreach(DataRow BMR in BMdt.Rows) {
        //    QIDS += QIDS == "" ? BMR[0].ToString() : "," + BMR[0].ToString(); //获取用户拥有的权限
        //    DataTable Ddt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [ID] from ERPBuMen where [DirID] in (" + BMR[0].ToString() + ") or  [ID] in (" + BMR[0].ToString() + ") order by [DirID]");
        //    //判断是否有存在下级ID，若存在，则保存在IDS
        //    if(Ddt.Rows.Count > 0) { //若数量大于0.则存在上级菜单
        //        foreach(DataRow DR in Ddt.Rows) {
        //            IDS += IDS == "" ? DR[0].ToString() : "," + DR[0].ToString();
        //        }
        //    }
        //}
        string dirid = "0";
        dirid = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 DirID from ERPBuMen where ID='" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "'");
        BindTree(this.ListTreeView.Nodes, Convert.ToInt32(dirid));
        #endregion
    }

    public void DataBindToGridview() {
        //ZWL.BLL.ERPGongGao MyModel = new ZWL.BLL.ERPGongGao();

        //GVData.DataSource = MyModel.GetList("TitleStr Like '%" + this.TextBox1.Text + "%' and (UserBuMen='所有部门' or UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' or ','+UserBuMen+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%') order by ID desc");
        string Condition = "";
        if(ZWL.Common.PublicMethod.GetSessionValue("UserName") == "admin") {
            Condition = " Where TitleStr Like '%" + this.TextBox1.Text + "%' order by ID desc";
        }
         else
        { 
            Condition=" Where TitleStr Like '%" + this.TextBox1.Text + "%' and (G.UserBuMen='所有部门' or G.UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' or ','+G.UserBuMen+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%') order by ID desc";
        }
        DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("SELECT G.[ID],G.[TitleStr],G.[TimeStr],G.[UserName],G.[UserBuMen],G.[NoticeType],G.[FuJian],G.[ContentStr],G.[TypeStr] ,G.[num],G.[SHR],G.[SHSJ],G.[ZT],U.[TrueName]" +
                                                              " FROM [ERPGongGao] as G left join [ERPUser] as U on G.[UserName]=U.[UserName]" + Condition );
        foreach(DataRow R in dt.Rows) {
            if(string.IsNullOrEmpty(R["TrueName"].ToString())) {
                R["TrueName"] = R["UserName"];
            }
            string UserNames="'"+R["UserBuMen"].ToString().Replace(",","','")+"'";
            DataTable dt2 = ZWL.DBUtility.DbHelperSQL.GetDataTable("Select [TrueName] from [ERPUser] where [UserName] in (" + UserNames + ")");
            string TrueName2 = "";
            foreach(DataRow R2 in dt2.Rows) {
                TrueName2 += TrueName2 == "" ? R2["TrueName"].ToString() : "," + R2["TrueName"].ToString();
            }
            R["UserBuMen"] = TrueName2;
        }

        GVData.DataSource = dt;
        GVData.DataBind();
        LabPageSum.Text = Convert.ToString(GVData.PageCount);
        LabCurrentPage.Text = Convert.ToString(((int)GVData.PageIndex + 1));
        this.GoPage.Text = LabCurrentPage.Text.ToString();
    }

    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e) {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }
    protected void btn_Search_Click(object sender, EventArgs e) {
        tb1_value.Value = TextBox1.Text;
        DataBindToGridview();
    }
    protected void btn_Add_Click(object sender, EventArgs e) {
        if(PersonList.Value != "") {
            Response.Redirect("GongGaoAdd.aspx?PersonList=" + PersonList.Value);
        }
        else {
            Response.Redirect("GongGaoAdd.aspx");
        }
    }
    protected void btn_Change_Click(object sender, EventArgs e) {
        string CheckStr = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        string[] CheckStrArray = CheckStr.Split(',');
        Response.Redirect("GongGaoModify.aspx?ID=" + CheckStrArray[0].ToString());
    }
    protected void btn_Del_Click(object sender, EventArgs e) {
        string IDlist = ZWL.Common.PublicMethod.CheckCbx(this.GVData, "CheckSelect", "LabVisible");
        if(ZWL.DBUtility.DbHelperSQL.ExecuteSQL("delete from ERPGongGao where ID in (" + IDlist + ")") == -1) {
            Response.Write("<script>alert('删除选中记录时发生错误！请重新登陆后重试！');</script>");
        }
        else {
            DataBindToGridview();
            //写系统日志
            ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
            MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            MyRiZhi.DoSomething = "用户删除公告通知";
            MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            MyRiZhi.Add();
            Response.Write("<script>alert('删除成功！');window.location.href='" + Request.RawUrl + "'</script>");
        }
    }
    protected void btn_Report_Click(object sender, EventArgs e) {

        DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet("select TitleStr,NoticeType,TypeStr,ZT,UserName,UserBuMen,TimeStr from ERPGongGao where TitleStr Like '%" + this.tb1_value.Value.Trim() + "%' and (UserBuMen='所有部门' or UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' or ','+UserBuMen+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%') order by ID desc");
        string pHeader = "信息主题|文件类型|电子围栏名称|状态|发布人|收阅人员|发布时间";
        ZWL.Common.ExcelHelper.DataTableExcel(ds.Tables[0], DateTime.Now.ToString("yyyyMMddHHmmss"), pHeader);
        //Hashtable MyTable = new Hashtable();
        //MyTable.Add("TitleStr", "信息主题");
        //MyTable.Add("UserName", "发布人");
        //MyTable.Add("TimeStr", "发布时间");
        //ZWL.Common.DataToExcel.GridViewToExcel(ZWL.DBUtility.DbHelperSQL.GetDataSet("select TitleStr,UserName,TimeStr from ERPGongGao where ID in (" + IDList + ") order by ID desc"), MyTable, "Excel报表");

    }

    #region  //分页方法

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
    #endregion

    /// <summary>
    /// 根据下级ID获取上级ID
    /// </summary>
    /// <param name="SuperiorID"></param>
    /// <returns></returns>
    private string GetSuperiorDepartmentID(string SuperiorID) {
        return ZWL.DBUtility.DbHelperSQL.GetSHSL("select [DirID] from hx_vERPBuMen where [ID]=" + SuperiorID);
    }

    private void BindTree(TreeNodeCollection Nds, int IDStr) {
        //Andy  20130925
        ArrayList listName = null;
        if(Request.QueryString["Condition"] != null && Request.QueryString["Condition"].ToString() != "") {
            listName = new ArrayList(Request.QueryString["Condition"].ToString().Split(','));
        }


        SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());
        string DepartmentID = "";
        string SuperiorID = ZWL.Common.PublicMethod.GetSessionValue("DepartmentID"); ;
        while(SuperiorID != "0") {
            DepartmentID = SuperiorID;
            SuperiorID = GetSuperiorDepartmentID(SuperiorID);
        }

        //SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());
        Conn.Open();

        // SqlCommand MyCmd = new SqlCommand("select * from ERPBuMen where DirID=" + IDStr.ToString() + " order by ID asc", Conn);
        SqlCommand MyCmd;
        if(ZWL.Common.PublicMethod.GetSessionValue("UserName") == "admin")
            MyCmd = new SqlCommand("select * from hx_vERPBuMen where DirID=" + IDStr.ToString() + " and CHARINDEX('" + DepartmentID + "',p_depart_ids)>0 order by ID asc", Conn);
        else
            MyCmd = new SqlCommand("select * from hx_vERPBuMen where DirID=" + IDStr.ToString() + " and CHARINDEX('," + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + ",',p_depart_ids)>0 order by ID asc", Conn);

        SqlDataReader MyReader = MyCmd.ExecuteReader();
        while(MyReader.Read()) {
            TreeNode OrganizationNode = new TreeNode();
            OrganizationNode.Text = MyReader["BuMenName"].ToString();
            OrganizationNode.Value = MyReader["ID"].ToString();
            int strId = int.Parse(MyReader["ID"].ToString());
            OrganizationNode.ImageUrl = "~/images/user_group.gif";
            OrganizationNode.SelectAction = TreeNodeSelectAction.Expand;
            //OrganizationNode.Expanded = true;
            //string ChildID = ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select top 1 ID from ERPBuMen where DirID=" + MyReader["ID"].ToString() + " order by ID asc");
            //if (ChildID.Trim() != "0")
            //{
            HaveChild = HaveChild + "|" + MyReader["BuMenName"].ToString() + "|";
            //}            
            OrganizationNode.ToolTip = MyReader["BuMenName"].ToString();
            //OrganizationNode.Collapse();

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            //在当前节点下加入用户    
            //SqlConnection Conn1 = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());

            SqlConnection Conn1 = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());
            Conn1.Open();
            SqlCommand MyCmd1 = new SqlCommand("select * from ERPUser "
              + "where Department = '" + MyReader["BuMenName"].ToString() + "' or Department like '%," + MyReader["BuMenName"].ToString()
              + "' or Department like '" + MyReader["BuMenName"].ToString() + ",%' or Department like '%," + MyReader["BuMenName"].ToString() + ",%' order by ID asc"
              , Conn1);
            SqlDataReader MyReader1 = MyCmd1.ExecuteReader();
            while(MyReader1.Read()) {
                TreeNode UserNode = new TreeNode();

                //Andy 20130925 选中文本框中传过来的用户
                if(listName != null) {
                    if(listName.Contains(MyReader1["UserName"].ToString())) {
                        UserNode.Checked = true;
                    }
                }

                //UserNode.Text = MyReader1["UserName"].ToString();
                UserNode.Text = MyReader1["TrueName"].ToString();
                UserNode.Value = MyReader1["ID"].ToString();
                UserNode.ImageUrl = OnLinePic(MyReader1["ID"].ToString());
                UserNode.ToolTip = MyReader1["UserName"].ToString();
                UserNode.SelectAction = TreeNodeSelectAction.Expand;

                OrganizationNode.ChildNodes.Add(UserNode);
                

            }

            MyReader1.Close();
            Conn1.Close();


            /////////////////////////////////////////////////////////////////////////////////////////////////////



            Nds.Add(OrganizationNode);
            //递归循环
            BindTree(Nds[Nds.Count - 1].ChildNodes, strId);
        }


        readTreeNode(this.ListTreeView.Nodes);
        MyReader.Close();
        Conn.Close();
    }

    //Andy 20130926 再次递归 设定选中的节点 
    protected void readTreeNode(TreeNodeCollection nodes) {
        foreach(TreeNode node in nodes) {
            if(node.ChildNodes != null) {
                readTreeNode(node.ChildNodes);

                if(node.Checked) {
                    TreeNode pNode = node.Parent;
                    while(pNode != null) {
                        pNode.Expand();
                        pNode = pNode.Parent;
                    }
                }
            }
            if(ZWL.Common.PublicMethod.GetSessionValue("UserName") != "admin") {
                #region  //控制多选框是否出现
                string Condition = "";
                if(UserName != "") {
                    Condition = " where [UserName]='" + UserName + "'";
                }
                DataTable Udt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [Department] from [ERPUser] " + Condition);
                string Departments = "";
                if(Udt.Rows.Count != 0) {
                    Departments = Udt.Rows[0][0].ToString();
                }


                int Num = 0;
                foreach(string D in Departments.Split(',')) {
                    if(node.Text == D) {
                        Num++;
                    }
                }

                if(Num == 0) {
                    if(node.Parent != null) {
                        if(node.Parent.ShowCheckBox.ToString() == "True") {
                            node.ShowCheckBox = true;
                        }
                        else {
                            node.ShowCheckBox = false;
                        }
                    }
                    else {
                        node.ShowCheckBox = false;
                    }
                }
                else {
                    node.ShowCheckBox = true;
                }

                #endregion
            }
        }
    }

    private string OnLinePic(string IDStr) {
        string ReturnStr = "~/images/U01.gif";
        //判断是否在线
        string OnlineUserName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select UserName from ERPUser where ID=" + IDStr + " and datediff(minute,ActiveTime,getdate()) < 10");
        if(OnlineUserName.Trim().Length > 0) {
            ReturnStr = "~/images/U01.gif";
        }
        else {
            ReturnStr = "~/images/U02.gif";
        }
        return ReturnStr;
    }
}