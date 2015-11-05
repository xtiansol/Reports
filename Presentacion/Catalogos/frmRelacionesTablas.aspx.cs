using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades.ConexionBD;
using Entidades.Helpers;

namespace Presentacion.Catalogos
{
    public partial class frmRelacionesTablas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //clsVistaRelaciones.SelectAll(0, 10);
        }
        [WebMethod()]
        public static string selectTables()
        {
            try
            {
                List<Tuple<int, string>> tables = clsTablasRelaciones.selectTables();
                var json = new JavaScriptSerializer();
                return json.Serialize(tables);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod()]
        public static PaginadoRelacion selectData(int skip, int take)
        {
            try
            {
                var data = new PaginadoRelacion();
                data = clsVistaRelaciones.SelectAll(skip, take);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod()]
        public static List<int> tablasRelacionadas(int id)
        {
            try
            {
                return clsVistaRelaciones.tablasRelacionadas(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod()]
        public static int saveData(int idTabla,string data,string Descripcion)
        {
            try
            {
                var json = new JavaScriptSerializer();
                //List<string[]> mystring = json.Deserialize<List<string[]>>(data);
                int[] m = json.Deserialize<int[]>(data);
                RelacionesTablas_BD rt = new RelacionesTablas_BD();
                rt.TablaID = idTabla;
                rt.Descripcion = Descripcion;
                int tables = clsTablasRelaciones.Insert(rt, m);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod()]
        public static string Delete(int id)
        {
            try
            {
                return clsTablasRelaciones.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}