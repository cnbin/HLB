<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <link href="Style/login.css" type="text/css" rel="STYLESHEET" />
</head>
<body onload="javascript:form1.TxtUserName.focus();">
    <form id="form1" runat="server">
    <div class="main">
        <div class="center">
            <div class="logo">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt="" src="images/login/logo.png" />
            </div>
            <div class="content">
                <div class="user">
                    <div class="user_left">
                        用户名：</div>
                    <div class="user_right">
                        <asp:TextBox ID="TxtUserName" runat="server" CssClass="user_textbox"></asp:TextBox></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="user">
                    <div class="user_left">
                        密 码：</div>
                    <div class="user_right">
                        <asp:TextBox ID="TxtUserPwd" runat="server" CssClass="user_textbox" TextMode="Password"></asp:TextBox></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="remember">
                    <%--<div class="rem_user" valign="top">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="记住用户名" /></div>--%>
                    <div class="rem_pass">
                        <asp:CheckBox ID="cbRememberId" runat="server" Text="记住用户名" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="btn_submit">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/login/btn1.png"
                        OnClick="ImageButton1_Click" /></div>
            </div>
            <%--<div class="copyright"><a href='../UploadFile/' style='text-decoration:none;'/>APP下载</div>--%>
            <div class="copyright">
                Copyright &copy; 2015 CHINA POST. All right reserved.</div>
        </div>
    </div>
    </form>
</body>
</html>
