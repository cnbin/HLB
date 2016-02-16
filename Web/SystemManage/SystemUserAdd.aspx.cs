using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class SystemManage_SystemUserAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZWL.Common.PublicMethod.CheckSession();
            //设置上传的附件为空
            ZWL.Common.PublicMethod.SetSessionValue("WenJianList","");
            ReturnInput.Value = Request.UrlReferrer.ToString() == null ? "../SystemManage/SystemUser.aspx" : Request.UrlReferrer.ToString();
        }
    }
   

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        //string FileNameStr = ZWL.Common.PublicMethod.UploadFileIntoDir(this.FileUpload1, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName));
        //if (ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Trim() == "")
        //{
        //    ZWL.Common.PublicMethod.SetSessionValue("WenJianList", FileNameStr);
        //}
        //else
        //{
        //    ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList") + "|" + FileNameStr);            
        //}
        //ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        //    {
        //        if (this.CheckBoxList1.Items[i].Selected==true)
        //        {
        //            ZWL.Common.PublicMethod.SetSessionValue("WenJianList", ZWL.Common.PublicMethod.GetSessionValue("WenJianList").Replace(this.CheckBoxList1.Items[i].Value, "").Replace("||", "|"));                                       
        //        }
        //    }
        //    ZWL.Common.PublicMethod.BindDDL(this.CheckBoxList1, ZWL.Common.PublicMethod.GetSessionValue("WenJianList"));
        //}
        //catch
        //{ }
    }
    private string getDictionaryData(Dictionary<string, object> data)
    {
        string ret = null;
        foreach (KeyValuePair<string, object> item in data)
        {
            if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
            {
                ret += "{";
                ret += getDictionaryData((Dictionary<string, object>)item.Value);
                ret += "};";
            }
            else
            {
                ret += (item.Value == null ? "null" : item.Value.ToString());
            }
        }
        return ret;
    }
    protected void btn_Sub_Click(object sender, EventArgs e) {
        //判断是否超过最大用户限制
        if(ZWL.Common.PublicMethod.IFExists("UserName", "ERPUser", 0, this.TextBox1.Text) == true) {
            if(ZWL.Common.PublicMethod.IFExists("Serils", "ERPUser", 0, this.TextBox4.Text) == true) {
                ZWL.BLL.ERPUser MyBuMen = new ZWL.BLL.ERPUser();
                MyBuMen.UserName = this.TextBox1.Text;
                MyBuMen.UserPwd = ZWL.Common.DEncrypt.DESEncrypt.Encrypt(this.TextBox2.Text);
                MyBuMen.TrueName = this.TextBox3.Text;
                MyBuMen.Serils = this.TextBox4.Text;
                MyBuMen.Department = this.TextBox5.Text;
                MyBuMen.JiaoSe = this.TextBox6.Text;
                MyBuMen.ZhiWei = this.TextBox7.Text;
                MyBuMen.ZaiGang = this.TextBox8.Text;
                MyBuMen.EmailStr = this.TextBox9.Text;
                MyBuMen.IfLogin = this.RadioButtonList1.SelectedItem.Text;
                MyBuMen.Sex = this.TextBox10.Text;
                MyBuMen.BackInfo = this.TextBox11.Text;
                //MyBuMen.BirthDay = this.TextBox12.Text;
                //MyBuMen.MingZu = this.TextBox13.Text;
                //MyBuMen.SFZSerils = this.TextBox14.Text;
                //MyBuMen.HunYing = this.TextBox15.Text;
                //MyBuMen.ZhengZhiMianMao = this.TextBox16.Text;
                //MyBuMen.JiGuan = this.TextBox17.Text;
                //MyBuMen.HuKou = this.TextBox18.Text;
                //MyBuMen.XueLi = this.TextBox19.Text;
                //MyBuMen.ZhiCheng = this.TextBox20.Text;
                //MyBuMen.BiYeYuanXiao = this.TextBox21.Text;
                //MyBuMen.ZhuanYe = this.TextBox22.Text;
                //MyBuMen.CanJiaGongZuoTime = this.TextBox23.Text;
                //MyBuMen.JiaRuBenDanWeiTime = this.TextBox24.Text;
                MyBuMen.JiaTingDianHua = this.TextBox25.Text;
                //MyBuMen.JiaTingAddress = this.TextBox26.Text;
                //MyBuMen.GangWeiBianDong = this.TextBox27.Text;
                //MyBuMen.JiaoYueBeiJing = this.TextBox28.Text;
                //MyBuMen.GongZuoJianLi = this.TextBox29.Text;
                //MyBuMen.SheHuiGuanXi = this.TextBox30.Text;
                //MyBuMen.JiangChengJiLu = this.TextBox31.Text;
                //MyBuMen.ZhiWuQingKuang = this.TextBox32.Text;
                //MyBuMen.PeiXunJiLu = this.TextBox33.Text;
                //MyBuMen.DanBaoJiLu = this.TextBox34.Text;
                //MyBuMen.NaoDongHeTong = this.TextBox35.Text;
                //MyBuMen.SheBaoJiaoNa = this.TextBox36.Text;
                //MyBuMen.TiJianJiLu = this.TextBox37.Text;
                //MyBuMen.BeiZhuStr = this.TextBox38.Text;
                MyBuMen.FuJian = ZWL.Common.PublicMethod.GetSessionValue("WenJianList");
                int UserID = MyBuMen.Add();
                #region 生成Voip
                string ACCOUNT_SID = "aaf98f894ff91386014ffacfd8ca02c4";
                string AUTH_TOKEN = "89a0d9b29af1480e96ec8e23486e7ee7";
                string APP_ID = "8a48b5514ff923b4014ffad344e40684";
                string ret = null;
                CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
                ZWL.BLL.ERPUser User = new ZWL.BLL.ERPUser();
                ZWL.BLL.AccountInfo AccountInfo = new ZWL.BLL.AccountInfo();
                //ip格式如下，不带https://
                if(UserID > 0) {
                    bool isInit = api.init("app.cloopen.com", "8883");
                    api.setAccount(ACCOUNT_SID, AUTH_TOKEN);
                    api.setAppId(APP_ID);
                    try {
                        if(isInit) {
                            Dictionary<string, object> retData = api.CreateSubAccount(this.TextBox1.Text);
                            ret = getDictionaryData(retData);
                        }
                        else {
                            ret = "初始化失败";
                        }
                    }
                    catch(Exception exc) {
                        ret = exc.Message;
                    }
                    finally {
                        BLLHelper.PassDataInsert passData = JsonHelper.JsonToObject<BLLHelper.PassDataInsert>(ret);
                        if(passData.statusCode != "111150") {
                            string voip = passData.SubAccount.voipAccount;
                            string subAccountSid = passData.SubAccount.subAccountSid;
                            User.ID = UserID;
                            User.VoipAccount = passData.SubAccount.voipAccount;
                            User.AddVoip();
                            AccountInfo.CreateDate = DateTime.Now;
                            AccountInfo.DateCreated = Convert.ToDateTime(passData.SubAccount.dateCreated);
                            AccountInfo.VoipAccount = passData.SubAccount.voipAccount;
                            AccountInfo.VoipPwd = passData.SubAccount.voipPwd;
                            AccountInfo.SubAccountSid = passData.SubAccount.subAccountSid;
                            AccountInfo.SubToken = passData.SubAccount.subToken;
                            AccountInfo.Add();
                        }
                    }
                }
                #endregion
                //写系统日志
                ZWL.BLL.ERPRiZhi MyRiZhi = new ZWL.BLL.ERPRiZhi();
                MyRiZhi.UserName = ZWL.Common.PublicMethod.GetSessionValue("UserName");
                MyRiZhi.DoSomething = "用户添加新用户(" + this.TextBox1.Text + ")";
                MyRiZhi.IpStr = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                MyRiZhi.Add();
                Response.Write("<script>alert('用户信息添加成功！');window.location.href='SystemUser.aspx';</script>");
                //ZWL.Common.MessageBox.ShowAndRedirect(this, "用户信息添加成功！", "SystemUser.aspx");
            }
            else {
                Response.Write("<script>alert('该用户编号已经存在，请更改其他用户编号！');</script>");
                //ZWL.Common.MessageBox.Show(this, "该用户编号已经存在，请更改其他用户编号！");
            }
        }
        else {
            Response.Write("<script>alert('该用户名已经存在，请更改其他用户名！');</script>");
            //ZWL.Common.MessageBox.Show(this, "该用户名已经存在，请更改其他用户名！");
        }
    }

   
}