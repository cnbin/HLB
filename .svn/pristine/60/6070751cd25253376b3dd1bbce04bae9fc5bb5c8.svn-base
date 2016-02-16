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

public partial class SystemManage_SystemJiaoSeUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            this.Label1.Text = Request.QueryString["JiaoSeName"].ToString();
             this.UserName_Input.Value= ReturnUserInJiaoSe(Request.QueryString["JiaoSeName"].ToString());
             string UserNames = "'" + UserName_Input.Value.Replace(",", "','") + "'";
             DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [TrueName] from ERPUser where [UserName] in (" + UserNames + ")");
             string TrueName = "";
            foreach(DataRow R in dt.Rows) {
                TrueName += TrueName == "" ? R["TrueName"].ToString() : "," + R["TrueName"].ToString();
             }
            this.TextBox1.Text = TrueName;
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/SystemJiaoSe.aspx" : Request.UrlReferrer.ToString();
        }
    }

    public string ReturnUserInJiaoSe(string JiaoSeName)
    {
        string ReturnStr = "";
        DataSet MYDT = ZWL.DBUtility.DbHelperSQL.GetDataSet("select UserName from ERPUser where ','+JiaoSe+','  like  '%," + JiaoSeName + ",%'");
        for (int i = 0; i < MYDT.Tables[0].Rows.Count; i++)
        {
            if (ReturnStr.Trim().Length > 0)
            {
                ReturnStr =ReturnStr+","+ MYDT.Tables[0].Rows[i]["UserName"].ToString();
            }
            else
            {
                ReturnStr = MYDT.Tables[0].Rows[i]["UserName"].ToString();
            }
        }
        return ReturnStr;
    }
   
    protected void btn_Sub_Click(object sender, EventArgs e) {
        ////设置所有在文本框中的用户角色加上当前角色，先去掉所有角色，再加上对应角色
        //string InitJiaoSeSql = "update ERPUser Set JiaoSe=','+JiaoSe+','";
        //string RemoveJiaoSeSql = "update ERPUser Set JiaoSe=replace(JiaoSe,'," + Request.QueryString["JiaoSeName"].ToString() + ",',',') ";
        ////替换后，如果只有这个角色，那么替换后，只有 ， 所以加入一个，防止Sql运行出错
        //string OKKTemp = "update ERPUser Set JiaoSe=',,' where JiaoSe=','";
        //string OKJiaoSeSql1 = "update ERPUser Set JiaoSe=left(JiaoSe,len(JiaoSe)-1)";
        //string OKJiaoSeSql2 = "update ERPUser Set JiaoSe=right(JiaoSe,len(JiaoSe)-1)";

        //string AddJiaoSeSql1 = "update ERPUser Set JiaoSe='" + Request.QueryString["JiaoSeName"].ToString() + "'   where len(JiaoSe)<=1 and UserName in('" + this.TextBox1.Text.ToString().Replace(",", "','") + "')";
        //string AddJiaoSeSql2 = "update ERPUser Set JiaoSe=JiaoSe+'," + Request.QueryString["JiaoSeName"].ToString() + "'   where len(JiaoSe)>1 and JiaoSe!='" + Request.QueryString["JiaoSeName"].ToString() + "' and UserName in('" + this.TextBox1.Text.ToString().Replace(",", "','") + "')";
        string DeleteUser = "";
        DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("Select [UserName] from [ERPUser] Where [JiaoSe] ='" + Request.QueryString["JiaoSeName"].ToString()+"'");
        foreach(DataRow R in dt.Rows) {
            DeleteUser += DeleteUser == "" ? "'"+R["UserName"]+"'" : ",'" + R["UserName"]+"'";
        }
        string DeleteSQLStr = "update ERPUser Set JiaoSe='' Where [UserName] in (" + DeleteUser + ")";
        ZWL.DBUtility.DbHelperSQL.ExecuteSQL(DeleteSQLStr);

        string UpdateAddUser = "'" + this.UserName_Input.Value.Replace(",", "','") + "'";
        string UpdateSQLStr = "update ERPUser Set JiaoSe ='" + Request.QueryString["JiaoSeName"].ToString() + "' Where UserName in (" + UpdateAddUser + ")";

        ZWL.DBUtility.DbHelperSQL.ExecuteSQL(UpdateSQLStr);
        //ZWL.DBUtility.DbHelperSQL.ExecuteSQL(RemoveJiaoSeSql);
        //ZWL.DBUtility.DbHelperSQL.ExecuteSQL(OKKTemp);
        //ZWL.DBUtility.DbHelperSQL.ExecuteSQL(OKJiaoSeSql1);
        //ZWL.DBUtility.DbHelperSQL.ExecuteSQL(OKJiaoSeSql2);
        //ZWL.DBUtility.DbHelperSQL.ExecuteSQL(AddJiaoSeSql1);
        //ZWL.DBUtility.DbHelperSQL.ExecuteSQL(AddJiaoSeSql2);
        Response.Write("<script>alert('角色用户设置成功！');window.location.href='SystemJiaoSe.aspx';</script>");
        //ZWL.Common.MessageBox.ShowAndRedirect(this, "角色用户设置成功！", "SystemJiaoSe.aspx");
    }
}
