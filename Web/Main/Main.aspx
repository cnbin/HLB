<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta http-equiv="cache-control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <link rel="Stylesheet" type="text/css" href="../Controls/ExtJS/resources/css/ext-all.css"
        charset="gb2312" />
    <link href="../Controls/ExtJS/Css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../Controls/AzureCalendar/Theme/Default/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Controls/ExtJS/adapter/ext/ext-base.js"></script>
    <script type="text/javascript" src="../Controls/ExtJS/ext-all.js"></script>
    <script src="../JS/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Controls/js/bottom.js"></script>
    <script type="text/javascript" src="../Controls/js/rightKeyTabPanel.js"></script>
    <script src="../Controls/js/centerGrid.js" type="text/javascript"></script>
    <script src="../Controls/ext-ux/statusbar/StatusBar.js" type="text/javascript"></script>
    <script src="../Controls/js/Changepwd.js" type="text/javascript"></script>
    <script src="../Controls/js/JsHelper.js" type="text/javascript"></script>
    <script src="../JS/jquery.timers-1.2.js" type="text/javascript"></script>
    <script src="../JS/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="../Style/globle.css"" rel="stylesheet" type="text/css" />
    <link href="../Style/iframestyle.css" rel="stylesheet" type="text/css" />
    <link href="../Style/main.css" rel="Stylesheet" type="text/css" />
     <script src="../JS/NewSearch.js" type="text/javascript"></script>
    <style type="text/css">
        .x-panel-body p
        {
            margin: 5px;
        }

        .x-column-layout-ct .x-panel
        {
            margin-bottom: 5px;
        }

        .x-column-layout-ct .x-panel-dd-spacer
        {
            margin-bottom: 5px;
        }

        .settings
        {
            background-image: url(../shared/icons/fam/folder_wrench.png) !important;
        }

        .nav
        {
            background-image: url(../shared/icons/fam/folder_go.png) !important;
        }

        .panel_icon
        {
            background-image: url(../Controls/images/first.gif);
        }

        .my_icon
        {
            background-image: url(../Controls/images/plugin.gif);
        }

        .x-tree-node div.menu-node
        {
            background: #eee url(cmp-bg.gif) repeat-x;
            margin-top: 1px;
            border-top: 1px solid #ddd;
            border-bottom: 1px solid #ccc;
            padding-top: 2px;
            padding-bottom: 1px;
            font-weight: bold;
        }

        .menu-node .x-tree-node-icon
        {
        }

        .menu-node2
        {
            border: 1px solid #fff;
            background-image: url(../Content/icons/bullet_green.png);
            background-repeat: no-repeat;
            padding-right: 20px;
            background-position: 1px 1px;
        }

        .no-node-icon
        {
            display: none;
        }

        .menu-node2 .x-tree-ec-icon
        {
        }

        .error
        {
            background-image: url(../Content/icons/exclamation.gif);
        }

        .information
        {
            background-image: url(../Content/icons/calendar_view_month.png) !important;
        }
    </style>
</head>
<body>
 <div id="divTop" style="height: 50px; background-image: url('../images/topbg.jpg');">
        <table style="width: 100%;">
            <tr>
                <td rowspan="2">
                    <img src="../Content/Newicons/logo.png" style="width: 700px; height: 50px;" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <script type="text/javascript">

            function BuildTree() {
                //上面
                var toolbar = new Ext.Toolbar
        ({
            border: false, x: 0, y: 0, id: "toolbars",
            items:
             [
               {
                   xtype: 'label',
                   html: ' <div class="hlb-topcap"><div class="hlb-topcap-logo"><img src="../Content/Newicons/logo.png" /></div></div>'
               }, "->",
             {
                 xtype: 'label',
                 html: ' <div class="hlb-topcap"><div class="hlb-topcap-message"><ul class="text">欢迎您!&nbsp;&nbsp;<%= ZWL.Common.PublicMethod.GetSessionValue("TrueName")%></ul> ' +
                     '<ul class="ico"><a href="#" onclick="OpenIframe(\'个人办公/个人设置/012\',\'修改密码\',\'../Personal/ChangPwd.aspx\')" title="修改密码"><img src="../Content/Newicons/hlb-locked.png" /></a>' +
                     '<a href="#" onclick="OverUser()" title="退出登录"><img src="../Content/Newicons/hlb-led.png" /></a></ul></div></div>'
             }
             ]
        });

         var toolbar1 = new Ext.Toolbar
 ({
     border: false, x: 0, y: 0, id: "toolbars1",
     items:
      [
      {
          xtype: 'label',
          height: 80,
          html: '<div class="hlb-mune"></div>'
      }]
 });

         var panel_toolbar = new Ext.Panel
 ({
     border: false, x: 0, y: 0,
     items: [toolbar]
 });


         var panel_north = new Ext.Panel({
             id: "panel_north", region: "north",
             title: "",
             border: false,
             html: '',
             height: 25,
             buttonAlign: 'right',
             margin: '0 0 0 0',
             tbar: toolbar,
             bbar: toolbar1
         });




                //中间
         var tabpanel = new Ext.TabPanel
 ({
     activeTab: 0, autoWidth: true, autoScroll: true, border: true, frame: true, id: "TabPanelID", enableTabScroll: true,
     items:
     [
         {
             xtype: "panel", layout: 'fit', title: "工作台", border: false, frame: false, iconCls: 'panel_icon',
             html: "<iframe id='iframe' scrolling='true' width='100%' height='100%'  frameborder='0' src='OnlineUser.aspx'></iframe>"
             //html: "<iframe id='iframe' scrolling='true' width='100%' height='100%'  frameborder='0' src='../Test.aspx'></iframe>"
         }
     ]
 });
         var panel_center = new Ext.Panel
 ({
     id: 'panleCenter', frame: false, border: false,
     region: 'center',
     split: true,
     items: [tabpanel],
     layout: 'fit',
 });

                //左面
         var panel_west = new Ext.Panel
 ({
     id: 'panWestMenu',
     region: 'west',
     title: '功能菜单',
     iconCls: 'system_icon',
     width: 180,
     split: true,
     minSize: 180,
     maxSize: 250,
     collapsed: true, //左边缩进
     collapsible: true,
     margins: '0 0 0 0',
     layout: 'accordion',
     layoutConfig: { animate: true }
 });
         var viewport = new Ext.Viewport
 ({
     id: 'vpMain',
     layout: 'border',
     items:
     [
         panel_north,
         panel_center,
         panel_west
     ]
 });
                //加载左面的数据
         var loadPanelWest = function (init) {
             Ext.Ajax.request
      ({
          url: 'Main.aspx?method=GetData',
          success: function (response, options) {
              try {

                  var panWestMenu = Ext.getCmp("panWestMenu");
                  if (panWestMenu) {
                      var children = panWestMenu.findByType('panel');
                      if (children) {
                          for (var i = 0, len = children.length; i < len; i++) {
                              panWestMenu.remove(children[i], true);
                          }
                      }
                  }
                  var toolBars1 = Ext.getCmp("toolbars1");
                  var toolBars = Ext.getCmp("toolbars");
                  var menusArray = Ext.util.JSON.decode(response.responseText);

                  var body = document.getElementsByTagName("body")[0];
                  var nav_div = document.createElement('div');

                  nav_div.className = 'hlb-nav jslist';
                  nav_div.id = 'hlb-nav';

                  ////一级菜单
                  for (var j = 0; j < menusArray.length; j++) {

                      var mp = CreateMenuPanel(menusArray[j].TypeTitle, menusArray[j].TypeID, menusArray[j].icon, menusArray[j].iconCls);
                      var allbtn_div = document.createElement('div');
                      allbtn_div.className = 'allbtn';
                      allbtn_div.innerHTML = '<h2><a href="#" onclick="clickMune(' + j + ')"><strong>&nbsp;</strong>' + menusArray[j].TypeTitle + '<i>&nbsp;</i></a></h2>';
                      var ul = document.createElement('ul');
                      ul.className = 'jspop box';
                      //ul.innerHTML = '<li class=a1><div class=tx><a href="#">各地名优茶</a> </div></li><li class=a1><div class=tx><a href="#">各地名优茶</a> </div></li><li class=a1><div class=tx><a href="#">各地名优茶</a> </div></li><li class=a1><div class=tx><a href="#">各地名优茶</a> </div></li>';
                      $.ajax({
                          type: "post",                   //提交方式
                          url: "Main.aspx/GetMume",   //提交的页面/方法名
                          data: "{'TextStr':'" + menusArray[j].id + "'}",    //参数（如果没有参数：null）
                          dataType: "json",//类型
                          async: false,//是否异步
                          contentType: "application/json; charset=utf-8",
                          success: function (data) {
                              //返回的数据用data.d获取内容  
                              var obj = eval(data.d);
                              for (var i = 0; i < obj.length; i++) {
                                  var li = document.createElement('li');
                                  var li_div = document.createElement('div');
                                  li_div.className = 'tx';
                                  var li_div_position = document.createElement('div');
                                  li_div_position.className = 'position';
                                  if (obj[i].NavigateUrlStr == "") //如果有三级菜单
                                  {
                                      $.ajax({
                                          type: "post",                   //提交方式
                                          url: "Main.aspx/GetMume",   //提交的页面/方法名
                                          data: "{'TextStr':'" + obj[i].TextStr + "'}",    //参数（如果没有 参数：null）
                                          dataType: "json",//类型
                                          async: false,//是否异步
                                          contentType: "application/json; charset=utf-8",
                                          success: function (data2) {
                                              var obj2 = eval(data2.d);
                                              var li_div_pop = document.createElement('div');
                                              li_div_pop.className = 'pop';
                                              li_div.innerHTML = '<a href="#">' + obj[i].TextStr + '</a>'; 
                                              var k = 0//无需onclick
                                              for (; k < obj2.length; k++) {
                                                  li_div_pop.innerHTML += '<a href="#" onclick="OpenIframe(\'' + menusArray[j].id + '/' + obj[i].TextStr + '/' + obj2[k].ValueStr + '\',\'' + obj2[k].TextStr + '\',\'' + obj2[k].NavigateUrlStr + '\')"><dl>' + obj2[k].TextStr + '</dl></a>'
                                              }
                                              if (k > 0) {  //如果二级菜单下没有三级菜单，则删除二级菜单
                                                  li_div_position.appendChild(li_div_pop);
                                                  li.appendChild(li_div_position);
                                                  li.appendChild(li_div);
                                                  ul.appendChild(li);
                                              }
                                          },
                                          error: function (err)
                                          {
                                              //li_div.innerHTML = '<a href="#">' + obj[i].TextStr + '</a>' //无需onclick
                                              
                                              alert('平台代码出错');
                                          }
                                      })
                                  }
                                  else {
                                      li_div.innerHTML = '<a href="#" onclick="OpenIframe(\'' + menusArray[j].id + '/' + obj[i].ValueStr + '\',\'' + obj[i].TextStr + '\',\'' + obj[i].NavigateUrlStr + '\')">' + obj[i].TextStr + '</a>' //新增onclick
                                      li.appendChild(li_div_position);
                                      li.appendChild(li_div);
                                      ul.appendChild(li);
                                  }
                                 
                              }

                          },
                          error: function (err) {
                              alert('平台代码出错');

                          }
                      });


                     
                      allbtn_div.appendChild(ul);

                      panWestMenu.add(mp);
                      if (init == "load") {

                          var tempBtn = CreteButton(menusArray[j].TypeTitle, menusArray[j].TypeID, menusArray[j].icon);
                          // toolBars1.addItem(tempBtn);
                      }
                      nav_div.appendChild(allbtn_div);
                      body.appendChild(nav_div);
                  }

                  panWestMenu.doLayout(); //形成二级菜单



              }
              catch (e) {

              }
          }
      });
         };
         loadPanelWest("load");
                //创建单个treePanel
         var CreateMenuPanel = function (title, TypeID, icons, iconcls) {
             return new Ext.Panel
           ({
               title: title, layout: 'fit', border: false, iconCls: iconcls,
               items:
               [{
                   xtype: 'treepanel', singleExpand: true, animate: true, autoScroll: true, containerScroll: true,
                   border: false, layout: 'fit', rootVisible: false, autoHeight: false, lines: true, iconCls: icons, spilt: true, // 美化界面
                   width: 180, height: 370, enableDD: false, dropConfig: { appendOnly: true },
                   loader: new Ext.tree.TreeLoader({ dataUrl: "Main.aspx" }),
                   root: new Ext.tree.AsyncTreeNode
                   ({
                       id: TypeID,
                       text: title,
                       draggable: false,
                       scope: this,
                       scripts: true,
                       expanded: true

                   })

                   , listeners: {
                       "click": function (node, e) {
                           if (node.attributes.action == "") {
                               //Ext.Msg.alert("提示消息","不可以对根节点执行右键操作！");
                               return;
                           }

                           //console.info(node);
                           var _Id = node.attributes.id;
                           var _TypeID = node.attributes.TypeID;
                           var _TypeTitle = node.attributes.TypeTitle;
                           var _TypeEName = node.attributes.TypeEName;
                           var action = node.attributes.action;

                           var tabs = Ext.getCmp("TabPanelID");
                           var title = _TypeTitle;
                           for (var i = 0; i < tabs.items.length; i++) {
                               if (tabs.items.items[i].title == title) {
                                   // Ext.Msg.alert("消息", "该菜单项[ " + node.attributes.text + " ]已经存在Tab里面！");
                                   var activeTab = tabs.getActiveTab();
                                   //                                      activeTab.load({
                                   //                                          url: node.attributes.action,
                                   //                                          discardUrl: false,
                                   //                                          scope:this,
                                   //                                          scripts: true
                                   //                                      });
                                   //                                      var updater = activeTab.getUpdater();
                                   //                                      if (updater) {
                                   //                                         
                                   //                                          updater.loadScripts = true;
                                   //                                          updater.defaultUrl = node.attributes.action;
                                   //                                          updater.refresh();
                                   //                                      }
                                   tabs.activate(tabs.items.items[i]);
                                   return;
                               }
                           }
                           var pnl = new BuildGridView(_Id, title, action).gridView;
                           // console.info(pnl);
                           tabs.add(pnl);
                           tabs.activate(pnl);
                       }
                   }


               }]
           });
         };
                //创建单个按钮
         var CreteButton = function (MenuTitle, MenuID, icons) {
             return new Ext.Toolbar.Button
         ({
             id: MenuID, text: MenuTitle, cls: 'x-btn-text-icon', icon: icons,
             tooltip: MenuTitle,
             listeners:
               {
                   "click": function (o, e) {

                       var panWestMenu = Ext.getCmp('panWestMenu');

                       var toolbars = Ext.getCmp('toolbars1');
                       var panel_north = Ext.getCmp('panel_north');
                       if (toolbars && toolbars.items.length > 0) {
                           for (var i = 0; i < toolbars.items.length; i++) {
                               if (toolbars.items.items[i].id == o.id) {
                                   toolbars.items.items[i].pressed = true;
                               }
                               else {
                                   toolbars.items.items[i].pressed = false;
                               }
                           }
                       }
                       if (panWestMenu) {
                           var children = panWestMenu.findByType('panel');
                           if (children) {
                               for (var i = 0, len = children.length; i < len; i++) {
                                   panWestMenu.remove(children[i], true);
                               }
                           }

                           var mp = CreateMenuPanel(o.tooltip, o.id, o.iconCls);
                           panWestMenu.add(mp);
                           panWestMenu.doLayout();
                       }
                   }
               }
         });
         };

     }

     //今日日程提醒窗口
     Ext.ux.ToastWindowMgr = {
         positions: []
     };

     Ext.ux.ToastWindow = Ext.extend(Ext.Window, {
         initComponent: function () {
             Ext.apply(this, {
                 iconCls: this.iconCls || 'information',
                 width: 400,
                 height: 280,
                 autoScroll: true,
                 autoDestroy: true,
                 plain: false
             });
             this.task = new Ext.util.DelayedTask(this.hide, this);
             Ext.ux.ToastWindow.superclass.initComponent.call(this);
         },
         setMessage: function (msg) {
             this.body.update(msg);
         },
         setTitle: function (title, iconCls) {
             Ext.ux.ToastWindow.superclass.setTitle.call(this, title, iconCls || this.iconCls);
         },
         onRender: function (ct, position) {
             Ext.ux.ToastWindow.superclass.onRender.call(this, ct, position);
         },
         onDestroy: function () {
             Ext.ux.ToastWindowMgr.positions.remove(this.pos);
             Ext.ux.ToastWindow.superclass.onDestroy.call(this);
         },
         afterShow: function () {
             Ext.ux.ToastWindow.superclass.afterShow.call(this);
             this.on('move', function () {
                 Ext.ux.ToastWindowMgr.positions.remove(this.pos);
                 this.task.cancel();
             }
 , this);
             this.task.delay(10000);
         },
         animShow: function () {
             this.pos = 0;
             while (Ext.ux.ToastWindowMgr.positions.indexOf(this.pos) > -1)
                 this.pos++;
             Ext.ux.ToastWindowMgr.positions.push(this.pos);
             this.setSize(400, 280);
             this.el.alignTo(document, "br-br", [-20, -20 - ((this.getSize().height + 10) * this.pos)]);
             this.el.slideIn('b', {
                 duration: 1,
                 callback: this.afterShow,
                 scope: this
             });
         },
         animHide: function () {
             Ext.ux.ToastWindowMgr.positions.remove(this.pos);
             this.el.ghost("b", {
                 duration: 1,
                 remove: true,
                 scope: this,
                 callback: this.destroy
             });
         }
     });
        </script>
        <script type="text/javascript">
            var tabpanels;
            var Txtime = 0.0; var ttime; var aa;
            var coun = 1;
            function SendTX() {
                if (coun > 0) {

                    var num = Math.random();
                    new Ext.ux.ToastWindow({
                        title: '今日提醒',
                        html: "<iframe scrolling='true' width='100%' height='100%'  frameborder='0' src='SmsShow.aspx?rad=\" + num + \"'></iframe>",
                        iconCls: 'information'
                    }).show(document);
                }

            }

            function sendRequest() {
                $.ajax({
                    type: "POST",
                    url: "OnlineCount.aspx?Online=on",
                    async: false,
                    success: function (mes) {
                        if (mes != "null") {
                            $("#spnOnLineUserCount").html(mes.toString().split(',')[0]);
                            $("#mailcount").html(mes.toString().split(',')[1]);
                            coun = mes.toString().split(',')[1];
                            ttime = mes.toString().split(',')[2];

                            var iftix = mes.toString().split(',')[3];
                            var t;
                            if (iftix == '否') {
                                $(document).stopTime('C');
                            } else {
                                if (parseFloat(ttime) != parseFloat(Txtime)) {

                                    $(document).stopTime('C');
                                    $(document).everyTime(parseFloat(ttime), 'C', function () { SendTX(); });

                                    Txtime = ttime;
                                }
                            }
                        } else {
                            alert("帐号过期，请重新登陆！");
                            location.href = "../Default.aspx";
                        }
                    }
                });
            }
            function ready() {
                BuildBottomPanel();
                BuildTree();
                RightKeyTabPanel();
                tabpanels = Ext.getCmp("TabPanelID");
                sendRequest();
                //SendTX();
                //                sendRequest();
                //                console.info(Txtime);
                $(document).everyTime(61100, 'A', function (i) { sendRequest(); });

            }
            function sett() {
                $(document).everyTime(parseFloat(ttime), 'C', function () { SendTX(); });
            }
            document.onready = ready();
            //   Ext.onReady(ready);

        </script>
    </div>
    <asp:TreeView ID="ListTreeView" runat="server" ExpandDepth="0" ForeColor="Black"
        Visible="false" Width="100%" ShowLines="True">
        <ParentNodeStyle HorizontalPadding="2px" />
        <RootNodeStyle HorizontalPadding="2px" Height="20px" Width="100%" />
        <LeafNodeStyle HorizontalPadding="2px" />
    </asp:TreeView>
</body>
</html>
