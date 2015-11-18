using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace Presentacion.Reportes
{
    public partial class frmSelectTablas : System.Web.UI.Page
    {
        //static Label[] arregloLabels;
        //static TextBox[] arregloTextBoxs;
        //static DropDownList[] arregloCombos;
        static int contadorControles;
        static ArrayList campos = new ArrayList();
        static ArrayList camposFin = new ArrayList();

        //
        static ArrayList nomCamSel = new ArrayList();
        static ArrayList nomTaCamSel = new ArrayList();
        static ArrayList nomTaSel = new ArrayList();
        static ArrayList aliasTaSel = new ArrayList();
        static string tablaEnUso = "";
        static string aliasEnUso = "";
        static int idTablas = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            int cont = CamposSeleccionadosFin.Items.Count;

            if (!IsPostBack)
            {
                //arregloLabels = new Label[20];
                //arregloTextBoxs = new TextBox[20];
                //arregloCombos = new DropDownList[20];
                contadorControles = 0;
                //Listas relacionadas de filtros tablas, campos y alias seleccionadas
                Session["filtrosTablasAlias"] = new ArrayList();
                Session["filtrosSinAlias"] = new ArrayList();
                Session["filtrosConAlias"] = new ArrayList();
                Session["filtrosCampos"] = new ArrayList();

                //Listas tablas y alias filtros
                Session["tablasSel"] = new ArrayList();
                Session["aliasTablasSel"] = new ArrayList();

                //Listas relacionadas de tablas, campos y alias seleccionadas
                Session["tablasCampos"] = new ArrayList();
                Session["aliasCampos"] = new ArrayList();
                Session["campos"] = new ArrayList();

                //Query generado dinámico
                Session["listGenReporte"] = new ArrayList();

                //Tabla en uso
                Session["tablaEnUso"] = "";

            }
            else
            {

                ArrayList tablasSel = (ArrayList)Session["tablasSel"];
                ArrayList aliasTablasSel = (ArrayList)Session["aliasTablasSel"];

                if (tablasSel != null && aliasTablasSel != null)
                {
                    TablaBaseSel.Items.Clear();
                    foreach (string tabla in tablasSel)
                    {
                        TablaBaseSel.Items.Add(tabla);
                    }
                }


                string tablaEnUso = (string)Session["tablaEnUso"];
                if (tablaEnUso != null)
                {
                    ArrayList listCamposTabla = ServiciosGen.getCamposTablasBaseSQLServer(tablaEnUso);

                    if (listCamposTabla.Count > 0)
                    {
                        CamposSeleccionados.Items.Clear();
                        foreach (ArrayList lcampos in listCamposTabla)
                        {
                            foreach (string rcampo in lcampos)
                            {
                                CamposSeleccionados.Items.Add(rcampo);
                            }
                        }
                    }
                }


                ArrayList tablasCampos = (ArrayList)Session["tablasCampos"];
                ArrayList aliasCampos = (ArrayList)Session["aliasCampos"];
                ArrayList campos = (ArrayList)Session["campos"];


                if (tablasCampos != null && aliasCampos != null && campos != null)
                {
                    CamposSeleccionados.Items.Clear();
                    foreach (string campo in campos)
                    {
                        CamposSeleccionados.Items.Add(campo);
                    }
                }

                ArrayList filtrosFin = (ArrayList)Session["filtrosSinAlias"];
                if (filtrosFin != null)
                {
                    CamposSeleccionadosFin.Items.Clear();
                    foreach (string filtro in filtrosFin)
                    {
                        CamposSeleccionadosFin.Items.Add(filtro);
                    }
                }

            }
            principal();
        }

        protected void principal()
        {
            generaCampos();
            if ((TablaBaseSel.Items.Count <= 0))
            {
                // Se crea Collection para almacenar los datos recuperados de la consulta
                ArrayList colbd = new ArrayList();
                // Se invoca al servicio General getTablasBase
                colbd = ServiciosGen.getTablasBase();
                int cont = 0;
                while (cont < colbd.Count)
                {
                    ArrayList reg = new ArrayList();
                    reg = (ArrayList)colbd[cont];
                    TablasBD.Items.Add((string)reg[1]);
                    cont++;
                }

            }
        }

        protected void agregaFiltros()
        {
            camposFin.Clear();
            Panel1.Controls.Clear();
            Panel1.Controls.Add(new LiteralControl("<table border='1'> <tr><td>NOMBRE CAMPO</td><td>OPERADOR</td><td>VALOR</td></tr> "));
            for (int cont = 0; cont < CamposSeleccionados.Items.Count; cont++)
            {
                agregarFiltro(CamposSeleccionados.Items[cont].Value);
            }
            Panel1.Controls.Add(new LiteralControl("</table>"));
        }
        protected void agregarFiltro(string nombre)
        {
            try
            {
                int numeroRegistro = contadorControles;
                Label campoSel = new Label();
                campoSel.Text = nombre;
                //arregloLabels[numeroRegistro] = campoSel;

                TextBox nuevoTxt = new TextBox();
                nuevoTxt.ID = "txt" + nombre;
                nuevoTxt.Width = 100;
                //arregloTextBoxs[numeroRegistro] = nuevoTxt;
                DropDownList nuevoCmb = new DropDownList();
                nuevoCmb.ID = "cmb" + nombre;
                nuevoCmb.Items.Add("--Seleccione operador--");
                nuevoCmb.Items.Add("<");
                nuevoCmb.Items.Add(">");
                nuevoCmb.Items.Add(">=");
                nuevoCmb.Items.Add("<=");
                nuevoCmb.Items.Add("=");
                nuevoCmb.Items.Add("<>");
                nuevoCmb.SelectedIndex = 0;
                //arregloCombos[numeroRegistro] = nuevoCmb;
                HiddenField nuevoHidF = new HiddenField();
                nuevoHidF.ID = "hdf" + nombre;
                nuevoHidF.Value = nombre;
                AgregarControles(campoSel, nuevoTxt, nuevoCmb, nuevoHidF, numeroRegistro);
                contadorControles++;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void AgregarControles(Label nombreContr, TextBox txt, DropDownList cmb, HiddenField nhf, int cont)
        {
            try
            {
                /*
                pnlSeleccionarDatos.Controls.Add(nombreContr);
                pnlSeleccionarDatos.Controls.Add(new LiteralControl(" "));
                pnlSeleccionarDatos.Controls.Add(cmb);
                pnlSeleccionarDatos.Controls.Add(new LiteralControl(" "));
                pnlSeleccionarDatos.Controls.Add(txt);
                pnlSeleccionarDatos.Controls.Add(new LiteralControl(" "));
                */
                Panel1.Controls.Add(new LiteralControl("<tr>"));
                Panel1.Controls.Add(new LiteralControl("<td>"));
                Panel1.Controls.Add(nombreContr);
                Panel1.Controls.Add(new LiteralControl("</td><td>"));
                Panel1.Controls.Add(cmb);
                Panel1.Controls.Add(new LiteralControl("</td><td>"));
                Panel1.Controls.Add(txt);
                Panel1.Controls.Add(nhf);
                Panel1.Controls.Add(new LiteralControl("</td></tr>"));
            }
            catch (Exception ex)
            {
                return;
            }
        }


        protected void generaCampos()
        {
            int iCount = Request.QueryString.Count;
            //Recorremos cada uno de los valores recibidos
            for (int i = 1; i <= iCount; i++)
            {
                //Mandamos a escribir los valores a la pagina
                Response.Write(Request.QueryString[i - 1]);
            }
        }

        protected void AgregaTablaBase_Click(object sender, EventArgs e)
        {
            if ((TablasBD.Items.Count > 0) && (TablasBD.SelectedIndex > -1))
            {
                if (TablaBaseSel.Items.Count == 0)
                {
                    aliasTaSel.Clear();
                }
                CamposTalbaBaseSel.Items.Clear();
                if (aliasTaSel.IndexOf("alias" + TablasBD.SelectedItem.Text/*+idTablas*/) < 0)
                {
                    TablaBaseSel.Items.Add(TablasBD.SelectedItem.Text);
                    aliasTaSel.Add("alias" + TablasBD.SelectedItem.Text/*+idTablas*/);
                    idTablas++;
                    // TablasBD.Items.RemoveAt(TablasBD.SelectedIndex)
                    ArrayList colTBS = new ArrayList();
                    int cont = 0;
                    while ((cont < TablaBaseSel.Items.Count))
                    {
                        colTBS.Add(TablaBaseSel.Items[cont].Text);
                        cont++;
                    }

                    ArrayList colbd = new ArrayList();
                    colbd = ServiciosGen.getTablasRelacionadas(colTBS);
                    cont = 0;
                    TablasBD.Items.Clear();
                    while (cont < colbd.Count)
                    {
                        ArrayList reg = new ArrayList();
                        reg = (ArrayList)colbd[cont];
                        TablasBD.Items.Add((string)reg[1]);
                        cont++;
                    }
                }

            }
            agregaFiltros();
        }

        protected void QuitaTablaBase_Click(object sender, EventArgs e)
        {
            CamposTalbaBaseSel.Items.Clear();
            if ((TablaBaseSel.Items.Count > 0) && (TablaBaseSel.SelectedIndex > -1))
            {
                //Elimina relaciones de todas las listas
                string nombreTabla = TablaBaseSel.SelectedItem.Text;
                ArrayList listaTablasCampos = ((ArrayList)Session["tablasCampos"]);
                for (int index = 0; index < listaTablasCampos.Count; index++)
                {
                    if ((string)listaTablasCampos[index] == nombreTabla)
                    {
                        ArrayList filtrosTablasAlias = ((ArrayList)Session["filtrosTablasAlias"]);
                        for (int index2 = 0; index2 < filtrosTablasAlias.Count; index2++)
                        {
                            if ((string)filtrosTablasAlias[index2] == nombreTabla)
                            {
                                ((ArrayList)Session["filtrosTablasAlias"]).RemoveAt(index2);
                                ((ArrayList)Session["filtrosSinAlias"]).RemoveAt(index2);
                                ((ArrayList)Session["filtrosConAlias"]).RemoveAt(index2);
                                ((ArrayList)Session["filtrosCampos"]).RemoveAt(index2);
                                CamposSeleccionadosFin.Items.RemoveAt(index2);
                                filtrosTablasAlias = ((ArrayList)Session["filtrosTablasAlias"]);
                                index2 = -1;
                            }
                        }
                        ((ArrayList)Session["tablasCampos"]).RemoveAt(index);
                        ((ArrayList)Session["campos"]).RemoveAt(index);
                        ((ArrayList)Session["aliasCampos"]).RemoveAt(index);
                        CamposSeleccionados.Items.RemoveAt(index);
                        nomTaCamSel.RemoveAt(index);
                        index = -1;
                        listaTablasCampos = ((ArrayList)Session["tablasCampos"]);

                    }
                }

                TablasBD.Items.Add(TablaBaseSel.SelectedItem.Text);
                aliasTaSel.RemoveAt(TablaBaseSel.SelectedIndex);
                TablaBaseSel.Items.RemoveAt(TablaBaseSel.SelectedIndex);
                ArrayList colTBS = new ArrayList();
                int cont = 0;
                while (cont < TablaBaseSel.Items.Count)
                {
                    colTBS.Add(TablaBaseSel.Items[cont].Text);
                    cont++;
                }
                if (TablaBaseSel.Items.Count > 0)
                {  //Quitar condicíón cuando la tabla de relaciones este completa
                    ArrayList colbd = new ArrayList();
                    colbd = ServiciosGen.getTablasRelacionadas(colTBS);
                    cont = 0;
                    TablasBD.Items.Clear();
                    while ((cont < colbd.Count))
                    {
                        ArrayList reg = new ArrayList();
                        reg = (ArrayList)colbd[cont];
                        TablasBD.Items.Add((String)reg[1]);
                        cont++;
                    }
                }//Quitar condicíón cuando la tabla de relaciones este completa
                else//Quitar condicíón cuando la tabla de relaciones este completa
                {
                    nomCamSel.Clear();
                    nomTaCamSel.Clear();
                    nomTaSel.Clear();
                    aliasTaSel.Clear();
                    CamposSeleccionados.Items.Clear();
                    principal();
                }//Quitar condicíón cuando la tabla de relaciones este completa
            }
            else
            {
                aliasTaSel.Clear();
                nomTaCamSel.Clear();
            }
            agregaFiltros();
        }

        protected void ObtieneCampos_Click(object sender, EventArgs e)
        {
            if ((TablaBaseSel.Items.Count > 0) && (TablaBaseSel.SelectedIndex > -1))
            {
                ArrayList colTab = new ArrayList();
                CamposTalbaBaseSel.Items.Clear();
                // Se invoca al servicio General getCamposTablasBase
                tablaEnUso = TablaBaseSel.SelectedItem.Text;
                aliasEnUso = (string)aliasTaSel[TablaBaseSel.SelectedIndex];
                idTablas++;
                colTab = ServiciosGen.getCamposTablasBaseSQLServer(TablaBaseSel.SelectedItem.Text);
                int cont = 0;
                while (cont < colTab.Count)
                {
                    ArrayList reg = new ArrayList();
                    reg = (ArrayList)colTab[cont];
                    CamposTalbaBaseSel.Items.Add((string)reg[2]);
                    cont++;
                }

            }
            agregaFiltros();
        }

        protected void AgregaCampoTablaSel_Click(object sender, EventArgs e)
        {
            if ((CamposTalbaBaseSel.Items.Count > 0) && (CamposTalbaBaseSel.SelectedIndex > -1))
            {
                if (CamposSeleccionados.Items.FindByText(CamposTalbaBaseSel.SelectedItem.Text) == null)
                {
                    CamposSeleccionados.Items.Add(CamposTalbaBaseSel.SelectedItem.Text);
                    nomTaCamSel.Add(aliasEnUso + "." + CamposTalbaBaseSel.SelectedItem.Text);
                    ((ArrayList)Session["tablasCampos"]).Add(tablaEnUso);
                    ((ArrayList)Session["campos"]).Add(CamposTalbaBaseSel.SelectedItem.Text);
                    ((ArrayList)Session["aliasCampos"]).Add(aliasEnUso);
                    nomCamSel.Add(CamposTalbaBaseSel.SelectedItem.Text);
                    CamposTalbaBaseSel.Items.RemoveAt(CamposTalbaBaseSel.SelectedIndex);
                }

            }

            agregaFiltros();
        }

        protected void QuitaCampoTablaSel_Click(object sender, EventArgs e)
        {
            if ((CamposSeleccionados.Items.Count > 0) && (CamposSeleccionados.SelectedIndex > -1))
            {
                CamposTalbaBaseSel.Items.Add(CamposSeleccionados.SelectedItem.Text);
                int index = nomCamSel.IndexOf(CamposSeleccionados.SelectedItem.Text);
                if (index >= 0)
                {
                    //Elimina relaciones de filtros
                    string nombreTabla = CamposSeleccionados.SelectedItem.Text;
                    ArrayList filtrosCampos = ((ArrayList)Session["filtrosCampos"]);
                    ArrayList prueba = ((ArrayList)Session["filtrosTablasAlias"]);
                    for (int index2 = 0; index2 < filtrosCampos.Count; index2++)
                    {
                        if ((string)filtrosCampos[index2] == nombreTabla)
                        {
                            ((ArrayList)Session["filtrosTablasAlias"]).RemoveAt(index2);
                            ((ArrayList)Session["filtrosSinAlias"]).RemoveAt(index2);
                            ((ArrayList)Session["filtrosConAlias"]).RemoveAt(index2);
                            ((ArrayList)Session["filtrosCampos"]).RemoveAt(index2);
                            CamposSeleccionadosFin.Items.RemoveAt(index2);
                            filtrosCampos = ((ArrayList)Session["filtrosCampos"]);
                            index2 = -1;
                        }
                    }

                    nomCamSel.RemoveAt(index);
                    nomTaCamSel.RemoveAt(index);
                    ((ArrayList)Session["tablasCampos"]).RemoveAt(index);
                    ((ArrayList)Session["campos"]).RemoveAt(index);
                    ((ArrayList)Session["aliasCampos"]).RemoveAt(index);
                }
                CamposSeleccionados.Items.RemoveAt(CamposSeleccionados.SelectedIndex);

            }
            agregaFiltros();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //GridView1.DataSource = obtienDatosTabla();
            //GridView1.DataBind();
            //agregaFiltros();
        }

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

        //Genera Reporte en PDF

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (idNombreReporte.Text != "")
            {
                //generaPDF(idNombreReporte.Text);
                generaPDF2(idNombreReporte.Text);
            }
            else
            {
                Response.Write("<script language=javascript>alert('Ingrese el nombre del reporte...');</script>");
            }

        }

        protected void generaPDF(string reporte)
        {
            GridView GridView2 = new GridView();
            GridView2.AllowPaging = false;
            GridView2.DataSource = obtienDatosTabla();
            GridView2.DataBind();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + reporte + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView2.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void generaPDF2(string tituloRep)
        {
            SendOutPDF(new CustomReports().CreatePDF("Reporte: " + tituloRep, obtienDatosTablaArray()), tituloRep);
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



        //Genera Reporte en XLS
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (idNombreReporte.Text != "")
            {
                generaXLS(idNombreReporte.Text);
            }
            else
            {
                Response.Write("<script language=javascript>alert('Ingrese el nombre del reporte...');</script>");
            }
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

        //Genera Reporte en CSV
        protected void Button4_Click(object sender, EventArgs e)
        {
            DataTable DataTable;
            DataTable = obtienDatosTabla();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DataTable.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            StringBuilder sb = new StringBuilder();
            for (int k = 0; (k
                        <= (DataTable.Columns.Count - 1)); k++)
            {
                // add separator
                sb.Append((DataTable.Columns[k].ColumnName + ","));
            }

            // append new line
            sb.Append(("\r" + "\n"));
            for (int i = 0; (i
                        <= (DataTable.Rows.Count - 1)); i++)
            {
                for (int k = 0; (k
                            <= (DataTable.Columns.Count - 1)); k++)
                {
                    // add separator
                    sb.Append((DataTable.Rows[i][k].ToString().Replace(",", ";") + ","));
                }

                // append new line
                sb.Append(("\r" + "\n"));
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        //Genera Reporte en WORD
        protected void Button5_Click(object sender, EventArgs e)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = obtienDatosTabla();
            GridView1.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DataTable.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            agregaFiltros();
        }

        protected void btnAsistente_Click(object sender, EventArgs e)
        {
            agregaFiltros();
        }

        protected void ResetAll_Click(object sender, EventArgs e)
        {

            TablaBaseSel.Items.Clear();
            CamposTalbaBaseSel.Items.Clear();
            CamposSeleccionados.Items.Clear();
            CamposSeleccionadosFin.Items.Clear();

            nomCamSel.Clear();
            nomTaCamSel.Clear();
            nomTaSel.Clear();
            aliasTaSel.Clear();

            ((ArrayList)Session["filtrosTablasAlias"]).Clear();
            ((ArrayList)Session["filtrosConAlias"]).Clear();
            ((ArrayList)Session["filtrosSinAlias"]).Clear();
            ((ArrayList)Session["filtrosCampos"]).Clear();
            //Listas relacionadas de tablas, campos y alias seleccionadas
            ((ArrayList)Session["tablasCampos"]).Clear();
            ((ArrayList)Session["campos"]).Clear();
            ((ArrayList)Session["aliasCampos"]).Clear();


            principal();
            agregaFiltros();

        }

        //Genera Reporte en XLS
        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if (idNombreReporte.Text != "")
            {
                Session["nombreReporte"] = idNombreReporte.Text;
                ArrayList listGenReporte = (ArrayList)Session["listGenReporte"];
                if (listGenReporte != null)
                {
                    if (ServiciosGen.agregaReporteConsulta(idNombreReporte.Text, (string)listGenReporte[0], (string)listGenReporte[1]))
                    {
                        Response.Write("<script language=javascript>alert('Consulta Guardada en Historial.');</script>");
                    }
                    else
                        Response.Write("<script language=javascript>alert('Error al agregar reporte a historial.');</script>");
                }
            }
            else
            {
                Response.Write("<script language=javascript>alert('Ingrese el nombre del reporte...');</script>");
            }
        }
    }
}