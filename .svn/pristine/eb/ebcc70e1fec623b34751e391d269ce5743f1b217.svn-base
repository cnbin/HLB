using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZWL.Common;
using System.Data.SqlClient;

public partial class GongGao_GongGaoAdd : System.Web.UI.Page
{
    public string HaveChild = "";
    //string IDS = ""; //用来保存有存在上级的ID
    //string QIDS = "";//部门权限ID
    string UserName = ""; //用户名
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //设置上传的附件为空
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", "");
            DataEntityDataContext context = new DataEntityDataContext();
            var T = context.ERPCommon.Where(p => p.Code == "TZWJLX").OrderBy(p => p.CSort);


            //不是单位通知，那么不显示收阅部门信息
            try {
                this.Send_Person.Value = Request.QueryString["UserName"];
            }
            catch {
            }
            if(Request.UrlReferrer.ToString().IndexOf("Main.aspx") == -1) {
                ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../GongGao/GongGao.aspx" : Request.UrlReferrer.ToString();
            }
            else {
                ReturnInput.Value = "../GongGao/GongGao.aspx";
            }

            #region  //树状图
            UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            string Condition = "";
            if(UserName != "") {
                Condition = " where [UserName]='" + UserName + "'";
            }
            string Departments = "";
            DataTable Udt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [Department] from [ERPUser] " + Condition);
            if(Udt.Rows.Count !=0) {
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
            this.ListTreeView.Attributes.Add("onclick", "CheckEvent(event)");
            string dirid = "0";
            dirid = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 DirID from ERPBuMen where ID='" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "'");
            BindTree(this.ListTreeView.Nodes, Convert.ToInt32(dirid));
           
            #endregion
        }
    }
   

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
            {
                Response.Write("<script>window.open('../DsoFramer/ReadFile.aspx?FilePath=" + this.CheckBoxList1.SelectedItem.Value + "');</script>");
            }
        }
        catch
        { }
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.CheckBoxList1.SelectedItem.Text.Trim().Length > 0)
            {
                Response.Write("<script>window.open('../DsoFramer/EditFile.aspx?FilePath=" + this.CheckBoxList1.SelectedItem.Value + "');</script>");
            }
        }
        catch
        { }
    }
    protected void btn_Sub_Click(object sender, EventArgs e) {
        ZWL.BLL.ERPGongGao Model = new ZWL.BLL.ERPGongGao();
        Model.TitleStr = this.TextBox1.Text;
        Model.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        Model.UserBuMen = this.Send_Person.Value;
        Model.NoticeType = this.DropDownList1.SelectedValue;
        Model.ZT = this.ZT.SelectedValue;
        Model.FuJian = ZWL.Common.PublicMethod.GetSessionValue("WenJianList");
        Model.TypeStr = this.TextBox3.Text;
        Model.ContentStr = this.TxtContent.Text;
        Model.Num = 0;
        int id = Model.Add();

        DataEntityDataContext context = new DataEntityDataContext();
        if(!string.IsNullOrEmpty(this.Send_Person.Value)) {
            string[] ss =this.Send_Person.Value.Split(',');
            for(int i = 0; i < ss.Length; i++) {
                ERPGongGaoYD model = new ERPGongGaoYD();
                model.FID = id;
                model.SSBM = context.ERPUser.SingleOrDefault(p => p.UserName == ZWL.Common.PublicMethod.GetSessionValue("UserName")).Department;
                model.XM = ss[i];
                model.SFYY = "否";
                model.BZ1 = "";
                model.BZ2 = "";
                context.ERPGongGaoYD.InsertOnSubmit(model);
                context.SubmitChanges();
            }
        }


        //写系统日志
        ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
        MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        MyRiZhi.DoSomething = "用户添加通知公告信息(" + this.TextBox1.Text + ")";
        MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        MyRiZhi.Add();
        Response.Write("<script>alert('通知公告信息添加成功！');window.location.href='GongGao.aspx'</script>");
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e) {
        string FileNameStr = ZWL.Common.PublicMethod.UploadFileIntoDir(this.FileUpload1, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName));
        if(ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Trim() == "") {
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", FileNameStr);
        }
        else {
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList") + "|" + FileNameStr);
        }
        ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e) {
        try {
            for(int i = 0; i < this.CheckBoxList1.Items.Count; i++) {
                if(this.CheckBoxList1.Items[i].Selected == true) {
                    ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Replace(this.CheckBoxList1.Items[i].Value, "").Replace("||", "|"));
                }
            }
            ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
        }
        catch {
        }
    }

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
        string DepartmentID = "";
        string SuperiorID = ZWL.Common.PublicMethod.GetSessionValue("DepartmentID"); ;
        while(SuperiorID != "0") {
            DepartmentID = SuperiorID;
            SuperiorID = GetSuperiorDepartmentID(SuperiorID);
        }

        SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());


        //SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());
        Conn.Open();

        // SqlCommand MyCmd = new SqlCommand("select * from ERPBuMen where DirID=" + IDStr.ToString() + " order by ID asc", Conn);
        //SqlCommand MyCmd = new SqlCommand("select * from hx_vERPBuMen where DirID=" + IDStr.ToString() + " and CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0 order by ID asc", Conn);
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
            //OrganizationNode.ToolTip = MyReader["BuMenName"].ToString();
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
                //int Num = 0;
                //foreach(string Q in QIDS.Split(',')) {
                //    if(Q == strId.ToString()) {
                //        Num++;
                //    }
                //}
                //foreach(string I in IDS.Split(',')) {
                //    if(I == strId.ToString()) {
                //        Num++;
                //    }
                //}

                //if(Num > 0 || UserName == "")//存在权限，才可以把用户加入
                //{
                    OrganizationNode.ChildNodes.Add(UserNode);
                //}

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
                string PersonList = "";
                try {
                    PersonList = Request.QueryString["PersonList"].ToString();
                }
                catch {
                }
                if(Num == 0) {
                    if(node.Parent != null) {
                        if(node.Parent.ShowCheckBox.ToString() == "True") {

                            node.ShowCheckBox = true;
                            if(PersonList != "") {
                                foreach(string P in PersonList.Split(',')) {
                                    if(P == node.ToolTip) {
                                        node.Checked = true;
                                    }
                                }
                            }
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
                    if(PersonList != "") {
                        foreach(string P in PersonList.Split(',')) {
                            if(P == node.ToolTip) {
                                node.Checked = true;
                            }
                        }
                    }

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