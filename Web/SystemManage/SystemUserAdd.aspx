<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemUserAdd.aspx.cs" Inherits="SystemManage_SystemUserAdd" %>
<html>
	<head>
		<title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">  
         <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>

          <script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=f1d3d82f283f2be724897552202a730c"></script>
  <script language="javascript">
  function PrintTable()
    {
        document.getElementById("PrintHide") .style.visibility="hidden"    
        print();
        document.getElementById("PrintHide") .style.visibility="visible"    
  }
  function CreateSerialNumber () {
      var PhoneNumber = document.getElementById('TextBox25');
      var SerialNumber = document.getElementById('TextBox4');
      if (PhoneNumber.value.length > 5) {
          SerialNumber.value = PhoneNumber.value.substr(PhoneNumber.value.length - 5, 5);
      }
  }
  function SelFenceXX() {
      var wName;
      var RadNum = Math.random();
      wName = window.showModalDialog('../Main/SelectFenceXX.aspx', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
      if (wName == null) { } else {
          var Name = new Array();
          Name = wName.split('|');
          document.getElementById('TextBox12').value = Name[0];
          document.getElementById('FenceId').value = Name[1];
      }
  }
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
              document.getElementById('TextBox12').value = e.obj.getPath().join(";");
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

      document.getElementById('TextBox12').value = e.lnglat.getLng();
      document.getElementById('TextBox12').value = e.lnglat.getLat();
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
                                    当前位置：网站首页&nbsp;-&nbsp;系统管理&nbsp;-&nbsp;添加用户
                                </ul>
                            </div>
                            <div class="hlb-navmune-return">
                               <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />    </div>
                        </div>
                    </td>
            </tr>
        </table>
        <div class="hlb-title">
            	<h1>添加用户</h1>
             <ul> <li><asp:Button  id="btn_Sub" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_Click"></asp:Button></li></ul>
               </div>
          <div class="hlb-listbox">
              <b class="LB"></b>
              <b class="RB"></b>
               <div class="hlb-listboderbox">
                     <b class="R" ></b>
                	<b class="LB"></b><b class="RB"></b>
    <table style="width:100%;" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">  
         <tr >
            <td colspan="3" style="background-color: #D3E6F4; height:10px; " >
            </td>
        </tr>             
      
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                用户名：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator></td>
             <td rowspan="14" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
        </tr>
        <tr>
          <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                用户密码：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox2" runat="server" Width="350px" Height="23px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                    ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                真实姓名：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox3" runat="server" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"
                    ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator></td>
        </tr>
        
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                所属部门：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox5" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectDanWei.aspx?TableName=ERPBuMen&LieName=BuMenName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox5').value=wName;}"
                    src="../images/Button/search.gif" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox5"
                    Display="Dynamic" ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                所属角色：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox6" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPJiaoSe&LieName=JiaoSeName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox6').value=wName;}"
                    src="../images/Button/search.gif" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox6"
                    Display="Dynamic" ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                职位：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox7" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=ZhiWei&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox7').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
         <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                Email地址：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox9" runat="server" Width="350px"></asp:TextBox></td>
        </tr>
          <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                手机号码：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox25" runat="server" Width="350px" onblur="CreateSerialNumber();"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*手机号码格式错误" ValidationExpression="1\d{10}" ControlToValidate="TextBox25"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                用户编号：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox4" runat="server" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox4"
                    ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                电子围栏：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox12" runat="server" Width="350px"></asp:TextBox>
                <input type="hidden" id="FenceId" value="" />
                <img class="HerCss" onclick="SelFenceXX()"
                    src="../images/Button/search.gif" />

            </td>
               
        </tr>
        <tr>
          <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                在岗状态：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox8" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=ZaiGang&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox8').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
      
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                是否允许登陆：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">是</asp:ListItem>
                    <asp:ListItem>否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                性别：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox10" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=Sex&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox10').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                IMEI码：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox11" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        </table>
       <%-- <table id="TabHide" style="width: 100%; visibility:hidden;" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">  
        <tr>
            <td align="center" colspan="2" style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <strong>人事信息</strong></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                出生日期：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox12" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var dataString = showModalDialog('../JS/calendar.htm', 'yyyy-mm-dd' ,'dialogWidth:286px;dialogHeight:221px;status:no;help:no;');if(dataString==null){}else{document.getElementById('TextBox12').value=dataString;}" src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                民族：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox13" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=MingZu&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox13').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                身份证号：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox14" runat="server" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                婚姻状况：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox15" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=HunYing&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox15').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                政治面貌：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox16" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=ZhengZhiMianMao&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox16').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                籍贯：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox17" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=JiGuan&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox17').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                户口所在地：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox18" runat="server" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                学历：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox19" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=XueLi&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox19').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                职称：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox20" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=ZhiCheng&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox20').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                毕业院校：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox21" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=BiYeYuanXiao&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox21').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                专业：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox22" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPUser&LieName=ZhuanYe&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox22').value=wName;}"
                    src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                参加工作时间：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox23" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var dataString = showModalDialog('../JS/calendar.htm', 'yyyy-mm-dd' ,'dialogWidth:286px;dialogHeight:221px;status:no;help:no;');if(dataString==null){}else{document.getElementById('TextBox23').value=dataString;}" src="../images/Button/search.gif" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                加入本单位时间：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox24" runat="server" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var dataString = showModalDialog('../JS/calendar.htm', 'yyyy-mm-dd' ,'dialogWidth:286px;dialogHeight:221px;status:no;help:no;');if(dataString==null){}else{document.getElementById('TextBox24').value=dataString;}" src="../images/Button/search.gif" /></td>
        </tr>
      
          <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                Email地址：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox9" runat="server" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                家庭详细住址：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox26" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                岗位变动情况：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox27" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                教育背景：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox28" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                工作简历：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox29" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                社会关系：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox30" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                奖惩记录：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox31" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                职务情况：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox32" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                培训记录：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox33" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                担保记录：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox34" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                劳动合同签订情况：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox35" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                社保缴纳情况：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox36" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                体检记录：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox37" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                备 注：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox38" runat="server" Height="75px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                附件：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                </asp:CheckBoxList>&nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                    ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton3_Click" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D6E2F3">
                上传附件：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                <asp:ImageButton ID="ImageButton2" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/UpLoad.jpg" OnClick="ImageButton2_Click" CausesValidation="False" /></td>
        </tr>
        </table>--%>
               </div> </div> 
    </div>
          <table style="width:100%">
             <tr>
                 <td align="left" style=" background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                   请选择位置:
                    <div id="container" style=" height:600px;">
        
             </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>