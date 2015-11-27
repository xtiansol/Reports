using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Reportes
{
    public partial class frmFiltros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            agregaFiltros();
        }

        protected void agregaFiltros()
        {
            //            camposFin.Clear();
            PanelFiltros.Controls.Clear();
            PanelFiltros.Controls.Add(new LiteralControl("<table border='1'> <tr><td>NOMBRE CAMPO</td><td>OPERADOR</td><td>VALOR</td></tr> "));
            ArrayList tablasCampos = (ArrayList)Session["tablasCampos"];
            ArrayList aliasCampos = (ArrayList)Session["aliasCampos"];
            ArrayList campos = (ArrayList)Session["campos"];


            if (tablasCampos != null && aliasCampos != null && campos != null)
            {
                for (int cont = 0; cont < campos.Count; cont++)
                {
                    agregarFiltro((string)campos[cont]);
                }
            }

            PanelFiltros.Controls.Add(new LiteralControl("</table>"));


        }
        protected void agregarFiltro(string nombre)
        {
            try
            {
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
                AgregarControles(campoSel, nuevoTxt, nuevoCmb, nuevoHidF, 0);
                //contadorControles++;
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
                PanelFiltros.Controls.Add(new LiteralControl("<tr>"));
                PanelFiltros.Controls.Add(new LiteralControl("<td>"));
                PanelFiltros.Controls.Add(nombreContr);
                PanelFiltros.Controls.Add(new LiteralControl("</td><td>"));
                PanelFiltros.Controls.Add(cmb);
                PanelFiltros.Controls.Add(new LiteralControl("</td><td>"));
                PanelFiltros.Controls.Add(txt);
                PanelFiltros.Controls.Add(nhf);
                PanelFiltros.Controls.Add(new LiteralControl("</td></tr>"));
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}