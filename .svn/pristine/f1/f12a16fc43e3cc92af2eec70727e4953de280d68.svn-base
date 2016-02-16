using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Data;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPLanEmailShou。
    /// </summary>
    public class ERPLanEmailShou
    {
        public ERPLanEmailShou()
        { }
        #region Model
        private int _id;
        private string _shouuser;
        private string _emailtitle;
        private DateTime? _timestr;
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
        public DateTime? TimeStr
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


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPLanEmailShou");
            strSql.Append(" where ID=" + ID + " ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPLanEmailShou(");
            strSql.Append("ShouUser,EmailTitle,EmailContent,FuJian,FromUser,ToUser,EmailState,TimeStr)");
            strSql.Append(" values (");
            strSql.Append("@ShouUser,@EmailTitle,@EmailContent,@FuJian,@FromUser,@ToUser,@EmailState,@TimeStr)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ShouUser", SqlDbType.VarChar,50),		
					new SqlParameter("@EmailTitle", SqlDbType.VarChar,500),					
					new SqlParameter("@EmailContent", SqlDbType.Text),
					new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
					new SqlParameter("@FromUser", SqlDbType.VarChar,50),
					new SqlParameter("@ToUser", SqlDbType.VarChar,50),
					new SqlParameter("@EmailState", SqlDbType.VarChar,50),
                    new SqlParameter("@TimeStr", SqlDbType.DateTime,20)};
            parameters[0].Value = ShouUser;
            parameters[1].Value = EmailTitle;
            parameters[2].Value = EmailContent;
            parameters[3].Value = FuJian;
            parameters[4].Value = FromUser;
            parameters[5].Value = ToUser;
            parameters[6].Value = EmailState;
            parameters[7].Value = TimeStr;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPLanEmailShou set ");
            strSql.Append("ShouUser=@ShouUser,");
            strSql.Append("EmailTitle=@EmailTitle,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("EmailContent=@EmailContent,");
            strSql.Append("FuJian=@FuJian,");
            strSql.Append("FromUser=@FromUser,");
            strSql.Append("ToUser=@ToUser,");
            strSql.Append("EmailState=@EmailState");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@ShouUser", SqlDbType.VarChar,50),	
					new SqlParameter("@EmailTitle", SqlDbType.VarChar,500),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
					new SqlParameter("@EmailContent", SqlDbType.Text),
					new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
					new SqlParameter("@FromUser", SqlDbType.VarChar,50),
					new SqlParameter("@ToUser", SqlDbType.VarChar,50),
					new SqlParameter("@EmailState", SqlDbType.VarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = ShouUser;
            parameters[2].Value = EmailTitle;
            parameters[3].Value = TimeStr;
            parameters[4].Value = EmailContent;
            parameters[5].Value = FuJian;
            parameters[6].Value = FromUser;
            parameters[7].Value = ToUser;
            parameters[8].Value = EmailState;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPLanEmailShou ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,ShouUser,EmailTitle,TimeStr,EmailContent,FuJian,FromUser,ToUser,EmailState ");
            strSql.Append(" FROM ERPLanEmailShou ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                ShouUser = ds.Tables[0].Rows[0]["ShouUser"].ToString();

                EmailTitle = ds.Tables[0].Rows[0]["EmailTitle"].ToString();

                if (ds.Tables[0].Rows[0]["TimeStr"].ToString() != "")
                {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }

                EmailContent = ds.Tables[0].Rows[0]["EmailContent"].ToString();

                FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();

                FromUser = ds.Tables[0].Rows[0]["FromUser"].ToString();

                ToUser = ds.Tables[0].Rows[0]["ToUser"].ToString();

                EmailState = ds.Tables[0].Rows[0]["EmailState"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[ShouUser],[EmailTitle],[TimeStr],[EmailContent],[FuJian],[FromUser],[ToUser],[EmailState] ");
            strSql.Append(" FROM ERPLanEmailShou ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}
