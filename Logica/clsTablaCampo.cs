using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer = Datos.clsTablaCampos;

namespace Logica
{
    public class clsTablaCampo
    {
        public static List<Tuple<int,string>> tablaMaster()
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.tablaMaster();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<Tuple<int, string>> tablaDetail(int id)
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.tablaDetail(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<string> selectColumn(string tableName)
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.selectColumn(tableName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
