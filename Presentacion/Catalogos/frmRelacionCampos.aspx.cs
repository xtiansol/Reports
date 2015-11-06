using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using Logica;

namespace Presentacion.Catalogos
{
    public partial class frmRelacionCampos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            selectColumn(1);
        }
        [WebMethod()]
        public static string selectTables()
        {
            try
            {
                List<Tuple<int, string>> tables = clsTablaCampo.tablaMaster();
                var json = new JavaScriptSerializer();
                return json.Serialize(tables);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod()]
        public static string selectDetail(int id)
        {
            try
            {
                List<Tuple<int, string>> tables = clsTablaCampo.tablaDetail(id);
                var json = new JavaScriptSerializer();
                return json.Serialize(tables);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<string> selectColumn(int id)
        {
            try
            {
                return clsTablaCampo.selectColumn(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}