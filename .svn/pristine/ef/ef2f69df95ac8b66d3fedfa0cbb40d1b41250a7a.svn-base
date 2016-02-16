<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DanWeiInfo.aspx.cs" Inherits="SystemManage_DanWeiInfo" %>
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
                            <div class="hlb-navmune"style="width:100%">
                                <ul class="hlb-navmunebtn">
                                    <a href="javascript:void(0);" onclick="hidemenu()"><img src="../Content/Newicons/hlb-01.png"  /></a>
                                </ul>
                                <ul class="hlb-navmunehome">
                                    <img src="../Content/Newicons/hlb-02.png" />
                                </ul>
                                <ul class="hlb-navmunelink">
                                    当前位置：网站首页&nbsp;-&nbsp;系统管理&nbsp;-&nbsp;单位信息管理
                                </ul>
                            </div>
                           
                    </td>
            </tr>

        </table>
         <div class="hlb-title">
            	<h1>单位信息管理</h1>
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
        <tr>
            <td colspan="3" style="background-color: #D3E6F4; height:10px; " >
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;" >
                单位名称：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator></td>
             <td rowspan="9" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                电话：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                传真：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox3" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                邮编：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox4" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                地址：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox5" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                网站：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox6" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                电子信箱：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox7" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                开户行：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox8" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                账号：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox9" runat="server" CssClass="inputCss" Width="365px"></asp:TextBox></td>
        </tr>
        </table></div></div></div>
    </form>
</body>
</html>