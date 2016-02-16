﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FenceModify.aspx.cs" Inherits="QDGL_FenceModify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
         <script src="../UEditor/editor_config.js" type="text/javascript"></script>
    <script src="../UEditor/editor_all.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../UEditor/themes/default/ueditor.css" />
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">
     <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>
  <script language="javascript">
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
<body>
    <form id="form1" runat="server" >
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
                                    当前位置：网站首页&nbsp;-&nbsp;签到管理&nbsp;-&nbsp;修改电子围栏
                                </ul>
                            </div>
                            <div class="hlb-navmune-return">
                               <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />    </div>
                        </div>
                    </td>
            </tr>
        </table>
        <div class="hlb-title">
            	<h1>修改电子围栏</h1>
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
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    类型：
                </td>
                <td style="padding-left: 5px; background-color: #ffffff">
                    <asp:DropDownList ID="ddl_LX" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    所属县区：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txt_DZ" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    坐标集：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txt_Coords" runat="server" Width="353px" TextMode="MultiLine" Height="48px" Font-Size="13px"></asp:TextBox>
                </td>
            </tr>   
            <tr> 
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                围栏人员：</td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                      <asp:TextBox ID="txt_FenceUser" runat="server" Width="150px" disabled="disabled"></asp:TextBox>
                    <img class="HerCss" onclick="SelPerson()"
                        src="../images/Button/search.gif" />
                      <input type="hidden" id="UserName_Input"  runat="server"  value="" />
                </td>
           </tr>       
            <tr>
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    描述：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txt_BZ2" runat="server" Width="350px"></asp:TextBox>
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
        </table></div></div>
        </div>
    </form>
</body>
</html>
