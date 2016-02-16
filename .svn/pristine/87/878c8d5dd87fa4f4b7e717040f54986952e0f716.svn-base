<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HuiBaoView.aspx.cs" Inherits="WorkPlan_WorkPlanView" %>
<html>
	<head>
		<title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">
            <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>
  <script language="javascript">
  function PrintTable()
    {
        document.getElementById("PrintHide") .style.visibility="hidden"    
        print();
        document.getElementById("PrintHide") .style.visibility="visible"    
  }
  function SelPerson()
  {
      var wName;
      var RadNum = Math.random();
      var input_value = document.getElementById('UserName_Input').value;
      wName = window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr=' + RadNum + '&PersonList=' + input_value, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
      if (wName == '|')
      { }
      else {
          var Name = new Array();
          Name = wName.split('|');
          document.getElementById('UserName_Input').value = Name[0];
          document.getElementById('TextBox2').value = Name[1];
      }
  }
  </script>
</head>
<body>
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
                                    当前位置：网站首页&nbsp;-&nbsp;预警信息&nbsp;-&nbsp;查看工作报告
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
            	<h1>查看工作报告</h1>
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
                报告主题：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
            <td rowspan="7" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
        </tr>

        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                附件：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="Label3" runat="server"></asp:Label></td>
        </tr>
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                用户：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="Label4" runat="server"></asp:Label>
              </td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                发送时间：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="Label5" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                类型：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="Label8" runat="server"></asp:Label></td>
        </tr>
        <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                报告内容：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="Label6" runat="server"></asp:Label></td>
        </tr>
               <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                状态：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:Label ID="Label7" runat="server"></asp:Label> 
                     转发人员<asp:TextBox ID="TextBox2" runat="server" Width="150px"></asp:TextBox>
                    <input type="hidden" value="" id="UserName_Input"  runat="server"/>
                    <img class="HerCss" onclick="SelPerson();"
                        src="../images/Button/search.gif" />
                    <asp:Button Text="确定" runat="server" ID="QD" onclick="QD_Click" />&nbsp;&nbsp;
                    <asp:Button Text="预警保存" runat="server" ID="ZT1" OnClick="ZT1_Click"/>&nbsp;&nbsp;
                    <asp:Button Text="预警取消" runat="server" ID="ZT3" OnClick="ZT2_Click"/>&nbsp;&nbsp;
                </td>
        </tr>
          <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                转发人员列表：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="Label2" runat="server"></asp:Label></td>
        </tr>
        </table></div>
    </form>
</body>
</html>