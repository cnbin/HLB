<%@ WebHandler Language="C#" Class="DataValidator" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

public class DataValidator : IHttpHandler, IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) {
        try
        {
            string action = context.Request["action"];
            string username = context.Request["username"];
            string x = context.Request["x"];
            string y = context.Request["y"];
            if (string.IsNullOrEmpty(action))
            {
                context.Response.Write("{}");
                context.Response.End();
            }
            else
            {
                action = action.ToLower();
            }
            switch (action)
            {
                case "getcoords":
                    context.Response.ContentType = "Application/json";                    
                    context.Response.Write(this.GetCoords(username));
                    break;
                case "getusers":
                    context.Response.ContentType = "Application/json";
                    context.Response.Write(this.GetUsers(username));
                    break;
                case "getpoi":
                    context.Response.ContentType = "Application/json";
                    context.Response.Write(this.GetPoi(x,y));
                    break;
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("{}");
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private string GetCoords(string username)
    {
        string Coords;
        FenceXX Model = new FenceXX();
        DataEntityDataContext context1 = new DataEntityDataContext();
        var T1 = context1.FenceXX.Where(f => f.ID > 0);
        T1 = T1.Where(f => f.FenceUser.Contains(username));
        T1 = T1.OrderByDescending(f => f.WHSJ);
        if (T1.Count() > 0)
            Coords = T1.ToList()[0].Coords;
        else
            Coords = "00";
        return Newtonsoft.Json.JsonConvert.SerializeObject(Coords);
    }
    private string GetUsers(string username)
    {
        ZWL.BLL.ERPUser MyModel = new ZWL.BLL.ERPUser();
        List<ZWL.BLL.ERPUser> list = MyModel.GetListByUserName("UserName in ('" + username + "') order by ID desc");
        foreach(var M in list) {
          
        string  Time=ZWL.DBUtility.DbHelperSQL.GetSHSL("Select Top 1 [TimeStr] from [ERPHuiBao] where [UserName]='" + M.UserName + "' order by [TimeStr]");
        if(string.IsNullOrEmpty(Time)) {
            M.CanJiaGongZuoTime = "暂无";
        }
        else {
            M.CanJiaGongZuoTime = Convert.ToDateTime(Time).ToString("yyyy年MM月dd日");
        }
        }
        if(list.Count > 0) {
            
            return Newtonsoft.Json.JsonConvert.SerializeObject(list); ;
        }
        else
            return "";
    }
    private string GetPoi(string x,string y)
    {
        //string xx = x;
        //string yy = y;
        DataEntityDataContext context = new DataEntityDataContext();
        var T = context.WZXX.Where(p => p.ID > 0 && p.X == x && p.Y == y);
        T = T.OrderByDescending(p => p.RQ);
        string res = "";
        if (T.Count() > 0)
        {
            var item = T.First();
            res = item.POI;
            return Newtonsoft.Json.JsonConvert.SerializeObject(res);
        }
        else
            return "";
        
    }
}