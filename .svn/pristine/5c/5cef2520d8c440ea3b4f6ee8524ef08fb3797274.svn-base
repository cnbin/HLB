<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailView.aspx.cs" Inherits="LanEmail_EmailView" %>
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
  function ReturnClicks() {
      var ReturnInput = document.getElementById('ReturnInput');
      if (ReturnInput.value.indexOf('LanEmailAdd') != -1)
      {
          var iframeid= document.getElementById('iframeid');
          var iframes = parent.document.getElementById(iframeid.value);
          ReturnInput.value = iframes.src;
      }
      ReturnClick();
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
                                    当前位置：网站首页&nbsp;-&nbsp;任务调度&nbsp;-&nbsp;查看邮件
                                </ul>
                            </div>
                             <div class="hlb-navmune-return">
                                 <input type="button" onclick="ReturnClicks();"  class="hlb-iframebtn5_1s" value="返回"/>
                                 <input type="hidden" value="" id="ReturnInput" runat="server" />
                                 <input type="hidden" value="" id="iframeid" runat="server" /></div>
                        </div>
                    </td>
            </tr>
        </table>
        <div class="hlb-title">
            	<h1>查看邮件</h1>
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
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                邮件主题：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
            <td rowspan="6" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
        </tr>
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                发送人：</td>
            <td style="height: 25px; background-color: #ffffff; padding-left:5px;">
                <asp:Label ID="Label2" runat="server"></asp:Label>
                &nbsp; 
                <a class="BlueCss" href="LanEmailAdd.aspx?Type=HuiFu&ID=<%=Request.QueryString["ID"].ToString()%>">回复</a> 
                &nbsp; 
                <a class="BlueCss" href="LanEmailAdd.aspx?Type=ZhuanFa&ID=<%=Request.QueryString["ID"].ToString()%>">转发</a> </td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                接收人：</td>
            <td style="height: 25px; background-color: #ffffff; padding-left:5px;">
                <asp:Label ID="Label3" runat="server"></asp:Label></td>
        </tr>
        <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                发送时间：</td>
            <td style="height: 25px; background-color: #ffffff; padding-left:5px;">
                <asp:Label ID="Label4" runat="server"></asp:Label></td>
        </tr>
        <tr>
           <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                邮件附件：</td>
            <td style="height: 25px; background-color: #ffffff; padding-left:5px;">
                <asp:Label ID="Label5" runat="server"></asp:Label></td>
        </tr>
        <tr>
             <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                邮件内容：</td>
            <td style="height: 25px; background-color: #ffffff; padding-left:5px;">
                <asp:Label ID="Label6" runat="server"></asp:Label></td>
        </tr>
        </table></div></div></div>
    </form>
</body>
</html>