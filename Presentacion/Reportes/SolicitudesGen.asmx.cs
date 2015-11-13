using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Presentacion.Reportes
{
    /// <summary>
    /// Summary description for SolicitudesGen
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SolicitudesGen : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string AgregaFiltros(String filtroFin, String campos)
        {
            string filtrosCom = filtroFin;
            string camposNom = campos;
            if (Session["filtrosSinAlias"] == null)
                Session["filtrosSinAlias"] = new ArrayList();
            ArrayList filtrosTablasAlias = ((ArrayList)Session["filtrosTablasAlias"]);
            ArrayList filtrosSinAlias = (ArrayList)Session["filtrosSinAlias"];
            ArrayList filtrosConAlias = (ArrayList)Session["filtrosConAlias"];
            ArrayList filtrosCampos = (ArrayList)Session["filtrosCampos"];


            string[] words = filtrosCom.Split('|');
            string[] wordsCamp = campos.Split('|');
            filtrosTablasAlias.Clear();
            filtrosSinAlias.Clear();
            filtrosConAlias.Clear();
            for (int cont = 0; cont < words.Length; cont++)
            {
                String filtro = words[cont];
                string camposRec = wordsCamp[cont];
                if (filtro != "" && camposRec != "")
                {
                    int index = ((ArrayList)Session["campos"]).IndexOf(camposRec);
                    string alias = (string)((ArrayList)Session["aliasCampos"])[index];
                    string tab = (string)((ArrayList)Session["tablasCampos"])[index];
                    filtrosTablasAlias.Add(tab);
                    filtrosSinAlias.Add(filtro);
                    filtrosConAlias.Add(alias + "." + filtro);
                    filtrosCampos.Add(camposRec);
                }
            }

            return string.Format("{{ \"mensaje\" : \"{0} \" }}", "Agregado");
        }

        [WebMethod(EnableSession = true)]
        public string MantieneTabSel(String tablasSel, String aliasTablasSel)
        {
            //Listas relacionadas de tablas, alias y campos
            if (Session["tablasSel"] == null || Session["aliasTablasSel"] == null)
            {
                Session["tablasSel"] = new ArrayList();
                Session["aliasTablasSel"] = new ArrayList();
            }
            else
            {
                ((ArrayList)Session["tablasSel"]).Clear();
                ((ArrayList)Session["aliasTablasSel"]).Clear();
            }



            string[] words1 = tablasSel.Split('|');
            string[] words2 = aliasTablasSel.Split('|');

            //string[] words4 = aliasTablasCampoSel.Split('|');


            for (int cont = 0; cont < words1.Length; cont++)
            {
                String tabla = words1[cont];
                String alias = words2[cont];

                if (tabla != "" && alias != "")
                {
                    ((ArrayList)Session["tablasSel"]).Add(tabla);
                    ((ArrayList)Session["aliasTablasSel"]).Add(alias);
                }
            }


            return string.Format("{{ \"mensaje\" : \"{0} \" }}", "Agregado");
        }

        [WebMethod(EnableSession = true)]
        public string MantieneCamposSelTabSel(String tablasCampoSel, String aliasTablasCampoSel, string camposSel)
        {
            //Listas relacionadas de tablas, alias y campos
            if (Session["tablasCampos"] == null || Session["aliasCampos"] == null || Session["campos"] == null)
            {
                Session["tablasCampos"] = new ArrayList();
                Session["aliasCampos"] = new ArrayList();
                Session["campos"] = new ArrayList();

            }
            else
            {
                ((ArrayList)Session["tablasCampos"]).Clear();
                ((ArrayList)Session["aliasCampos"]).Clear();
                ((ArrayList)Session["campos"]).Clear();
            }


            string[] words1 = tablasCampoSel.Split('|');
            string[] words2 = aliasTablasCampoSel.Split('|');
            string[] words3 = camposSel.Split('|');
            //string[] words4 = aliasTablasCampoSel.Split('|');


            for (int cont = 0; cont < words1.Length; cont++)
            {
                String tabla = words1[cont];
                String alias = words2[cont];
                String campo = words3[cont];

                if (tabla != "" && alias != "" && campo != "")
                {
                    ((ArrayList)Session["tablasCampos"]).Add(tabla);
                    ((ArrayList)Session["aliasCampos"]).Add(alias);
                    ((ArrayList)Session["campos"]).Add(campo);
                }
            }

            return string.Format("{{ \"mensaje\" : \"{0} \" }}", "Agregado");
        }

        [WebMethod(EnableSession = true)]
        public string ObtieneTablasBD()
        {
            string respuesta = "{\"resp\":{\"listaGen\":";
            string resp1 = "";
            string sep = "";

            ArrayList listaTablas = new ArrayList();
            int cont;

            ArrayList colbd = new ArrayList();
            colbd = ServiciosGen.getTablasBase();
            cont = 0;
            while (cont < colbd.Count)
            {
                ArrayList reg = new ArrayList();
                reg = (ArrayList)colbd[cont];
                resp1 = resp1 + sep + "\"" + ((string)reg[1]) + "\" ";
                sep = ",";
                cont++;
            }

            respuesta = respuesta + "[" + resp1 + "], \"respuesta\":\"Exito\"}}";

            return respuesta;
            //agregaFiltros();
        }

        [WebMethod(EnableSession = true)]
        public string ObtieneTablasRelacionadas(String tablas)
        {
            string respuesta = "{\"resp\":{\"listaGen\":";
            string resp1 = "";
            string sep = "";
            if (tablas != "")
            {
                string[] words = tablas.Split('|');
                ArrayList listaTablas = new ArrayList();
                int cont;
                for (cont = 0; cont < words.Length; cont++)
                {
                    if (words[cont] != "")
                    {
                        listaTablas.Add(words[cont]);
                    }

                }

                ArrayList colbd = new ArrayList();
                colbd = ServiciosGen.getTablasRelacionadas(listaTablas);
                cont = 0;
                while (cont < colbd.Count)
                {
                    ArrayList reg = new ArrayList();
                    reg = (ArrayList)colbd[cont];
                    resp1 = resp1 + sep + "\"" + ((string)reg[1]) + "\" ";
                    sep = ",";
                    cont++;
                }

                respuesta = respuesta + "[" + resp1 + "], \"respuesta\":\"Exito\"}}";
            }
            else
            {
                respuesta = respuesta + "[], \"respuesta\":\"Error\", \"Error\":\"Error\"}}";
            }

            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public string ObtieneCamposTablas(String tablas)
        {
            string respuesta = "{\"resp\":{\"listaGen\":";
            string resp1 = "";
            string sep = "";
            if (tablas != "")
            {
                string[] words = tablas.Split('|');
                ArrayList listaTablas = new ArrayList();
                int cont;
                for (cont = 0; cont < words.Length; cont++)
                {
                    if (words[cont] != "")
                    {
                        listaTablas.Add(words[cont]);
                    }

                }

                for (cont = 0; cont < listaTablas.Count; cont++)
                {
                    ArrayList colbd = new ArrayList();
                    colbd = ServiciosGen.getCamposTablasBaseSQLServer((string)listaTablas[cont]);
                    cont = 0;
                    while (cont < colbd.Count)
                    {
                        ArrayList reg = new ArrayList();
                        reg = (ArrayList)colbd[cont];
                        resp1 = resp1 + sep + "\"" + ((string)reg[2]) + "\" ";
                        sep = ",";
                        cont++;
                    }
                }

                respuesta = respuesta + "[" + resp1 + "], \"respuesta\":\"Exito\"}}";
            }
            else
            {
                respuesta = respuesta + "[], \"respuesta\":\"Error\", \"Error\":\"Error\"}}";
            }

            return respuesta;
        }


        [WebMethod(EnableSession = true)]
        public string GuardaNombreReporte(String nombreReporte, String cadenaConsultaReporte)
        {
            if (nombreReporte != "" && cadenaConsultaReporte != "")
            {
                Session["nombreReporte"] = nombreReporte;
                Session["cadenaConsultaReporte"] = cadenaConsultaReporte;
                if (ServiciosGen.agregaReporteConsulta(nombreReporte, cadenaConsultaReporte))
                {
                    return string.Format("{{ \"mensaje\" : \"{0} \" }}", "Agregado");
                }
                else
                    return string.Format("{{ \"mensaje\" : \"{0} \" }}", "Error al agregar reporte.");
            }
            else
            {
                if (nombreReporte == "")
                {
                    return string.Format("{{ \"mensaje\" : \"{0} \" }}", "Ingrese un nombre valido.");
                }
                else
                {
                    return string.Format("{{ \"mensaje\" : \"{0} \" }}", "No hay información de la consulta a guardar.");
                }
            }

        }

    }
}
