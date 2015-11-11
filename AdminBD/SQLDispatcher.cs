using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminBD;

namespace AdminBD
{

    public class SQLDispatcher
    {

        private AdminBD admBD;

        public void getConexion(string bd, string serv, string us, string pwd, int tipoBD)
        {
            admBD = new AdminBD(bd, serv, us, pwd, tipoBD);
        }

        public bool ejecutaSQL(string sql)
        {
            if (admBD.abrirBD())
            {
                return admBD.executeSQL(sql);
            }

            return false;
        }

        public bool ejecutaTransaccion(string sql)
        {
            if (admBD.abrirBD())
            {
                return admBD.executeSQL(sql);
            }

            return false;
        }

        public ArrayList getColConsulta(string sql)
        {
            try
            {
                if (admBD.abrirBD())
                {
                    return admBD.getColRegString(sql);
                }

                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object getColConsultaResultSet(string sql)
        {
            if (admBD.abrirBD())
            {
                return admBD.getColRegResultSet(sql);
            }

            return null;
        }

        public void procedimiento(string proc)
        {
            if (admBD.abrirBD())
            {
                admBD.procedimiento(proc);
            }

        }

        public void addDatoProc(string nom, string val, string tip, int tam)
        {
            admBD.addParametroProcedimiento(nom, val, tip, tam);
        }

        public void addDatoOutProc(string nom, object val, string tip, int tam)
        {
            admBD.addParametroOutProcedimiento(nom, val, tip, tam);
        }

        public int execProcedure()
        {
            return admBD.ejecutaProcedimiento();
        }

        public ArrayList execProcedureRes()
        {
            return admBD.ejecutaProcedimientoRes();
        }

        public object execProcedureResultSet()
        {
            return admBD.ejecutaProcedimientoResultSet();
        }

        public SQLDispatcher(string bd, string serv, string us, string pwd, int tipoBD)
        {
            admBD = new AdminBD(bd, serv, us, pwd, tipoBD);
        }

        public SQLDispatcher()
        {
            admBD = new AdminBD();
        }
    }
}
