<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RJOnlineUser.aspx.cs" Inherits="RJOnlineUser" %>

<html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <script src="../JS/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../JS/DateAndTime.js"></script>
    <link href="../Style/DT.css" rel="stylesheet" type="text/css" />
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
            height: 30px;
            background-color: #fff;
            padding-left: 10px;
            padding-right: 10px;
            border: 1px solid #969696;
            position: absolute;
            font-size: 12px;
            right: 10px;
            bottom: 30px;
            border-radius: 3px;
            line-height: 28px;
        }
        
        #tip input[type='button']
        {
            height: 28px;
            line-height: 28px;
            outline: none;
            text-align: center;
            padding-left: 5px;
            padding-right: 5px;
            color: #FFF;
            background-color: #0D9BF2;
            border: 0;
            border-radius: 3px;
           
            margin-left: 5px;
            cursor: pointer;
            margin-right: 10px;
        }
    </style>
    <script type="text/javascript">
        //识别不同的浏览器
  
    </script>
</head>
<body onload="mapInit()">
    <form id="form1" runat="server">
    <div>
        <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">
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
                                    当前位置：网站首页&nbsp;-&nbsp;定位管理&nbsp;-&nbsp;历史轨迹
                                </ul>
                            </div>
                             
                    </td>
            </tr>
        
        </table>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #C0C0C0;
        table-layout: fixed; width: 100%; background-color:#e6e4e4"">
        <tr>
            <td valign="top" style="width: 20%;" rowspan="2">
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
                    <input type="button"  id="SettingWorkTime" runat="server" onclick="CheckedUser('SettingWorkTime');"  class="hlb-iframebtn5_1s" value="任务调度"/>
               </td>
                <td>
                   <input type="button" id="Notice" runat="server"  onclick="CheckedUser('Notice');" class="hlb-iframebtn5_1s" value="通知"/>
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
            <td valign="top" style="width: 80%; height: 30px;">
                请选择回放日期：<asp:TextBox ID="txtYuJiTiXing" onfocus="setday(this)" runat="server" Width="130px" Visible="False"></asp:TextBox>  <asp:TextBox
                    ID="TextBox1" runat="server" onfocus="setday(this);" Width="164px"></asp:TextBox>&nbsp;<input type="button" value="查询" onclick="search();" />
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 80%;">
                <div id="iCenter" style="height: 500px;">
                    <div id="tip" style="z-index:100">
                    <input type="button" value="开始回放"  id="btStart" onclick="startAnimation()" />
                    <input type="button" value="停止回放" id="btStop"  onclick="stopAnimation()" />
                </div>
                </div>
                
                <script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=f1d3d82f283f2be724897552202a730c"></script>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<script language="javascript">
    var mapObj, toolBar; //初始化地图对象，加载地图

    var marker;
    var uname = "";
    var Tname = "";
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
        evt = (evt) ? evt : ((window.event) ? window.event : " ");
        if (evt == " ") {
            return;
        }
        var obj = getTargetElement(evt);
        var hasTreeNode = false;
        if (obj.tagName) {
            if (obj.tagName == "INPUT" && obj.type == "checkbox") {
                if (lastC) lastC.checked = false
                lastC = obj;
                obj.checked = true;
                uname = obj.nextSibling.title;
                Tname = obj.nextSibling.innerHTML;
                //alert(uname);


            }
        }
    }
    //初始化地图对象，加载地图
    function mapInit() {
        mapObj = new AMap.Map("iCenter", {
            //二维地图显示视口        
            view: new AMap.View2D({
                center: new AMap.LngLat(113.595832, 24.808443), //地图中心点   
                zoom: 17 //地图显示的缩放级别   
            })
        });
        //地图中添加地图操作ToolBar插件   
        mapObj.plugin(["AMap.ToolBar"], function () { toolBar = new AMap.ToolBar(); mapObj.addControl(toolBar); });
        //地图初始化时，在地图上添加一个marker标记,鼠标点击marker可弹出自定义的信息窗体
        // a();

        // AMap.event.addListener(mapObj, "complete", completeEventHandler(''));
    }

    function search() {
        //var stime = $("#txtYuJiTiXing").val();
        var stime ;
        var etime = $("#TextBox1").val();
        if(uname=="") {
            alert("请选择要查看的人员");
            return "";
        }
        /** 只显示当前历史记录，屏蔽开始时间，默认从0点开始
        if (stime == "") {
            alert("请选择开始时间");
            return "";
        }
        **/
        if (etime == "") {
            alert("请选择要查看的人员");
            return "";
        }
        stime = etime.substr(0, 10);
        //alert("uname:" + uname);
        $.ajax({
            type: "Post",
            url: "RJOnlineUser.aspx",
            // data: "{'token':'ajax'}",// 使用这种方式竟然无法传递参数，各位有知道原因的告诉一下啊。
            data: "token=ajax&username=" + uname + "&stime=" + stime + "&etime="+etime,
            success: function (data) {
             // alert(data);
               //ss= "120.1629,33.345077,120.157527,33.341529,120.157504,33.341456"
                completeEventHandler(data)
            }
        });

       
    }

    //地图图块加载完毕后执行函数
    function completeEventHandler(res) {
       
        if (res == null || res == "") {
            alert("您好，本查询结果没有相关位置信息，请重新选择日期，谢谢!");
            return;
        }
      
     
        var lngX = 120.157504;
        var latY = 33.341456;
        lineArr = new Array();
        //lineArr.push(new AMap.LngLat(lngX, latY));

        var strs = res.split('|');

        for (var i = 0; i < strs.length; i++) {
            if (strs[i] != "") {
                
                var s = strs[i].split(',');
                if (s[0] == "" || s[0] == "null" || s[1] == "" || s[1] == "null")
                    continue;
                    lineArr.push(new AMap.LngLat(s[0], s[1]));
            }
        }
        if (lineArr[0] != null) {
            marker = new AMap.Marker({
                map: mapObj,
                //draggable:true, //是否可拖动24.8104188868,113.5972304363
                position: lineArr[0], //基点位置
                icon: "http://code.mapabc.com/images/car_03.png", //marker图标，直接传递地址url
                offset: new AMap.Pixel(-26, -13), //相对于基点的位置
                autoRotation: true
            });
        } else {
            marker = new AMap.Marker({
                map: mapObj,
                //draggable:true, //是否可拖动24.8104188868,113.5972304363
                position: new AMap.LngLat(113.5972304363, 24.8104188868), //基点位置
                icon: "http://code.mapabc.com/images/car_03.png", //marker图标，直接传递地址url
                offset: new AMap.Pixel(-26, -13), //相对于基点的位置
                autoRotation: true
            });
        }   
        AMap.event.addListener(marker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
            //实例化信息窗体 
            var infoWindow = new AMap.InfoWindow({
                isCustom: true,  //使用自定义窗体
                content: createInfoWindow(uname, Tname), offset: new AMap.Pixel(16, -45)//-113, -140
            });
            infoWindow.open(mapObj, marker.getPosition());
        });
       
       

        //                        for (var i = 1; i < 3; i++) {
        //                            lngX = lngX + Math.random() * 0.05;
        //                            if (i % 2) {
        //                                latY = latY + Math.random() * 0.0001;
        //                            } else {
        //                                latY = latY + Math.random() * 0.06;
        //                            }
        //                           
        //                        }
     
        //  mapObj.clear();
        setStartPoint(lineArr);//设置起点图标
        setEndPoint(lineArr);//设置终点图标
        //绘制轨迹
        var polyline = new AMap.Polyline({
            map:mapObj,
            path: lineArr,
            strokeColor: "#FF33FF", //线颜色
            strokeOpacity: 1, //线透明度
            strokeWeight: 5, //线宽
            strokeStyle: "solid"//线样式
        });
        //polyline.setMap(mapObj);
        //alert(lineArr[lineArr.length - 1].getLat() + ","+lineArr[lineArr.length - 1].getLng());
        mapObj.setZoomAndCenter(18, lineArr[lineArr.length - 1]);
       //mapObj.setFitView();
    }
    function setStartPoint(tlineArr) {
           // alert(tlineArr[0]);
            if (tlineArr[0] != null) {
                //添加点标记，并使用自己的icon
                new AMap.Marker({
                    map: mapObj,
                    position: tlineArr[0],
                    icon: new AMap.Icon({
                        size: new AMap.Size(40, 50),  //图标大小
                        image: "../images/hximg/start_point.png",
                        imageOffset: new AMap.Pixel(0, 0)
                    })
                });
            }
    
       
    }
    function setEndPoint(tlineArr) {
        //alert(tlineArr.length);
        if (tlineArr.length >=1 && tlineArr[tlineArr.length - 1] != null) {
           // alert(tlineArr[tlineArr.length - 1]);
            //添加点标记，并使用自己的icon
            new AMap.Marker({
                map: mapObj,
                position: tlineArr[tlineArr.length - 1],
                icon: new AMap.Icon({
                    size: new AMap.Size(40, 50),  //图标大小
                    image: "../images/hximg/end_point.png",
                    imageOffset: new AMap.Pixel(0, 0)
                })
            });
        }
    }
    function startAnimation() {
        //alert(lineArr);
        marker.moveAlong(lineArr, 10000);
    }
    function stopAnimation() {
        marker.stopMove();
    }



    function SetXX(mes) {
        AMap.event.addListener(mapObj, "complete", completeEventHandler(mes));
    }


    //显示选中的节点
    function ShowCheck() {
        var inputs = document.getElementsByTagName("input");
        var result = "";
        //parent.mainFrame.document.getElementById("textarlea").value = document.body.innerHTML;
        for (var i = inputs.length - 1; i >= 0; i--) {
            if (inputs[i].type == "checkbox" && inputs[i].checked == true) {
                var title = inputs[i].nextSibling.title;
                if (result.indexOf(title) == -1) {
                    result += title + ",";
                    alert(result);
                }
            }
        }
        result = result.substring(0, result.length - 1);
        alert(result);
        return result;
    }

    //构建自定义信息窗体 
    function createInfoWindow(title, content) {
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
    function closeInfoWindow() { mapObj.clearInfoWindow(); } 

</script>
