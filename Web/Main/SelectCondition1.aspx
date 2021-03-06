﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCondition1.aspx.cs" Inherits="SelectCondition1" %>

<HTML>
	<HEAD>
		<title>选择条件</title>
		<meta http-equiv="Content-Language" content="zh-cn">
		<LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target=_self />	
		<script  language="javascript">			
		    var  getFromParent=window.dialogArguments;  
		    function CheckSelect()
		    {
		    var aaaa="";
             for(var i=0;i<window.document.Form1.elements.length;i++)
               {
                  var e = Form1.elements[i];
                  if (e.checked)
                  {
                        if(aaaa=="")
                        {
                            aaaa=e.value;
                        }
                        else                        
                        {
                            aaaa=aaaa+","+e.value;
                        }
                  }                   
               }
               return aaaa;
            }
                       
           function  sendFromChild()  
            {       
                window.returnValue=CheckSelect();  
                window.close();  
            } 
            
            function  CCC()  
            {       
                window.returnValue="";  
                window.close();  
            } 
        </script> 	
</HEAD>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<table border="0" width="100%" cellspacing="0" cellpadding="0" height="100%" bordercolorlight="#c0c0c0" bordercolordark="#ffffff">
				<tr>
					<td height="22" background="../images/show_02.gif" align="left" style="font-size: 12px; font-family: 宋体"> 　请选择您需要的项，然后点“确定”！</td>
				</tr>
				<tr>
			    <td valign="top" style="text-align: center">				  查询：<asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="100px"></asp:TextBox><asp:ImageButton
                        ID="ImageButton4" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnSerch.jpg"
                        OnClick="ImageButton4_Click" /><br />
                    <table  border="0" cellspacing="0" cellpadding="0" style="width: 318px; height: 49px">
                    <tr>
                        <td colspan="2" style="height: 31px; text-align: center;">
                        
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" BorderWidth="1px" Width="100%" ShowHeader="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10">
                                <PagerSettings FirstPageText="首页" LastPageText="最后一页" NextPageText="下一页" PreviousPageText="上一页" />
                                <FooterStyle BackColor="#F2F5FA" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <input id="Checkbox1" value='<%#DataBinder.Eval(Container.DataItem, "SelectContent")%>' type="checkbox" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SelectContent")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="20px" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    &nbsp;&nbsp;你所查看的选项无可用信息！
                                </EmptyDataTemplate>
                                <PagerStyle BackColor="White" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="12px"
                                    Font-Strikeout="False" Font-Underline="False" ForeColor="HotTrack" Height="30px" />
                            </asp:GridView>
                        </td>
                    </tr>
                      <tr>
                          <td colspan="2" style="height: 31px; text-align: center;">
                              &nbsp;<INPUT  TYPE="button"  VALUE="确定"  onclick="sendFromChild();" style="width: 70px" class="BottonCss"></td>
                      </tr>                        
                    </table></td>
				</tr>
				<tr>
					<td height="22" background="../images/show_02.gif">
					</td>
				</tr>
			</table>
		</form>
	</body>			
</HTML>