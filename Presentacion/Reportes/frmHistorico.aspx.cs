using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Presentacion.Reportes
{
    public partial class frmHistorico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            agregaTablaHistorico();

        }

        protected void agregaTablaHistorico()
        {
            PanelHistorico.Controls.Clear();
            PanelHistorico.Controls.Add(new LiteralControl("<table border='1'> <tr><td>NOMBRE REPORTE</td><td>FECHA REGISTRO</td><td>OPERACIÓN</td></tr> "));


            ArrayList listaHist = ServiciosGen.getHistorico();


            if (listaHist != null)
            {
                for (int cont = 0; cont < listaHist.Count; cont++)
                {
                    agregarFiltro((ArrayList)listaHist[cont]);
                }
            }

            PanelHistorico.Controls.Add(new LiteralControl("</table>"));


        }

        protected void agregarFiltro(ArrayList dat)
        {
            try
            {
                Label nombreRep = new Label();
                nombreRep.ID = "idNombreRep" + dat[0];
                nombreRep.Text = (string)dat[1];

                Label fechaRep = new Label();
                fechaRep.ID = "idFechaRep" + dat[0];
                fechaRep.Text = (string)dat[4];

                HyperLink linkPDF = new HyperLink();
                linkPDF.ID = "linkRepHis" + dat[0];
                linkPDF.ImageUrl = "../Reportes/img/PDF-icon3.png";
                linkPDF.NavigateUrl = "/Reportes/frmGenRep.aspx?tipo=PDF&rep=" + dat[0];
                linkPDF.Attributes.Add("onClick", "javascript:return AgregaCampoTablaSel_Click(" + dat[0] + ", this);");

                HyperLink linkExl = new HyperLink();
                linkExl.ID = "linkRepHis" + dat[0];
                linkExl.ImageUrl = "../Reportes/img/Excel-icon3.png";
                linkExl.NavigateUrl = "/Reportes/frmGenRep.aspx?tipo=XLS&rep=" + dat[0];
                linkExl.Attributes.Add("onClick", "javascript:return AgregaCampoTablaSel_Click(" + dat[0] + ", this);");

                HyperLink linkLatLong = null;

                if (dat[2]!=null && ((string)dat[2]).ToUpper().IndexOf("LATITUD") >= 0 && ((string)dat[2]).ToUpper().IndexOf("LONGITUD") >= 0)
                {
                    linkLatLong = new HyperLink();
                    linkLatLong.ID = "linkRepHisLatLon" + dat[0];
                    linkLatLong.ImageUrl = "../Reportes/img/mapa.gif";
                    linkLatLong.NavigateUrl = "/XXXXXXX/XXXXXXXXX.aspx?historicoID="+dat[0]+"&nombre=" + dat[1];
                    linkLatLong.Attributes.Add("onClick", "javascript:return AbreVentanaMapa(" + dat[0] + ", '"+dat[1]+"');");
                }


                AgregarControles(nombreRep, fechaRep, linkPDF, linkExl, (string)dat[0], linkLatLong);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void AgregarControles(Label nombreRep, Label fechaRep, HyperLink pdf, HyperLink xls, string id, HyperLink latLong)
        {
            try
            {
                PanelHistorico.Controls.Add(new LiteralControl("<tr>"));
                PanelHistorico.Controls.Add(new LiteralControl("<td>"));
                PanelHistorico.Controls.Add(nombreRep);
                PanelHistorico.Controls.Add(new LiteralControl("</td><td>"));
                PanelHistorico.Controls.Add(fechaRep);
                PanelHistorico.Controls.Add(new LiteralControl("</td><td>"));
                PanelHistorico.Controls.Add(pdf);
                PanelHistorico.Controls.Add(new LiteralControl("&nbsp;"));
                PanelHistorico.Controls.Add(xls);
                if (latLong != null)
                {
                    PanelHistorico.Controls.Add(new LiteralControl("&nbsp;"));
                    PanelHistorico.Controls.Add(latLong);
                }
                PanelHistorico.Controls.Add(new LiteralControl("</td></tr>"));

            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}