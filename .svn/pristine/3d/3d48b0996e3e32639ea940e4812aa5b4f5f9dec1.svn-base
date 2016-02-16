using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Web;//请先添加引用
using System.Collections.Generic;
using System.Reflection;

namespace ZWL.BLL
{
    public class AccountInfo
    {
        public AccountInfo()
		{}
        #region Model
        private int _id;
        private DateTime? _createdate;
        private string _voipaccount;
        private DateTime? _datecreated;
        private string _voippwd;
        private string _subtoken;
        private string _subsccountsid;
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
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VoipAccount
        {
            set { _voipaccount = value; }
            get { return _voipaccount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateCreated
        {
            set { _datecreated = value; }
            get { return _datecreated; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VoipPwd
        {
            set { _voippwd = value; }
            get { return _voippwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubToken
        {
            set { _subtoken = value; }
            get { return _subtoken; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubAccountSid
        {
            set { _subsccountsid = value; }
            get { return _subsccountsid; }
        }
        #endregion

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into hx_tblAccountInfo(");
            strSql.Append("CreateDate,VoipAccount,DateCreated,VoipPwd,SubToken,SubAccountSid)");
            strSql.Append(" values (");
            strSql.Append("@CreateDate,@VoipAccount,@DateCreated,@VoipPwd,@SubToken,@SubAccountSid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@VoipAccount", SqlDbType.VarChar,50),
					new SqlParameter("@DateCreated", SqlDbType.DateTime),
					new SqlParameter("@VoipPwd", SqlDbType.VarChar,50),
					new SqlParameter("@SubToken", SqlDbType.VarChar,50),
					new SqlParameter("@SubAccountSid", SqlDbType.VarChar,50)};
            parameters[0].Value = CreateDate;
            parameters[1].Value = VoipAccount;
            parameters[2].Value = DateCreated;
            parameters[3].Value = VoipPwd;
            parameters[4].Value = SubToken;
            parameters[5].Value = SubAccountSid;
            
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
        #endregion
    }
}
