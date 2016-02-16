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
using ZWL.DBUtility;
using System.Data.SqlClient;

public partial class OnlineCount : System.Web.UI.Page
{
    public string TiXingJianGe = "30";
    public string TanChuStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ͳһģ�鷢������
            SendTXMail("ERPCustomInfo", "YuJiTiXing", "CustomName", "��", "���趨�Ŀͻ���Ϣ����ʱ���ѵ���");
            SendTXMail("ERPLinkMan", "BirthDay", "NameStr", "��", "���Ŀͻ���ϵ�����������ѵ���");
            SendTXMail("ERPHuiYuan", "ChuShengDate", "NameStr", "��", "���Ļ�Ա���������ѵ���");
            SendTXMail("ERPLinkLog", "CusBakE", "LinkTitle", "��", "���趨�Ŀͻ���ϵ��¼����ʱ���ѵ���");
            SendTXMail("ERPSongYang", "CusBakE", "SongYangLiaoHao", "��", "���趨�Ŀͻ�������¼����ʱ���ѵ���");
            SendTXMail("ERPContract", "TiXingDate", "HeTongName", "��", "���趨�����ۺ�ͬ����ʱ���ѵ���");
            SendTXMail("ERPBuyOrder", "TiXingDate", "OrderName", "��", "���趨�Ĳɹ���������ʱ���ѵ���");
            SendTXMail("ERPSupplyLink", "ShengRi", "LinkManName", "��", "���Ĺ�Ӧ����ϵ�����������ѵ���");
            SendTXMail("ERPProject", "TiXingDate", "ProjectName", "��", "���趨����Ŀ��Ϣ����ʱ���ѵ���");

            //����Ʒ����������
            CheckKuCun();



            //�����Ҫ������Ϣ���ѵ���---�ճ̰���
            CheckRiCheng();
            //�����Ҫ������Ϣ���ѵ���---�����豸
            CheckYiQi();
            //�����Ҫ������Ϣ���ѵ���---���շ���
            CheckBX();

            //�����Ҫ������Ϣ���ѵ���---��ʱ���ʹ߰�����
            CheckChaoShi();

            //CheckRW();


            ZWL.Common.PublicMethod.CheckSession();
            //ˢ�µ�ǰ�û��ļ���ʱ��
            DbHelperSQL.ExecuteSQL("update ERPUser set ActiveTime=getdate() where UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "'");
            this.HyperLink1.Text = DbHelperSQL.GetSHSL("select count(*) from ERPUser where datediff(minute,ActiveTime,getdate())<20");

            //������ѵļ��ʱ�䣬�Ƿ�����
            ZWL.BLL.ERPTiXing MyModel = new ZWL.BLL.ERPTiXing();
            MyModel.GetModel(int.Parse(ZWL.Common.PublicMethod.GetSessionValue("UserID")));
            TiXingJianGe = MyModel.TiXingTime;

            //�Ƿ���Ҫ����
            string IFTanChu = MyModel.IfTiXing;
            //��ȡ���ʼ�����
            int NewMailCount = int.Parse(ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select count(*) from ERPLanEmail where ToUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and EmailState='δ��'"));
            this.HyperLink2.Text = NewMailCount.ToString();

            //��Ҫ���ѣ��������Ѵ���
            if (IFTanChu.Trim() == "��")
            {
                if (NewMailCount > 0)
                {
                    TanChuStr = "<script language=\"javascript\">var num=Math.random();var abc=screen.height-250;focusid=setTimeout(\"focus();window.showModelessDialog('SmsShow.aspx?rad=\" + num + \"','','scroll:1;status:0;help:0;resizable:0;dialogLeft:3px;dialogTop:\"+abc+\"px;dialogWidth:350px;dialogHeight:200px')\",0000)</script>";
                }
            }
        }
        catch
        { }

        if (Request.Params["Online"] != null)
        {
            GetOnlineCount();
        }
    }

    public void GetOnlineCount()
    {
        DbHelperSQL.ExecuteSQL("update ERPUser set ActiveTime=getdate() where UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "'");
        string count = DbHelperSQL.GetSHSL("select count(*) from ERPUser where datediff(minute,ActiveTime,getdate())<20");
        int NewMailCount = int.Parse(ZWL.DBUtility.DbHelperSQL.GetSHSLInt("select count(*) from ERPLanEmail where ToUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and EmailState='δ��'"));
        //������ѵļ��ʱ�䣬�Ƿ�����
        ZWL.BLL.ERPTiXing MyModel = new ZWL.BLL.ERPTiXing();
        MyModel.GetModel(int.Parse(ZWL.Common.PublicMethod.GetSessionValue("UserID")));
        int TXSJ = int.Parse(MyModel.TiXingTime)*1000;
        Response.Write(count + ',' + NewMailCount.ToString() + ',' + TXSJ + ',' + MyModel.IfTiXing);
        Response.End();
    }

    //����ͳһ�Ե������ʼ�
    protected void SendTXMail(string TableName, string LieName, string TitleLieName, string IFOnlyDate, string MailContent)
    {
        string ToDayStr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();

        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from " + TableName);
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            if (IFOnlyDate == "��")
            {
                ToDayStr = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                if (ZWL.Common.PublicMethod.LongToShortStr(MyDataSet.Tables[0].Rows[j][LieName].ToString(), 5) == ToDayStr)
                {
                    ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
                    MyModel.EmailContent = MailContent + "(" + MyDataSet.Tables[0].Rows[j][TitleLieName].ToString() + ")";
                    MyModel.EmailState = "δ��";
                    MyModel.EmailTitle = MailContent + "(" + MyDataSet.Tables[0].Rows[j][TitleLieName].ToString() + ")";
                    MyModel.FromUser = "ϵͳ��Ϣ";
                    MyModel.FuJian = "";
                    MyModel.TimeStr = DateTime.Now;
                    MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
                    if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
                    {
                        MyModel.Add();
                    }
                }
            }
            else
            {
                if (MyDataSet.Tables[0].Rows[j][LieName].ToString() == ToDayStr)
                {
                    ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
                    MyModel.EmailContent = MailContent + "(" + MyDataSet.Tables[0].Rows[j][TitleLieName].ToString() + ")";
                    MyModel.EmailState = "δ��";
                    MyModel.EmailTitle = MailContent + "(" + MyDataSet.Tables[0].Rows[j][TitleLieName].ToString() + ")";
                    MyModel.FromUser = "ϵͳ��Ϣ";
                    MyModel.FuJian = "";
                    MyModel.TimeStr = DateTime.Now;
                    MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
                    if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
                    {
                        MyModel.Add();
                    }
                }
            }
        }
    }

    protected void CheckChaoShi()
    {
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPNWorkToDo where getdate()>LateTime and StateNow='���ڰ���' and  ','+ShenPiUserList+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' and ','+OKUserList+',' not like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%' order by ID asc");
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
            MyModel.EmailContent = "���д��칤��δ����ʱ���ѳ�ʱ����(" + MyDataSet.Tables[0].Rows[j]["WorkName"].ToString() + "-������ˮ�ţ�" + MyDataSet.Tables[0].Rows[j]["ID"].ToString() + ")";
            MyModel.EmailState = "δ��";
            MyModel.EmailTitle = "���д��칤��δ����ʱ���ѳ�ʱ��(" + MyDataSet.Tables[0].Rows[j]["WorkName"].ToString() + "-������ˮ�ţ�" + MyDataSet.Tables[0].Rows[j]["ID"].ToString() + ")";
            MyModel.FromUser = "ϵͳ��Ϣ";
            MyModel.FuJian = "";
            MyModel.TimeStr = DateTime.Now;
            MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
            {
                MyModel.Add();
            }
        }
    }

    //����Ʒ���
    protected void CheckKuCun()
    {
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPProduct  where NowKuCun<=KuCunBaoJing  order by ID asc");
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
            MyModel.EmailContent = "���Ĳ�Ʒ��治�㣬�뼰ʱ����(" + MyDataSet.Tables[0].Rows[j]["ProductName"].ToString() + ")";
            MyModel.EmailState = "δ��";
            MyModel.EmailTitle = "���Ĳ�Ʒ��治�㣬�뼰ʱ����(" + MyDataSet.Tables[0].Rows[j]["ProductName"].ToString() + ")";
            MyModel.FromUser = "ϵͳ��Ϣ";
            MyModel.FuJian = "";
            MyModel.TimeStr = DateTime.Now;
            MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
            {
                MyModel.Add();
            }
        }
    }

    protected void CheckBX()
    {
        string ToDayStr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPCarBaoXian where UserName='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "'  and TiXingDate='" + ToDayStr + "'");
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
            MyModel.EmailContent = "�����ƶ��ĳ������շ�������ʱ�䵽��(" + MyDataSet.Tables[0].Rows[j]["CarName"].ToString() + "--" + MyDataSet.Tables[0].Rows[j]["FeiYongName"].ToString() + ")";
            MyModel.EmailState = "δ��";
            MyModel.EmailTitle = "�����ƶ��ĳ������շ�������ʱ�䵽��(" + MyDataSet.Tables[0].Rows[j]["CarName"].ToString() + "--" + MyDataSet.Tables[0].Rows[j]["FeiYongName"].ToString() + ")";
            MyModel.FromUser = "ϵͳ��Ϣ";
            MyModel.FuJian = "";
            MyModel.TimeStr = DateTime.Now;
            MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
            {
                MyModel.Add();
            }
        }
    }

    protected void CheckRW()
    {
        string ToDayStr = DateTime.Now.ToString();
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPTaskFP where  SFFK='��'  and FKSJ<='" + ToDayStr + "'");

        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            SendMainAndSms.SendMobileMessage("����Ҫ����������������ʱ���ѵ����뾡�췴����(" + MyDataSet.Tables[0].Rows[j]["TaskTitle"].ToString() + "--" + MyDataSet.Tables[0].Rows[j]["FromUser"].ToString() + "����:" + MyDataSet.Tables[0].Rows[j]["TaskContent"].ToString() + ")", MyDataSet.Tables[0].Rows[j]["ToUserList"].ToString() + "," + MyDataSet.Tables[0].Rows[j]["FromUser"].ToString());
            ZWL.DBUtility.DbHelperSQL.Query("update ERPTaskFP set SFFK='�ѷ�' where ID =" + MyDataSet.Tables[0].Rows[j]["ID"].ToString() + " ");
        }
    }


    protected void CheckSK()
    {
        string ToDayStr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPRecive where CreateUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and NowState='S' and TiXingDate='" + ToDayStr + "'");
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
            MyModel.EmailContent = "�����ƶ����տ�ƻ�����ʱ���ѵ���(" + MyDataSet.Tables[0].Rows[j]["HeTongName"].ToString() + "--" + MyDataSet.Tables[0].Rows[j]["QianYueKeHu"].ToString() + ")";
            MyModel.EmailState = "δ��";
            MyModel.EmailTitle = "�����ƶ����տ�ƻ�����ʱ���ѵ���(" + MyDataSet.Tables[0].Rows[j]["HeTongName"].ToString() + "--" + MyDataSet.Tables[0].Rows[j]["QianYueKeHu"].ToString() + ")";
            MyModel.FromUser = "ϵͳ��Ϣ";
            MyModel.FuJian = "";
            MyModel.TimeStr = DateTime.Now;
            MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
            {
                MyModel.Add();
            }
        }
    }

    protected void CheckFK()
    {
        string ToDayStr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPRecive where CreateUser='" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "' and NowState='F' and TiXingDate='" + ToDayStr + "'");
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
            MyModel.EmailContent = "�����ƶ��ĸ���ƻ�����ʱ���ѵ���(" + MyDataSet.Tables[0].Rows[j]["HeTongName"].ToString() + "--" + MyDataSet.Tables[0].Rows[j]["QianYueKeHu"].ToString() + ")";
            MyModel.EmailState = "δ��";
            MyModel.EmailTitle = "�����ƶ��ĸ���ƻ�����ʱ���ѵ���(" + MyDataSet.Tables[0].Rows[j]["HeTongName"].ToString() + "--" + MyDataSet.Tables[0].Rows[j]["QianYueKeHu"].ToString() + ")";
            MyModel.FromUser = "ϵͳ��Ϣ";
            MyModel.FuJian = "";
            MyModel.TimeStr = DateTime.Now;
            MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
            {
                MyModel.Add();
            }
        }
    }

    protected void CheckYiQi()
    {
        string ToDayStr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPSheBei where ','+TiXingUser+',' like '%," + ZWL.Common.PublicMethod.GetSessionValue("UserName") + ",%'  and TiXingDate='" + ToDayStr + "'");
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
            MyModel.EmailContent = "�����ƶ��������豸��Դ����ʱ�䵽��(" + MyDataSet.Tables[0].Rows[j]["SheBeiName"].ToString() + ")";
            MyModel.EmailState = "δ��";
            MyModel.EmailTitle = "�����ƶ��������豸��Դ����ʱ�䵽��(" + MyDataSet.Tables[0].Rows[j]["SheBeiName"].ToString() + ")";
            MyModel.FromUser = "ϵͳ��Ϣ";
            MyModel.FuJian = "";
            MyModel.TimeStr = DateTime.Now;
            MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
            {
                MyModel.Add();
            }
        }
    }

    protected void CheckRiCheng()
    {
        string ToDayStr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
        DataSet MyDataSet = ZWL.DBUtility.DbHelperSQL.GetDataSet("select * from ERPAnPai where UserName= '" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "'  and TimeTiXing='" + ToDayStr + "'");
        for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
        {
            ZWL.BLL.ERPLanEmail MyModel = new ZWL.BLL.ERPLanEmail();
            MyModel.EmailContent = "�����ƶ����ճ̰�������ʱ�䵽��(" + MyDataSet.Tables[0].Rows[j]["TitleStr"].ToString() + ")";
            MyModel.EmailState = "δ��";
            MyModel.EmailTitle = "�����ƶ����ճ̰�������ʱ�䵽��(" + MyDataSet.Tables[0].Rows[j]["TitleStr"].ToString() + ")";
            MyModel.FromUser = "ϵͳ��Ϣ";
            MyModel.FuJian = "";
            MyModel.TimeStr = DateTime.Now;
            MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
            if (ZWL.Common.PublicMethod.IFExists("EmailTitle", "ERPLanEmail", 0, MyModel.EmailTitle) == true)
            {
                MyModel.Add();
            }
        }
    }
}
