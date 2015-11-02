using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelacionesTablas_BD = Entidades.ConexionBD.RelacionesTablas_BD;
using DataLayer = Datos.clsTablasRelaciones;

namespace Logica
{
    public class clsTablasRelaciones
    {
        public static List<Tuple<int, string>> selectTables()
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.selectTables();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int Insert(RelacionesTablas_BD tablas, int[] m)
        {
            try
            {
                var procedimiento = new DataLayer();
                for (int i = 0; i < m.Length; i++)
                {
                    tablas.TablaRelacionada = m[i];
                    procedimiento.Insert(tablas);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
