using System;
using System.Collections;
using AdminBD;

namespace Presentacion.Reportes
{
    public class ServiciosGen
    {
        private static Config config = new Config();

        private static ConfBD confBD;

        private static ConfMail confMail;

        private static ConfArch confArch;

        private static SQLDispatcher sqlDispatcher = new SQLDispatcher();

        // Valida si el usuario ingresado es valido string us, string pwd
        public static bool esUsuario(string us, string pwd)
        {
            config.getConfiguraciones();
            confArch = config.getConfArch();
            confBD = config.getConfBD();
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            ArrayList colbd = new ArrayList();
            colbd = sqlDispatcher.getColConsulta(("SELECT                    " + ("P.*               " + ("FROM                       " + ("usuario2 P               "
                            + (("where P.clave = \'"
                            + (us + "\' ")) + ("and P.password = \'"
                            + (pwd + "\' "))))))));
            if ((!(colbd == null)
                        && (colbd.Count > 0)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // Recupera las tablas base de la BD
        public static ArrayList getTablasBase()
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(("SELECT                    " + ("P.*               " + ("FROM                       " + "TABLAS_BD P               "))));
        }

        // Recupera las tablas base de la BD
        public static ArrayList getCamposTablasBaseOracle(string tabla)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(("SELECT a.table_name nombreTabla, a.column_name nombreColumna, a.data_type tipoDato, a.data_length tam" +
                "anioCampo, a.data_precision decimales " + (" from all_tab_columns a " + (" where " + (" a.table_name= \'"
                            + (tabla + "\'"))))));
        }


        public static ArrayList getCamposTablasBaseSQLServer(string tabla)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta("SELECT TABLE_CATALOG, TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
                                               " WHERE TABLE_NAME = '" + tabla + "'" +
                                               "ORDER BY ordinal_position");
        }


        public static ArrayList getTablasRelacionadas(ArrayList tablasBaseSel)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            string sql1 = (" SELECT DISTINCT * FROM " + (" Tablas_BD T, RelacionesTablas_BD RT " + ("      WHERE " + (" T.TablaID = RT.TABLARELACIONADA" + (" AND  RT.RelacionID IN (" + (" Select TablaID " + ("       FROM " + "       TABLAS_BD ")))))));
            int cont = 0;
            if (tablasBaseSel.Count > 0)
            {
                string sql2 = " where NombreTabla in ";
                string sql3 = " (";
                string sql4 = "AND  T.NOMBRETABLA not in ";
                string sep = "";
                while (cont < tablasBaseSel.Count)
                {
                    sql3 = (sql3
                                + (sep + (" \'"
                                + (tablasBaseSel[cont] + "\' "))));
                    sep = ",";
                    cont = (cont + 1);
                }

                sql3 = (sql3 + ") ");
                sql2 = (sql2 + sql3);
                sql4 = (sql4 + sql3);
                sql1 = (sql1
                            + (sql2 + ")"));
                sql1 = (sql1 + sql4);
            }
            else
            {
                sql1 = (sql1 + ")");
            }

            return sqlDispatcher.getColConsulta(sql1);
        }

        public static ArrayList reporteDinamico(ArrayList campos, ArrayList tablas, ArrayList relaciones, ArrayList filtros)
        {
            string sqlF = "SELECT ";

            string sqlCampos = "";
            string sep = "";

            foreach (string campo in campos)
            {
                sqlCampos = sqlCampos + sep + campo;
                sep = ", ";
            }

            string sqlTablasSel = " FROM ";
            sep = "";

            foreach (string tabla in tablas)
            {
                sqlTablasSel = sqlTablasSel + sep + tabla;
                sep = ", ";
            }

            string sqlCondicion = "";
            if (relaciones.Count > 0 || filtros.Count > 0)
            {
                sqlCondicion = " WHERE ";
                foreach (string relacion in relaciones)
                {
                    sqlCondicion = sqlCondicion + sep + relacion;
                    sep = " AND ";
                }
                foreach (string filtro in filtros)
                {
                    sqlCondicion = sqlCondicion + sep + filtro;
                    sep = " AND ";
                }
            }

            sqlF = sqlF + sqlCampos + sqlTablasSel + sqlCondicion;

            return sqlDispatcher.getColConsulta(sqlF);

        }
    }
}