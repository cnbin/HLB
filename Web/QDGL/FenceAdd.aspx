<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FenceAdd.aspx.cs" Inherits="QDGL_FenceAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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

            //AMap.event.addListener(mapObj, 'click', getLnglat);
            //设置多边形的属性
            var polygonOption = {
                strokeColor: "#FF33FF",
                strokeOpacity: 1,
                strokeWeight: 2
            };
            //在地图中添加MouseTool插件
            mapObj.plugin(["AMap.MouseTool"], function () {
                var mouseTool = new AMap.MouseTool(mapObj);
                //使用鼠标工具绘制多边形
                mouseTool.polygon(polygonOption);
                AMap.event.addListener(mouseTool, "draw", function (e) {
                    //obj属性就是绘制完成的覆盖物对象
                    var drawObj = e.obj;
                    //获取节点个数
                    var pointsCount = e.obj.getPath().length;
                    //alert("多边形节点数：" + pointsCount);
                    document.getElementById('txt_Coords').value = e.obj.getPath().join(";");
                });
            });
            // 加载比例尺插件
            mapObj.plugin(["AMap.Scale"], function () {
                scale = new AMap.Scale();
                mapObj.addControl(scale);
            });


        }
        //鼠标在地图上点击，获取经纬度坐标
        function getLnglat(e) {

            document.getElementById('txt_Coords').value = e.lnglat.getLng();
            document.getElementById('txt_Coords').value = e.lnglat.getLat();
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
      
        function SelPerson() {
            var wName;
            var RadNum = Math.random();
            var input_value = document.getElementById('UserName_Input').value;
            wName = window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr=' + RadNum + '&PersonList=' + input_value, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null) { }
            else {
                var Name = new Array();
                Name = wName.split('|');
                document.getElementById('UserName_Input').value = Name[0];
                document.getElementById('txt_FenceUser').value = Name[1];
            }
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
                                    当前位置：网站首页&nbsp;-&nbsp;签到管理&nbsp;-&nbsp;添加电子围栏&nbsp;
                                </ul>
                            </div>
                            <div class="hlb-navmune-return">
                               <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />    </div>
                        </div>
                    </td>
            </tr>
        </table>
        <div class="hlb-title">
            	<h1>添加电子围栏</h1>
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
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    电子围栏名称：
                </td>
                <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                    <asp:TextBox ID="txt_MC" runat="server" Width="350px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_MC"
                        ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator>
                </td>
                 <td rowspan="7" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    类型：
                </td>
                <td style="padding-left: 5px; background-color: #ffffff">
                    <asp:DropDownList ID="ddl_LX" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    所属县区：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txt_DZ" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    坐标集：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txt_Coords" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>   
            <tr> 
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                围栏人员：</td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txt_FenceUser" runat="server" Width="150px" disabled="disabled"></asp:TextBox>
                    <img class="HerCss" onclick="SelPerson()"
                        src="../images/Button/search.gif" />
                    <input type="hidden" id="UserName_Input"  runat="server"  value="" />
                </td>
           </tr>       
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    描述：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txt_BZ2" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>               
            <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
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
                   <table style="width:100%">
             <tr>
                 <td align="left" style=" background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                   请选择位置:
                    <div id="container" style=" height:600px;">
        
             </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
