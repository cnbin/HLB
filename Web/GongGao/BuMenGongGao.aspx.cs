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

public partial class GongGao_BuMenGongGao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //判断是否是列表显示参数
          


            ZWL.Common.PublicMethod.CheckSession();
            DataBindToGridview();
          //  GetDaoHang(int.Parse(Request.QueryString["DirID"].ToString()));
            //绑定部门树
            BindBuMenTree(this.ListTreeView.Nodes, 0);
        }       

       

      
    }
    /// <summary>
    /// 有权限就显示对应按钮
    /// </summary>
    /// <param name="ViewStr"></param>
    /// <param name="QuanXianStr"></param>
    /// <returns></returns>
    public string IFView(string ViewStr,string QuanXianStr)
    {
        if (ZWL.Common.PublicMethod.StrIFIn(QuanXianStr, ZWL.Common.PublicMethod.GetSessionValue("QuanXian")) == true)
        {
            return ViewStr;
        }
        else
        {
            return "";
        }
    }
    public void BindBuMenTree(TreeNodeCollection Nds, int IDStr)
    {
        DataSet MYDT=ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPBuMen where DirID=" + IDStr.ToString() + " order by ID asc");
        for(int i=0;i<MYDT.Tables[0].Rows.Count;i++)
        {
            TreeNode OrganizationNode = new TreeNode();
            string CharManStr = "";
            if (MYDT.Tables[0].Rows[i]["ChargeMan"].ToString().Trim().Length <= 0)
            {
                CharManStr = "<font color=\"Red\">[未设置负责人]</font>";
            }
            else
            {
                CharManStr = MYDT.Tables[0].Rows[i]["ChargeMan"].ToString().Trim();
            }

            OrganizationNode.Text = MYDT.Tables[0].Rows[i]["BuMenName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;主管：" + MYDT.Tables[0].Rows[i]["ChargeMan"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公告：" + IFView("<a class=\"BlueCss\" href=\"GongGao.aspx?bmID=" + MYDT.Tables[0].Rows[i]["ID"].ToString() + "&Type=部门1&DirID=单位" + "\">[" + ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select Count(*) as sn from GongGaoBuMen where BuMenID1=" + MYDT.Tables[0].Rows[i]["ID"].ToString()) + "]</a>", "|998A|");
            OrganizationNode.ToolTip = "部门主管：" + MYDT.Tables[0].Rows[i]["ChargeMan"].ToString() + "\n电话：" + MYDT.Tables[0].Rows[i]["TelStr"].ToString() + "\n传真：" + MYDT.Tables[0].Rows[i]["ChuanZhen"].ToString() + "\n备注：" + MYDT.Tables[0].Rows[i]["BackInfo"].ToString();
            
            OrganizationNode.Value = MYDT.Tables[0].Rows[i]["ID"].ToString();
            int strId = int.Parse(MYDT.Tables[0].Rows[i]["ID"].ToString());
            OrganizationNode.ImageUrl = "~/images/user_group.gif";
            OrganizationNode.SelectAction = TreeNodeSelectAction.Expand;
            
            OrganizationNode.Expand();
            Nds.Add(OrganizationNode);
            //递归循环
            BindBuMenTree(Nds[Nds.Count - 1].ChildNodes, strId);
        }
    }
   
    public void DataBindToGridview()
    {
        ZWL.BLL.ERPBuMen MyERPBuMen = new ZWL.BLL.ERPBuMen();
        string DirID = "0";
        try
        {
            DirID = Request.QueryString["DirID"].ToString();
        }
        catch { }


    }
    
    protected void GVData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ZWL.Common.PublicMethod.GridViewRowDataBound(e);
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        DataBindToGridview();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("BuMenInfoAdd.aspx?View=List&Type=" + Request.QueryString["Type"].ToString() + "&DirID=" + Request.QueryString["DirID"].ToString());
    }
   
    
    
}
