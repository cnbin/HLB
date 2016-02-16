using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Runtime.Serialization.Json;

/// <summary>
/// JsonHelper 的摘要说明
/// </summary>
public class JsonHelper
{
    /// <summary>
    /// 把Json转成List<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="html"></param>
    /// <returns></returns>
    public static IList<T> JsonToList<T>(string html)
    {
        IList<T> result = new List<T>();
        html = FormatJson(html);
        try
        {
            DataContractJsonSerializer _Json = new DataContractJsonSerializer(result.GetType());
            byte[] _Using = System.Text.Encoding.UTF8.GetBytes(html);
            System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
            _MemoryStream.Position = 0;
            object obj = _Json.ReadObject(_MemoryStream); ;
            result = (IList<T>)obj;
        }
        catch (Exception ex)
        {
            try
            {
                html = AttributeToVariable(html);

                DataContractJsonSerializer _Json = new DataContractJsonSerializer(result.GetType());
                byte[] _Using = System.Text.Encoding.UTF8.GetBytes(html);
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
                _MemoryStream.Position = 0;
                object obj = _Json.ReadObject(_MemoryStream); ;
                result = (IList<T>)obj;
            }
            catch (Exception ee)
            {
                html = VariableToAttribute(html);
                DataContractJsonSerializer _Json = new DataContractJsonSerializer(result.GetType());
                byte[] _Using = System.Text.Encoding.UTF8.GetBytes(html);
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
                _MemoryStream.Position = 0;
                object obj = _Json.ReadObject(_MemoryStream); ;
                result = (IList<T>)obj;
            }
        }
        return result;
    }

    public static T JsonToObject<T>(string html)
    {
        T result = System.Activator.CreateInstance<T>();
        #region // 转换
        //return (NResult<T>)Newtonsoft.Json.JavaScriptConvert.DeserializeObject(html, typeof(NResult<T>));

        html = FormatJson(html);
        try
        {
            DataContractJsonSerializer _Json = new DataContractJsonSerializer(result.GetType());
            byte[] _Using = System.Text.Encoding.UTF8.GetBytes(html);
            System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
            _MemoryStream.Position = 0;
            object obj = _Json.ReadObject(_MemoryStream); ;
            result = (T)obj;
        }
        catch (Exception ex)
        {
            try
            {
                html = AttributeToVariable(html);

                DataContractJsonSerializer _Json = new DataContractJsonSerializer(result.GetType());
                byte[] _Using = System.Text.Encoding.UTF8.GetBytes(html);
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
                _MemoryStream.Position = 0;
                object obj = _Json.ReadObject(_MemoryStream); ;
                result = (T)obj;
            }
            catch (Exception ee)
            {
                html = VariableToAttribute(html);
                DataContractJsonSerializer _Json = new DataContractJsonSerializer(result.GetType());
                byte[] _Using = System.Text.Encoding.UTF8.GetBytes(html);
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
                _MemoryStream.Position = 0;
                object obj = _Json.ReadObject(_MemoryStream); ;
                result = (T)obj;
            }
        }
        #endregion // 转换
        return result;
    }
    
    #region // 格式化Json字符串
    /// <summary>
    /// 格式化Json字符串，使之能转换成List
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string FormatJson(string value)
    {
        string p = @"(new Date)\(+([0-9,-]+)+(\))";
        MatchEvaluator matchEvaluator = new MatchEvaluator(FormatJsonMatch);
        Regex reg = new Regex(p);
        bool isfind = reg.IsMatch(value);
        value = reg.Replace(value, matchEvaluator);
        return value;
    }
    /// <summary>
    /// 将Json序列化的时间由new Date(1373387734703)转为字符串"\/Date(1373387734703)\/"
    /// </summary>
    private static string FormatJsonMatch(Match m)
    {
        return string.Format("\"\\/Date({0})\\/\"", m.Groups[2].Value);
    }

    #endregion // 格式化Json字符串

    #region // 格式化日期
    /// <summary>
    /// 将Json序列化的时间由new Date(1373390933250) 或Date(1373390933250)或"\/Date(1373390933250+0800)\/"
    /// 转为2013-07-09 17:28:53
    /// 主要用于传给前台
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string FormatJsonDate(string value)
    {
        string p = @"(new Date)\(+([0-9,-]+)+(\))";
        MatchEvaluator matchEvaluator = new MatchEvaluator(FormatJsonDateMatch);
        Regex reg = new Regex(p);
        value = reg.Replace(value, matchEvaluator);

        p = @"(Date)\(+([0-9,-]+)+(\))";
        matchEvaluator = new MatchEvaluator(FormatJsonDateMatch);
        reg = new Regex(p);
        value = reg.Replace(value, matchEvaluator);

        p = "\"\\\\\\/" + @"Date(\()([0-9,-]+)([+])([0-9,-]+)(\))" + "\\\\\\/\"";
        matchEvaluator = new MatchEvaluator(FormatJsonDateMatch);
        reg = new Regex(p);
        value = reg.Replace(value, matchEvaluator);

        return value;

    }
    /// <summary>
    /// 将Json序列化的时间由Date(1294499956278+0800)转为字符串
    /// </summary>
    private static string FormatJsonDateMatch(Match m)
    {

        string result = string.Empty;

        DateTime dt = new DateTime(1970, 1, 1);

        dt = dt.AddMilliseconds(long.Parse(m.Groups[2].Value));

        dt = dt.ToLocalTime();

        result = dt.ToString("yyyy-MM-dd HH:mm:ss");

        return result;
    }
    #endregion // 格式化日期

    #region // 属性和变量转换
    /// <summary>
    /// 属性转变量
    /// 把"<address>k__BackingField":"1",
    /// 转成"address":"1",
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string AttributeToVariable(string value)
    {
        string p = @"\<([A-Z,a-z,0-9,_]*)\>k__BackingField";
        MatchEvaluator matchEvaluator = new MatchEvaluator(AttributeToVariableMatch);
        Regex reg = new Regex(p);
        bool isfind = reg.IsMatch(value);
        value = reg.Replace(value, matchEvaluator);
        return value;
    }
    private static string AttributeToVariableMatch(Match m)
    {
        return m.Groups[1].Value;
    }

    /// <summary>
    /// 变量转属性
    /// 把"address":"1",
    /// 转成"<address>k__BackingField":"1",
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string VariableToAttribute(string value)
    {
        string p = "\\\"([A-Z,a-z,0-9,_]*)\\\"\\:";
        MatchEvaluator matchEvaluator = new MatchEvaluator(VariableToAttributeMatch);
        Regex reg = new Regex(p);
        bool isfind = reg.IsMatch(value);
        value = reg.Replace(value, matchEvaluator);
        return value;
    }
    private static string VariableToAttributeMatch(Match m)
    {
        return string.Format("\"<{0}>k__BackingField\":", m.Groups[1].Value);
    }

    #endregion // 属性和变量转换


}