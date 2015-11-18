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

        // Valida si el usuario ingresado es valido
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
            config.getConfiguraciones();
            confArch = config.getConfArch();
            confBD = config.getConfBD();
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
            string sql1 = (" SELECT DISTINCT * FROM " + (" TABLAS_BD T, RELACIONESTABLAS_BD RT " + ("      WHERE " + (" T.TablaID = RT.TABLARELACIONADA" + (" AND  RT.TABLABASEID IN (" + (" Select TablaID " + ("       FROM " + "       TABLAS_BD ")))))));
            int cont = 0;
            if (tablasBaseSel.Count > 0)
            {
                string sql2 = " where Nombre in ";
                string sql3 = " (";
                string sql4 = "AND  T.NOMBRE not in ";
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

        public static ArrayList generaCamposRelacion(ArrayList tabSelec, ArrayList alias, ArrayList camposRel)
        {
            ArrayList resp = new ArrayList();
            if (camposRel != null)
            {
                foreach (ArrayList regCam in camposRel)
                {
                    string nombreTabB = (string)regCam[2];
                    string nombreTabR = (string)regCam[3];

                    string nombreCampoTB = (string)regCam[4];
                    string nombreCampoTR = (string)regCam[5];

                    int indexTB = tabSelec.IndexOf(nombreTabB);
                    int indexTR = tabSelec.IndexOf(nombreTabR);

                    string aliasTB = (string)alias[indexTB];
                    string aliasTR = (string)alias[indexTR];

                    resp.Add(aliasTB + "." + nombreCampoTB + " = " + aliasTR + "." + nombreCampoTR);

                }
            }

            return resp;
        }


        public static ArrayList obtieneCamosRelacion(ArrayList tabSelec)
        {
            string sqlTablas = "";
            string sep = "";
            foreach (string tabla in tabSelec)
            {
                sqlTablas = sqlTablas + sep + " '" + tabla + "'";
                sep = ", ";
            }

            string sqlF = "select " +
                        " rtb.TablaBaseID, rtb.RelacionID, tb1.Nombre nombreTB, tb2.Nombre nombreTR, rtcb.campoTB, rtcb.campoTR" +
                        " from" +
                        " RELACIONESTABLAS_BD rtb, RelacionCamposTablas_BD rtcb, TABLAS_BD tb1, TABLAS_BD tb2" +
                        " where" +
                        " (" +
                        " rtb.TablaBaseID in (" +
                        " select tb.TablaID from" +
                        " tablas_bd tb" +
                        " where" +
                        " tb.Nombre in (" + sqlTablas + ")" +
                        " )" +
                        " or" +
                        " rtb.TablaRelacionada in (" +
                        " select tb.TablaID from" +
                        " tablas_bd tb" +
                        " where" +
                        " tb.Nombre in (" + sqlTablas + " )" +
                        " )" +
                        " )" +
                        " and" +
                        " (" +
                        " tb1.Nombre in (" + sqlTablas + ")" +
                        " and tb2.Nombre in (" + sqlTablas + " )" +
                        " )" +
                        " and rtcb.RelacionID = rtb.RelacionID" +
                        " and rtb.TablaBaseID = tb1.TablaID" +
                        " and rtb.RelacionID = tb2.TablaID ";

            return sqlDispatcher.getColConsulta(sqlF);

        }

        public static ArrayList reporteDinamico(ArrayList campos, ArrayList tablas, ArrayList alias, ArrayList relaciones, ArrayList filtros, ref ArrayList sqlGenOut)
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

            for (int contTa = 0; contTa < tablas.Count; contTa++)
            {
                string tabla = (string)tablas[contTa];
                string alia = (string)alias[contTa];
                sqlTablasSel = sqlTablasSel + sep + tabla + " " + alia;
                sep = ", ";
            }

            string sqlCondicion = "";
            if (relaciones.Count > 0 || filtros.Count > 0)
            {
                sqlCondicion = " WHERE ";
                sep = "";
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
            sqlGenOut.Add(sqlF);

            return sqlDispatcher.getColConsulta(sqlF);

        }

        public static Boolean agregaReporteConsulta(string nombreReporte, string cadenaCampos, string cadenaConsultaReporte)
        {
            cadenaConsultaReporte = cadenaConsultaReporte.Replace("'", "''");
            string sql = "INSERT INTO HISTORICO_REPORTES VALUES('" + nombreReporte + "', '" + cadenaCampos + "','" + cadenaConsultaReporte + "', GetDate(), 1);";
            return sqlDispatcher.ejecutaSQL(sql);
        }

        public static ArrayList joinNombreAlias(ArrayList lNombre, ArrayList lAlias, string sep)
        {
            ArrayList resp = new ArrayList();
            if (sep == "")
            {
                sep = ".";
            }

            if (lNombre != null && lAlias != null)
            {
                for (int cont = 0; cont < lNombre.Count; cont++)
                {
                    resp.Add(lAlias[cont] + sep + lNombre[cont]);
                }

            }
            return resp;
        }

        public static string toStringArrayList(ArrayList lista, string sep)
        {
            string resp = "";
            string sepAux = "";
            if (sep == null)
            {
                sep = ",";
            }
            if (lista != null)
            {
                for (int cont = 0; cont < lista.Count; cont++)
                {
                    resp = resp + sepAux + lista[cont];
                    sepAux = sep;
                }
            }
            return resp;
        }

        // Recupera las tablas base de la BD
        public static ArrayList getHistorico()
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta("SELECT TOP 30 * FROM HISTORICO_REPORTES ORDER BY FECHAREG desc");
        }
        public static ArrayList getHistoricoById(string id)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta("SELECT * FROM HISTORICO_REPORTES WHERE HISTORICOID = " + id + " ORDER BY FECHAREG");
        }

        public static ArrayList getResultadoSQL(string sql)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(sql);
        }
    }
}