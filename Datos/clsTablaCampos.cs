using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Entidades.ConexionBD;
using System.Data;


namespace Datos
{
    public class clsTablaCampos
    {
        public List<Tuple<int,string>> tablaMaster()
        {
            try
            {
                List<Tuple<int, string>> data = new List<Tuple<int, string>>();
                using(var context = new BarandillasEntities())
                {
                    var query = (from i in context.RelacionesDeTablas
                                 select new
                                 {
                                     ID = i.TablaID,
                                     NombreTabla = i.Nombre
                                 }).Distinct().ToList();
                    foreach(var d in query)
                    {
                        data.Add(new Tuple < int, string > (d.ID, d.NombreTabla));
                    }
                    return data;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Tuple<int, string>> tablaDetail(int id)
        {
            try
            {
                List<Tuple<int, string>> data = new List<Tuple<int, string>>();
                using (var context = new BarandillasEntities())
                {
                    var query = (from t1 in context.Tablas_BD
                                 join t2 in context.RelacionesTablas_BD on t1.TablaID equals t2.TablaRelacionada
                                 where t2.TablaBaseID == id
                                 select new
                                 {
                                     NombreTabla = t1.Nombre,
                                     ID = t2.RelacionID
                                 });

                    foreach (var d in query)
                    {
                        data.Add(new Tuple<int, string>(d.ID, d.NombreTabla));
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public  List<string> selectColumn(string tableName)
        {
            try
            {
                DataTable dt = new DataTable();                
                string SqlString = "SELECT Column_Name from INFORMATION_SCHEMA.COLUMNS where table_name = @tableName";
                List<string> data = new List<string>();
                using (var context = new BarandillasEntities())
                {
                    String con = System.Configuration.ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("tableName", tableName);
                            SqlDataAdapter sda = new SqlDataAdapter();
                            try
                            {
                                conn.Open();
                                sda.SelectCommand = cmd;
                                sda.Fill(dt);                                
                                data= dt.AsEnumerable().Select(n => n.Field<string>(0)).ToList();
                            }
                            catch(Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                                cmd.Dispose();
                                sda.Dispose();
                            }
                        }
                    }                    
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //List<Tuple<string, string>>
        public List<Tuple<string, string>> selectData()
        {
            try
            {
                DataTable dt = new DataTable();
                string SqlString = "SELECT t1.CampoTB,"+"CamposForaneos = REPLACE((SELECT CampoTR AS[data()]"+
                                   "FROM RelacionCamposTablas_BD t2 WHERE t2.CampoTB = t1.CampoTB ORDER BY CampoTR FOR XML PATH('') ), ' ', ',') FROM RelacionCamposTablas_BD t1 GROUP BY CampoTB ";
                List<Tuple<string,string>> data = new List<Tuple<string, string>>();
                String con = System.Configuration.ConfigurationManager.ConnectionStrings["Prueba"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddWithValue("tableName", tableName);
                        SqlDataAdapter sda = new SqlDataAdapter();
                        try
                        {
                            conn.Open();
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);

                            List<DataRow> rows = dt.Rows.Cast<DataRow>().ToList();
                            foreach (DataRow dr in rows)
                            {
                                data.Add(new Tuple<string, string> ( dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString()));
                            }
                            //    //data.Add(dt.Rows[0][i].ToString());
                            //    data.Add(new Tuple<string, string>(dt.Rows[0][i].ToString(), dt.Rows[0][i].ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                            cmd.Dispose();
                            sda.Dispose();
                        }
                    }
                }
                    return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int Insert(RelacionCamposTablas_BD tablas)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    context.RelacionCamposTablas_BD.Add(tablas);
                    context.SaveChanges();
                    return tablas.RelacionID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCampo(string id)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    var query = (from i in context.RelacionCamposTablas_BD
                                 where i.CampoTB == id
                                 select i).ToList();
                    foreach (var d in query)
                    {
                        context.RelacionCamposTablas_BD.Remove(d);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
