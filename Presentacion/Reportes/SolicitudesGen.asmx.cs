using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BarandillasRepV2c
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


    }
}
