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
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

public partial class RJOnlineUser : System.Web.UI.Page
{
    public List<TXX> list = new List<TXX>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindTree(this.ListTreeView.Nodes, 0);
            string dirid = "0";
            dirid = ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 DirID from ERPBuMen where ID='" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "'");
            BindTree(this.ListTreeView.Nodes, Convert.ToInt32(dirid));
            
            //this.txtYuJiTiXing.Text = DateTime.Now.ToString("yyyy-MM-dd")+" 00:00:00";//只选当天记录;
            this.TextBox1.Text = DateTime.Now.ToString();
            ListTreeView.Attributes.Add("onclick ", "OnClientTreeNodeChecked(event) ");
            string uname = Request["username"];
            if ((Request["token"] ?? "") == "ajax" && !string.IsNullOrEmpty(uname) )
            {
                DataEntityDataContext context = new DataEntityDataContext();
                var T = context.WZXX.Where(p => p.ID > 0  && p.XM==uname);
  
                if (!string.IsNullOrEmpty(Request["stime"]))
                {
                    T = T.Where(p => p.RQ >= DateTime.Parse(Request["stime"]));
                }
                if (!string.IsNullOrEmpty(Request["etime"]))
                {
                    T = T.Where(p => p.RQ <= DateTime.Parse(Request["etime"]));
                }
                T = T.OrderByDescending(p => p.RQ);
                string res = "";
                foreach (var item in T)
                {
                    res += item.X + "," + item.Y + "|";
                }
                Response.Write(res);
                Response.End();
            }
            SettingWorkTime.Visible = ZWL.Common.PublicMethod.StrIFIn("|001A|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            Notice.Visible = ZWL.Common.PublicMethod.StrIFIn("|004|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            AttendanceSel.Visible = ZWL.Common.PublicMethod.StrIFIn("|107|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
            WarningSel.Visible = ZWL.Common.PublicMethod.StrIFIn("|109|", ZWL.Common.PublicMethod.GetSessionValue("QuanXian"));
        }
    }
    protected static string DecryptDBStr(string Text, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        int len;
        len = Text.Length / 2;
        byte[] inputByteArray = new byte[len];
        int x, i;
        for (x = 0; x < len; x++)
        {
            i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
            inputByteArray[x] = (byte)i;
        }
        des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Encoding.Default.GetString(ms.ToArray());
    }

    /// <summary>
    /// 根据下级ID获取上级ID
    /// </summary>
    /// <param name="SuperiorID"></param>
    /// <returns></returns>
    private string GetSuperiorDepartmentID(string SuperiorID) {
        return ZWL.DBUtility.DbHelperSQL.GetSHSL("select [DirID] from hx_vERPBuMen where [ID]=" + SuperiorID);
    }

    private void BindTree(TreeNodeCollection Nds, int IDStr)
    {
        //SqlConnection Conn = new SqlConnection(DecryptDBStr(ConfigurationManager.AppSettings["SQLConnectionString"].ToString(),"zhangweilong"));
        SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());
        Conn.Open();
        string DepartmentID = "";
        string SuperiorID = ZWL.Common.PublicMethod.GetSessionValue("DepartmentID"); ;
        while(SuperiorID != "0") {
            DepartmentID = SuperiorID;
            SuperiorID = GetSuperiorDepartmentID(SuperiorID);
        }

        SqlCommand MyCmd;
        if (ZWL.Common.PublicMethod.GetSessionValue("UserName") == "admin")
            MyCmd = new SqlCommand("select * from hx_vERPBuMen where DirID=" + IDStr.ToString() + "and CHARINDEX('" + DepartmentID + "',p_depart_ids)>0 order by ID asc", Conn);
        else
            MyCmd = new SqlCommand("select * from hx_vERPBuMen where DirID=" + IDStr.ToString() + " and CHARINDEX('," + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + ",',p_depart_ids)>0 order by ID asc", Conn);

        SqlDataReader MyReader = MyCmd.ExecuteReader();
        while (MyReader.Read())
        {
            TreeNode OrganizationNode = new TreeNode();
            OrganizationNode.Text = MyReader["BuMenName"].ToString();
            OrganizationNode.Value = MyReader["ID"].ToString();
            int strId = int.Parse(MyReader["ID"].ToString());
            OrganizationNode.ImageUrl = "~/images/user_group.gif";
            OrganizationNode.SelectAction = TreeNodeSelectAction.Expand;
            OrganizationNode.Expanded = true;           

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            //在当前节点下加入用户    
            SqlConnection Conn1 = new SqlConnection(ConfigurationManager.AppSettings["SQLConnectionString"].ToString());
            Conn1.Open();
           // SqlCommand MyCmd1 = new SqlCommand("select * from ERPUser where Department like '%" + MyReader["BuMenName"].ToString() + "%' order by ID asc", Conn1);
            SqlCommand MyCmd1 = new SqlCommand("select * from ERPUser where Department = '" + MyReader["BuMenName"].ToString() + "' order by ID asc", Conn1);
            SqlDataReader MyReader1 = MyCmd1.ExecuteReader();
            while (MyReader1.Read())
            {
                TreeNode UserNode = new TreeNode();
                //UserNode.Text = MyReader1["UserName"].ToString();
                UserNode.Text = MyReader1["TrueName"].ToString();
                UserNode.Value = MyReader1["ID"].ToString();
                UserNode.ToolTip = MyReader1["UserName"].ToString();
                UserNode.ImageUrl = OnLinePic(MyReader1["ID"].ToString());
                UserNode.NavigateUrl ="javascript:void(0);";
                OrganizationNode.ChildNodes.Add(UserNode);              
            }
            MyReader1.Close();
            Conn1.Close();
            /////////////////////////////////////////////////////////////////////////////////////////////////////

            Nds.Add(OrganizationNode);

            //递归循环
            BindTree(Nds[Nds.Count - 1].ChildNodes, strId);
        }
        MyReader.Close();
        Conn.Close(); 
    }

    private string OnLinePic(string IDStr)
    {
        string ReturnStr = "~/images/U01.gif";
        //判断是否在线
        string OnlineUserName = ZWL.DBUtility.DbHelperSQL.GetSHSL("select UserName from ERPUser where ID=" + IDStr + " and datediff(minute,ActiveTime,getdate())<20");
        if (OnlineUserName.Trim().Length > 0)
        {
            ReturnStr = "~/images/U01.gif";
        }
        else
        {
            ReturnStr = "~/images/U02.gif";
        }
        return ReturnStr;
    }
    protected void QD_Click(object sender, EventArgs e)
    {
        
       
    }
}

public class TXX
{
public string jd{get;set;}
    public string wd{get;set;}
}
