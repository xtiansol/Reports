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
                return LogicaLayer.SelectAll(skip, take);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

