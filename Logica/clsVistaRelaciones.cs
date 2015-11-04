using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tablas_BD = Entidades.ConexionBD.Tablas_BD;
using Paginado = Entidades.Helpers.PaginadoRelacion;
using DataLayer = Datos.clsVistaRelaciones;

namespace Logica
{
    public class clsVistaRelaciones
    {
        public static Paginado SelectAll(int skip, int take)
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.selectAll(skip, take);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static List<int> tablasRelacionadas(int id)
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.tablasRelacionadas(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
