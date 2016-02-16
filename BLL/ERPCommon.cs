using System;
using System.Collections.Generic;
using System.Text;
using ZWL.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace ZWL.BLL
{
   public class ERPCommon
    {
       public ERPCommon() { }

       #region Model
       public int ID { get; set; }

       public string Code { get; set; }

       public string CName { get; set; }

       public string CType { get; set; }

       public int CSort { get; set; }

       public string CDescription { get; set; }

       public DateTime UpdateTime { get; set; } 
       #endregion

       #region 成员方法

       /// <summary>
       /// 判读是否存在数据,str条件优先考虑
       /// </summary>
       /// <param name="ID">ID值</param>
       /// <param name="str">查询条件</param>
       /// <returns></returns>
       public bool Exists(int ID, string str)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select count(1) from ERPCommon ");
           if (!string.IsNullOrEmpty(str))
           {
               sb.Append(" where " + str + "");
           }
           else sb.Append(" where ID=" + ID + "");

           return DbHelperSQL.Exists(sb.ToString());

       }

       /// <summary>
       /// 删除一条记录
       /// </summary>
       /// <param name="ID">ID</param>
       /// <returns></returns>
       public int Delete(int ID)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("delete ERPCommon where ID=" + ID + "");

           return DbHelperSQL.ExecuteSQL(sb.ToString());
       }

       /// <summary>
       /// 获得一个实体对象
       /// </summary>
       /// <param name="ID"></param>
       public void GetModel(int ID)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select ID, Code, CName, CType, CSort, CDescription, UpdateTime from ERPCommon where ID=" + ID + " ");
           SqlParameter[] parameter = { new SqlParameter("@ID", SqlDbType.Int, 5) };
           parameter[0].Value = ID;
           DataSet ds = new DataSet();
           ds = DbHelperSQL.Query(sb.ToString(), parameter);
           if (ds.Tables[0].Rows.Count > 0)
           {
               if (ds.Tables[0].Rows[0]["ID"].ToString() != "") ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
               Code = ds.Tables[0].Rows[0]["Code"].ToString();
               CName = ds.Tables[0].Rows[0]["CName"].ToString();
               CType = ds.Tables[0].Rows[0]["CType"].ToString();
               if (ds.Tables[0].Rows[0]["CSort"].ToString() != "") CSort = int.Parse(ds.Tables[0].Rows[0]["CSort"].ToString());
               CDescription = ds.Tables[0].Rows[0]["CDescription"].ToString();
               if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "") UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
           }

       }

       /// <summary>
       /// 获取数据列表
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public DataSet GetList(string str)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select ID, Code, CName, CType, CSort, CDescription, UpdateTime from ERPCommon ");
           if (!string.IsNullOrEmpty(str))
           {
               sb.Append(" where " + str + "");
           }
           return DbHelperSQL.Query(sb.ToString());
       }

       /// <summary>
       /// 新增一条数据
       /// </summary>
       /// <returns></returns>
       public int Add()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("insert into ERPCommon (Code, CName, CType, CSort, CDescription, UpdateTime) values(");
           sb.Append("@code,@cname,@ctype,@csort,@cdescription,@updatetime)");
           SqlParameter[] parameters ={
                                   new SqlParameter("@code",SqlDbType.VarChar,10),
                                   new SqlParameter("@cname",SqlDbType.NVarChar,50),
                                   new SqlParameter("@ctype",SqlDbType.VarChar,50),
                                   new SqlParameter("@csort",SqlDbType.Int,4),
                                   new SqlParameter("@cdescription",SqlDbType.NVarChar,200),
                                   new SqlParameter("@updatetime",SqlDbType.DateTime)
                                   };
           parameters[0].Value = Code;
           parameters[1].Value = CName;
           parameters[2].Value = CType;
           parameters[3].Value = CSort;
           parameters[4].Value = CDescription;
           parameters[5].Value = UpdateTime;
           return DbHelperSQL.ExecuteSql(sb.ToString(), parameters);
       }

       /// <summary>
       /// 修改一条记录
       /// </summary>
       /// <returns></returns>
       public int Update()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("update ERPCommon set ");
           sb.Append("Code=@code,");
           sb.Append("CName=@cname,");
           sb.Append("CType=@ctype,");
           sb.Append("CSort=@csort,");
           sb.Append("CDescription=@cdescription,");
           sb.Append("UpdateTime=@updatetime ");
           sb.Append(" where ID=@ID");
           SqlParameter[] parameters = {
                                       new SqlParameter("@code",SqlDbType.VarChar,10),
                                   new SqlParameter("@cname",SqlDbType.NVarChar,50),
                                   new SqlParameter("@ctype",SqlDbType.VarChar,50),
                                   new SqlParameter("@csort",SqlDbType.Int,4),
                                   new SqlParameter("@cdescription",SqlDbType.NVarChar,200),
                                   new SqlParameter("@updatetime",SqlDbType.DateTime),
                                   new SqlParameter("@ID",SqlDbType.Int,5)
                                       };
           parameters[0].Value = Code;
           parameters[1].Value = CName;
           parameters[2].Value = CType;
           parameters[3].Value = CSort;
           parameters[4].Value = CDescription;
           parameters[5].Value = UpdateTime;
           parameters[6].Value = ID;
           return DbHelperSQL.ExecuteSql(sb.ToString(), parameters);
       }


        
       #endregion


    }
}
