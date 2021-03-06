﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XueXiXinDeAdd.aspx.cs" Inherits="DocFile_XueXiXinDeAdd" %>

<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="index,follow" name="robots" />
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta content="minimum-scale=1.0, width=device-width, maximum-scale=0.6667, user-scalable=no"
        name="viewport" />
    <link href="../Style/Mobile/css/Style.css" rel="stylesheet" media="screen" type="text/css" />
    <link href="../Style/Mobile/css/developer-style.css" rel="stylesheet" type="text/css" />
    <script src="../Style/Mobile/javascript/functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="topbar">
        <div id="title">
            添加信息</div>
        <div id="leftnav">
            <a href="../Main.aspx">
                <img alt="home" src="../Style/Mobile/images/home.png" /></a>
        </div>
    </div>
    <div id="content">
        <fieldset>
            <span class="graytitle">�ĵñ��⣺</span>
            <ul class="pageitem">
                <li class="bigfield">
                
                    <asp:TextBox ID="txtXinDeTitle" runat="server" Width="350px"></asp:TextBox>
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtXinDeTitle"
                        ErrorMessage="*�������Ϊ��"></asp:RequiredFieldValidator>
                </li>
            </ul>
            <span class="graytitle">�ĵ����ݣ�</span>
            <ul class="pageitem">
                <li class="textbox">
                 
                    <asp:TextBox ID="txtXinDeContent" runat="server" Width="100%" Rows="10" TextMode="MultiLine"></asp:TextBox>
                     </li>
            </ul>
            <span class="graytitle">������</span>
            <ul class="pageitem">
                <li class="checkbox" style="height: 100px;">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </li>
                <li class="button">
                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" Width="80"
                        Height="20" ImageAlign="AbsMiddle" ImageUrl="../../images/Button/DelFile.jpg"
                        OnClick="ImageButton3_Click" />
                </li>
            </ul>
            <span class="graytitle">�ϴ�������</span>
            <ul class="pageitem">
                <li class="textbox">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" />
                </li>
                <li class="button">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageAlign="AbsMiddle" ImageUrl="../../images/Button/UpLoad.jpg"
                        OnClick="ImageButton2_Click" Width="60" Height="20" CausesValidation="False" />
                </li>
            </ul>
            <ul class="pageitem">
                <li class="button" style="text-align: center;">
                    <asp:Button ID="ImageButton1" runat="server" Text="����" OnClick="ImageButton1_Click"
                        Width="80px" />
                </li>
            </ul>
        </fieldset>
    </div>
    </form>
</body>
</html>
