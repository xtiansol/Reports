using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.ConexionBD;
namespace Datos
{
    public class clsTablasRelaciones
    {
        public List<Tuple<int, string>> selectTables()
        {
            try
            {
                List<Tuple<int, string>> tablas = new List<Tuple<int, string>>();
                using(var context = new BarandillasEntities())
                {
                    var query = (from t in context.Tablas_BD
                                 orderby t.TablaID
                                 select new
                                 {
                                     ID = t.TablaID,
                                     Nombretabla = t.NombreTabla
                                 }).ToList();
                    foreach(var i in query)
                    {
                        tablas.Add(new Tuple<int, string>(i.ID, i.Nombretabla));
                    }
                    return tablas;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int Insert(RelacionesTablas_BD tablas)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    context.RelacionesTablas_BD.Add(tablas);
                    context.SaveChanges();
                    return tablas.RelacionID;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
