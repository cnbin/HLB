<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineUser.aspx.cs" Inherits="OnlineUser" %>

<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <script src="../JS/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Style/DT.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=f1d3d82f283f2be724897552202a730c""></script>
    <link href="../Style/globle.css" rel="stylesheet" type="text/css" />
    <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/NewSearch.js"></script>
    <style type="text/css">
        body
        {
            margin: 0;
            height: 100%;
            width: 100%;
            position: absolute;
        }
        #mapContainer
        {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
        }
        #tip
        {
            height: 80px;
            background-color: #fff;
            padding-left: 10px;
            padding-right: 10px;
            position: absolute;
            font-size: 12px;
            right: 10px;
            bottom: 20px;
            border-radius: 3px;
            border: 1px solid #ccc;
        }
        
        #tip input[type='button']
        {
            margin-top: 10px;
            margin-bottom: 10px;
            background-color: #0D9BF2;
            height: 30px;
            text-align: center;
            line-height: 30px;
            color: #fff;
            font-size: 12px;
            border-radius: 3px;
            outline: none;
            border: 0;
        }
    </style>
    <script type="text/javascript">
        window.onscroll = function () {//滚动条进行滚动时
            var tipDiv = document.getElementById("tip");
            tipDiv.style.top = document.documentElement.scrollHeight - (document.documentElement.scrollHeight - document.body.scrollTop) + (document.body.clientHeight - 100); //控制上下位置
           
        }
        window.onresize = function (){//窗口变化事件
            var tipDiv = document.getElementById("tip");
            tipDiv.style.top = document.documentElement.scrollHeight - (document.documentElement.scrollHeight - document.body.scrollTop) + (document.body.clientHeight - 100); //控制上下位置
        }
</script> 
    <script language="javascript">
        var mapObj, toolBar, ruler1, ruler2;; //初始化地图对象，加载地图
        var checkUserList = new Array();//选择人员地图标注信息
        var uname = "";
        function getTargetElement(evt) {
            var elem
            if (evt.target) {
                elem = (evt.target.nodeType == 3) ? evt.target.parentNode : evt.target
            }
            else {
                elem = evt.srcElement
            }
            return elem
        }

        var lastC = null;
        function OnClientTreeNodeChecked(evt) {
            //alert(checkUserList.length);
            evt = (evt) ? evt : ((window.event) ? window.event : " ");
            if (evt == " ") {
                return;
            }
            var obj = getTargetElement(evt);
            var hasTreeNode = false;
            if (obj.tagName) {
                if (obj.tagName == "INPUT" && obj.type == "checkbox") {
                    //if (lastC) lastC.checked = false;
                    lastC = obj;
                    
                  
                    uname = obj.nextSibling.title;
                    if (obj.checked) {
                        //checkUserList[obj.nextSibling.title] = obj.nextSibling.title;
                        //obj.checked = false;
                        //alert(uname + "check");
                    } else {//如果取消选中人员，删除标注信息
                        // obj.checked = true;
                       // alert(uname+"uncheck");
                        removeMarker(uname);
                        return ;
                        
                    }
                    
                   // alert(checkUserList[uname]);
                   
                    //alert(checkUserList["test"]);
                    if (checkUserList.length > 0) {
                        alert(checkUserList[checkUserList.length - 1]);
                    }


                    $.ajax({
                        type: "Post",
                        url: "OnlineUser.aspx",
                        // data: "{'token':'ajax'}",// 使用这种方式竟然无法传递参数，各位有知道原因的告诉一下啊。
                        data: "token=ajax&username=" + uname,
                        success: function (data) {
                            //alert(data);
                            //ss= "120.1629,33.345077,120.157527,33.341529,120.157504,33.341456"
                            //  alert(data);
                            a(data)
                        }
                    });
                }
            }
        }

        //初始化地图对象，加载地图
        function mapInit() {
            mapObj = new AMap.Map("iCenter", {
                //二维地图显示视口        
                view: new AMap.View2D({
                    center: new AMap.LngLat(113.595832, 24.808443), //地图中心点   
                    zoom: 13 //地图显示的缩放级别   
                })
            });
            //地图中添加地图操作ToolBar插件   
            mapObj.plugin(["AMap.ToolBar"], function () { toolBar = new AMap.ToolBar(); mapObj.addControl(toolBar); });
            //地图初始化时，在地图上添加一个marker标记,鼠标点击marker可弹出自定义的信息窗体
            // a();
            mapObj.plugin(["AMap.RangingTool"], function () {
                ruler1 = new AMap.RangingTool(mapObj);
                AMap.event.addListener(ruler1, "end", function (e) {
                    ruler1.turnOff();
                });
                var sMarker = {
                    icon: new AMap.Icon({    //复杂图标
                        size: new AMap.Size(28, 37), //图标大小
                        image: "http://webapi.amap.com/images/custom_a_j.png", //大图地址
                        imageOffset: new AMap.Pixel(0, 0)//相对于大图的取图位置
                    })
                };
                var eMarker = {
                    icon: new AMap.Icon({    //复杂图标
                        size: new AMap.Size(28, 37), //图标大小
                        image: "http://webapi.amap.com/images/custom_a_j.png", //大图地址
                        imageOffset: new AMap.Pixel(-28, 0)//相对于大图的取图位置
                    }),
                    offset: new AMap.Pixel(-16, -35)
                };
                var lOptions = {
                    strokeStyle: "solid",
                    strokeColor: "#FF33FF",
                    strokeOpacity: 1,
                    strokeWeight: 2
                };
                var rulerOptions = { startMarkerOptions: sMarker, endMarkerOptions: eMarker, lineOptions: lOptions };
                ruler2 = new AMap.RangingTool(mapObj, rulerOptions);
            });
        }



        //启用默认样式测距
        function startRuler1() {
            ruler2.turnOff();
            ruler1.turnOn();
        }
        //启用自定义样式测距
        function startRuler2() {
            ruler1.turnOff();
            ruler2.turnOn();
        }
        function removeMarker(rusername) {

            if (checkUserList[rusername] != null) {
                checkUserList[rusername].setMap(null);
                checkUserList[rusername] = null;
            }
            if (checkUserList[rusername+"s"] != null) {
                checkUserList[rusername + "s"].setMap(null);
                checkUserList[rusername] = null;
            }
            
        }
        function a(mes) {


            //mapObj.clearMap();

            (function () {

                var strsnei = mes.split(',');
                var opts = { title: '' };
                mapObj.setZoomAndCenter(14, new AMap.LngLat(strsnei[0], strsnei[1]));
              
                addMarker(strsnei[0], strsnei[1], '<span style="font-size:14px;color:#0A8021">' + strsnei[2] + '</span>', "<div style='line-height:1.8em;font-size:12px;'>地址:" + strsnei[3] + "</br></div>", strsnei[2], strsnei[4], strsnei[5]);

            })();
            var strsnei = mes.split(',');
            // alert("setElerailing" + strsnei[0]);
            //获取人员围栏信息
            getElerailing();

            // alert("setElerailing end");

        }
        function addMarkerInformation(s,j) {


        }

        //添加marker标记,cuname为该标注的登录名
        function addMarker(l, s, title, content, cuname, Serils, Fence) {
           
            var marker = new AMap.Marker({
                map: mapObj, position: new AMap.LngLat(l, s),
                //位置
                icon: "http://webapi.amap.com/images/0.png" //复杂图标
            });
            marker.setLabel({//label默认蓝框白底左上角显示，样式className为：amap-marker-label
                offset: new AMap.Pixel(-30, 40),//修改label相对于maker的位置
                content: "护林员编号：" + Serils + "<br /> 围栏地址：" + Fence
            });

            var map = new AMap.Map("container", {
                resizeEnable: true
            });
            var contextMenu = new AMap.ContextMenu();  //创建右键菜单
            //右键放大
            contextMenu.addItem("发信息", function () {
                alert('未完成');
            }, 0);
            //右键缩小
            contextMenu.addItem("发启对讲", function () {
                alert('未完成');
            }, 1);

            map.setCenter(marker.getPosition());
            //绑定鼠标右击事件——弹出右键菜单
            marker.on('rightclick', function (e) {
                contextMenu.open(mapObj, new AMap.LngLat(l, s));
            });

            AMap.event.addListener(marker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
                //实例化信息窗体 
                var infoWindow = new AMap.InfoWindow({
                    isCustom: true,  //使用自定义窗体
                    content: createInfoWindow(title, content, cuname), offset: new AMap.Pixel(16, -45)//-113, -140
                });
                infoWindow.open(mapObj, marker.getPosition());
            });
            checkUserList[uname] = marker;//将标添加至列表

        }
        function getElerailing() {
            $.ajax({
                type: "POST",
                async: false,
                url: "../FileHandler/DataValidator.ashx",
                data: "action=getcoords&username=" + uname,
                complete: function (response, a, b) {
                    self.callback(response);
                    return;
                },
                success: function (data) {
                    // alert(data);
                    mos = data;//围栏坐标
                    //设置人员围栏信息
                    setElerailing(data)
                },
                error: function (error) {
                    alert('此用户未设定围栏', "验证失败");
                }
            });
        }
        function setElerailing(mos) {
            //if (uname != "莫柯芬")
            //    return;
            //alert(uname);
            //uname = "莫柯芬";
            var plarray;
            // var mos = "113.591697,24.815237;113.592298,24.816094;113.595559,24.809627;113.594186,24.792954;113.599937,24.794746;113.603198,24.80059;113.601053,24.80768;113.597448,24.815938;113.599164,24.821235;113.595903,24.822793;113.590238,24.821002;113.590238,24.820768"; l = l * 1;

            ////////
            //l = l * 1;            
            //s = s * 1;
            // alert("mos" + mos);
            var polygonArr = mos.split(";");//多边形覆盖物节点坐标数组
            for (i = 0; i < polygonArr.length; i++) {
                polygonArr[i] = polygonArr[i].split(",");
            }

            //alert(polygonArr[0] + "polygonArr[0]");
            //alert(polygonArr[0][1] + "polygonArr[0][1]");
            //alert(polygonArr.join("~~~"));
            polygon = new AMap.Polygon({
                path: polygonArr,//设置多边形边界路径
                strokeColor: "#FF33FF", //线颜色
                strokeOpacity: 0.5, //线透明度
                strokeWeight: 3,    //线宽
                fillColor: "#1791fc", //填充色
                fillOpacity: 0//填充透明度
            });
            polygon.setMap(mapObj);
            checkUserList[uname + "s"] = polygon;//将标添加至列表，登录名后可s表示该人员的围栏信息;

            //alert(polygonArr);
            //alert("mapObj" + mapObj);
            // alert(l*1 + 0.1);
            // polygonArr.push([l-0.03, s+0.02]);
            //polygonArr.push([l + 0.055, s + 0.02]);
            // polygonArr.push([l  +0.025, s - 0.025]);
            // polygonArr.push([l - 0.05, s - 0.02]);
            /*
            polygonArr.push([116.403322, 39.920255]);
            polygonArr.push([116.410703, 39.897555]);
            polygonArr.push([116.402292, 39.892353]);
            polygonArr.push([116.389846, 39.891365]);
            */
            /*
            
            
                polygon = new AMap.Polygon({
                    path: polygonArr, //设置多边形边界路径
                    strokeColor: "#FF33FF", //线颜色
                    strokeOpacity: 0.2, //线透明度
                    strokeWeight: 3,    //线宽
                    fillColor: "#1791fc", //填充色
                    fillOpacity: 0.35//填充透明度
                });
                polygon.setMap(mapObj);
                */
            //mapObj.setZoomAndCenter(14, polygonArr[0]);

        }


        //构建自定义信息窗体 
        function createInfoWindow(title, content, cuname) {
            //alert(content);
            //alert("cuname="+cuname);
            //alert("title="+title);
            var userinfo = '';
            $.ajax({
                type: "POST",
                async: false,
                url: "../FileHandler/DataValidator.ashx",
                data: "action=getusers&username=" + cuname,
                complete: function (response, a, b) {
                    self.callback(response);
                    return;
                },
                success: function (data) {
                    $.each(data, function (i, val) {
                        //alert(val.MC);
                        if (!val.MC && typeof (val.MC) != "undefined" && val.MC != 0)
                            val.MC = '';
                        userinfo = '<span style="line-height:1.8em;font-size:12px;">编号：' + val.ID + '<br>姓名：' + val.TrueName + '<br>职务：' + val.ZhiWei + '<br>部门：' + val.Department + '<br>手机号：' + val.UserName + '<br>责任区域：' + val.MC + '<br>' + content + '定位时间：' + val.CanJiaGongZuoTime + '</span>';
                    });
                },
                error: function (error) {
                    alert('获取用户信息失败', "验证失败");
                }
            });
            var info = document.createElement("div"); info.className = "info";
            //可以通过下面的方式修改自定义窗体的宽高  
            //info.style.width = "400px";      
            // 定义顶部标题   
            var top = document.createElement("div"); top.className = "info-top"; var titleD = document.createElement("div"); titleD.innerHTML = title; var closeX = document.createElement("img"); closeX.src = "http://webapi.amap.com/images/close2.gif"; closeX.onclick = closeInfoWindow; top.appendChild(titleD); top.appendChild(closeX); info.appendChild(top);
            // 定义中部内容     
            var middle = document.createElement("div"); middle.className = "info-middle"; middle.style.backgroundColor = 'white'; middle.innerHTML = userinfo; info.appendChild(middle);
            // 定义底部内容
            var bottom = document.createElement("div"); bottom.className = "info-bottom"; bottom.style.position = 'relative'; bottom.style.top = '0px'; bottom.style.margin = '0 auto'; var sharp = document.createElement("img"); sharp.src = "http://webapi.amap.com/images/sharp.png"; bottom.appendChild(sharp); info.appendChild(bottom); return info;
        }
        //关闭信息窗体
        function closeInfoWindow() { mapObj.clearInfoWindow(); }

    </script>
</head>
<body onload="mapInit()">
    <form id="form1" runat="server">
    <div>
        <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="middle"  style="height:30px;">
                        <div class="hlb-contact">
                            <div class="hlb-navmune"style="width:100%;">
                                <ul class="hlb-navmunebtn">
                                    <a href="javascript:void(0);" onclick="hidemenu()"><img src="../Content/Newicons/hlb-01.png"  /></a>
                                </ul>
                                <ul class="hlb-navmunehome">
                                    <img src="../Content/Newicons/hlb-02.png" />
                                </ul>
                                <ul class="hlb-navmunelink">
                                    当前位置：网站首页&nbsp;-&nbsp;定位管理&nbsp;-&nbsp;最新位置
                                </ul>
                            </div>
                          
                        </div>
                    </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%; background-color:#e6e4e4">
        <tr>
            <td valign="top" style="width: 20%;">
                <asp:TreeView ID="ListTreeView" runat="server" ShowCheckBoxes="Leaf" ExpandDepth="0"
                    ForeColor="Black" ShowLines="True">
                    <ParentNodeStyle HorizontalPadding="2px" />
                    <RootNodeStyle HorizontalPadding="2px" />
                    <LeafNodeStyle HorizontalPadding="2px" />
                    <Nodes>
                    </Nodes>
                </asp:TreeView>
             <table class="hlb-Tree-table">
               <tr>
                <td>
                    <input type="button" id="SettingWorkTime" runat="server"  onclick="CheckedUser('SettingWorkTime');"  class="hlb-iframebtn5_1s" value="任务调度"/>
               </td>
                <td>
                   <input type="button" id="Notice" runat="server" onclick="CheckedUser('Notice');" class="hlb-iframebtn5_1s" value="通知"/>
              </td>
              </tr>
                <tr>
                 <td>
                    <input type="button"  id="AttendanceSel" runat="server" onclick="CheckedUser('AttendanceSel');" class="hlb-iframebtn5_1s" value="考勤查询"/>
                 </td>
                <td>
                    <input type="button"  id="WarningSel" runat="server" onclick="CheckedUser('WarningSel');" class="hlb-iframebtn5_1s" value="预警查询"/>
                </td>
                </tr>
             </table>
            </td>
            <td valign="top" style="width: 80%;">
                <div id="iCenter" style="height: 500px;">
                </div>
                <div id="tip">
                    <div>
                        <input type="button" value="距离量测" onclick="javascript: startRuler2()" />
                    </div>
                    <div>
                        鼠标在地图上点击获取量测点，双击左键结束当前量距操作</div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
