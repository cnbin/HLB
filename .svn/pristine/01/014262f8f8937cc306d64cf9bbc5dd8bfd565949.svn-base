<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileSmsAdd.aspx.cs" Inherits="Mobile_MobileSmsAdd" %>
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
                                    当前位置：<a href="#">网站首页</a>-<a href="#">公告通知</a>-<a href="#">添加新信息</a>
                                </ul>
                            </div>
                             <div class="hlb-navmune-return">
                             <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/>
                             <input type="hidden" value="" id="ReturnInput" runat="server" />     </div>
                        </div>
                    </td>
            </tr>
        </table>
         <div class="hlb-title">
            	<h1>添加新信息</h1>
              
            </div>
          <div class="hlb-listbox">
              <b class="LB"></b><b class="RB"></b>
               <div class="hlb-listboderbox">
                     <b class="R" ></b>
                	<b class="LB"></b><b class="RB"></b>
    <table class="hlbdata-list" style="width:100%">     
          <tr >
            <td colspan="4" style="background-color: #D3E6F4; height:10px; " >

            </td>
            
        </tr>       
        <tr>
            <td align="right" colspan="3" style="height: 25px; background-color: #D3E6F4; text-align: center;font-size: 14px;color: #2A5573;">
                <strong><span style="font-size: 10pt">内部短信群发</span></strong></td>
          <td rowspan="9" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
        </tr>
        
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >
                接收用户：</td>
                <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                   <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox1').value=wName;}"
                    src="../images/Button/search.gif" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="*该项不可以为空" Display="Dynamic" ValidationGroup="Neibu"></asp:RequiredFieldValidator>&nbsp;
                    <span style="color: darkgray">* 请选择用户名，用于内部人员短信</span></td>
              
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573; ">
                信息内容：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox2" runat="server" Width="350px" Height="50px" TextMode="MultiLine" ></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
            </td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:Button  id="btn_Sub_In" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_In_Click" ></asp:Button>

            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 5px; height: 25px; background-color: #ffffff">
            </td>
        </tr><tr>
            <td align="right" style="height: 25px; background-color: #D3E6F4; text-align: center; font-size: 14px;color: #2A5573;" colspan="2">
                <strong><span style="font-size: 10pt">外部短信群发</span></strong></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;" >
                接收用户：</td>
            <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                <asp:TextBox ID="TextBox3" runat="server" Height="90px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectTXL.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox3').value=wName;}"
                    src="../images/Button/search.gif" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3"
                    Display="Dynamic" ErrorMessage="*该项不可以为空" ValidationGroup="WaiBu"></asp:RequiredFieldValidator>&nbsp;
                <span style="color: darkgray">* 请输入手机号码列表，用 "," 分隔。</span></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                信息内容：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox4" runat="server" Height="50px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
            </td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
               <asp:Button  id="btn_Sub_Out" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_Out_Click" ></asp:Button>

            </td>
        </tr>
        </table></div></div></div>
    </form>
</body>
</html>