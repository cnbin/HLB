<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HuiBaoM.aspx.cs" Inherits="HuiBaoM" %>
<html>
	<head>
		<title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
  <LINK href="../Style/Style.css" type="text/css" rel="STYLESHEET">
     <script src="../JS/jquery-1.4.1.js" type="text/javascript"></script>
        <link href="../Style/DT.css" rel="stylesheet" type="text/css" />
   <script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=f1d3d82f283f2be724897552202a730c"></script>
         <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
         <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../JS/NewSearch.js"></script>
</head>

<SCRIPT LANGUAGE="JavaScript">
		  		  var a;    
          function CheckValuePiece(){
           if(window.document.form1.GoPage.value == "")
            {
              alert("请输入跳转的页码！");
              window.document.form1.GoPage.focus();
              return false; 
            }
           return true;
           }
           function CheckAll(){            
            if(a==1)
            {
            for(var i=0;i<window.document.form1.elements.length;i++)
               {                
                  var e = form1.elements[i];
                  e.checked =false;                  
               }
               a=0;
           }       
           else
           {
                for(var i=0;i<window.document.form1.elements.length;i++)
               {                
                  var e = form1.elements[i];
                  e.checked =true;                  
               }
               a=1;
           }     
         }
           function CheckDel(){
             var number=0;
             for(var i=0;i<window.document.form1.elements.length;i++)
               {
                  var e = form1.elements[i];
                  if (e.Name != "CheckBoxAll")
                  {
                    if(e.checked==true)
                    {
                        number=number+1;
                    }
                  }
               }
               if(number==0)
                {
                  alert("请选择需要删除的项！");
                  return false;
                }
               if (window.confirm("你确认删除吗？"))
				{
				  return true;
				}
				else
				{
				  return false;
				}
             } 
             
             function CheckModify(){
             var Modifynumber=0;
             for(var i=0;i<window.document.form1.elements.length;i++)
               {
                  var e = form1.elements[i];
                  if (e.Name != "CheckBoxAll")
                  {
                    if(e.checked==true)
                    {
                        Modifynumber=Modifynumber+1;
                    }
                  }
               }
               if(Modifynumber==0)
                {
                  alert("请至少选择一项！");
                  return false;
                }
                if(Modifynumber>1)
                {
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
                 addMarker(113.5972304363, 24.8104188868, 'aaa', 'bbb');
                 getWarningList();

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
             var lastx = '113.595832';
             var lasty = '24.808443';
             //初始化预警标注信息
             function iniWarning(mes) {
                 mapObj.clearMap();
                 //alert(mes[0].title + mes);
                 for (i = 0; i < mes.length; i++) {//遍历json对象的每个key/value对,p为key
                     
                     var p = mes[i];
                     
                     if (p.BZ1 == "" || p.BZ1 == "null" || p.BZ1==null)
                         continue;
                   
                     //alert("for p" + p);
                     //alert("for" + p.title);
                     (function () {
                         if (p.BZ1 != null && p.BZ2 != null && p.BZ1 != '0.0' && p.BZ2 != '0.0') {
                             lastx = p.BZ1;
                             lasty = p.BZ2;
                             mapObj.setZoomAndCenter(14, new AMap.LngLat(p.BZ1, p.BZ2));
                             geocoder(p);
                         } else {
                             mapObj.setZoomAndCenter(14, new AMap.LngLat(lastx, lasty));
                             geocoder(p);
                         }
                         //addMarker(p.BZ1, p.BZ2, p.ID, "<div style='line-height:1.8em;font-size:12px;'><b>" + p.TitleStr + "</b><br>类别：" + p.DDWL + "<br>时间：" + p.TimeStr + "<br>现象描述：" + p.ContentStr + "<br>护林员：" + p.UserName + "<br>", p.TitleStr);
                     })();
                 }
              
             }
             
             //添加预警marker标记
             function addMarker(l, s, id, content, title) {
                 var marker = new AMap.Marker({ map: mapObj, position: new AMap.LngLat(l, s),
                     //位置   
                     icon: "../images/hximg/yj.png" //预警图标                    
                 });
                
                 AMap.event.addListener(marker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体                   
                     //实例化信息窗体 
                     
                     var infoWindow = new AMap.InfoWindow({
                         isCustom: true,  //使用自定义窗体
                         content: createInfoWindow(title, content),
                         offset: new AMap.Pixel(16, -45)//-113, -140
                     });
                     infoWindow.open(mapObj, marker.getPosition());
                    
                     //window.open('HuiBaoView.aspx?ID=' + id, '预警信息', 'height=200, width=400, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no'); 

                     //marker.setLabel({//label的父div默认蓝框白底右下角显示，样式className为：amap-marker-label
                     //    offset: new AMap.Pixel(20, 20),//修改父div相对于maker的位置
                     //    content: content + '地点：' + poi + '</div>'
                     //});
                 });

                 
                 // setElerailing(l, s);
             }
             //获取预警信息
             function getWarningList() {
                 //uname = '13800138000';

                 $.ajax({
                     type: "Post",
                     //url: "../WorkPlan/HuiBaoM.aspx",
                     url: "../WorkPlan/HuiBaoM.aspx/GetWarning",
                     // data: "{'token':'ajax'}",// 使用这种方式竟然无法传递参数，各位有知道原因的告诉一下啊。
                     //  data: "token=ajax&username=" + uname,.
                     dataType: "json",//类型
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         var dataJoson;
                         var x = 113.5972304363;
                         var y = 24.8104188868;
                        
                         eval("dataJoson=" + data.d);
                        // alert(dataJoson.length);
                        // var data = eval('(' + data + ')');
                       
                        // alert(dataJoson[0]);
                        //  alert(data);
                         //ss= "120.1629,33.345077,120.157527,33.341529,120.157504,33.341456"
                         //  alert(data);
                         data = [{ "title": "砍伐", "x": x + "", "y": y + "", "TimeStr": "2015-08-08 01:25:51" ,"id":"14"},
                         { "title": "有火", "x": x - 0.03 + "", "y": y + 0.02 + "", "TimeStr": "2015-08-08 01:25:51", "id": "15" }
                         ];
                         //alert(dataJoson[0].TitleStr + dataJoson[0].BZ1);
                         iniWarning(dataJoson);
                     }
                 });
                             
             }
             function geocoder(objp) {
                 var MGeocoder;
                 //加载地理编码插件
                 AMap.service(["AMap.Geocoder"], function () {
                     MGeocoder = new AMap.Geocoder({
                         radius: 1000,
                         extensions: "all"
                     });
                     //逆地理编码
                     MGeocoder.getAddress(new AMap.LngLat(objp.BZ1, objp.BZ2), function (status, result) {
                         if (status === 'complete' && result.info === 'OK') {
                             geocoder_CallBack(result, objp);
                         }
                     });
                 });

             }
             //回调函数
             function geocoder_CallBack(data, p) {
                 var resultStr = "";
                 var poiinfo = "";
                 var address;
                 //返回地址描述
                   address = data.regeocode.formattedAddress;
                // address = 'dizhi';
                 addMarker(p.BZ1, p.BZ2, p.ID, "<div style='line-height:1.8em;font-size:12px;'><b>" + p.TitleStr + "</b><br>类别：" + p.DDWL + "<br>时间：" + p.TimeStr + "<br>现象描述：" + p.ContentStr + "<br>护林员：" + p.UserName + "<br>地点：" + address + "</br></div>", p.TitleStr);
             }
             //构建自定义信息窗体 
             function createInfoWindow(title, content, cuname) {
                 var info = document.createElement("div"); info.className = "info";
                 //可以通过下面的方式修改自定义窗体的宽高  
                 //info.style.width = "400px";      
                 // 定义顶部标题   
                 var top = document.createElement("div"); top.className = "info-top"; var titleD = document.createElement("div"); titleD.innerHTML = title; var closeX = document.createElement("img"); closeX.src = "http://webapi.amap.com/images/close2.gif"; closeX.onclick = closeInfoWindow; top.appendChild(titleD); top.appendChild(closeX); info.appendChild(top);
                 // 定义中部内容     
                 var middle = document.createElement("div"); middle.className = "info-middle"; middle.style.backgroundColor = 'white'; middle.innerHTML = content; info.appendChild(middle);
                 // 定义底部内容
                 var bottom = document.createElement("div"); bottom.className = "info-bottom"; bottom.style.position = 'relative'; bottom.style.top = '0px'; bottom.style.margin = '0 auto'; var sharp = document.createElement("img"); sharp.src = "http://webapi.amap.com/images/sharp.png"; bottom.appendChild(sharp); info.appendChild(bottom); return info;
             }
             //关闭信息窗体
             function closeInfoWindow() {
                 mapObj.clearInfoWindow();
             }
		</SCRIPT>  
<body onload="initializeMap();">
    <form id="form1" runat="server">
    <div>    
     <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">            
          <tr>
                <td valign="middle"  style="height:30px;">
                        <div class="hlb-contact">
                            <div class="hlb-navmune" style="width:100%;" >
                                <ul class="hlb-navmunebtn">
                                    <a href="javascript:void(0);" onclick="hidemenu()"><img src="../Content/Newicons/hlb-01.png"  /></a>
                                </ul>
                                <ul class="hlb-navmunehome">
                                    <img src="../Content/Newicons/hlb-02.png" />
                                </ul>
                                <ul class="hlb-navmunelink">
                                    当前位置：网站首页&nbsp;-&nbsp;预警信息&nbsp;-&nbsp;我的报告&nbsp;
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
                	<li>报告主题：</li><li><asp:TextBox ID="TextBox1" runat="server" CssClass="hlb-text"></asp:TextBox>
                        <input type="hidden" value="" id="tb1_value" runat="server" />
                	              </li>
                    <li><asp:Button runat="server" id="btn_Search"  Cssclass="hlb-iframebtn5s"  Text="搜索" OnClick="btn_Search_Click" ></asp:Button></li>
                </ul>
            </div>
                  <input type="hidden"  id="hidden_input" value="none" runat="server" />
            <div class="hlb_selectbox_btn" onclick="HideSearch()"><img src="../Content/Newicons/hlb-03.png" /></div>
                 </div>
        </div>
             </td>
         </tr>
        </table>
    </div>
          <div class="hlb-title">
            	<h1>我的报告</h1>
              <ul>
               <li><asp:Button  id="btn_Add" runat="server" Cssclass="hlb-iframebtn5"  Text="添加" OnClick="btn_Add_Click"></asp:Button></li>
              <li><asp:Button  id="btn_Change" runat="server" Cssclass="hlb-iframebtn5"  Text="修改" OnClick="btn_Change_Click" OnClientClick="javascript:return CheckModify();" ></asp:Button></li>
                  <li><asp:Button  id="btn_Del" runat="server" Cssclass="hlb-iframebtn5"  Text="删除"  OnClientClick="javascript:return CheckDel();" OnClick="btn_Del_Click"></asp:Button></li>
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
            <td >
                <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                     OnRowDataBound="GVData_RowDataBound" PageSize="15"
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
                        <asp:TemplateField HeaderText="报告主题">
                            <ItemTemplate>                                
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                                    NavigateUrl='<%# "HuiBaoView.aspx?ID="+ DataBinder.Eval(Container.DataItem, "ID")%>'><%# DataBinder.Eval(Container.DataItem, "TitleStr")%></asp:HyperLink>
                            </ItemTemplate>                            
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>                       
                         <asp:BoundField DataField="SSBM" HeaderText="所属部门" >
                        </asp:BoundField>  
                         <asp:BoundField DataField="TrueName" HeaderText="姓名" >
                        </asp:BoundField>       
                          <asp:BoundField DataField="DDWL" HeaderText="预警类型" >
                        </asp:BoundField>    
                        <asp:BoundField DataField="TimeStr" HeaderText="发送时间" >
                        </asp:BoundField>
                          <asp:BoundField DataField="ZT" HeaderText="状态" >
                        </asp:BoundField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" Height="25px" />
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                         <tr>
                                    <th scope="col"  >报告主题</th>
                            <th scope="col"  >所属部门</th>   
                            <th scope="col"  >姓名</th>
                               <th scope="col"  >预警类型</th>
                              <th scope="col"  >发送时间</th>
                              <th scope="col" >状态</th>
                                </tr>
                        <tr>
                            <td  colspan="4" align="center" style="border-right: black 1px; border-top: black 1px;
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
                            <li>总共<asp:Label ID="LabPageSum" runat="server" Text="Label"></asp:Label>页/
                                当前第<asp:Label ID="LabCurrentPage" runat="server" Text="Label"></asp:Label>页</li>

                            <li>每页<asp:TextBox ID="TxtPageSize" runat="server"  CssClass="hlb-pagesizenum">15</asp:TextBox> 行 &nbsp;
                            </li>
                              <li> 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="hlb-pagesizenum" >1</asp:TextBox>页&nbsp;</li>
                            <li><asp:Button ID="ButtonGo" runat="server" CssClass="hlb-pagesizebtn" OnClick="ButtonGo_Click" OnClientClick="javascript:return CheckValuePiece();" Text="GO"></asp:Button></li>
                        </ul>
                  </div>

            </td>
        </tr>
        </table>
                  </div>  </div>
         
        <table style="width: 100%">
            <tr>
           
            <td >
             <div id="container" style=" height:600px;">        
             </div>
            </td>
             </tr>
            </table>
    </form>
</body>
</html>