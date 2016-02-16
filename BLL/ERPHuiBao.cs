using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using System.Collections.Generic;
using System.Reflection;

namespace ZWL.BLL {
    /// <summary>
    /// 类ERPHuiBao。
    /// </summary>
    public class ERPHuiBao {
        public ERPHuiBao() {
        }
        #region Model
        private int _id;
        private string _titlestr;
        private string _ssbm;
        private string _ddwl;
        private string _zt;
        private string _bz1;
        private string _bz2;
        private string _contentstr;
        private string _fujianstr;
        private string _username;
        private string _canlookuser;
        private DateTime? _timestr;
        /// <summary>
        /// 
        /// </summary>
        public int ID {
            set {
                _id = value;
            }
            get {
                return _id;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TitleStr {
            set {
                _titlestr = value;
            }
            get {
                return _titlestr;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSBM {
            set {
                _ssbm = value;
            }
            get {
                return _ssbm;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DDWL {
            set {
                _ddwl = value;
            }
            get {
                return _ddwl;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZT {
            set {
                _zt = value;
            }
            get {
                return _zt;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BZ1 {
            set {
                _bz1 = value;
            }
            get {
                return _bz1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BZ2 {
            set {
                _bz2 = value;
            }
            get {
                return _bz2;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentStr {
            set {
                _contentstr = value;
            }
            get {
                return _contentstr;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FuJianStr {
            set {
                _fujianstr = value;
            }
            get {
                return _fujianstr;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName {
            set {
                _username = value;
            }
            get {
                return _username;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CanLookUser {
            set {
                _canlookuser = value;
            }
            get {
                return _canlookuser;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeStr {
            set {
                _timestr = value;
            }
            get {
                return _timestr;
            }
        }
        #endregion Model


        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPHuiBao");
            strSql.Append(" where ID=" + ID + " ");

            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add() {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPHuiBao(");
            strSql.Append("TitleStr,ContentStr,FuJianStr,UserName,CanLookUser,TimeStr,SSBM,DDWL,ZT,BZ1,BZ2)");
            strSql.Append(" values (");
            strSql.Append("@TitleStr,@ContentStr,@FuJianStr,@UserName,@CanLookUser,@TimeStr,@SSBM,@DDWL,@ZT,@BZ1,@BZ2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@FuJianStr", SqlDbType.VarChar,2000),
					new SqlParameter("@UserName", SqlDbType.VarChar,100),
					new SqlParameter("@CanLookUser", SqlDbType.VarChar,8000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
                            new SqlParameter("@SSBM", SqlDbType.VarChar,100),
                                        new SqlParameter("@DDWL", SqlDbType.VarChar,100),
                                        new SqlParameter("@ZT", SqlDbType.VarChar,100),
                                        new SqlParameter("@BZ1", SqlDbType.VarChar,100),
                                        new SqlParameter("@BZ2", SqlDbType.VarChar,100)};
            parameters[0].Value = TitleStr;
            parameters[1].Value = ContentStr;
            parameters[2].Value = FuJianStr;
            parameters[3].Value = UserName;
            parameters[4].Value = CanLookUser;
            parameters[5].Value = TimeStr;
            parameters[6].Value = SSBM;
            parameters[7].Value = DDWL;
            parameters[8].Value = ZT;
            parameters[9].Value = BZ1;
            parameters[10].Value = BZ2;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if(obj == null) {
                return 1;
            }
            else {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update() {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPHuiBao set ");
            strSql.Append("TitleStr=@TitleStr,");
            strSql.Append("ContentStr=@ContentStr,");
            strSql.Append("FuJianStr=@FuJianStr,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("CanLookUser=@CanLookUser,");
            strSql.Append("TimeStr=@TimeStr,");
            strSql.Append("SSBM=@SSBM,");
            strSql.Append("DDWL=@DDWL,");
            strSql.Append("ZT=@ZT,");
            strSql.Append("BZ1=@BZ1,");
            strSql.Append("BZ2=@BZ2");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TitleStr", SqlDbType.VarChar,500),
					new SqlParameter("@ContentStr", SqlDbType.Text),
					new SqlParameter("@FuJianStr", SqlDbType.VarChar,2000),
					new SqlParameter("@UserName", SqlDbType.VarChar,100),
					new SqlParameter("@CanLookUser", SqlDbType.VarChar,8000),
					new SqlParameter("@TimeStr", SqlDbType.DateTime),
                                         new SqlParameter("@SSBM", SqlDbType.VarChar,100),
                                        new SqlParameter("@DDWL", SqlDbType.VarChar,100),
                                        new SqlParameter("@ZT", SqlDbType.VarChar,100),
                                        new SqlParameter("@BZ1", SqlDbType.VarChar,100),
                                        new SqlParameter("@BZ2", SqlDbType.VarChar,100)};
            parameters[0].Value = ID;
            parameters[1].Value = TitleStr;
            parameters[2].Value = ContentStr;
            parameters[3].Value = FuJianStr;
            parameters[4].Value = UserName;
            parameters[5].Value = CanLookUser;
            parameters[6].Value = TimeStr;
            parameters[7].Value = SSBM;
            parameters[8].Value = DDWL;
            parameters[9].Value = ZT;
            parameters[10].Value = BZ1;
            parameters[11].Value = BZ2;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateZT() {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPHuiBao set ");
            strSql.Append("ZT=@ZT");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                                        new SqlParameter("@ZT", SqlDbType.VarChar,100)};
            parameters[0].Value = ID;
            parameters[1].Value = ZT;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ERPHuiBao ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TitleStr,ContentStr,FuJianStr,UserName,CanLookUser,TimeStr,SSBM,DDWL,ZT,BZ1,BZ2 ");
            strSql.Append(" FROM ERPHuiBao ");
            strSql.Append(" where ID=" + ID + " ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)				};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if(ds.Tables[0].Rows.Count > 0) {
                if(ds.Tables[0].Rows[0]["ID"].ToString() != "") {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                TitleStr = ds.Tables[0].Rows[0]["TitleStr"].ToString();
                ContentStr = ds.Tables[0].Rows[0]["ContentStr"].ToString();
                FuJianStr = ds.Tables[0].Rows[0]["FuJianStr"].ToString();
                UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                CanLookUser = ds.Tables[0].Rows[0]["CanLookUser"].ToString();
                if(ds.Tables[0].Rows[0]["TimeStr"].ToString() != "") {
                    TimeStr = DateTime.Parse(ds.Tables[0].Rows[0]["TimeStr"].ToString());
                }
                SSBM = ds.Tables[0].Rows[0]["SSBM"].ToString();
                DDWL = ds.Tables[0].Rows[0]["DDWL"].ToString();
                ZT = ds.Tables[0].Rows[0]["ZT"].ToString();
                BZ1 = ds.Tables[0].Rows[0]["BZ1"].ToString();
                BZ2 = ds.Tables[0].Rows[0]["BZ2"].ToString();
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,string Orderby, bool IsDesc) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[TitleStr],[ContentStr],[FuJianStr],[UserName],[CanLookUser],[TimeStr],SSBM,DDWL,ZT,BZ1,BZ2 ");
            strSql.Append(" FROM hx_vERPHuiBao ");
            if(strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            if(!string.IsNullOrEmpty(Orderby)) {
                string Desc = IsDesc == true ? "Desc" : "Asc";
                strSql.Append(" order by " + Orderby+" " + Desc);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public string GetListByZT(string zt) {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            DataSet ds = GetList("ZT not in('" + zt + "')","",false);
            var list = List<ERPHuiBao>(ds.Tables[0]);
            string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);
            return jsonGroup0;
        }
        /// <summary>
        /// 根据用户和主题获取DataSet类型数据
        /// </summary>
        /// <param name="UserNameList"></param>
        /// <param name="TitleStr"></param>
        /// <returns></returns>
        public DataSet GetDataSetByUserNameTitleStr(string UserNameList, string TitleStr) {
            string[] UNs = UserNameList.Split(',');
            string ConditionUNs = "";
            string ConditionTS = "";
            string Condition = "";
            //for(int i = 0; i < UNs.Length; i++) {
            //    ConditionUNs += i > 0 ? "," : "";
            //    ConditionUNs += "'" + UNs[i] + "'";
            //}
            foreach(string UN in UNs) {
                if(!string.IsNullOrEmpty(UN)) {
                    ConditionUNs += ConditionUNs == "" ? "([UserName] like '%" + UN + "%')" : "or ( [UserName] like '%" + UN + "%')";
                }
            }
            if(ConditionUNs != "") {
                Condition += ConditionUNs;
            }
            if(!string.IsNullOrEmpty(TitleStr)) {
                ConditionTS += ConditionTS == "" ? "(TitleStr like '%" + TitleStr + "%')" : "or (TitleStr like '%" + TitleStr + "%')";
            }
            if(ConditionTS != "") {
                Condition += Condition == "" ? ConditionTS : "and " + ConditionTS;
            }
            return GetList(Condition,"ID",true);
        }

        /// <summary>
        /// 根据用户和主题获取String类型预警信息
        /// </summary>
        /// <param name="UserNames"></param>
        /// <returns></returns>
        public string GetListByUserNameTitleStr(string UserNameList, string TitleStr) {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            DataSet ds = GetDataSetByUserNameTitleStr(UserNameList, TitleStr);
            var list = List<ERPHuiBao>(ds.Tables[0]);
            string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);
            return jsonGroup0;
        }
        /// <summary>
        /// 根据部门获取预警信息
        /// </summary>
        /// <param name="UserNames"></param>
        /// <returns></returns>
        public string GetListByDeparment(int DepartmentID) {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            DataSet ds = GetList("CHARINDEX('" + ZWL.Common.PublicMethod.GetSessionValue("DepartmentID") + "',p_depart_ids)>0","ID",true);
            var list = List<ERPHuiBao>(ds.Tables[0]);
            string jsonGroup0 = Newtonsoft.Json.JsonConvert.SerializeObject(list, timeConverter);
            return jsonGroup0;
        }

        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> List<T>(DataTable dt) {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach(DataRow item in dt.Rows) {
                T s = System.Activator.CreateInstance<T>();
                for(int i = 0; i < dt.Columns.Count; i++) {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if(info != null) {
                        if(!Convert.IsDBNull(item[i])) {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
        #endregion  成员方法
    }
}