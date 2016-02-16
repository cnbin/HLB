<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangPwd.aspx.cs" Inherits="Personal_ChangPwd" %>
<html>
	<head>
		<title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">
        <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>
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
                                    当前位置：网站首页&nbsp;-&nbsp;个人办公&nbsp;-&nbsp;修改密码
                                </ul>
                            </div>
                             <div class="hlb-navmune-return">
                                   </div>
                    </td>
            </tr>
       
        </table>
         <div class="hlb-title">
            	<h1>修改密码</h1>
                <ul>
                	<li><asp:Button  id="btn_Sub" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_Click"></asp:Button></li>
                </ul>
            </div>
          <div class="hlb-listbox">
              <b class="LB"></b><b class="RB"></b>
               <div class="hlb-listboderbox">
                    <b class="R"></b>
                	<b class="LB"></b><b class="RB"></b>
    <table class="hlbdata-list" style="width:100%">    
                
        <tr >
            <td colspan="3" style="background-color: #D3E6F4; height:10px; " >

            </td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                用户名：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
            <td rowspan="4" style="background-color: #D3E6F4; width:14px;"></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573; ">
                旧密码：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="txt_pwd" runat="server" TextMode="Password" Width="250px" Height="20px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_pwd"
                    Display="Dynamic" ErrorMessage="*该项不能为空"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                新密码：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" Width="250px" Height="20px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    Display="Dynamic" ErrorMessage="*该项不能为空"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                重复密码：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" Width="250px" Height="20px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                    Display="Dynamic" ErrorMessage="*该项不能为空"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox1"
                    ControlToValidate="TextBox2" ErrorMessage="*前后密码不一致"></asp:CompareValidator></td>
        </tr>
       
        </table></div></div></div>
    </form>
</body>
</html>