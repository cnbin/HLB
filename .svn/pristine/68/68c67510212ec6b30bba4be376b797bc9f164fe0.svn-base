using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// BLLHelper 的摘要说明
/// </summary>
public class BLLHelper
{
	public BLLHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
        /// <summary>
        /// 插入数据
        /// </summary>
       [DataContract]
        public class DataModel
        {
            public DataModel()
            {
            }
            /// <summary>
            /// voip账号
            /// </summary>
            /// 
            [DataMember(IsRequired = false)]
            public string voipAccount
            {
                get;
                set;
            }
            /// <summary>
            /// 创建时间
            /// </summary>
            [DataMember(IsRequired = false)] 
            public string dateCreated
            {
                get;
                set;
            }
            /// <summary>
            /// voip密码
            /// </summary>
            [DataMember(IsRequired = false)] 
            public string voipPwd
            {
                get;
                set;
            }
            /// <summary>
            /// 子密码
            /// </summary>
            [DataMember(IsRequired = false)] 
            public string subToken
            {
                get;
                set;
            }
            /// <summary>
            /// 子账号
            /// </summary>
           [DataMember(IsRequired = false)] 
            public string subAccountSid
            {
                get;
                set;
            }
        }
        /// <summary>
        /// 插入
        /// </summary>
       [DataContract]
       public class PassDataInsert
       {
           /// <summary>
           /// 表名
           /// </summary>
           [DataMember(IsRequired = false)]
           public string statusCode
           {
               get;
               set;
           }
           /// <summary>
           /// 数据集
           /// </summary>
           [DataMember(IsRequired = false)]
           public DataModel SubAccount
           {
               get;
               set;
           }
       }
}