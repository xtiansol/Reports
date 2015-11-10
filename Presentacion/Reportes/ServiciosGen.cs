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



        // Recupera las tablas base de la BD
        public static ArrayList getTablasBase()
        {
            config.getConfiguraciones();
            confArch = config.getConfArch();
            confBD = config.getConfBD();
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(("SELECT " + ("P.* " + ("FROM " + "TABLAS_BD P "))));
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
            string sql1 = (" SELECT DISTINCT * FROM " + (" TABLAS_BD T, RELACIONESTABLAS_BD RT " + ("      WHERE " + (" T.TablaID = RT.TablaRelacionada" + (" AND  RT.TablaID IN (" + (" Select TablaID " + ("       FROM " + "       TABLAS_BD ")))))));
            int cont = 0;
            if (tablasBaseSel.Count > 0)
            {
                string sql2 = " where NombreTabla in ";
                string sql3 = " (";
                string sql4 = "AND  T.NombreTabla not in ";
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
                        " rtb.TablaID, rtb.RelacionID, tb1.NombreTabla nombreTB, tb2.NombreTabla nombreTR, rtcb.CampoTablaBase, rtcb.CampoTablaRelacion" +
                        " from" +
                        " RELACIONESTABLAS_BD rtb, RelacionCamposTablas_BD rtcb, TABLAS_BD tb1, TABLAS_BD tb2" +
                        " where" +
                        " (" +
                        " rtb.TablaID in (" +
                        " select tb.TablaID from" +
                        " tablas_bd tb" +
                        " where" +
                        " tb.NombreTabla in (" + sqlTablas + ")" +
                        " )" +
                        " or" +
                        " rtb.TablaRelacionada in (" +
                        " select tb.TablaID from" +
                        " tablas_bd tb" +
                        " where" +
                        " tb.NombreTabla in (" + sqlTablas + " )" +
                        " )" +
                        " )" +
                        " and" +
                        " (" +
                        " tb1.NombreTabla in (" + sqlTablas + ")" +
                        " and tb2.NombreTabla in (" + sqlTablas + " )" +
                        " )" +
                        " and rtcb.RelacionID = rtb.RelacionID" +
                        " and rtb.TablaID = tb1.TablaID" +
                        " and rtb.RelacionID = tb2.TablaID ";

            return sqlDispatcher.getColConsulta(sqlF);

        }

        public static ArrayList reporteDinamico(ArrayList campos, ArrayList tablas, ArrayList alias, ArrayList relaciones, ArrayList filtros)
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

            return sqlDispatcher.getColConsulta(sqlF);

        }
    }
}