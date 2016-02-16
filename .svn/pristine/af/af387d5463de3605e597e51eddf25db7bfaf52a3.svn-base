<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeListView.aspx.cs" Inherits="SystemManage_TreeListView" %>
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
                                    当前位置：网站首页&nbsp;-&nbsp;系统管理&nbsp;-&nbsp;查看信息
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
            	<h1>查看信息</h1>
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
		显示文字：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblTextStr" runat="server"></asp:Label>
	</td>
         <td rowspan="9" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
	</tr>
	<tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		所用图片：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblImageUrlStr" runat="server"></asp:Label>
	</td></tr>
    <tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		主菜单样式：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblClass" runat="server"></asp:Label>
	</td></tr>
	<tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		后台数值：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblValueStr" runat="server"></asp:Label>
	</td></tr>
	<tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		链接地址：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblNavigateUrlStr" runat="server"></asp:Label>
	</td></tr>
	<tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		目标框架：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblTarget" runat="server"></asp:Label>
	</td></tr>
	<tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		父节点：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblParentID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		权限：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblQuanXianList" runat="server"></asp:Label>
	</td></tr>
	<tr>
	 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
		排序：
	</td>
	<td style="padding-left: 5px; height: 25px; background-color: #ffffff" >
		<asp:Label id="lblPaiXuStr" runat="server"></asp:Label>
	</td></tr>
</table></div></div>
		</div>
	</form>
</body>
</html>
