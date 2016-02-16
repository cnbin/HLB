using System;
using System.Collections.Generic;
using System.Text;
using System.Web;



namespace ZWL.Common
{
    public  class  ExcelHelper
    {
        /// <summary>
        /// DataTable导出到Excel
        /// </summary>
        /// <param name="pData">DataTable</param>
        /// <param name="pFileName">导出文件名</param>
        /// <param name="pHeader">导出标题以|分割</param>
        public static void DataTableExcel(System.Data.DataTable pData, string pFileName, string pHeader)
        {
            System.Web.UI.WebControls.DataGrid dgExport = null;
            // 当前对话 
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            // IO用于导出并返回excel文件 
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;
            if (pData != null)
            {
                string UserAgent = curContext.Request.ServerVariables["http_user_agent"].ToLower();
                if (UserAgent.IndexOf("firefox") == -1)//火狐浏览器
                {
                    pFileName = HttpUtility.UrlEncode(pFileName, System.Text.Encoding.UTF8);
                }
                curContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + pFileName + ".xls");
                curContext.Response.ContentType = "application/vnd.ms-excel";
                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
                // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid 
                dgExport = new System.Web.UI.WebControls.DataGrid();
                
                dgExport.DataSource = pData.DefaultView;

                dgExport.AllowPaging = false;
                dgExport.ShowHeader = false;//去掉标题
                dgExport.Attributes.Add("style", "vnd.ms-excel.numberformat:@;font-size:15px;");
                for(int i = 0; i < dgExport.Items.Count; i++) {
                    dgExport.Items[i].Width = dgExport.Items[i].ToString().Length;
                }
                dgExport.ItemStyle.Height = 36;
                
                //spanRow(dgExport);
                dgExport.DataBind();
                
                string[] arrHeader = pHeader.Split('|');
                string strHeader = "<table border=\"1\" style=\"font-weight:bold; font-size:15px; \"><tr>";
                foreach (string j in arrHeader)
                {
                    strHeader += "<td align='center' height='16px' >" + j.ToString() + "</td>";
                }
                strHeader += "</tr></table>";
                //int num = 1;
                //for (int i = 1; i < dgExport.Items.Count; i++)
                //{

                //    if (dgExport.Items[i].Cells[0].Text == "&nbsp;")//订单号等于空的情况下,此处是遍历行,获取行号i
                //    {
                //        num++;

                //        for (int j = 0; j < dgExport.Items[i].Cells.Count; j++)//寻找空行的列号,此处是遍历单元格
                //        {
                //            if (dgExport.Items[i].Cells[j].Text == "&nbsp;")
                //            {

                //                dgExport.Items[i].Cells[j].Visible = false;
                //                dgExport.Items[i - num + 1].Cells[j].RowSpan = num;
                //            }
                //        }

                //    }
                //    else { num = 1; }
                //}


                // 返回客户端 
                dgExport.RenderControl(htmlWriter);
                
                
                string strMeta = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=GB2312\"/>";
                string strWriter2 = strWriter.ToString().Replace("<tr", "<tr style=\"vertical-align:top\"");
                string OutExcel = strMeta + strHeader +strWriter2;
                curContext.Response.Write(OutExcel);
                curContext.Response.End();
            }
        }

       
       
    }
}
