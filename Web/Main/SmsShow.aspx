<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmsShow.aspx.cs" Inherits="Main_SmsShow" %>

<html>
<head>
    <title>���ʼ�����</title>
    <base target="_self" />
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td>
                        <table class="TableHeader" cellspacing="0" cellpadding="3" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <img alt="���˶���" src="../images/comm.gif" align="absMiddle" width="16">
                                        ���˶���<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
                                            width="0" height="0"><param name="movie" value="../Flash/Sms.swf"><param name="quality"
                                                value="high"><param name="wmode" value="transparent"><embed src="../Flash/Sms.swf"
                                                    width="0" height="0" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                                    type="application/x-shockwave-flash" wmode="transparent"></embed></object>
                                    </td>
                                    <td class="small" align="right">
                                        �� &nbsp;<span class="big4"><a target="rform" onclick='window.close();' href="../LanEmail/LanEmailShou.aspx"><asp:Label
                                            ID="Label1" runat="server" Text="0" Font-Bold="True" ForeColor="Red"></asp:Label></a>
                                        </span>�����ʼ�
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr class="TableData">
                    <td>
                        <table class="small" cellspacing="0" cellpadding="5" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td style="color: #0000ff">
                                        <asp:Label ID="Label2" runat="server" Text="������"></asp:Label>&nbsp;&nbsp;<asp:Label
                                            ID="Label3" runat="server" Text="����ʱ��"></asp:Label>
                                    </td>
                                    <td style="color: #0000ff" align="right">
                                        �����ˣ�<asp:Label ID="Label4" runat="server" Text="������"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top" height="40">
                                    <td colspan="2">
                                        <asp:Label ID="Label5" runat="server" Text="����"></asp:Label><hr />
                                        <asp:Label ID="Label6" runat="server" Text="����"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div align="right">
                                            <asp:Label ID="Label7" runat="server" Text="EmailID" Visible="False"></asp:Label>
                                            
                                            &nbsp;
                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">��֪����</asp:LinkButton>&nbsp;&nbsp;
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../LanEmail/LanEmailShou.aspx"
                                                onclick='window.close();' Target="rform">�鿴����</asp:HyperLink>&nbsp;&nbsp;
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">ɾ��</asp:LinkButton>&nbsp;&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
