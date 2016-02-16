using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
namespace ZWL.BLL
{
	/// <summary>
	/// 类ERPWorkFlowToDoUser。
	/// </summary>
	public class ERPNWorkFlowToDoUser
	{
		public ERPNWorkFlowToDoUser()
		{}
		#region Model
		private int _id;
        private int _todoid;
        private int _nodeid;
        private int _workflowid;
		private string _shenpiuserlist;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int ToDoID
        {
            set { _todoid = value; }
            get { return _todoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NodeID
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int WorkFlowID
        {
            set { _workflowid = value; }
            get { return _workflowid; }
        }

		/// <summary>
		/// 简要说明
		/// </summary>
		public string ShenPiUserList
		{
            set { _shenpiuserlist = value; }
            get { return _shenpiuserlist; }
		}
		#endregion Model


		#region  成员方法

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ERPNWorkFlowToDoUser(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,ToDoID,NodeID,WorkFlowID,ShenPiUserList ");
            strSql.Append(" FROM ERPNWorkFlowToDoUser ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
                if (ds.Tables[0].Rows[0]["ToDoID"].ToString() != "")
                {
                    ToDoID = int.Parse(ds.Tables[0].Rows[0]["ToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NodeID"].ToString() != "")
                {
                    NodeID = int.Parse(ds.Tables[0].Rows[0]["NodeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                ShenPiUserList = ds.Tables[0].Rows[0]["ShenPiUserList"].ToString();
			}
		}

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{

            return DbHelperSQL.GetMaxID("ID", "ERPNWorkFlowToDoUser"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from ERPNWorkFlowToDoUser");
			strSql.Append(" where ID=@ID ");

			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ToDoID,int NodeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPNWorkFlowToDoUser");
            strSql.Append(" where ToDoID=@ToDoID And  NodeID=@NodeID");

            SqlParameter[] parameters = {
					new SqlParameter("@ToDoID", SqlDbType.Int,4),
                    new SqlParameter("@NodeID", SqlDbType.Int,4)};
            parameters[0].Value = ToDoID;
            parameters[1].Value = NodeID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into ERPNWorkFlowToDoUser(");
            strSql.Append("ToDoID,NodeID,WorkFlowID,ShenPiUserList)");
			strSql.Append(" values (");
            strSql.Append("@ToDoID,@NodeID,@WorkFlowID,@ShenPiUserList)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ToDoID", SqlDbType.Int,4),
					new SqlParameter("@NodeID", SqlDbType.Int,4),
                    new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@ShenPiUserList", SqlDbType.VarChar,200)};
            parameters[0].Value = ToDoID;
            parameters[1].Value = NodeID;
            parameters[2].Value = WorkFlowID;
            parameters[3].Value = ShenPiUserList;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update ERPNWorkFlowToDoUser set ");
            strSql.Append("ToDoID=@ToDoID,");
            strSql.Append("NodeID=@NodeID,");
            strSql.Append("WorkFlowID=@WorkFlowID,");
            strSql.Append("ShenPiUserList=@ShenPiUserList");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ToDoID", SqlDbType.Int,4),
					new SqlParameter("@NodeID", SqlDbType.Int,4),
                    new SqlParameter("@WorkFlowID", SqlDbType.Int,4),
					new SqlParameter("@ShenPiUserList", SqlDbType.VarChar,200)};
			parameters[0].Value = ID;
            parameters[1].Value = ToDoID;
            parameters[2].Value = NodeID;
            parameters[3].Value = WorkFlowID;
            parameters[4].Value = ShenPiUserList;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateUser()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPNWorkFlowToDoUser set ");
            strSql.Append("ShenPiUserList=@ShenPiUserList");
            strSql.Append(" where ToDoID=@ToDoID And NodeID=@NodeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ToDoID", SqlDbType.Int,4),
					new SqlParameter("@NodeID", SqlDbType.Int,4),
					new SqlParameter("@ShenPiUserList", SqlDbType.VarChar,200)};

            parameters[0].Value = ToDoID;
            parameters[1].Value = NodeID;
            parameters[2].Value = ShenPiUserList;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from ERPNWorkFlowToDoUser ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public void GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,ToDoID,NodeID,WorkFlowID,ShenPiUserList ");
            strSql.Append(" FROM ERPNWorkFlowToDoUser ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ToDoID"].ToString() != "")
                {
                    ToDoID = int.Parse(ds.Tables[0].Rows[0]["ToDoID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NodeID"].ToString() != "")
                {
                    NodeID = int.Parse(ds.Tables[0].Rows[0]["NodeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkFlowID"].ToString() != "")
                {
                    WorkFlowID = int.Parse(ds.Tables[0].Rows[0]["WorkFlowID"].ToString());
                }
                ShenPiUserList = ds.Tables[0].Rows[0]["ShenPiUserList"].ToString();
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM ERPNWorkFlowToDoUser ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  成员方法
	}
}

