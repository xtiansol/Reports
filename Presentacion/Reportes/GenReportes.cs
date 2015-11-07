using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace Presentacion.Reportes
{
    public class GenReportes
    {
        public void ExportToWord(DataTable dataTable, HttpResponse response)
        {
            // Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
            response.Clear();
            response.Buffer = true;
            response.AddHeader("content-disposition", "attachment;filename=DataTable.doc");
            response.Charset = "";
            response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            response.Output.Write(sw.ToString());
            response.Flush();
            response.End();
        }

        public void ExportToExcel(DataTable dataTable, HttpResponse response)
        {
            // Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
            response.Clear();
            response.Buffer = true;
            response.AddHeader("content-disposition", "attachment;filename=DataTable.xls");
            response.Charset = "";
            response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; (i
                        <= (GridView1.Rows.Count - 1)); i++)
            {
                // Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }

            GridView1.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode{mso-number-format:\\@;}</style>";
            response.Write(style);
            response.Output.Write(sw.ToString());
            response.Flush();
            response.End();
        }

        public void ExportToPDF(DataTable dataTable, ref GridView GridView1, HttpResponse response)
        {
            // Create a dummy GridView
            // Dim GridView1 As New GridView()
            // GridView1.AllowPaging = False
            // GridView1.DataSource = dataTable
            // GridView1.DataBind()
            response.ContentType = "application/pdf";
            response.AddHeader("content-disposition", "attachment;filename=DataTable.pdf");
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            response.Write(pdfDoc);
            response.End();
        }

        public void ExportToCSV(DataTable dataTable, HttpResponse response)
        {
            response.Clear();
            response.Buffer = true;
            response.AddHeader("content-disposition",
                "attachment;filename=DataTable.csv");
            response.Charset = "";
            response.ContentType = "application/text";


            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dataTable.Columns.Count; k++)
            {
                //add separator
                sb.Append(dataTable.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int k = 0; k < dataTable.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dataTable.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            response.Output.Write(sb.ToString());
            response.Flush();
            response.End();
        }
    }
}