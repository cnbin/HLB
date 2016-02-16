<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GongGaoAdd.aspx.cs" Inherits="GongGao_GongGaoAdd" %>

<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <script src="../UEditor/editor_config.js" type="text/javascript"></script>
    <script src="../UEditor/editor_all.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../UEditor/themes/default/ueditor.css" />
      <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
         <script type="text/javascript" src="../JS/NewSearch.js"></script>
    <script type="text/javascript" >
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }

        function SelPerson()
        {
            var val = "";
            var returnVal = new Array();
            var ListTreeView=document.getElementById('ListTreeView');
            var inputs = ListTreeView.getElementsByTagName("Input");
            var n = 0;
            for (var i = 0; i < inputs.length; i++) // 遍历页面上所有的 input
            {
                var e = inputs[i];

                if (e.checked) {
                    var strValue = e.title;
                    if (strValue != null) {
                        val += val == "" ? strValue : ',' + strValue;
                        n = n + 1;
                    }
                }
            }
            if (n==0)
            {
                alert('请至少选择一位发送人！');
                return;
            }
            
            document.getElementById('Send_Person').value = val;
            
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
                                    当前位置：网站首页&nbsp;-&nbsp;个人办公&nbsp;-&nbsp;添加公告通知
                                </ul>
                            </div>
                             <div class="hlb-navmune-return">
                              <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />    </div>
                        </div>
                    </td>
            </tr>
        </table>
         <div class="hlb-title">
            	<h1>添加公告通知</h1>
                <ul>
                	<li><asp:Button  id="btn_Sub" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_Click" OnClientClick="SelPerson();"></asp:Button></li>
                </ul>
            </div>

        <table  style="width:100%">
            <tr>
                <td style="width:15%;vertical-align:top;"> 
                     <asp:TreeView ID="ListTreeView" runat="server" onclick="OnTreeNodeChecked();" ShowCheckBoxes="All"
                        ShowLines="True" ExpandDepth="1" Font-Bold="False">
                    </asp:TreeView>
                    <div style="margin:5px 0px;">
                        <input type="hidden" runat="server" id="Send_Person" value="" />
                 </div>  

                </td>
                <td style="vertical-align:top;">
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
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    信息主题：
                </td>
                <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator>
                </td>
                 <td rowspan="8" style="background-color: #D3E6F4;  width:14px; padding:0px;"></td>
            </tr>
            <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    文件类型：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>通知</asp:ListItem>
                        <asp:ListItem>公告</asp:ListItem>
                      
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    紧急程度：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:DropDownList ID="DropDownList2" runat="server">
                         <asp:ListItem>一般</asp:ListItem>
                        <asp:ListItem>紧急</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    发送状态：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:DropDownList ID="ZT" runat="server">
                        <asp:ListItem Value="已发送" Text="已发送"></asp:ListItem>
                        <asp:ListItem Value="未发送" Text="未发送"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    电子围栏名称：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TextBox3"  runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    附件：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                    &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                        ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton3_Click" />
                    &nbsp; &nbsp;
                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/ReadFile.gif" OnClick="ImageButton4_Click" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/EditFile.gif" OnClick="ImageButton5_Click" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    上传附件：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="../images/Button/UpLoad.jpg" OnClick="ImageButton2_Click" />
                </td>
            </tr>
            <%--<tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4 ;font-size: 14px;color: #2A5573;">
                    发送人员：
                </td>
                <td style="padding-left: 5px; background-color: #ffffff">
                    <asp:TextBox ID="TextBox2" runat="server" Width="350px"></asp:TextBox>
                    <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox2').value=wName;}"
                        src="../images/Button/search.gif" />
                </td>
            </tr>--%>
            <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;font-size: 14px;color: #2A5573;">
                    详细内容：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TxtContent" runat="server" Width="100%" Rows="10" TextMode="MultiLine"></asp:TextBox>
                    <script type="text/javascript">
                        var editor = new baidu.editor.ui.Editor({ id: 'editor', minFrameHeight: 300 }); editor.render("TxtContent");
                    </script>
                </td>
            </tr>
            
        </table>
    </div></div></td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
