﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPGongGao。
    /// </summary>
    public class ERPGongGao
    {
        public ERPGongGao()
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
        private string _shr;
        private string _shsj;
        private string _zt;
        private int _num;
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
        public string SHR
        {
            set { _shr = value; }
            get { return _shr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SHSJ
        {
            set { _shsj = value; }
            get { return _shsj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZT
        {
            set { _zt = value; }
            get { return _zt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int Num
        {
            set { _num = value; }
            get { return _num; }
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


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPGongGao");
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
            strSql.Append("insert into ERPGongGao(");
            strSql.Append("TitleStr,UserName,UserBuMen,FuJian,ContentStr,TypeStr,NoticeType,ZT,Num)");
            strSql.Append(" values (");
            strSql.Append("@TitleStr,@UserName,@UserBuMen,@FuJian,@ContentStr,@TypeStr,@NoticeType,@ZT,@Num)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserBuMen", SqlDbType.VarChar,5000),
					new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@TypeStr", SqlDbType.VarChar,50),
                                        new SqlParameter("@NoticeType",SqlDbType.NVarChar,50),
                                         new SqlParameter("@ZT",SqlDbType.NVarChar,50),
                                         new SqlParameter("@Num",SqlDbType.Int,4)};
            parameters[0].Value = TitleStr;
            parameters[1].Value = UserName;
            parameters[2].Value = UserBuMen;
            parameters[3].Value = FuJian;
            parameters[4].Value = ContentStr;
            parameters[5].Value = TypeStr;
            parameters[6].Value = NoticeType;
            parameters[7].Value = ZT;
            parameters[8].Value = Num;
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
            strSql.Append("update ERPGongGao set ");
            strSql.Append("TitleStr=@TitleStr,");            
            strSql.Append("FuJian=@FuJian,");
            strSql.Append("ContentStr=@ContentStr,");
            strSql.Append("TypeStr=@TypeStr,");
            strSql.Append("NoticeType=@NoticeType,");
            strSql.Append("TimeStr=getdate(), ");
            strSql.Append("Num=@Num, ");
            strSql.Append("ZT=@ZT ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),					
					new SqlParameter("@FuJian", SqlDbType.VarChar,2000),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@TypeStr", SqlDbType.VarChar,50),
                    new SqlParameter("@NoticeType",SqlDbType.NVarChar,50),
                    new SqlParameter("@Num",SqlDbType.Int,4),
                    new SqlParameter("@ZT",SqlDbType.NVarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = TitleStr;            
            parameters[2].Value = FuJian;
            parameters[3].Value = ContentStr;
            parameters[4].Value = TypeStr;
            parameters[5].Value = NoticeType;
            parameters[6].Value = Num;
            parameters[7].Value = ZT;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPGongGao ");
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
            strSql.Append("select ID,TitleStr,UserName,UserBuMen,FuJian,ContentStr,TypeStr,TimeStr,NoticeType,ZT,Num ");
            strSql.Append(" FROM ERPGongGao ");
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
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                UserBuMen = ds.Tables[0].Rows[0]["UserBuMen"].ToString();
                FuJian = ds.Tables[0].Rows[0]["FuJian"].ToString();
                ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
                TypeStr = ds.Tables[0].Rows[0]["TypeStr"].ToString();
                TimeStr = ds.Tables[0].Rows[0]["TimeStr"].ToString();
                NoticeType = ds.Tables[0].Rows[0]["NoticeType"].ToString();
                Num =int.Parse(ds.Tables[0].Rows[0]["Num"].ToString());
                ZT = ds.Tables[0].Rows[0]["ZT"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[TitleStr],[TimeStr],[UserName],[UserBuMen],[FuJian],[ContentStr],[TypeStr],[NoticeType],[ZT],[SHR],[SHSJ],Num ");
            strSql.Append(" FROM ERPGongGao ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}