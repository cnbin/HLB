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

public partial class QDGL_FenceView : System.Web.UI.Page
{
    protected FenceXX Model;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();

            DataEntityDataContext context = new DataEntityDataContext();
            Model = context.FenceXX.SingleOrDefault(f => f.ID == int.Parse(Request.QueryString["ID"].ToString()));
            var T = context.ERPCommon.Where(p => p.Code == "QDDLX").OrderBy(p => p.CSort);
           
            this.lbl_MC.Text = Model.MC;
            string Coords = "";
            string[] CoordsList = Model.Coords.Split(';');
            for(int i = 0; i < CoordsList.Length;i++ ) {
                if((i+1)%4==0) { //换行
                    Coords += CoordsList[i]+ ";" +"<br />";
                }
                else {
                    Coords += CoordsList[i] + ";" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
            }
            this.lbl_Coords.Text = Coords;
            string FenceUsers = "'" + Model.FenceUser.Replace(",", "','") + "'";
            DataTable dt = ZWL.DBUtility.DbHelperSQL.GetDataTable("select [TrueName] from ERPUser Where UserName  in (" + FenceUsers + ")");
            string TrueName = "";
            foreach(DataRow R in dt.Rows) {
                TrueName += TrueName == "" ? R["TrueName"] : "," + R["TrueName"];
            }
            this.lbl_FenceUser.Text = TrueName;
            this.lbl_BZ2.Text = Model.BZ2;
            this.lbl_LX.Text = Model.LX;
            this.lbl_DZ.Text = Model.DZ;
            this.lbl_ZT.Text = Model.BZ1;
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../QDGL/Fence.aspx" : Request.UrlReferrer.ToString();
        }
    }
}