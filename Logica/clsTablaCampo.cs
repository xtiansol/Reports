using Entidades.ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
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
        //List<Tuple<string, string>>
        public static List<Tuple<string, string>> selectData()
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.selectData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int Insert(RelacionCamposTablas_BD tablas, int[] m, string[] pk)
        {
            try
            {
                var procedimiento = new DataLayer();
                for (int i = 0; i < m.Length; i++)
                {
                    tablas.RelacionID = m[i];
                    tablas.CampoTR = pk[i];
                    procedimiento.Insert(tablas);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteCampo(string id)
        {
            try
            {
                var procedimiento = new DataLayer();
                procedimiento.DeleteCampo(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
