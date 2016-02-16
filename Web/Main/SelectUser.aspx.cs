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
using System.Security.Cryptography;
using System.Text;

public partial class SelectUser2 : System.Web.UI.Page {

    public string HaveChild = "";
    string IDS = ""; //用来保存有存在上级的ID
    string QIDS = "";//部门权限ID
    string UserName = ""; //用户名，由URL传入
    string ClickPage = ""; //点击页面
    protected void Page_Load(object sender, EventArgs e) {

        if(!Page.IsPostBack) {

            try {
                ClickPage = Request.QueryString["ClickPage"].ToString();
            }
            catch {
            }
            //查询得出角色对应部门，得出部门以后设置ID

            try {
                UserName = Request.QueryString["UserName"].ToString();
            }
            catch {
                UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName").ToString();
            }
            string Condition = "";
            if(UserName != "") {
                Condition = " where [UserName]='" + UserName + "'";
            }
            string Departments = "";
            DataTable Udt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [Department] from [ERPUser] " + Condition);
            Departments = Udt.Rows[0][0].ToString();
            Departments = "'" + Departments.Replace(",", "','") + "'";//拼装IDS，用于SQL语句
            //获取角色的部门
            DataTable BMdt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [ID],[DirID] from ERPBuMen where [BuMenName] in (" + Departments + ")");



            //dirid = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 DirID from ERPBuMen where ID='" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "'");
            //BindTree(this.ListTreeView.Nodes, Convert.ToInt32(dirid));

            foreach(DataRow BMR in BMdt.Rows) {
                QIDS += QIDS == "" ? BMR[0].ToString() : "," + BMR[0].ToString(); //获取用户拥有的权限
                DataTable Ddt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [ID] from ERPBuMen where [DirID] in (" + BMR[0].ToString() + ") or  [ID] in (" + BMR[0].ToString() + ") order by [DirID]");
                //判断是否有存在下级ID，若存在，则保存在IDS
                if(Ddt.Rows.Count > 0) { //若数量大于0.则存在上级菜单
                    foreach(DataRow DR in Ddt.Rows) {
                        IDS += IDS == "" ? DR[0].ToString() : "," + DR[0].ToString();
                    }
                }
            }
            string dirid = "";
            dirid = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 DirID from ERPBuMen where ID='" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "'");
            BindTree(this.ListTreeView.Nodes, Convert.ToInt32(dirid));
            //this.ListTreeView.Attributes.Add("onclick", "CheckEvent(event)");
            //BindTree(this.ListTreeView.Nodes, 0);

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


        SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());

        string DepartmentID = "";
        string SuperiorID = QIDS.Split(',')[0];

        while(SuperiorID != "0") {
            DepartmentID = SuperiorID;
            SuperiorID = GetSuperiorDepartmentID(SuperiorID);
        }

        //SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());
        Conn.Open();

        // SqlCommand MyCmd = new SqlCommand("select * from ERPBuMen where DirID=" + IDStr.ToString() + " order by ID asc", Conn);
        SqlCommand MyCmd;
        if(ZWL.Common.PublicMethod.GetSessionValue("UserName") == "admin" || ClickPage == "JiaoSe") {
            //读取全部
            MyCmd = new SqlCommand("select * from hx_vERPBuMen where DirID=" + IDStr.ToString() + " and CHARINDEX('" + DepartmentID + "',p_depart_ids)>0 order by ID asc", Conn);
        }
        else {
            //读取单个部门
            MyCmd = new SqlCommand("select * from hx_vERPBuMen where DirID=" + IDStr.ToString() + " and CHARINDEX('," + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + ",',p_depart_ids)>0 order by ID asc", Conn);
        }
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
            SqlCommand MyCmd1 = new SqlCommand("select * from ERPUser where Department like '%" + MyReader["BuMenName"].ToString() + "%' order by ID asc", Conn1);
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
            string PersonList = "";
                try {
                    PersonList = Request.QueryString["PersonList"].ToString();
                }
                catch {
                }

           
            if(ClickPage != "JiaoSe") { //如果为角色，则不进入判断
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
            else {
                if(PersonList != "") {
                    foreach(string P in PersonList.Split(',')) {
                        if(P == node.ToolTip) {
                            node.Checked = true;
                        }
                    }
                }
            }

            
               

        }
    }


    protected void btnSelect_Click(object sender, ImageClickEventArgs e) {
        string result = "";
        foreach(TreeNode parent in this.ListTreeView.Nodes) {
            foreach(TreeNode node in parent.ChildNodes) {
                StringBuilder sb = new StringBuilder();
                foreach(TreeNode subNode in node.ChildNodes) {
                    if(subNode.Checked) {
                        sb.AppendFormat("{0},", subNode.Text);
                    }
                }
                if(sb.Length > 0) {
                    //sb.Insert(0, string.Format("{0}(", node.Text));
                    //sb.Append(")");
                    //result += sb.ToString().Repla + "," ce(",)", ")") + ";";
                    result += sb.ToString() + ",";
                }
                else if(node.Checked) {
                    //result += node.Text ;
                    result += node.Text + ",";
                }
            }
        }
        //TextBox4.Text = result;
        //Response.Write("window.opener=null;window.close()");

        ZWL.Common.MessageBox.Show(this, result);
        //Helper.CloseWin(this, result.Trim(';'));
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

    /*
 protected void ListTreeView_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
 {
         //同级只展开一个使用下列程序
         //TreeNodeCollection ts = null;
         if (e.Node.Parent == null)
         {
             ts = ((TreeView)sender).Nodes;
         }
         else
             ts = e.Node.Parent.ChildNodes;
         foreach (TreeNode node in ts)
         {
             if (node != e.Node)
             {
                 node.Collapse();
             }
         } 
         //只展开一个第一级使用下列程序
         TreeNodeCollection ts = null;
         if (e.Node.Parent == null)
         {
             ts = ((TreeView)sender).Nodes;
             foreach (TreeNode node in ts)
             {
                 if (node != e.Node)
                 {
                     node.Collapse();
                 }
             }
         }
 }
 protected void ListTreeView_SelectedNodeChanged(object sender, EventArgs e)
 {
     for (int i = 0; i < this.ListTreeView.Nodes.Count; i++ )
     {
         if (this.ListTreeView.SelectedValue == this.ListTreeView.Nodes[i].Value)
         {
             this.ListTreeView.SelectedNode.Expanded = true;
         }
         else 
         {
             for (int j = 0; j < this.ListTreeView.SelectedNode.Parent.ChildNodes.Count;j++ )
             {
                 this.ListTreeView.SelectedNode.Parent.ChildNodes[j].CollapseAll();

             }
             this.ListTreeView.SelectedNode.Parent.Expanded = true;
             this.ListTreeView.SelectedNode.Expanded  = true;
         }
     }
 }

*/


}
