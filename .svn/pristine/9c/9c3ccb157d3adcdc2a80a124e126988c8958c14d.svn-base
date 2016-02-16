<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemJiaoSeView.aspx.cs" Inherits="SystemManage_SystemJiaoSeView" %>
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
                                    当前位置：网站首页&nbsp;-&nbsp;系统管理&nbsp;-&nbsp;查看角色
                                </ul>
                            </div>
                            <div class="hlb-navmune-return">
                               <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />    </div>
                        </div>
                    </td>
            </tr>
        </table>
        <div class="hlb-title">
            	<h1>查看角色</h1>
             <ul> 
                 <li><input type="button"  class="hlb-iframebtn5"  value="打印" OnClick="PrintTable()" /></li>
                 </ul>
               </div>
          <div class="hlb-listbox">
              
              <b class="RB"></b>
               <div class="hlb-listboderbox">
                     <b class="R" ></b>
                	<b class="RB"></b>
    <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">            
          <tr >
            <td colspan="3" style="background-color: #D3E6F4; height:10px; " >
            </td>
        </tr>     
        <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                角色名称：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
            <td rowspan="4" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
        </tr>
        <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                备注信息：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Label ID="Label2" runat="server"></asp:Label></td>
        </tr>        
        <tr>
            
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                权限配置：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                </td>
        </tr>
        <tr>
            <%--<td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">--%>
            <td colspan="2" align="center" style="height: 25px; background-color: #ffffff">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="8" RepeatDirection="Horizontal" Width="100%">
                   
                </asp:CheckBoxList></td>
        </tr>
        </table>
                </div></div></div>
    </form>
</body>
</html>