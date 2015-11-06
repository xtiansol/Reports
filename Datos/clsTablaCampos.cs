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
                                     NombreTabla = i.NombreTabla
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
                                 where t2.TablaID == id
                                 select new
                                 {
                                     NombreTabla = t1.NombreTabla,
                                     ID = t1.TablaID
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
    }
}
