using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using Logica;
using Entidades.ConexionBD;
using System.Data;

namespace Presentacion.Catalogos
{
    public partial class frmRelacionCampos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
        [WebMethod()]
        public static List<string> selectColumns(string nombre)
        {
            try
            {
                //List<string> tables = clsTablaCampo.selectColumn(nombre);
                //var json = new JavaScriptSerializer();
                //return json.Serialize(clsTablaCampo.selectColumn(nombre));
                return clsTablaCampo.selectColumn(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod()]
        public static string selectCampos()
        {
            try
            {
                List<Tuple<string, string>> tables = clsTablaCampo.selectData();
                var json = new JavaScriptSerializer();
                return json.Serialize(tables);
               // return clsTablaCampo.selectData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod()]
        public static int saveColumns(string idRelacion, string campoPK, string campoFK)
        {
            try
            {
                var json = new JavaScriptSerializer();

                int[] pk= json.Deserialize<int[]>(idRelacion);
                string[] fk = json.Deserialize<string[]>(campoFK);
                RelacionCamposTablas_BD rt = new RelacionCamposTablas_BD();
                rt.CampoTablaBase = campoPK;

                int tables = clsTablaCampo.Insert(rt, pk, fk);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}