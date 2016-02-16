<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeListAdd.aspx.cs" Inherits="SystemManage_TreeListAdd" %>

<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>

    <script language="javascript">
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
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
                                    当前位置：网站首页&nbsp;-&nbsp;系统管理&nbsp;-&nbsp;添加信息
                                </ul>
                            </div>
                             <div class="hlb-navmune-return">
                                 <input type="button" onclick="ReturnClick()"  class="hlb-iframebtn5_1s" value="返回"/>
                                 <input type="hidden" value="" id="ReturnInput" runat="server" />
                            </div>
                        </div>
                    </td>
            </tr>
           
        </table>
         <div class="hlb-title">
            	<h1>添加信息</h1>
                <ul>
                	<li><asp:Button  id="btn_Sub" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_Click"></asp:Button></li>
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
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtTextStr" runat="server" Width="350px"></asp:TextBox>
                    <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPTreeList&LieName=TextStr&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('txtTextStr').value=wName;}"
                        src="../images/Button/search.gif" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTextStr"
                        ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator>
                </td>
                   <td rowspan="9" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
            </tr>
            <tr>
               <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    所用图片：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtImageUrlStr" runat="server" Width="350px"></asp:TextBox>
                    <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPTreeList&LieName=ImageUrlStr&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('txtImageUrlStr').value=wName;}"
                        src="../images/Button/search.gif" />
                </td>
            </tr>
            <tr>
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    主菜单样式：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:DropDownList runat="server" ID="SelClass" Width="150px">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="system_icon" Text="system_icon"></asp:ListItem>
                        <asp:ListItem Value="workflow_icon" Text="workflow_icon"></asp:ListItem>
                        <asp:ListItem Value="kaoqing_icon" Text="kaoqing_icon"></asp:ListItem>
                        <asp:ListItem Value="jianzheng_icon" Text="jianzheng_icon"></asp:ListItem>
                        <asp:ListItem Value="renshi_icon" Text="renshi_icon"></asp:ListItem>
                        <asp:ListItem Value="peixun_icon" Text="peixun_icon"></asp:ListItem>
                        <asp:ListItem Value="tongji_icon" Text="tongji_icon"></asp:ListItem>
                        <asp:ListItem Value="gongcheng_icon" Text="gongcheng_icon"></asp:ListItem>
                        <asp:ListItem Value="gongzuo_icon" Text="gongzuo_icon"></asp:ListItem>
                        <asp:ListItem Value="bangong_icon" Text="bangong_icon"></asp:ListItem>
                        <asp:ListItem Value="jingyin_icon" Text="jingyin_icon"></asp:ListItem>
                        <asp:ListItem Value="kaohe_icon" Text="kaohe_icon"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    后台数值：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtValueStr" runat="server" Width="350px"></asp:TextBox>
                    当前最后一项为：<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>&nbsp;
                    子节点为数值，父节点可设置为和显示文字相同。
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    链接地址：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtNavigateUrlStr" runat="server" Width="350px"></asp:TextBox>
                    <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPTreeList&LieName=NavigateUrlStr&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('txtNavigateUrlStr').value=wName;}"
                        src="../images/Button/search.gif" />
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    目标框架：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtTarget" runat="server" Width="350px"></asp:TextBox>
                    <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPTreeList&LieName=Target&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('txtTarget').value=wName;}"
                        src="../images/Button/search.gif" />
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    父节点：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtParentID" runat="server" Width="350px"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtParentID"
                        Display="Dynamic" ErrorMessage="*该项必须输入数字" MaximumValue="10000" MinimumValue="0"
                        Type="Integer"></asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server" ControlToValidate="txtParentID" ErrorMessage="*该项不可为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                 <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    权限：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtQuanXianList" runat="server" Width="350px">A_添加|M_修改|D_删除|E_导出</asp:TextBox>
                    <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPTreeList&LieName=QuanXianList&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('txtQuanXianList').value=wName;}"
                        src="../images/Button/search.gif" />
                </td>
            </tr>
            <tr>
                  <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;" >

                    排序：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="txtPaiXuStr" runat="server" Width="350px"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPaiXuStr"
                        Display="Dynamic" ErrorMessage="*该项必须输入数字" MaximumValue="10000" MinimumValue="0"
                        Type="Integer"></asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            runat="server" ControlToValidate="txtPaiXuStr" ErrorMessage="*该项不可为空"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
</div></div>
    </div>
    </form>
</body>
</html>
