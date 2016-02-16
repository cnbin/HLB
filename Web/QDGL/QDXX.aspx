<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QDXX.aspx.cs" Inherits="QDGL_QDXX" %>

<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=f1d3d82f283f2be724897552202a730c"></script>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
     <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../JS/NewSearch.js"></script>
</head>
<script language="JavaScript">
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
    function initializeMap() {
        // alert("initialize");,
        var position = new AMap.LngLat(113.5972304363, 24.8104188868);
        mapObj = new AMap.Map("container", {
            view: new AMap.View2D({//创建地图二维视口
                center: position, //创建中心点坐标
                zoom: 14, //设置地图缩放级别
                rotation: 0 //设置地图旋转角度
            }),
            lang: "zh_cn"//设置地图语言类型，默认：中文简体
        }); //创建地图实例

        //AMap.event.addListener(mapObj, 'click', getLnglat);
        //addMarker(113.5972304363, 24.8104188868, 'aaa', 'bbb');
        //getWarningList();
        var hidden_input = document.getElementById('hidden_input');

        if (hidden_input.value == 'block') {
            document.getElementById('hlb_select').style.display = 'block';
            hidden_input.value = 'block';
        }
        else if (hidden_input.value == 'none') {
            document.getElementById('hlb_select').style.display = 'none';
            hidden_input.value = 'none';
        }


    }
    //添加预警marker标记
    function addMarker(l, s, id, content, title) {
        var marker = new AMap.Marker({
            map: mapObj, position: new AMap.LngLat(l, s),
            //位置   
            icon: "../images/hximg/yj.png" //预警图标                    
        });
        //AMap.event.addListener(marker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
        //实例化信息窗体 
        /**
        var infoWindow = new AMap.InfoWindow({ isCustom: true,  //使用自定义窗体
            content: createInfoWindow(title, content), offset: new AMap.Pixel(16, -45)//-113, -140
        });
        infoWindow.open(mapObj, marker.getPosition());
       */
        // window.open('HuiBaoView.aspx?ID=' + id, '预警信息', 'height=200, width=400, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
        //  });

        marker.setLabel({//label的父div默认蓝框白底右下角显示，样式className为：amap-marker-label
            offset: new AMap.Pixel(20, 20),//修改父div相对于maker的位置
            content: content
        });
        // setElerailing(l, s);
    }

    function showQD(l, s, id, time, title, xm) {
        //alert(l + "," + s + id + title + time);
        //alert(xm);
        mapObj.clearMap();

        addMarker(l, s, id, "<div style='line-height:1.8em;font-size:12px;'><b>姓名：" + xm + "</b><br>时间:" + time + "<br>地址:" + title + "</br></div>", title);
        //addMarker(113.5972304363, 24.8104188868, 'aaa', 'bbb');
        mapObj.setZoomAndCenter(14, new AMap.LngLat(l, s));

    }

</script>
<body onload="initializeMap();">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="middle"  style="height:30px;">
                        <div class="hlb-contact">
                            <div class="hlb-navmune" style="width:100%">
                                <ul class="hlb-navmunebtn">
                                    <a href="javascript:void(0);" onclick="hidemenu()"><img src="../Content/Newicons/hlb-01.png"  /></a>
                                </ul>
                                <ul class="hlb-navmunehome">
                                    <img src="../Content/Newicons/hlb-02.png" />
                                </ul>
                                <ul class="hlb-navmunelink">
                                    当前位置：网站首页&nbsp;-&nbsp;签到管理&nbsp;-&nbsp;签到详细表
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
                    <li>登录名：</li><li><asp:TextBox ID="TextBox1" runat="server" CssClass="hlb-text"></asp:TextBox>
                        <input type="hidden" runat="server" id="tb1_value" value="" />
                   </li>
                	<li>真实姓名：</li><li><asp:TextBox ID="TextBox2" runat="server" CssClass="hlb-text"></asp:TextBox>
                        <input type="hidden" runat="server" id="tb2_value" value="" />
                   </li>
                    <li>类型：<asp:DropDownList ID="Dr_Type" runat="server" Width="110px">
                        <asp:ListItem Selected="True" Value="全部">全部</asp:ListItem>
                        <asp:ListItem Value="上班">上班</asp:ListItem>
                        <asp:ListItem Value="签到">签到</asp:ListItem>
                        <asp:ListItem Value="下班">下班</asp:ListItem>
                        </asp:DropDownList></li>
                    
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
    </div>
          <div class="hlb-title">
            	<h1>签到详细表</h1>
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
            <td>
                <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                     OnRowDataBound="GVData_RowDataBound" PageSize="15"
                   width="100%" border="0" cellspacing="1" cellpadding="1" CssClass="hlbdata-list">
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'
                                    Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                            <HeaderTemplate>
                                <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                            </HeaderTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="SSBM" HeaderText="所属部门">
                             <ItemStyle Width="80px" />
                        </asp:BoundField>                    
                        <asp:TemplateField HeaderText="登录名（真实姓名）">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                                    NavigateUrl='<%#"javascript:showQD(\""+ DataBinder.Eval(Container.DataItem, "X")+"\",\""+DataBinder.Eval(Container.DataItem, "Y")+
                                "\",\""+DataBinder.Eval(Container.DataItem, "ID")+"\",\""+DataBinder.Eval(Container.DataItem, "QDSJ")+"\",\""+DataBinder.Eval(Container.DataItem, "BZ2")+"\",\""+DataBinder.Eval(Container.DataItem, "XM")+"\")"%>'><%# DataBinder.Eval(Container.DataItem, "XM")%></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RQ" HeaderText="日期">
                            <ItemStyle Width="88px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POI" HeaderText="POI点">
                             <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QDSJ" HeaderText="时间">
                            <ItemStyle Width="88px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BZ1" HeaderText="类型">
                             <ItemStyle Width="70px" />
                        </asp:BoundField>
                          <asp:BoundField DataField="BZ2" HeaderText="签到地点">
                              <ItemStyle Width="218px" />
                          </asp:BoundField>
                           <asp:BoundField DataField="X" HeaderText="x">
                             <ItemStyle Width="95px" />
                           </asp:BoundField>
                            <asp:BoundField DataField="Y" HeaderText="y">
                                  <ItemStyle Width="95px" />
                            </asp:BoundField>
                        <%--<asp:HyperLinkField DataNavigateUrlFields="BZ3" 
                     DataNavigateUrlFormatString="../UploadFile/{0}" 
                     DataTextField="BZ3" HeaderText="照片" />--%>
                   <asp:TemplateField HeaderText="照片">
                        <ItemTemplate>
                            <%#Common.GetLink(Convert.ToString(Eval("BZ3")))%>
                        </ItemTemplate>
                          <ItemStyle Width="160px" />
                    </asp:TemplateField>
                    </Columns>
                    <PagerSettings Visible="False" />
                    <RowStyle HorizontalAlign="Center" Height="25px" />
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                             <th scope="col" >所属部门</th>
                             <th scope="col"  >姓名</th>   
                             <th scope="col"  >日期</th>
                             <th scope="col"  >POI点</th>
                             <th scope="col" >时间</th>
                             <th scope="col" >类型</th>
                             <th scope="col" >签到地点</th>
                             <th scope="col" >X</th>
                             <th scope="col" >Y</th>
                             <th scope="col" >照片</th>
                                </tr>
                            <tr>
                                <td colspan="10" align="center" style="border-right: black 1px; border-top: black 1px; border-left: black 1px;
                                    border-bottom: black 1px; background-color: whitesmoke;">
                                    该列表中暂时无数据！
                                </td>
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
        </Table>
                     </div> </div>
         <Table style="width:100%">
        <tr>
             <td >
             <div id="container" style=" height:400px;">        
             </div>
            </td>
        </tr>
    </Table>
                 
    </form>
</body>
</html>
