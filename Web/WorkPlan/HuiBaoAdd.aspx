<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HuiBaoAdd.aspx.cs" Inherits="WorkPlan_MyWorkPlanAdd" %>

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
                                    当前位置：网站首页&nbsp;-&nbsp;预警信息&nbsp;-&nbsp;添加工作报告
                                </ul>
                            </div>
                            <div class="hlb-navmune-return">
                               <input type="button" onclick="ReturnClick();"  class="hlb-iframebtn5_1s" value="返回"/><input type="hidden" value="" id="ReturnInput" runat="server" />    </div>
                        </div>
                    </td>
            </tr>
        </table>
        <div class="hlb-title">
            	<h1>添加工作报告</h1>
             <ul> <li><asp:Button  id="btn_Sub" runat="server" Cssclass="hlb-iframebtn5"  Text="提交" OnClick="btn_Sub_Click"></asp:Button></li></ul>
               </div>
          <div class="hlb-listbox">
              <b class="LB"></b>
              <b class="RB"></b>
               <div class="hlb-listboderbox">
                     <b class="R" ></b>
                	<b class="LB"></b><b class="RB"></b>
        <table class="hlbdata-list" style="width:100%">
             <tr >
            <td colspan="3" style="background-color: #D3E6F4; height:10px; " >

            </td>
            
        </tr>       
            <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px;font-size: 14px;color: #2A5573;">
                    报告主题：
                </td>
                <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator>
                </td>
                 <td rowspan="6" style="background-color: #D3E6F4;width:14px; padding:0px;"></td>
            </tr>
             <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    电子围栏名称：
                </td>
                <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                    <asp:TextBox ID="TextBox3" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td align="right" style="width: 170px; background-color: #D3E6F4; height: 25px; font-size: 14px;color: #2A5573;">
                    类型：
                </td>
                <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                      <asp:DropDownList ID="ddl_WarningType" runat="server">
                          <asp:ListItem>火情</asp:ListItem>
                          <asp:ListItem>病虫害</asp:ListItem>
                          <asp:ListItem>报告围栏</asp:ListItem>
                          <asp:ListItem>其他</asp:ListItem>
                      </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    转发人员列表：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TextBox2" runat="server" Width="350px" disabled="disabled"></asp:TextBox>
                    <img class="HerCss" onclick="SelPerson()"
                        src="../images/Button/search.gif" />
                         &nbsp; &nbsp;
                    <input id="UserName_Input"  runat="server" type="hidden" value="" />
                   <%-- <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectGroup.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox2').value=wName;}"
                        src="../images/Button/Group.gif" />--%>
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
                    <!--
                    <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/ReadFile.gif" OnClick="ImageButton5_Click" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/EditFile.gif" OnClick="ImageButton6_Click" />-->
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
            <tr>
                <td align="right" style="width: 170px; height: 25px; background-color: #D3E6F4;font-size: 14px;color: #2A5573;">
                    报告内容：
                </td>
                <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                    <asp:TextBox ID="TxtContent" runat="server" Width="100%" Rows="10" TextMode="MultiLine"></asp:TextBox>
                    <script type="text/javascript">
                        var editor = new baidu.editor.ui.Editor({ id: 'editor', minFrameHeight: 300 }); editor.render("TxtContent");
                    </script>
                </td>
            </tr>
        </table>
    </div></div></div>
    </form>
</body>
</html>
