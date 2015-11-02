using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaLayer = Logica.clsTablasRelaciones;
using Entidades.ConexionBD;
namespace Presentacion.Catalogos
{
    public partial class frmRelacionesTablas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod()]
        public static string selectTables()
        {
            try
            {
                List<Tuple<int, string>> tables = LogicaLayer.selectTables();
                var json = new JavaScriptSerializer();
                return json.Serialize(tables);
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
                int tables = LogicaLayer.Insert(rt, m);

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}