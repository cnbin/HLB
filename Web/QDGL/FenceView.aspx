<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FenceView.aspx.cs" Inherits="QDGL_FenceView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">
     <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>
  <script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=f1d3d82f283f2be724897552202a730c"></script>
  <script language="javascript">
      function PrintTable() {
          document.getElementById("PrintHide").style.visibility = "hidden"
          print();
          document.getElementById("PrintHide").style.visibility = "visible"
      }
      var mapObj;
      var myArray = new Array();
      //alert("initialize~~~");
      function initialize() {
          //alert("initialize");
          var position = new AMap.LngLat(116.355733, 23.543778);
          mapObj = new AMap.Map("container", {
              view: new AMap.View2D({//创建地图二维视口
                  center: position, //创建中心点坐标
                  zoom: 14, //设置地图缩放级别
                  rotation: 0 //设置地图旋转角度
              }),
              lang: "zh_cn"//设置地图语言类型，默认：中文简体
          }); //创建地图实例

          //AMap.event.addListener(mapObj, 'click', getLnglat);
          //设置多边形的属性
          var polygonOption = {
              strokeColor: "#FF33FF",
              strokeOpacity: 1,
              strokeWeight: 2
          };
         
          // 加载比例尺插件
          mapObj.plugin(["AMap.Scale"], function () {
              scale = new AMap.Scale();
              mapObj.addControl(scale);
          });
          
          addPolygon();
      }
      //alert("initialize~e ~~");
      //添加多边形覆盖物
      polygon = null;
      function addPolygon() {
          //alert(polygon);
          
          var coordss = "<%=Model.Coords%>";  
          //alert(coordss);

          var polygonArr = coordss.split(";");//多边形覆盖物节点坐标数组
          for (i = 0; i < polygonArr.length; i++)
          {      
              polygonArr[i] = polygonArr[i].split(",");

          }

          //alert(polygonArr[0] + "polygonArr[0]");
          //alert(polygonArr[0][1] + "polygonArr[0][1]");
          //alert(polygonArr.join("~~~"));
          polygon = new AMap.Polygon({
              path: polygonArr,//设置多边形边界路径
              strokeColor: "#FF33FF", //线颜色
              strokeOpacity: 0.2, //线透明度
              strokeWeight: 3,    //线宽
              fillColor: "#1791fc", //填充色
              fillOpacity: 0.35//填充透明度
          });
          polygon.setMap(mapObj);
          //alert(polygonArr[0]);
          mapObj.setZoomAndCenter(14, polygonArr[0]);
      }
     
  </script>
</head>
<body onload="initialize();">
    <form id="form1" runat="server">
    <div>    
     <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">            
             <tr>
                <td valign="middle"  style="height:30px;">
                        <div class="hlb-contact">
                            <div class="hlb-navmune">
                                <ul class="hlb-navmunebtn">
                                    <a href="javascript:void(0);" onclick="hidemenu()"><img src="../Content/Newicons/hlb-01.png"  /></a>
                                </ul>
                                <ul class="hlb-navmunehome">
                                    <img src="../Content/Newicons/hlb-02.png" />
                                </ul>
                                <ul class="hlb-navmunelink">
                                    当前位置：网站首页&nbsp;-&nbsp;签到管理 &nbsp;-&nbsp;查看电子围栏
                                </ul>
                            </div>
                             <div class="hlb-navmune-return">
                                 <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />
                            </div>
                        </div>
                    </td>
            </tr>
        </table>
          <div class="hlb-title">
            	<h1>查看电子围栏</h1>
                <ul>
                	 	<li><input type="button"  class="hlb-iframebtn5"  value="打印" OnClick="PrintTable()" /></li>
                </ul>
            </div>
          <div class="hlb-listbox">
              <b class="LB"></b><b class="RB"></b>
               <div class="hlb-listboderbox">
                     <b class="R"></b>
                	<b class="LB"></b><b class="RB"></b>
     <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">           
        <tr >
            <td colspan="3" style="background-color: #D3E6F4; height:10px; " >
            </td>
             </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                电子围栏名称：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    
                    <asp:Label ID="lbl_MC" runat="server"></asp:Label></td>
            <td rowspan="7" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                类型 : </td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="lbl_LX" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                所属县区：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="lbl_DZ" runat="server"></asp:Label></td>
        </tr>
        <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; font-size: 14px;color: #2A5573;" >
                坐标集：</td>
            <td style="padding-left: 5px;  background-color: #ffffff">
                <asp:Label ID="lbl_Coords" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                围栏人员：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="lbl_FenceUser" runat="server"></asp:Label></td>
        </tr>
        <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                描述：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="lbl_BZ2" runat="server"></asp:Label></td>
        </tr>
         <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                状态：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="lbl_ZT" runat="server"></asp:Label></td>
        </tr>
        </table></div></div></div>
        <table style="width:100%">
          <tr>
               <td align="center" style=" background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                   电子转栏位置:
                    <div id="container" style=" height:600px;">
        
                 </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
