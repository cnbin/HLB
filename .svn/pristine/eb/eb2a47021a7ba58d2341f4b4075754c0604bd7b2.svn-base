


function hidemenu() { //每个页面控制左侧菜单的隐藏于展示
    var bodychildNodes = parent.document.getElementById("iframe").parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.childNodes;
    bodychildNodes[2].childNodes[0].childNodes[0].click();
}

function ReturnClick() //每个页面的返回按钮
{
    var ReturnInput = document.getElementById('ReturnInput');
    window.location.href = ReturnInput.value;
}


function Load_Select() {   //页面刷新时执行
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



function HideSearch() {  //点击事件执行
    var hidden_input = document.getElementById('hidden_input');

    if (hidden_input.value == 'none') {
        document.getElementById('hlb_select').style.display = 'block';
        hidden_input.value = 'block';
    }
    else if (hidden_input.value == 'block') {
        document.getElementById('hlb_select').style.display = 'none';
        hidden_input.value = 'none';
    }
}
function OpenIframe(id, Title, Src) {  //生成ifame页面
    var tabs = tabpanels;
    for (var i = 0; i < tabs.items.length; i++) {
        if (tabs.items.items[i].title == Title) {
            tabs.activate(tabs.items.items[i]);
            return;
        }
    }
    var ifameidlist = new Array();
    ifameidlist = id.split('/');
    tabs.add({
        title: Title,
        id: "PanelArticleViewID" + id,
        html: "<iframe id='" + ifameidlist[ifameidlist.length-1] + "' scrolling='true' width='100%' height='100%'  frameborder='0' src='" + Src + "'></iframe>",
        closable: true
    });
    tabs.activate('PanelArticleViewID' + id);
}

function OverUser() {  //首页注销按钮
    
    if (confirm("确定要注销当前用户并回到登录页吗？"))
    {
        window.location.href = '../Default.aspx';
    }
}

function clickMune(id) {  //上面导航栏一级菜单点击效果
    var bodychildNodes = document.getElementsByTagName("body")[0].childNodes;
    bodychildNodes[2].childNodes[1].childNodes[0].childNodes[id].childNodes[0].click();
}

var UserName='';
function CheckedUser(Type)
{
    UserName = '';
    for (var i = 0; i < window.document.form1.elements.length; i++) {
        var e = form1.elements[i];
        if (e.checked) {
            UserName += UserName == '' ? '' : ',';
            UserName += e.title;
        }
    }
    if (UserName == '') {
        alert('请至少选中一项！');
        return;
    }
    if (Type == 'SettingWorkTime'){
        window.parent.OpenIframe('个人办公/任务调度/001', '收件箱', '../LanEmail/LanEmailAdd.aspx?UserName=' + UserName);
    }
    else if (Type == 'Notice'){
        window.parent.OpenIframe('个人办公/通知公告/004', '通知公告', '../GongGao/GongGaoAdd.aspx?PersonList=' + UserName);
    }
    else if (Type == 'AttendanceSel') {
        window.parent.OpenIframe('签到管理/详细签到表/113', '签到详细表', '../QDGL/QDXX.aspx?UserName=' + UserName);
    }
    else if (Type == 'WarningSel') {
        window.parent.OpenIframe('预警信息/预警情况/109', '预警情况', '../WorkPlan/Warning.aspx?UserName=' + UserName);
    }
   
}

    function CheckEvent(evt) 
 { 
     evt=window.event||evt;
     var   objNode  =   evt.srcElement||evt.target;
     if(objNode.tagName == "INPUT"  &&   objNode.type== "checkbox") 
      {
         var objParentDiv = objNode.id.replace("CheckBox","Nodes");
         if(objNode.checked==true) 
          { 
              setChildCheckState(objParentDiv,true); 
                    
              //setParentCheckeState(objNode,true); 
          }
         else 
          {
              setChildCheckState(objParentDiv,false);
                    
             if(!HasOtherChecked(objNode)){    
                  setParentCheckeState(objNode,false); 
              }
          }
      }
 }
 
//判断是否有并行的其他节点被选中
 function HasOtherChecked(objNode)
 {
    var   objParentDiv  =   WebForm_GetParentByTagName(objNode, "div"); 
     
     var chks = objParentDiv.getElementsByTagName("INPUT");
     for(var i=0;i<chks.length;i++){
         if(chks[i].checked && chks[i].id != objNode.id)
          {
             return true;
          }
      }
     return false;
 }
 
 //设置父节点
 function   setParentCheckeState(objNode,chkstate) 
 {         
     try{
         var   objParentDiv  =   WebForm_GetParentByTagName(objNode, "div");         
                 
         if(objParentDiv == null  ||   objParentDiv  ==  "undefined "){ 
                 return; 
          } 
         else{
             var objParentChkId = objParentDiv.id.replace("Nodes", "CheckBox");
             if (objParentChkId == null || objParentChkId == "undefined" || objParentChkId=='') { return; }
             var objParentCheckBox = document.getElementById(objParentChkId);            
             if(objParentCheckBox){  
                  objParentCheckBox.checked = chkstate;
                  setParentCheckeState(objParentDiv,chkstate);
              }
          }
       }
      catch(e){}
 } 
 
 //设置子节点
 function   setChildCheckState(nodeid,chkstate) 
 {  
     var node = document.getElementById(nodeid);
     
    if(node){
         var chks = node.getElementsByTagName("INPUT");
         for(var i=0;i<chks.length;i++){
              chks[i].checked = chkstate;
          }
      }
 } 
