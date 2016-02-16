<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WZCJ.aspx.cs" Inherits="QDGL_WZCJ" %>
<html>
	<head>
		<title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <script type="text/javascript" language="javascript" src="../JS/DateAndTime.js"></script>
          <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
     <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/NewSearch.js"></script>
        <script type="text/javascript" src="../Controls/My97DatePicker/My97DatePicker/WdatePicker.js"></script>
</head>
<SCRIPT LANGUAGE="JavaScript">
    var a;
    function CheckValuePiece() {
        if (window.document.form1.GoPage.value == "") {
            alert("请输入跳转的页码！");
            window.document.form1.GoPage.focus();
            return false;
        }
        return true;
    }
    function CheckAll() {
        if (a == 1) {
            for (var i = 0; i < window.document.form1.elements.length; i++) {
                var e = form1.elements[i];
                e.checked = false;
            }
            a = 0;
        }
        else {
            for (var i = 0; i < window.document.form1.elements.length; i++) {
                var e = form1.elements[i];
                e.checked = true;
            }
            a = 1;
        }
    }
    function CheckDel() {
        var number = 0;
        for (var i = 0; i < window.document.form1.elements.length; i++) {
            var e = form1.elements[i];
            if (e.Name != "CheckBoxAll") {
                if (e.checked == true) {
                    number = number + 1;
                }
            }
        }
        if (number == 0) {
            alert("请选择需要删除的项！");
            return false;
        }
        if (window.confirm("你确认删除吗？")) {
            return true;
        }
        else {
            return false;
        }
    }

    function CheckModify() {
        var Modifynumber = 0;
        for (var i = 0; i < window.document.form1.elements.length; i++) {
            var e = form1.elements[i];
            if (e.Name != "CheckBoxAll") {
                if (e.checked == true) {
                    Modifynumber = Modifynumber + 1;
                }
            }
        }
        if (Modifynumber == 0) {
            alert("请至少选择一项！");
            return false;
        }
        if (Modifynumber > 1) {
            alert("只允许选择一项！");
            return false;
        }

        return true;
    }
             
             
             
		</SCRIPT>  
<body onload="Load_Select();">
    <form id="form1" runat="server">
    <div>    
     <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">            
           <tr>
                <td valign="middle"  style="height:30px;">
                        <div class="hlb-contact">
                            <div class="hlb-navmune"style="width:100%">
                                <ul class="hlb-navmunebtn">
                                    <a href="javascript:void(0);" onclick="hidemenu()"><img src="../Content/Newicons/hlb-01.png"  /></a>
                                </ul>
                                <ul class="hlb-navmunehome">
                                    <img src="../Content/Newicons/hlb-02.png" />
                                </ul>
                                <ul class="hlb-navmunelink">
                                    当前位置：网站首页&nbsp;-&nbsp;定位管理&nbsp;-&nbsp;位置采集
                                </ul>
                            </div>
                            
                    </td>
            </tr>
         <tr>
             <td>
            <div class="hlb-contact">
            <div class="hlb_selectbox">
        	<!--搜索框-->
            <div id="hlb_select" runat="server" class="hlb_select">
            	<ul>
                	<li>所属部门：</li><li><asp:TextBox ID="TextBox1" runat="server" CssClass="hlb-text" Width="120px"></asp:TextBox>
                        <input type="hidden" runat="server" id="tb1_value" value="" />                	             
                    </li>
                    <li>姓名：</li><li><asp:TextBox ID="TextBox2" runat="server" CssClass="hlb-text" Width="150px"></asp:TextBox>
                        <input type="hidden" runat="server" id="tb2_value" value="" />
                                </li>
                    <li>起始时间：</li><li><asp:TextBox ID="txtYuJiTiXing" runat="server" CssClass="hlb-text" Width="150px" onfocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})"></asp:TextBox>
                         <input type="hidden" runat="server" id="tbyjt_value" value="" />
                                  </li>
                    <li>结束时间：</li><li><asp:TextBox ID="TextBox3" runat="server" CssClass="hlb-text" onfocus="WdatePicker({startDate:'%y-%M-01 00:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss',alwaysUseStartDate:true})" Width="150px"></asp:TextBox>
                         <input type="hidden" runat="server" id="tb3_value" value="" />
                                  </li>
                    <li><asp:Button runat="server" id="btn_Search"  Cssclass="hlb-iframebtn5s"  Text="搜索" OnClick="btn_Search_Click" ></asp:Button></li>
                </ul>
            </div>
                 <input type="hidden"  id="hidden_input" runat="server" value="none"/>
            <div class="hlb_selectbox_btn" onclick="HideSearch()"><img src="../Content/Newicons/hlb-03.png" /></div>
                 </div>
        </div>
             </td>
         </tr>
        </table>
        <div class="hlb-title">
            	<h1>位置采集</h1>
                <ul>
                	 <li><asp:Button runat="server" id="btn_Report" Cssclass="hlb-iframebtn5"  Text="导出" OnClick="btn_Report_Click"></asp:Button></li>
                </ul>
            </div>
          <div class="hlb-listbox">
               <b class="LB"></b><b class="RB"></b>
               <div class="hlb-listboderbox">
                     <b class="L"></b><b class="R"></b>
                	<b class="LB"></b><b class="RB"></b>
        <table style="width: 100%">
            <tr>
            <td ><asp:GridView ID="GVData" runat="server"  AutoGenerateColumns="False"
                     OnRowDataBound="GVData_RowDataBound" PageSize="15" AllowPaging="True" AllowSorting="True"
                    width="100%" border="0" cellspacing="1" cellpadding="1" CssClass="hlbdata-list">
                    <PagerSettings Mode="NumericFirstLast" Visible="False" />
                    <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#F3F3F3" Font-Size="12px" ForeColor="Black" Height="20px" /><AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'
                                    Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                            <HeaderTemplate>
                                <input id="CheckBoxAll" onclick="CheckAll()"  type="checkbox" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                          <asp:BoundField DataField="SSBM" HeaderText="所属部门" >
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="TrueName" HeaderText="姓名" >
                        </asp:BoundField>
                        
                        
                        <asp:BoundField DataField="RQ" HeaderText="定位时间" >
                            <ItemStyle Width="130px" />
                        </asp:BoundField>                    
                         <asp:BoundField DataField="X" HeaderText="X坐标" >
                        </asp:BoundField>   
                         <asp:BoundField DataField="Y" HeaderText="Y坐标" >
                        </asp:BoundField>    
                         <asp:BoundField DataField="BZ1" HeaderText="定位状态" >
                        </asp:BoundField>                  
                    </Columns>
                    <RowStyle HorizontalAlign="Center" Height="25px" />
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <th scope="col">所属部门</th>
                             <th scope="col">姓名</th>
                             <th scope="col" Width="130px">定位时间</th>
                             <th scope="col">X坐标</th>
                             <th scope="col">Y坐标</th>
                             <th scope="col">定位状态</th>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" style="border-right: black 1px; border-top: black 1px;
                                border-left: black 1px; border-bottom: black 1px; background-color: whitesmoke;">
                                该列表中暂时无数据！</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                </asp:GridView>
                </td>
        </tr>
        <tr>
             <td>
             <div class="hlb-pagesize">
                   	  <ul>
                        	<li> <asp:Button ID="BtnFirst"  runat="server" CommandName="First" Text="首页" CssClass="hlb-pagebtn" OnClick="PagerButtonClick" /></li>
                            <li><asp:Button ID="BtnPre"  runat="server" CommandName="Pre" Text="上一页" CssClass="hlb-pagebtn" OnClick="PagerButtonClick" /></li>
                            <li><asp:Button ID="BtnNext"  runat="server" CommandName="Next" Text="下一页" CssClass="hlb-pagebtn" OnClick="PagerButtonClick" /></li>
                            <li><asp:Button ID="BtnLast"  runat="server" CommandName="Last" Text="最后一页" CssClass="hlb-pagebtn" OnClick="PagerButtonClick" /></li>
                            <li>当前第<asp:Label ID="LabCurrentPage" runat="server" Text="Label"></asp:Label>页/
                                总共<asp:Label ID="LabPageSum" runat="server" Text="Label"></asp:Label>页</li>

                            <li>每页<asp:TextBox ID="TxtPageSize" runat="server"  CssClass="hlb-pagesizenum">15</asp:TextBox> 行 &nbsp;
                            </li>
                              <li> 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="hlb-pagesizenum" >1</asp:TextBox>页&nbsp;</li>
                            <li><asp:Button ID="ButtonGo" runat="server" CssClass="hlb-pagesizebtn" OnClick="ButtonGo_Click" OnClientClick="javascript:return CheckValuePiece();" Text="GO"></asp:Button></li>
                        </ul>
                  </div>

            </td>
        </tr>
        </table>
                   </div>
              </div>
        </div>
    </form>
</body>
</html>
