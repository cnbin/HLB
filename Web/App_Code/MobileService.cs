using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;

/// <summary>
///MobileService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
[System.Web.Script.Services.ScriptService]
public class MobileService : System.Web.Services.WebService
{
    const int iPageSize = 10;
    string appName = "dwt", orgName = "woye";
    string clientID = "YXA6-62JYEWBEeW3HruhaRA2Yw", clientSecret = "YXA6nQ96j0XEzo5wTbmdWqLMQpHliPg";
    public MobileService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    public static DataTable GetDataByPager2000(string sql, string orderfield, int pageindex, int pagesize, int TotalCount)
    {
        string cmd = "proc_SelectForPager";
        SqlParameter[] para = new SqlParameter[5];
        para[0] = new SqlParameter("@Sql", sql);
        para[1] = new SqlParameter("@Order", orderfield);
        para[2] = new SqlParameter("@CurrentPage", pageindex);
        para[3] = new SqlParameter("@PageSize", pagesize);
        para[4] = new SqlParameter("@TotalCount", TotalCount);

        return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SQLConnectionString"], CommandType.StoredProcedure, cmd, para).Tables[0];

    }


    [WebMethod]
    public string Login(string uname, string pass)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.system_info.Where(p => p.LX == "POI点显示范围设置");
        var TT = context.system_info.Where(p => p.LX == "位置采集时间间隔");
        string poifw = "0";
        string cjjg = "0";
        string coords = "0";
        string truename = "";
        if (T.Count() > 0)
        {
            poifw = T.First().CJJGSJ.ToString();
        }
        if (TT.Count() > 0)
        {
            cjjg = TT.First().CJJGSJ.ToString();
        }
        ZWL.BLL.ERPNWorkToDo Mymodel = new ZWL.BLL.ERPNWorkToDo();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string res = "0";
        List<User> list = new List<User>();
        string strSql = "select [ID],[UserName],[UserPwd],[TrueName],[Serils],[Department],[JiaoSe],[ActiveTime],[ZhiWei],[ZaiGang],[EmailStr],[GroupName],[JiaTingDianHua],[VoipAccount],[VoipPwd],[SubAccountSid],[SubToken] from hx_vERPUser where UserName='" + uname + "' and UserPwd='" + ZWL.Common.DEncrypt.DESEncrypt.Encrypt(pass) + "' and IfLogin='是' ";
        // string strSql2 = "select [ID],[Coords] from FenceXX where CHARINDEX('" + uname + "',FenceUser)>0 ";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", 1, iPageSize, 0);
        // DataTable dt2 = GetDataByPager2000(strSql2, "ID desc", 1, iPageSize, 0);
        //if (dt2.Rows.Count>0)
        //    coords = dt2.Rows[0]["Coords"].ToString();
        User oGCZLCXData = new User();
        foreach (DataRow dr in dt.Rows)
        {
            // User oGCZLCXData = new User();
            oGCZLCXData.ID = int.Parse(dr["ID"].ToString());

            oGCZLCXData.UserName = dr["UserName"].ToString();
            oGCZLCXData.UserPwd = dr["UserPwd"].ToString();
            oGCZLCXData.TrueName = dr["TrueName"].ToString();
            oGCZLCXData.Mobile = dr["JiaTingDianHua"].ToString();
            oGCZLCXData.Department = dr["Department"].ToString();
            oGCZLCXData.JiaoSe = dr["JiaoSe"].ToString();
            if (dr["ActiveTime"].ToString() != "")
            {
                oGCZLCXData.ActiveTime = DateTime.Parse(dr["ActiveTime"].ToString()).ToString("yyyy-MM-dd");
            }
            oGCZLCXData.ZhiWei = dr["ZhiWei"].ToString();
            oGCZLCXData.ZaiGang = dr["ZaiGang"].ToString();
            oGCZLCXData.EmailStr = dr["EmailStr"].ToString();
            oGCZLCXData.GroupName = dr["GroupName"].ToString();

            oGCZLCXData.WZCJJG = cjjg;
            oGCZLCXData.POIFW = poifw;
            //oGCZLCXData.eFence = coords;
            truename = dr["TrueName"].ToString();
            oGCZLCXData.VoipAccount = dr["VoipAccount"].ToString();
            oGCZLCXData.VoipPwd = dr["VoipPwd"].ToString();
            oGCZLCXData.SubAccountSid = dr["SubAccountSid"].ToString();
            oGCZLCXData.SubToken = dr["SubToken"].ToString();
            list.Add(oGCZLCXData);
        }
        string strSql2 = "select top 1 [ID],[Coords],[WHSJ] from FenceXX where CHARINDEX('" + uname + "',FenceUser)>0 or CHARINDEX('" + truename + "',FenceUser)>0";
        DataTable dt2 = GetDataByPager2000(strSql2, "WHSJ desc", 1, iPageSize, 0);
        if (dt2.Rows.Count > 0)
            coords = dt2.Rows[0]["Coords"].ToString();
        oGCZLCXData.eFence = coords;
        res = jss.Serialize(list);
        return res;
    }

    [WebMethod]
    public string Regist(string uname, string pass, string sj, string xb, string imei)
    {

        if (ZWL.Common.PublicMethod.IFExists("UserName", "ERPUser", 0, uname) == true)
        {

            ZWL.BLL.ERPUser MyBuMen = new ZWL.BLL.ERPUser();
            MyBuMen.UserName = uname;
            MyBuMen.UserPwd = ZWL.Common.DEncrypt.DESEncrypt.Encrypt(pass);
            MyBuMen.TrueName = uname;
            MyBuMen.Serils = uname;
            MyBuMen.Department = "";
            MyBuMen.JiaoSe = "";
            MyBuMen.ZhiWei = "";
            MyBuMen.ZaiGang = "在岗";
            MyBuMen.EmailStr = "";
            MyBuMen.IfLogin = "否";
            MyBuMen.Sex = xb;
            MyBuMen.BackInfo = imei;
            MyBuMen.BirthDay = "";
            MyBuMen.MingZu = "";
            MyBuMen.SFZSerils = "";
            MyBuMen.HunYing = "";
            MyBuMen.ZhengZhiMianMao = "";
            MyBuMen.JiGuan = "";
            MyBuMen.HuKou = "";
            MyBuMen.XueLi = "";
            MyBuMen.ZhiCheng = "";
            MyBuMen.BiYeYuanXiao = "";
            MyBuMen.ZhuanYe = "";
            MyBuMen.CanJiaGongZuoTime = "";
            MyBuMen.JiaRuBenDanWeiTime = "";
            MyBuMen.JiaTingDianHua = sj;
            MyBuMen.JiaTingAddress = "";
            MyBuMen.GangWeiBianDong = "";
            MyBuMen.JiaoYueBeiJing = "";
            MyBuMen.GongZuoJianLi = "";
            MyBuMen.SheHuiGuanXi = "";
            MyBuMen.JiangChengJiLu = "";
            MyBuMen.ZhiWuQingKuang = "";
            MyBuMen.PeiXunJiLu = "";
            MyBuMen.DanBaoJiLu = "";
            MyBuMen.NaoDongHeTong = "";
            MyBuMen.SheBaoJiaoNa = "";
            MyBuMen.TiJianJiLu = "";
            MyBuMen.BeiZhuStr = "";
            MyBuMen.FuJian = "";
            MyBuMen.Add();

            EaseMobDemo easeMob = new EaseMobDemo(clientID, clientSecret, appName, orgName);
            StringBuilder _build = new StringBuilder();
            _build.Append("{");
            _build.AppendFormat("\"username\": \"{0}\",\"password\": \"{1}\"", uname, pass);
            _build.Append("}");
            easeMob.ReqUrl(easeMob.easeMobUrl + "users", "POST", _build.ToString(), easeMob.token);
            return "1";
        }
        else
        {
            return "0";
        }
    }

    [WebMethod]
    public string ChangePwd(string username, string pwd)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.ERPUser.SingleOrDefault(p => p.UserName == username);

        T.UserPwd = ZWL.Common.DEncrypt.DESEncrypt.Encrypt(pwd);
        context.ERPUser.Context.SubmitChanges();
        EaseMobDemo easeMob = new EaseMobDemo(clientID, clientSecret, appName, orgName);
        easeMob.ReqUrl(easeMob.easeMobUrl + "users/" + username + "/password", "PUT", "{\"newpassword\" : \"" + pwd + "\"}", easeMob.token);
        return "1";
    }

    [WebMethod]
    public string GetUserInfo(string uname)
    {
        try
        {
            ZWL.BLL.ERPNWorkToDo Mymodel = new ZWL.BLL.ERPNWorkToDo();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string res = "";
            List<User> list = new List<User>();
            string strSql = "select [ID],[UserName],[UserPwd],[TrueName],[Serils],[Department],[JiaoSe],[ActiveTime],[ZhiWei],[ZaiGang],[EmailStr],[GroupName] from ERPUser where UserName='" + uname + "'";
            DataTable dt = GetDataByPager2000(strSql, "ID desc", 1, iPageSize, 0);
            foreach (DataRow dr in dt.Rows)
            {

                User oGCZLCXData = new User();
                oGCZLCXData.ID = int.Parse(dr["ID"].ToString());

                oGCZLCXData.UserName = dr["UserName"].ToString();
                oGCZLCXData.UserPwd = dr["UserPwd"].ToString();
                oGCZLCXData.TrueName = dr["TrueName"].ToString();
                oGCZLCXData.Serils = dr["Serils"].ToString();
                oGCZLCXData.Department = dr["Department"].ToString();
                oGCZLCXData.JiaoSe = dr["JiaoSe"].ToString();
                if (dr["ActiveTime"].ToString() != "")
                {
                    oGCZLCXData.ActiveTime = DateTime.Parse(dr["ActiveTime"].ToString()).ToString("yyyy-MM-dd");
                }
                oGCZLCXData.ZhiWei = dr["ZhiWei"].ToString();
                oGCZLCXData.ZaiGang = dr["ZaiGang"].ToString();
                oGCZLCXData.EmailStr = dr["EmailStr"].ToString();

                oGCZLCXData.GroupName = dr["GroupName"].ToString();
                list.Add(oGCZLCXData);
            }

            return res = jss.Serialize(list);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [WebMethod]
    public string GetEmail(int iPageNo, string uname)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        List<LanEmailShou> list = new List<LanEmailShou>();
        string res = "";
        string strSql = "select [ID],[EmailTitle],[TimeStr],[EmailContent],[FuJian],[FromUser],[ToUser],[EmailState] FROM ERPLanEmail where TimeStr is not null and  (EmailState='未读' or EmailState='已读') and ToUser='" + uname + "'";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", iPageNo, iPageSize, 0);
        foreach (DataRow dr in dt.Rows)
        {

            LanEmailShou oGCZLCXData = new LanEmailShou();
            oGCZLCXData.ID = int.Parse(dr["ID"].ToString());
            oGCZLCXData.EmailContent = dr["EmailContent"].ToString().Replace("/../UEditor/net", "http://218.92.231.10:1013/UEditor/net");
            oGCZLCXData.EmailState = dr["EmailState"].ToString();
            oGCZLCXData.EmailTitle = dr["EmailTitle"].ToString();
            oGCZLCXData.FromUser = dr["FromUser"].ToString();
            oGCZLCXData.FuJian = dr["FuJian"].ToString();
            oGCZLCXData.ShouUser = "";
            oGCZLCXData.TimeStr = DateTime.Parse(dr["TimeStr"].ToString()).ToString("yyyy-MM-dd HH:hh");
            oGCZLCXData.ToUser = dr["ToUser"].ToString();
            list.Add(oGCZLCXData);
        }

        return res = jss.Serialize(list);
    }

    [WebMethod]
    public string SetEmailZT(int id, string uname)
    {

        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            DataEntityDataContext context = new DataEntityDataContext();
            var T = context.ERPLanEmail.SingleOrDefault(p => p.ID == id);
            //设置为已读
            if (T.ToUser.Trim() == uname)
            {
                if (T.EmailState == "未读")
                {
                    ZWL.DBUtility.DbHelperSQL.ExecuteSQL("update ERPLanEmail set EmailState='已读' where ID=" + id);
                }
            }
            return jss.Serialize("1");
        }
        catch (Exception ex)
        {
            return jss.Serialize("0" + ex.Message);
        }
    }

    [WebMethod]
    public string GetDZGG(int iPageNo, string uname)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.ERPUser.SingleOrDefault(p => p.UserName == uname);
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string res = "";
        List<GongGao> list = new List<GongGao>();
        string strSql = "select [ID],[TitleStr],[TimeStr],[UserName],[UserBuMen],[FuJian],[ContentStr],[TypeStr],[NoticeType] from ERPGongGao where ZT='已发送' and (  UserName='" + uname + "' or ','+UserBuMen+',' like '%," + uname + ",%')";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", iPageNo, iPageSize, 0);
        foreach (DataRow dr in dt.Rows)
        {

            GongGao oGCZLCXData = new GongGao();
            oGCZLCXData.ID = int.Parse(dr["ID"].ToString());
            oGCZLCXData.ContentStr = dr["ContentStr"].ToString();
            oGCZLCXData.FuJian = dr["FuJian"].ToString();
            oGCZLCXData.NoticeType = dr["NoticeType"].ToString();
            oGCZLCXData.TitleStr = dr["TitleStr"].ToString();
            oGCZLCXData.TypeStr = dr["TypeStr"].ToString();
            oGCZLCXData.UserBuMen = dr["UserBuMen"].ToString();
            oGCZLCXData.TimeStr = DateTime.Parse(dr["TimeStr"].ToString()).ToString("yyyy-MM-dd HH:hh");
            oGCZLCXData.UserName = dr["UserName"].ToString();
            list.Add(oGCZLCXData);
        }

        return res = jss.Serialize(list);
    }

    [WebMethod]
    public string GetGongGao(int iPageNo, string uname)
    {

        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.ERPUser.SingleOrDefault(p => p.UserName == uname);
        JavaScriptSerializer jss = new JavaScriptSerializer();
        string res = "";
        List<GongGao> list = new List<GongGao>();
        //string strSql = "select [ID],[TitleStr],[TimeStr],[UserName],[UserBuMen],[FuJian],[ContentStr],[TypeStr],[NoticeType] from ERPGongGao where ZT='已发送' and UserBuMen like '%" + uname + "%'";
        string strSql = "SELECT g.BuMenID1,g.ID,g.TimeStr,g.TitleStr,g.UserName,g.UserBuMen,g.FuJian,g.ContentStr," +
  "g.TypeStr,g.BuMenName,g.num,g.NoticeType from [GongGaoBuMen] g where g.BuMenName " +
  " in (select BuMenName FROM [DWT].[dbo].[ERPBuMen] where id " +
 " in (select DirID FROM [DWT].[dbo].[ERPBuMen] where BuMenName " +
 " in (select u.Department from [DWT].[dbo].hx_vERPUser u where u.UserName='" + uname + "'))) and g.UserBuMen like '%" + uname + "%' ";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", iPageNo, iPageSize, 0);
        foreach (DataRow dr in dt.Rows)
        {

            GongGao oGCZLCXData = new GongGao();
            oGCZLCXData.ID = int.Parse(dr["ID"].ToString());
            oGCZLCXData.ContentStr = dr["ContentStr"].ToString();
            oGCZLCXData.FuJian = dr["FuJian"].ToString();
            oGCZLCXData.NoticeType = dr["NoticeType"].ToString();
            oGCZLCXData.TitleStr = dr["TitleStr"].ToString();
            oGCZLCXData.TypeStr = dr["TypeStr"].ToString();
            oGCZLCXData.UserBuMen = dr["UserBuMen"].ToString();
            oGCZLCXData.TimeStr = DateTime.Parse(dr["TimeStr"].ToString()).ToString("yyyy-MM-dd HH:hh");
            oGCZLCXData.UserName = dr["UserName"].ToString();
            list.Add(oGCZLCXData);
        }

        return res = jss.Serialize(list);
    }

    [WebMethod]
    public string AddGongGao(string TitleStr, string UserName, string UserBuMen, string ContentStr, string FuJian)
    {
        try
        {
            ZWL.BLL.ERPGongGao Model = new ZWL.BLL.ERPGongGao();
            Model.TitleStr = TitleStr;
            Model.UserName = UserName;
            Model.UserBuMen = UserBuMen;
            Model.NoticeType = "通知";
            Model.ZT = "已发送";
            Model.FuJian = FuJian;
            Model.TypeStr = "";
            Model.ContentStr = ContentStr;
            Model.Num = 0;
            int id = Model.Add();
            return id.ToString();
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    [WebMethod]
    public string GetUsers(int iPageNo)
    {
        try
        {
            ZWL.BLL.ERPNWorkToDo Mymodel = new ZWL.BLL.ERPNWorkToDo();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string res = "";
            List<User> list = new List<User>();
            string strSql = "select [ID],[UserName],[UserPwd],[TrueName],[Serils],[Department],[JiaoSe],[ActiveTime],[ZhiWei],[ZaiGang],[EmailStr],JiaTingDianHua,[VoipAccount],[VoipPwd],[SubAccountSid],[SubToken] from hx_vERPUser";
            DataTable dt = GetDataByPager2000(strSql, "ID desc", iPageNo, 1000, 0);
            foreach (DataRow dr in dt.Rows)
            {
                User oGCZLCXData = new User();
                oGCZLCXData.ID = int.Parse(dr["ID"].ToString());
                oGCZLCXData.UserName = dr["UserName"].ToString();
                oGCZLCXData.UserPwd = dr["UserPwd"].ToString();
                oGCZLCXData.TrueName = dr["TrueName"].ToString();
                oGCZLCXData.Serils = dr["Serils"].ToString();
                oGCZLCXData.Department = dr["Department"].ToString();
                oGCZLCXData.JiaoSe = dr["JiaoSe"].ToString();
                if (dr["ActiveTime"].ToString() != "")
                {
                    oGCZLCXData.ActiveTime = DateTime.Parse(dr["ActiveTime"].ToString()).ToString("yyyy-MM-dd");
                }
                oGCZLCXData.ZhiWei = dr["ZhiWei"].ToString();
                oGCZLCXData.ZaiGang = dr["ZaiGang"].ToString();
                oGCZLCXData.EmailStr = dr["EmailStr"].ToString();

                oGCZLCXData.Mobile = dr["JiaTingDianHua"].ToString();
                oGCZLCXData.VoipAccount = dr["VoipAccount"].ToString();
                oGCZLCXData.VoipPwd = dr["VoipPwd"].ToString();
                oGCZLCXData.SubAccountSid = dr["SubAccountSid"].ToString();
                oGCZLCXData.SubToken = dr["SubToken"].ToString();
                list.Add(oGCZLCXData);
            }

            return res = jss.Serialize(list);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [WebMethod]
    public string GetTrueName(int iPageNo, string VoipAccount)
    {
        try
        {
            ZWL.BLL.ERPNWorkToDo Mymodel = new ZWL.BLL.ERPNWorkToDo();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string res = "";
            List<User> list = new List<User>();
            string strSql = "select [ID],[UserName],[UserPwd],[TrueName],[Serils],[Department],[JiaoSe],[ActiveTime],[ZhiWei],[ZaiGang],[EmailStr],JiaTingDianHua,[VoipAccount],[VoipPwd],[SubAccountSid],[SubToken] from hx_vERPUser where VoipAccount='" + VoipAccount + "'";
            DataTable dt = GetDataByPager2000(strSql, "ID desc", iPageNo, 1, 0);
            foreach (DataRow dr in dt.Rows)
            {
                //User oGCZLCXData = new User();
                //oGCZLCXData.ID = int.Parse(dr["ID"].ToString());
                //oGCZLCXData.TrueName = dr["TrueName"].ToString();
                res = dr["TrueName"].ToString();
            }

            return res;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [WebMethod]
    public string GetUserList(int iPageNo, string Department)
    {
        try
        {
            ZWL.BLL.ERPNWorkToDo Mymodel = new ZWL.BLL.ERPNWorkToDo();
            ZWL.BLL.ERPBuMen MyERPBuMen = new ZWL.BLL.ERPBuMen();
            int DepartmentID = 0;
            if (!string.IsNullOrEmpty(Department))
                DepartmentID = MyERPBuMen.GetBuMenID(Department);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string res = "";
            List<User> list = new List<User>();
            if (DepartmentID > 0)
            {
                string strSql = "select [ID],[UserName],[UserPwd],[TrueName],[Serils],[Department],[JiaoSe],[ActiveTime],[ZhiWei],[ZaiGang],[EmailStr],JiaTingDianHua,[VoipAccount],[VoipPwd],[SubAccountSid],[SubToken] from hx_vERPUser where CHARINDEX('," + DepartmentID + ",',p_depart_ids)>0 ";
                DataTable dt = GetDataByPager2000(strSql, "ID desc", iPageNo, 1000, 0);
                foreach (DataRow dr in dt.Rows)
                {
                    User oGCZLCXData = new User();
                    oGCZLCXData.ID = int.Parse(dr["ID"].ToString());
                    oGCZLCXData.UserName = dr["UserName"].ToString();
                    oGCZLCXData.UserPwd = dr["UserPwd"].ToString();
                    oGCZLCXData.TrueName = dr["TrueName"].ToString();
                    oGCZLCXData.Serils = dr["Serils"].ToString();
                    oGCZLCXData.Department = dr["Department"].ToString();
                    oGCZLCXData.JiaoSe = dr["JiaoSe"].ToString();
                    if (dr["ActiveTime"].ToString() != "")
                    {
                        oGCZLCXData.ActiveTime = DateTime.Parse(dr["ActiveTime"].ToString()).ToString("yyyy-MM-dd");
                    }
                    oGCZLCXData.ZhiWei = dr["ZhiWei"].ToString();
                    oGCZLCXData.ZaiGang = dr["ZaiGang"].ToString();
                    oGCZLCXData.EmailStr = dr["EmailStr"].ToString();

                    oGCZLCXData.GroupName = dr["JiaTingDianHua"].ToString();
                    oGCZLCXData.VoipAccount = dr["VoipAccount"].ToString();
                    oGCZLCXData.VoipPwd = dr["VoipPwd"].ToString();
                    oGCZLCXData.SubAccountSid = dr["SubAccountSid"].ToString();
                    oGCZLCXData.SubToken = dr["SubToken"].ToString();
                    list.Add(oGCZLCXData);
                }
            }

            return res = jss.Serialize(list);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    [WebMethod]
    public string GetAppUpdate()
    {
        return "1_更新内容：已知bug修复";
    }

    [WebMethod]
    public string IGetAppUpdate()
    {
        return "1_更新内容：已知bug修复";
    }

    [WebMethod]
    public string FJUpload(byte[] fs, string fileName)
    {
        if (fileName.Length > 0)
        {
            MemoryStream m = new MemoryStream(fs);
            ///定义实际文件对象，保存上载的文件。
            FileStream f = new FileStream(HttpContext.Current.Server.MapPath("~") + "\\UploadFile\\" + fileName, FileMode.OpenOrCreate);
            ///把内内存里的数据写入物理文件
            m.WriteTo(f);
            m.Close();
            f.Close();
            f = null;
            m = null;

            return "UploadFile/" + fileName;
        }
        return "";
    }



    [WebMethod]
    public string CSFJUpload()
    {
        int n = HttpContext.Current.Request.Files.Count;
        string Filename = HttpContext.Current.Request.Form["Filename"].ToString();
        for (int i = 0; i < n; i++)
        {
            var fa = HttpContext.Current.Request.Files[i];
            if (fa.FileName.Length > 0)
            {

                string[] pn = fa.FileName.Split('\\');
                int k = fa.ContentLength;
                byte[] dbuff = new byte[k];
                Stream stream = fa.InputStream;
                stream.Read(dbuff, 0, k);


                MemoryStream ms = new MemoryStream(dbuff);
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~") + "\\UploadFile\\" + Filename, FileMode.OpenOrCreate);
                ms.WriteTo(fs);
                ms.Close();
                fs.Close();
                ms = null;
                fs = null;
                return Filename;
            }
        }
        return "";
    }

    #region 检查条件
    /// <summary>
    /// 检测条件，然后返回下一字段，否则返回默认节点ID
    /// </summary>
    /// <returns></returns>
    public int CheckCondition(string DefaultNodeID, ZWL.BLL.ERPNWorkToDo MyModel)
    {

        //格式如：DEFG_请假天数→大于→10→3|ABCD_请假天数→大于→10→3
        string[] TiaoJianList = ZWL.DBUtility.DbHelperSQL.GetSHSL("select ConditionSet from ERPNWorkFlowNode where ID=" + MyModel.JieDianID.ToString()).Split('|');
        for (int i = 0; i < TiaoJianList.Length; i++)
        {
            if (TiaoJianList[i].Trim().Length > 0)
            {
                string NextIDStr = CheckTiaoJian(TiaoJianList[i].ToString());
                if (NextIDStr != "0")
                {
                    return int.Parse(NextIDStr);
                }
            }
        }
        return int.Parse(DefaultNodeID);
    }
    /// <summary>
    /// 比较两个字符串，返回结果是否正确
    /// </summary>
    /// <param name="Str1"></param>
    /// <param name="Str2"></param>
    /// <param name="BiJiaoTiaoJian"></param>
    /// <param name="LeiXing"></param>
    /// <returns></returns>
    protected bool BiaoJiaoTwoStr(string Str1, string Str2, string BiJiaoTiaoJian)
    {
        try
        {
            double A1 = double.Parse(Str1);
            double A2 = double.Parse(Str2); //大于  大于等于   小于  小于等于   等于   不等于  包含  不包含
            if (BiJiaoTiaoJian == "大于" && A1 > A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "大于等于" && A1 >= A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "小于" && A1 < A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "小于等于" && A1 <= A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "等于" && A1 == A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不等于" && A1 != A2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "包含" && ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不包含")
            {
                if (ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            if (BiJiaoTiaoJian == "等于" && Str1 == Str2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不等于" && Str1 != Str2)
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "包含" && ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
            {
                return true;
            }
            else if (BiJiaoTiaoJian == "不包含")
            {
                if (ZWL.Common.PublicMethod.StrIFIn(Str2, Str1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 判断条件，返回下一节点ID
    /// </summary>
    /// <param name="FormCcontent"></param>
    /// <param name="TiaoJianStr"></param>
    /// <param name="WorkFlowIDStr"></param>
    /// <returns></returns>
    protected string CheckTiaoJian(string TiaoJianStr)
    {
        return "";
    }

    #endregion

    #region 定位信息

    [WebMethod]
    public string AddWZCJ(string username, string bumen, string x, string y, string wz)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        WZXX model = new WZXX();
        try
        {
            //if (x != null && y != null)
            //{
            model.XM = username;
            model.SSBM = bumen;
            model.X = x;
            model.Y = y;
            model.QDSJ = DateTime.Now;
            model.RQ = DateTime.Now;
            model.POI = wz;
            model.BZ1 = "最新";
            context.WZXX.InsertOnSubmit(model);
            context.SubmitChanges();
            // }
            return "1";
        }
        catch (Exception ex)
        {
            return "0";
        }

    }

    [WebMethod]
    public string AddWZCJLoc(String username, String bumen, String x, String y, String wz, String jq)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        WZXX model = new WZXX();
        try
        {
            //if (x != null && y != null)
            //{
            model.XM = username;
            model.SSBM = bumen;
            model.X = x;
            model.Y = y;
            model.RQ = DateTime.Now;
            model.POI = wz;
            if (string.IsNullOrEmpty(jq))
                model.BZ1 = "实时";
            else
            {
                model.BZ1 = "离线";
                model.QDSJ = Convert.ToDateTime(jq);
            }
            context.WZXX.InsertOnSubmit(model);
            context.SubmitChanges();
            // }
            return "1";
        }
        catch (Exception ex)
        {
            return "0";
        }

    }

    [WebMethod]
    public string GetLSGJ(string truename, string sj, string esj)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        string username = "";
        try
        {
            List<User> list = new List<User>();
            string strSql = "select [ID],[UserName],[UserPwd],[TrueName],[Serils],[Department],[JiaoSe],[ActiveTime],[ZhiWei],[ZaiGang],[EmailStr],[GroupName],[JiaTingDianHua],[VoipAccount],[VoipPwd],[SubAccountSid],[SubToken] from hx_vERPUser where TrueName='" + truename + "' and IfLogin='是' ";
            DataTable dt = GetDataByPager2000(strSql, "ID desc", 1, iPageSize, 0);
            foreach (DataRow dr in dt.Rows)
            {
                User oGCZLCXData = new User();
                oGCZLCXData.ID = int.Parse(dr["ID"].ToString());
                oGCZLCXData.UserName = dr["UserName"].ToString();
                username = dr["UserName"].ToString();
            }

            var T = context.WZXX.Where(p => p.XM == username);
            if (!string.IsNullOrEmpty(sj))
            {
                T = T.Where(p => p.RQ >= DateTime.Parse(sj));
            }
            if (!string.IsNullOrEmpty(esj))
            {
                T = T.Where(p => p.RQ <= DateTime.Parse(esj));
            }

            string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(T.ToList(), timeConverter);

            return jsonGroup0;
        }
        catch (Exception ex)
        {
            return "0";
        }

    }
    #endregion

    #region 考勤签到管理

    [WebMethod]
    public string AddKQXX(string username, string bumen, string poi, string wz, string lx, string x, string y, string tp)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        QDXX model = new QDXX();
        try
        {
            model.XM = username;
            model.SSBM = bumen;
            model.RQ = DateTime.Now;
            model.QDSJ = DateTime.Now;
            model.POI = poi;
            model.BZ1 = lx;
            model.BZ2 = wz;
            model.x = x;
            model.y = y;
            model.BZ3 = tp;
            context.QDXX.InsertOnSubmit(model);
            context.SubmitChanges();
            return "1";
        }
        catch (Exception ex)
        {
            return "0";
        }

    }

    [WebMethod]
    public string GetPOI(string username, string x, string y)
    {
        DataEntityDataContext context = new DataEntityDataContext();
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        var FW = context.system_info.Where(p => p.LX == "POI点显示范围设置");
        if (string.IsNullOrEmpty(x))
        {
            x = "0";
        }
        if (string.IsNullOrEmpty(x))
        {
            y = "0";
        }
        string poifw = "0";
        if (FW.Count() > 0)
        {
            poifw = FW.First().CJJGSJ.ToString();
        }
        List<POI> list = new List<POI>();
        try
        {
            var T = context.POIXX.Where(p => p.ID > 0 && p.BZ1 == "启用");
            foreach (var item in T)
            {
                POI po = new POI();
                double jl = GetDistance(double.Parse(x), double.Parse(y), double.Parse(item.X), double.Parse(item.Y));
                if (jl <= double.Parse(poifw))
                {
                    po.mc = item.MC;
                    po.x = item.X;
                    po.y = item.Y;
                    po.jl = jl.ToString();
                    list.Add(po);
                }
            }
            string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

            return jsonGroup0;
        }
        catch (Exception ex)
        {
            return "0";
        }

    }

    private const double EARTH_RADIUS = 6378.137;//地球半径
    private static double rad(double d)
    {
        return d * Math.PI / 180.0;
    }

    public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
    {
        double radLat1 = rad(lat1);
        double radLat2 = rad(lat2);
        double a = radLat1 - radLat2;
        double b = rad(lng1) - rad(lng2);

        double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
         Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
        s = s * EARTH_RADIUS;
        s = Math.Round(s * 10000) / 10000 * 1000;
        return s;
    }
    #endregion

    //#region 任务管理

    //[WebMethod]
    //public string GetTaskList(int pageNo, string userid)
    //{
    //    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
    //    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
    //    string strSql = "select * from ERPTaskFP where ','+ToUserList+',' like '%," + userid + ",%'";
    //    DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
    //    var list = List<ERPTaskFP>(dt);

    //    string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

    //    return jsonGroup0;
    //}

    //#endregion

    #region 日志管理

    [WebMethod]
    public string GetRiZhiList(int pageNo, string userid)
    {
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        string strSql = "select * from ERPWorkRiZhi where UserName= '" + userid + "'";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
        var list = List<ERPWorkRiZhi>(dt);

        string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

        return jsonGroup0;
    }



    #endregion

    #region 汇报管理

    [WebMethod]
    public string GetHuiBaoList(int pageNo, string userid)
    {
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        string strSql = "select * from ERPHuiBao where UserName= '" + userid + "'";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
        var list = List<ERPHuiBao>(dt);

        string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);
        return jsonGroup0;
    }

    [WebMethod]
    public string GetHuiBao(int pageNo, string userid)
    {
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        //string strSql = "select * from ERPHuiBao where CanLookUser like '%" + userid + "%'";
        string strSql = "select * from ERPHuiBao where SSBM in(select BuMenName from hx_vERPBuMen where DirID in (select id from hx_vERPBuMen where BuMenName in (select Department from hx_vERPUser where UserName='" + userid + "')))";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
        var list = List<ERPHuiBao>(dt);

        string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);
        return jsonGroup0;
    }

    [WebMethod]
    public string AddHuiBao(string username, string title, string content, string fujian)
    {
        ZWL.BLL.ERPHuiBao Model = new ZWL.BLL.ERPHuiBao();

        Model.TitleStr = title;
        Model.ContentStr = content;
        Model.CanLookUser = "";
        Model.DDWL = "";
        Model.TimeStr = DateTime.Now;
        Model.FuJianStr = fujian;
        Model.UserName = username;
        Model.SSBM = ZWL.Common.PublicMethod.GetSessionValue("Department");

        Model.ZT = "待办预警";

        Model.Add();


        return "";
    }

    [WebMethod]
    public string AddHuiBaoX(string username, string title, string content, string fujian, string x, string y, string type)
    {
        ZWL.BLL.ERPHuiBao Model = new ZWL.BLL.ERPHuiBao();
        ZWL.BLL.ERPUser UserModel = new ZWL.BLL.ERPUser();

        Model.TitleStr = title;
        Model.ContentStr = content;
        Model.CanLookUser = "";
        Model.DDWL = type;
        Model.TimeStr = DateTime.Now;
        Model.FuJianStr = fujian;
        Model.UserName = username;
        //Model.SSBM = ZWL.Common.PublicMethod.GetSessionValue("Department");
        Model.SSBM = UserModel.GetList("UserName='" + username + "'").Tables[0].Rows[0]["Department"].ToString();
        Model.BZ1 = x;
        Model.BZ2 = y;
        Model.ZT = "待办预警";

        Model.Add();


        return "";
    }

    [WebMethod]
    public string Test(string value)
    {
        ZWL.BLL.ERPHuiBao Model = new ZWL.BLL.ERPHuiBao();
        ZWL.BLL.ERPUser Model2 = new ZWL.BLL.ERPUser();
        //Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        //timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        //DataSet ds = Model.GetList("ZT not in('"+zt+"')");
        //var list = List<ERPHuiBao>(ds.Tables[0]);

        //string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

        //return jsonGroup0;
        //return Model.GetListByZT(value);
        return Model2.GetListByBM(value);
    }
    #endregion

    #region 意见反馈

    [WebMethod]
    public string GetWorkPlanList(int pageNo, string userid)
    {
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        string strSql = "select * from ERPWorkPlan where UserName= '" + userid + "'";
        DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
        var list = List<ERPWorkPlan>(dt);

        string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

        return jsonGroup0;
    }

    [WebMethod]
    public string AddYJFK(string xj, string nr, string username)
    {
        ZWL.BLL.ERPWorkPlan Model = new ZWL.BLL.ERPWorkPlan();

        Model.TitleStr = xj;
        Model.ContentStr = nr;
        Model.CanLookUser = "";
        Model.TimeStr = DateTime.Now;
        Model.FuJianStr = "";
        Model.UserName = username;
        Model.Add();
        return "1";
    }

    #endregion

    //#region 客户管理

    //[WebMethod]
    //public string GetKeHuList(int pageNo, string userid)
    //{
    //    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
    //    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
    //    string strSql = "select * from ERPCustomInfo where UserName= '" + userid + "'";
    //    DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
    //    var list = List<ERPCustomInfo>(dt);

    //    string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

    //    return jsonGroup0;
    //}

    //#endregion

    //#region 联系人管理

    //[WebMethod]
    //public string GetGRLXRList(int pageNo, string userid)
    //{
    //    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
    //    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
    //    string strSql = "select *  from ERPTongXunLu where TypeStr='个人通讯簿' and UserName= '" + userid + "'";
    //    DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
    //    var list = List<ERPTongXunLu>(dt);

    //    string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

    //    return jsonGroup0;
    //}

    //[WebMethod]
    //public string GetGGLXRList(int pageNo, string userid)
    //{
    //    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
    //    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
    //    string strSql = "select *  from ERPTongXunLu where TypeStr='公共通讯簿' ";
    //    DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
    //    var list = List<ERPTongXunLu>(dt);

    //    string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

    //    return jsonGroup0;
    //}

    //[WebMethod]
    //public string GetGXLXRList(int pageNo, string userid)
    //{
    //    Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
    //    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
    //    string strSql = "select *  from ERPTongXunLu where  IfShare='是' ";
    //    DataTable dt = GetDataByPager2000(strSql, "ID desc", pageNo, iPageSize, 0);
    //    var list = List<ERPTongXunLu>(dt);

    //    string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);

    //    return jsonGroup0;
    //}

    //#endregion

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static List<T> List<T>(DataTable dt)
    {
        var list = new List<T>();
        Type t = typeof(T);
        var plist = new List<PropertyInfo>(typeof(T).GetProperties());

        foreach (DataRow item in dt.Rows)
        {
            T s = System.Activator.CreateInstance<T>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                if (info != null)
                {
                    if (!Convert.IsDBNull(item[i]))
                    {
                        info.SetValue(s, item[i], null);
                    }
                }
            }
            list.Add(s);
        }
        return list;
    }


}

public class LanEmailShou
{
    public LanEmailShou()
    { }
    #region Model
    private int _id;
    private string _shouuser;
    private string _emailtitle;
    private string _timestr;
    private string _emailcontent;
    private string _fujian;
    private string _fromuser;
    private string _touser;
    private string _emailstate;
    /// <summary>
    /// 
    /// </summary>
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ShouUser
    {
        set { _shouuser = value; }
        get { return _shouuser; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string EmailTitle
    {
        set { _emailtitle = value; }
        get { return _emailtitle; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string TimeStr
    {
        set { _timestr = value; }
        get { return _timestr; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string EmailContent
    {
        set { _emailcontent = value; }
        get { return _emailcontent; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string FuJian
    {
        set { _fujian = value; }
        get { return _fujian; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string FromUser
    {
        set { _fromuser = value; }
        get { return _fromuser; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ToUser
    {
        set { _touser = value; }
        get { return _touser; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string EmailState
    {
        set { _emailstate = value; }
        get { return _emailstate; }
    }
    #endregion Model

}

public class GongGao
{
    public GongGao()
    { }
    #region Model
    private int _id;
    private string _titlestr;
    private string _username;
    private string _userbumen;
    private string _noticetype;
    private string _fujian;
    private string _contentstr;
    private string _typestr;
    private string _timestr;
    /// <summary>
    /// 
    /// </summary>
    public string TimeStr
    {
        set { _timestr = value; }
        get { return _timestr; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string TitleStr
    {
        set { _titlestr = value; }
        get { return _titlestr; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string UserName
    {
        set { _username = value; }
        get { return _username; }
    }
    /// <summary>
    /// 部门公告通知用此列
    /// </summary>
    public string UserBuMen
    {
        set { _userbumen = value; }
        get { return _userbumen; }
    }

    public string NoticeType
    {
        set { _noticetype = value; }
        get { return _noticetype; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string FuJian
    {
        set { _fujian = value; }
        get { return _fujian; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ContentStr
    {
        set { _contentstr = value; }
        get { return _contentstr; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string TypeStr
    {
        set { _typestr = value; }
        get { return _typestr; }
    }
    #endregion Model
}

public class NWorkToDo
{
    public NWorkToDo()
    { }

    private int _id;
    private string _workname;
    private int? _formid;
    private int? _workflowid;
    private string _username;
    private string _timestr;
    private string _formcontent;
    private string _fujianlist;
    private string _shenpiyijian;
    private int? _jiedianid;
    private string _jiedianname;
    private string _shenpiuserlist;
    private string _okuserlist;
    private string _statenow;
    private string _latetime;
    private string _wenhao;
    private string _beiyong1;
    private string _beiyong2;
    private List<NodeItem> _noteitem;
    /// <summary>
    /// 
    /// </summary>
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 工作名称
    /// </summary>
    public string WorkName
    {
        set { _workname = value; }
        get { return _workname; }
    }

    /// <summary>
    /// 工作名称
    /// </summary>
    public List<NodeItem> NodeItem
    {
        set { _noteitem = value; }
        get { return _noteitem; }
    }
    /// <summary>
    /// 所用表单
    /// </summary>
    public int? FormID
    {
        set { _formid = value; }
        get { return _formid; }
    }
    /// <summary>
    /// 所用工作流程
    /// </summary>
    public int? WorkFlowID
    {
        set { _workflowid = value; }
        get { return _workflowid; }
    }
    /// <summary>
    /// 发起人
    /// </summary>
    public string UserName
    {
        set { _username = value; }
        get { return _username; }
    }
    /// <summary>
    /// 发起时间
    /// </summary>
    public String TimeStr
    {
        set { _timestr = value; }
        get { return _timestr; }
    }
    /// <summary>
    /// 表单内容
    /// </summary>
    public string FormContent
    {
        set { _formcontent = value; }
        get { return _formcontent; }
    }
    /// <summary>
    /// 附件文件
    /// </summary>
    public string FuJianList
    {
        set { _fujianlist = value; }
        get { return _fujianlist; }
    }
    /// <summary>
    /// 签注审批
    /// </summary>
    public string ShenPiYiJian
    {
        set { _shenpiyijian = value; }
        get { return _shenpiyijian; }
    }
    /// <summary>
    /// 当前所在节点
    /// </summary>
    public int? JieDianID
    {
        set { _jiedianid = value; }
        get { return _jiedianid; }
    }
    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string JieDianName
    {
        set { _jiedianname = value; }
        get { return _jiedianname; }
    }
    /// <summary>
    /// 当前审批用户（可以多个人）
    /// </summary>
    public string ShenPiUserList
    {
        set { _shenpiuserlist = value; }
        get { return _shenpiuserlist; }
    }
    /// <summary>
    /// 当前已审批通过的用户（可以多个人）
    /// </summary>
    public string OKUserList
    {
        set { _okuserlist = value; }
        get { return _okuserlist; }
    }
    /// <summary>
    /// 当前状态
    /// </summary>
    public string StateNow
    {
        set { _statenow = value; }
        get { return _statenow; }
    }
    /// <summary>
    /// 超时时间（何时超时）
    /// </summary>
    public String LateTime
    {
        set { _latetime = value; }
        get { return _latetime; }
    }

    /// <summary>
    /// 工作名称
    /// </summary>
    public string WenHao
    {
        set { _wenhao = value; }
        get { return _wenhao; }
    }


    /// <summary>
    /// 工作名称
    /// </summary>
    public string BeiYong1
    {
        set { _beiyong1 = value; }
        get { return _beiyong1; }
    }

    /// <summary>
    /// 工作名称
    /// </summary>
    public string BeiYong2
    {
        set { _beiyong2 = value; }
        get { return _beiyong2; }
    }
}

public class User
{
    public User()
    { }
    #region Model
    private int _id;
    private string _username;
    private string _userpwd;
    private string _truename;
    private string _serils;
    private string _department;
    private string _jiaose;
    private string _groupname;
    private string _activetime;
    private string _zhiwei;
    private string _zaigang;
    private string _emailstr;
    private string _wzcjjg;
    private string _poifw;
    private string _eFence;
    private string _voipaccount;
    private string _voippwd;
    private string _subaccountsid;
    private string _subtoken;
    private string _mobile;
    /// <summary>
    /// 
    /// </summary>
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string UserName
    {
        set { _username = value; }
        get { return _username; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string UserPwd
    {
        set { _userpwd = value; }
        get { return _userpwd; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string TrueName
    {
        set { _truename = value; }
        get { return _truename; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string Serils
    {
        set { _serils = value; }
        get { return _serils; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string Department
    {
        set { _department = value; }
        get { return _department; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string JiaoSe
    {
        set { _jiaose = value; }
        get { return _jiaose; }
    }

    public string GroupName
    {
        set { _groupname = value; }
        get { return _groupname; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ActiveTime
    {
        set { _activetime = value; }
        get { return _activetime; }
    }
    /// <summary>
    /// 职位
    /// </summary>
    public string ZhiWei
    {
        set { _zhiwei = value; }
        get { return _zhiwei; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ZaiGang
    {
        set { _zaigang = value; }
        get { return _zaigang; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string EmailStr
    {
        set { _emailstr = value; }
        get { return _emailstr; }
    }

    public string WZCJJG
    {
        set { _wzcjjg = value; }
        get { return _wzcjjg; }
    }
    public string POIFW
    {
        set { _poifw = value; }
        get { return _poifw; }
    }
    public string eFence
    {
        set { _eFence = value; }
        get { return _eFence; }
    }
    public string VoipAccount
    {
        set { _voipaccount = value; }
        get { return _voipaccount; }
    }
    public string VoipPwd
    {
        set { _voippwd = value; }
        get { return _voippwd; }
    }
    public string SubAccountSid
    {
        set { _subaccountsid = value; }
        get { return _subaccountsid; }
    }
    public string SubToken
    {
        set { _subtoken = value; }
        get { return _subtoken; }
    }
    public string Mobile
    {
        set { _mobile = value; }
        get { return _mobile; }
    }
    #endregion Model
}

public class NodeItem
{
    public NodeItem()
    { }

    private string texts;
    private string values;
    private string tiaojian;
    private string spms;
    private string psms;
    private string mrspr;
    /// <summary>
    /// 
    /// </summary>
    public string Tiaojian
    {
        set { tiaojian = value; }
        get { return tiaojian; }
    }

    /// <summary>
    /// 
    /// </summary>
    public string Spms
    {
        set { spms = value; }
        get { return spms; }
    }

    /// <summary>
    /// 
    /// </summary>
    public string Psms
    {
        set { psms = value; }
        get { return psms; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string Mrspr
    {
        set { mrspr = value; }
        get { return mrspr; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string Texts
    {
        set { texts = value; }
        get { return texts; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string Values
    {
        set { values = value; }
        get { return values; }
    }
}

public class POI
{
    public string mc { get; set; }
    public string x { get; set; }
    public string y { get; set; }
    public string jl { get; set; }
}