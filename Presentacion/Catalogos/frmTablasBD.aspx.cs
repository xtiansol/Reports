using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Services;
using System.Web.Script.Serialization;
using Paginado = Entidades.Helpers.Paginado;
using LogicaLayer = Logica.clsTablaBD;
using Entidades.ConexionBD;

namespace Presentacion.Catalogos
{
    public partial class frmTablasBD : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Paginado getData(int skip, int take)
        {
            try
            {
                var data = new Paginado();

                data = LogicaLayer.SelectAll(skip, take);
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod]
        public static string saveData(string NombreTabla, string Descripcion, string TipoTabla)
        {
            try
            {
                Tablas_BD t = new Tablas_BD();
                t.Nombre = NombreTabla;
                t.Descripcion = Descripcion;
                t.TipoTabla = TipoTabla;
                t.Estatus = true;
                string data = LogicaLayer.Insert(t);
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod]
        public static string updateData(int id, string NombreTabla, string Descripcion, string TipoTabla)
        {
            try
            {
                Tablas_BD t = new Tablas_BD();
                t.TablaID = id;
                t.Nombre = NombreTabla;
                t.Descripcion = Descripcion;
                t.TipoTabla = TipoTabla;
                t.Estatus = true;
                string data = LogicaLayer.Update(t);
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [WebMethod]
        public static string deleteData(int id)
        {
            try
            {
                string data = LogicaLayer.Delete(id);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

