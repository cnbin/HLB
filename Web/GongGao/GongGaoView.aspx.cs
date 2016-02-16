using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class GongGao_GongGaoView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            ZWL.BLL.ERPGongGao MyModel = new ZWL.BLL.ERPGongGao();
            MyModel.GetModel(int.Parse(Request.QueryString["ID"].ToString()));
            this.Label1.Text = MyModel.TitleStr;
            this.Label2.Text = ZWL.Common.PublicMethod.GetWenJian(MyModel.FuJian, "../UploadFile/");
            this.Label4.Text = MyModel.ContentStr;
            this.Label5.Text = MyModel.TimeStr;
            this.Label3.Text = ZWL.DBUtility.DbHelperSQL.GetSHSL("select TrueName from ERPUser where UserName ='"+MyModel.UserName+"'");
            string UserNames = "'"+MyModel.UserBuMen.Replace(",","','")+"'";
            
            DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select TrueName from ERPUser where UserName in (" + UserNames + ")");
            string TrueName = "";
            foreach(DataRow R in dt.Rows)
            {
                TrueName += TrueName == "" ?  R["TrueName"].ToString() : "," + R["TrueName"].ToString();
            } 
            this.Label6.Text = TrueName;

            this.Label7.Text = MyModel.Num.ToString();
            this.Label8.Text = MyModel.NoticeType.ToString();
            this.Label9.Text = MyModel.TypeStr.ToString();
            ZWL.DBUtility.DbHelperSQL.ExecuteSQL("update ERPGongGao set num= num+1 where ID=" + Request.QueryString["ID"].ToString());
            DataEntityDataContext context = new DataEntityDataContext();
            ERPGongGaoYD model = context.ERPGongGaoYD.SingleOrDefault(p => p.FID == int.Parse(Request.QueryString["ID"].ToString()) && p.XM == ZWL.Common.PublicMethod.GetSessionValue("UserName"));
            if(model!=null)
            {
                model.SFYY = "是";
                model.YDSJ = DateTime.Now;
                context.SubmitChanges();
            }
         

            //var T = context.ERPGongGaoYD.Where(p => p.FID == int.Parse(Request.QueryString["ID"].ToString()));
            DataTable Gdt = ZWL.DBUtility.DbHelperSQL.GetDataTable("SELECT G.[ID],G.[FID],G.[SSBM] ,G.[XM] ,G.[SFYY] ,G.[YDSJ],G.[BZ1],G.[BZ2],U.[TrueName] "+
                                                                   "FROM [ERPGongGaoYD] as G left join [ERPUser] as U on G.XM=U.UserName "+
                                                                   "where G.[FID]='"+Request.QueryString["ID"].ToString()+"'");
            foreach(DataRow R in Gdt.Rows){
                if(string.IsNullOrEmpty(R["TrueName"].ToString())) {
                    R["TrueName"] = R["XM"];
                }
            }
            this.GVData.DataSource = Gdt;
            GVData.DataBind();
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../GongGao/GongGao.aspx" : Request.UrlReferrer.ToString();
        }
       
    }
}
