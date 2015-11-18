using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Reportes
{
    public partial class frmGenRep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String nombreRep = Request.QueryString["nomRep"];
            String tipoArch = Request.QueryString["tipo"];

            if (nombreRep != null && nombreRep != "")
            {
                if (tipoArch != null && tipoArch != "")
                {
                    if (tipoArch == "PDF")
                    {
                        //generaPDF(idNombreReporte.Text);
                        generaPDF(nombreRep);
                    }
                    else if (tipoArch == "XLS")
                    {
                        generaXLS(nombreRep);
                    }

                }
                else
                {
                    Response.Write("<script language=javascript>alert('Ingrese el nombre del reporte...');</script>");
                }
            }
            else
            {

                String idHistorico = Request.QueryString["rep"];

                if (idHistorico != null && tipoArch != null)
                {
                    ArrayList regHist = ServiciosGen.getHistoricoById(idHistorico);
                    if (regHist != null && regHist.Count > 0)
                    {
                        if (tipoArch == "PDF")
                        {
                            generaPDF((ArrayList)regHist[0]);
                        }
                        else if (tipoArch == "XLS")
                        {
                            generaXLS((ArrayList)regHist[0]);
                        }

                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('No existe identificador');</script>");
                    }

                }
                else
                {
                    Response.Write("<script language=javascript>alert('Datos invalidos');</script>");
                }

            }

        }

        protected void generaPDF(string tituloRep)
        {
            SendOutPDF(new CustomReports().CreatePDF("Reporte: " + tituloRep, obtienDatosTablaArray()), tituloRep);
        }

        protected void generaPDF(ArrayList reg)
        {
            SendOutPDF(new CustomReports().CreatePDF("Reporte: " + (string)reg[1], obtienDatosTablaArray((string)reg[2], (string)reg[3])), (string)reg[1]);
        }


        //----------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Sends a Stream of bytes to Client as a PDF file
        /// </summary>
        /// <param name="PDFData">Stream containing bytes</param>
        protected void SendOutPDF(System.IO.MemoryStream PDFData, String tituloRep)
        {

            // Clear response content & headers
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.Charset = string.Empty;
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.AddHeader("Content-Disposition",
                "attachment;filename=" + tituloRep.Replace(" ", "").Replace(":", "-") + ".pdf");
            Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }


        protected void generaXLS(string reporte)
        {
            GridView GridView2 = new GridView();
            GridView2.AllowPaging = false;
            GridView2.DataSource = obtienDatosTabla();
            GridView2.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + reporte + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; (i
                        <= (GridView2.Rows.Count - 1)); i++)
            {
                // Apply text style to each Row
                GridView2.Rows[i].Attributes.Add("class", "textmode");
            }

            GridView2.RenderControl(hw);
            string style = "<style> .textmode{mso-number-format:\\@;}</style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        //Genera Reporte en XLS
        protected void generaXLS(ArrayList reg)
        {
            generaXLS((string)reg[1], (string)reg[2], (string)reg[3]);
        }

        protected void generaXLS(string reporte, string campos, string sql)
        {
            GridView GridView2 = new GridView();
            GridView2.AllowPaging = false;
            GridView2.DataSource = obtienDatosTabla(campos, sql);
            GridView2.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + reporte + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; (i
                        <= (GridView2.Rows.Count - 1)); i++)
            {
                // Apply text style to each Row
                GridView2.Rows[i].Attributes.Add("class", "textmode");
            }

            GridView2.RenderControl(hw);
            string style = "<style> .textmode{mso-number-format:\\@;}</style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        //-----------------------------------------------------------------------------------

        protected ArrayList obtienDatosTablaArray()
        {
            ArrayList dt = new ArrayList();
            int cont = 0;

            //Relaciones tablas y alias seleccionados
            ArrayList tablasSel = (ArrayList)Session["tablasSel"];
            ArrayList aliasTablasSel = (ArrayList)Session["aliasTablasSel"];



            //Relaciones tablas, alias y campos seleccionados
            ArrayList tablasCampos = (ArrayList)Session["tablasCampos"];
            ArrayList aliasCampos = (ArrayList)Session["aliasCampos"];
            ArrayList campos = (ArrayList)Session["campos"];

            if (tablasSel != null && aliasTablasSel != null && tablasCampos != null && aliasCampos != null && campos != null)
            {
                ArrayList headCol = new ArrayList();
                ArrayList sqlGen = new ArrayList();

                while (cont < campos.Count)
                {
                    headCol.Add((string)campos[cont]);
                    cont = (cont + 1);
                }

                sqlGen.Add(ServiciosGen.toStringArrayList(campos, ","));

                dt.Add(headCol);

                //  Total number of rows.
                int rowCnt = 0;
                //  Current row count
                int rowCtr = 0;
                //  Total number of cells (columns).
                int cellCtr = 0;
                //  Current cell counter.
                int cellCnt = 0;
                rowCnt = 10;
                cellCnt = campos.Count;

                ArrayList filtros = (ArrayList)Session["filtrosConAlias"];

                ArrayList camposRel = ServiciosGen.generaCamposRelacion(tablasSel, aliasTablasSel, ServiciosGen.obtieneCamosRelacion(tablasSel));

                ArrayList resp = ServiciosGen.reporteDinamico(ServiciosGen.joinNombreAlias(campos, aliasCampos, "."), tablasSel, aliasTablasSel, camposRel, filtros, ref sqlGen);
                Session["listGenReporte"] = sqlGen;
                if (resp != null)
                {
                    ArrayList datCol = new ArrayList();
                    for (rowCtr = 0; (rowCtr < resp.Count); rowCtr++)
                    {
                        datCol = new ArrayList();

                        ArrayList camposRec = (ArrayList)resp[rowCtr];
                        for (cellCtr = 0; cellCtr < camposRec.Count; cellCtr++)
                        {
                            datCol.Add(camposRec[cellCtr]);
                        }

                        //  Add new row to table.
                        dt.Add(datCol);
                    }
                }
            }

            return dt;
        }


        protected DataTable obtienDatosTabla()
        {
            DataTable dt = new DataTable();
            int cont = 0;

            //Relaciones tablas y alias seleccionados
            ArrayList tablasSel = (ArrayList)Session["tablasSel"];
            ArrayList aliasTablasSel = (ArrayList)Session["aliasTablasSel"];



            //Relaciones tablas, alias y campos seleccionados
            ArrayList tablasCampos = (ArrayList)Session["tablasCampos"];
            ArrayList aliasCampos = (ArrayList)Session["aliasCampos"];
            ArrayList campos = (ArrayList)Session["campos"];

            if (tablasSel != null && aliasTablasSel != null && tablasCampos != null && aliasCampos != null && campos != null)
            {
                ArrayList sqlGen = new ArrayList();
                while (cont < campos.Count)
                {
                    dt.Columns.Add(new DataColumn((string)campos[cont], typeof(string)));
                    cont = (cont + 1);
                }

                sqlGen.Add(ServiciosGen.toStringArrayList(campos, ","));

                //  Total number of rows.
                int rowCnt = 0;
                //  Current row count
                int rowCtr = 0;
                //  Total number of cells (columns).
                int cellCtr = 0;
                //  Current cell counter.
                int cellCnt = 0;
                rowCnt = 10;
                cellCnt = campos.Count;

                ArrayList filtros = (ArrayList)Session["filtrosConAlias"];

                ArrayList camposRel = ServiciosGen.generaCamposRelacion(tablasSel, aliasTablasSel, ServiciosGen.obtieneCamosRelacion(tablasSel));
                ArrayList resp = ServiciosGen.reporteDinamico(ServiciosGen.joinNombreAlias(campos, aliasCampos, "."), tablasSel, aliasTablasSel, camposRel, filtros, ref sqlGen);
                Session["listGenReporte"] = sqlGen;
                if (resp != null)
                {
                    for (rowCtr = 0; (rowCtr < resp.Count); rowCtr++)
                    {
                        ArrayList camposRec = (ArrayList)resp[rowCtr];
                        DataRow tRow = dt.NewRow();
                        for (cellCtr = 0; cellCtr < camposRec.Count; cellCtr++)
                        {
                            tRow[cellCtr] = camposRec[cellCtr];
                            // cellCtr = cellCtr + 1
                        }

                        //  Add new row to table.
                        dt.Rows.Add(tRow);
                    }
                }
            }

            return dt;
        }
        //-----------------------------------------------------------------------------------
        protected ArrayList obtienDatosTablaArray(string headCampos, string sql)
        {
            ArrayList dt = new ArrayList();
            int cont = 0;


            string[] words1 = headCampos.Split(',');
            //string[] words4 = aliasTablasCampoSel.Split('|');

            ArrayList headCol = new ArrayList();
            for (cont = 0; cont < words1.Length; cont++)
            {
                String campo = words1[cont];

                if (campo != "")
                {
                    headCol.Add(campo);
                }
            }
            dt.Add(headCol);
            ArrayList resp = ServiciosGen.getResultadoSQL(sql);
            if (resp != null)
            {
                ArrayList datCol = new ArrayList();
                for (int rowCtr = 0; (rowCtr < resp.Count); rowCtr++)
                {
                    datCol = new ArrayList();

                    ArrayList camposRec = (ArrayList)resp[rowCtr];
                    for (int cellCtr = 0; cellCtr < camposRec.Count; cellCtr++)
                    {
                        datCol.Add(camposRec[cellCtr]);
                    }

                    //  Add new row to table.
                    dt.Add(datCol);
                }
            }
            return dt;
        }


        protected DataTable obtienDatosTabla(string headCampos, string sql)
        {
            DataTable dt = new DataTable();
            int cont = 0;


            string[] words1 = headCampos.Split(',');
            //string[] words4 = aliasTablasCampoSel.Split('|');

            ArrayList headCol = new ArrayList();
            for (cont = 0; cont < words1.Length; cont++)
            {
                String campo = words1[cont];

                if (campo != "")
                {
                    headCol.Add(campo);
                    dt.Columns.Add(new DataColumn(campo, typeof(string)));
                }
            }

            ArrayList resp = ServiciosGen.getResultadoSQL(sql);
            if (resp != null)
            {
                ArrayList datCol = new ArrayList();
                for (int rowCtr = 0; (rowCtr < resp.Count); rowCtr++)
                {
                    datCol = (ArrayList)resp[rowCtr];

                    DataRow tRow = dt.NewRow();
                    for (int cellCtr = 0; cellCtr < datCol.Count; cellCtr++)
                    {
                        tRow[cellCtr] = datCol[cellCtr];
                    }
                    //  Add new row to table.
                    dt.Rows.Add(tRow);
                }
            }
            return dt;

        }
    }
}