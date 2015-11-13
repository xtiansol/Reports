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
    public partial class frmReportePrev : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView2.DataSource = obtienDatosTabla();
            GridView2.DataBind();
            //agregaFiltros();


        }



        protected DataTable obtienDatosTabla()
        {
            DataTable dt = new DataTable();
            int cont = 0;

            //Relaciones tablas y alias seleccionados
            ArrayList tablasSel = (ArrayList)Session["tablasSel"];
            ArrayList aliasTablasSel = (ArrayList)Session["aliasTablasSel"];



            //Relaciones tablas, alias y campos seleccionados
            ArrayList tablasCampos = (ArrayList)Session["tablasCampos"];
            ArrayList aliasCampos = (ArrayList)Session["aliasCampos"];
            ArrayList campos = (ArrayList)Session["campos"];

            if (tablasSel != null && aliasTablasSel != null && tablasCampos != null && aliasCampos != null && campos != null)
            {

                while (cont < campos.Count)
                {
                    dt.Columns.Add(new DataColumn((string)campos[cont], typeof(string)));
                    cont = (cont + 1);
                }

                //  Total number of rows.
                int rowCnt = 0;
                //  Current row count
                int rowCtr = 0;
                //  Total number of cells (columns).
                int cellCtr = 0;
                //  Current cell counter.
                int cellCnt = 0;
                rowCnt = 10;
                cellCnt = campos.Count;

                ArrayList filtros = (ArrayList)Session["filtrosConAlias"];

                ArrayList camposRel = ServiciosGen.generaCamposRelacion(tablasSel, aliasTablasSel, ServiciosGen.obtieneCamosRelacion(tablasSel));

                ArrayList resp = ServiciosGen.reporteDinamico(ServiciosGen.joinNombreAlias(campos, aliasCampos, "."), tablasSel, aliasTablasSel, camposRel, filtros);
                if (resp != null)
                {
                    for (rowCtr = 0; (rowCtr < resp.Count); rowCtr++)
                    {
                        ArrayList camposRec = (ArrayList)resp[rowCtr];
                        DataRow tRow = dt.NewRow();
                        for (cellCtr = 0; cellCtr < camposRec.Count; cellCtr++)
                        {
                            tRow[cellCtr] = camposRec[cellCtr];
                            // cellCtr = cellCtr + 1
                        }

                        //  Add new row to table.
                        dt.Rows.Add(tRow);
                    }
                }
            }

            return dt;
        }


    }


}