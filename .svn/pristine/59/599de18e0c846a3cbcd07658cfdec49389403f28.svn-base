<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POIAdd.aspx.cs" Inherits="GongGao_GongGaoAdd" %>

<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <script src="../UEditor/editor_config.js" type="text/javascript"></script>
    <script src="../UEditor/editor_all.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../UEditor/themes/default/ueditor.css" />
     <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>
    <script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=f1d3d82f283f2be724897552202a730c"></script>
    <script language="javascript">
        var mapObj;
        var myArray = new Array();
        function initialize() {
           // alert("initialize");
            var position = new AMap.LngLat(116.355733, 23.543778);
            mapObj = new AMap.Map("container", {
                view: new AMap.View2D({//创建地图二维视口
                    center: position, //创建中心点坐标
                    zoom: 14, //设置地图缩放级别
                    rotation: 0 //设置地图旋转角度
                }),
                lang: "zh_cn"//设置地图语言类型，默认：中文简体
            }); //创建地图实例

            AMap.event.addListener(mapObj, 'click', getLnglat);

        }
        //鼠标在地图上点击，获取经纬度坐标
        function getLnglat(e) {

            document.getElementById('TextBox3').value = e.lnglat.getLng();
            document.getElementById('TextBox4').value = e.lnglat.getLat();
            geocoder(e);
        }
        function geocoder(e) {
            //alert(e.lnglat);
            var MGeocoder;
            //加载地理编码插件
            AMap.service(["AMap.Geocoder"], function () {
                MGeocoder = new AMap.Geocoder({
                    radius: 1000,
                    extensions: "all"
                });
                //逆地理编码
                MGeocoder.getAddress(e.lnglat, function (status, result) {
                    if (status === 'complete' && result.info === 'OK') {
                        geocoder_CallBack(result, e);
                    }
                });
            });
        }
        function geocoder_CallBack(data, e) {
            var resultStr = "";
            var poiinfo = "";
            var address;
            //返回地址描述
            address = data.regeocode.formattedAddress;
            //alert(data);
            var info = [];
            info.push("<div><div><img style=\"float:left;\" src=\"../Controls/images/hlb.png\"/></div><br/> ");
            info.push("<div style=\"padding:0px 0px 0px 4px;\"><b>信息点(POI)位置</b>");
            info.push("地址 : " + address + "</div><br/></div>");

            infoWindow = new AMap.InfoWindow({
                content: info.join("<br/>")  //使用默认信息窗体框样式，显示信息内容
            });
            infoWindow.open(mapObj, e.lnglat);

        }  
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }
    </script>
</head>
<body onload="initialize();" >
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
                                    当前位置：网站首页&nbsp;-&nbsp;签到管理&nbsp;-&nbsp;添加POI点
                                </ul>
                            </div>
                            <div class="hlb-navmune-return">
                               <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />    </div>
                        </div>
                    </td>
            </tr>
        </table>
         <div class="hlb-title">
            	<h1>添加POI点</h1>
             <ul> <li><asp:Button  id="btn_Sub" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_Click"></asp:Button></li></ul>
               </div>
          <div class="hlb-listbox">
              <b class="LB"></b>
              <b class="RB"></b>
               <div class="hlb-listboderbox">
                     <b class="R" ></b>
                	<b class="LB"></b><b class="RB"></b>
        <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">
                <tr >
            <td colspan="3" style="background-color: #D3E6F4; height:10px; " >
            </td>
        </tr>       
            <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    POI名称：
                </td>
                <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator>
                </td>
                  <td rowspan="7" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
            </tr>
            <tr>
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    类型
                </td>
                <td style="padding-left: 5px; background-color: #ffffff">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    地址：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TxtContent" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    X坐标：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TextBox3" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    Y坐标：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TextBox4" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    描述：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TextBox5" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    状态：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:DropDownList ID="ZT" runat="server">
                        <asp:ListItem Value="启用" Text="启用"></asp:ListItem>
                        <asp:ListItem Value="不启用" Text="不启用"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             </table>
                    </div></div>
            <table style="width: 100%">
             <tr>
                 <td align="left" style=" background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                   请选择位置:
                    <div id="container" style=" height:600px;"> </div>
                </td>
            </tr>

        </table>
   </div>
    </form>
</body>
</html>
