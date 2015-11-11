using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.ConexionBD;
using Entidades.Helpers;

namespace Datos
{
    public class clsVistaRelaciones
    {
        public Entidades.Helpers.PaginadoRelacion selectAll(int skip, int take)
        {
            try
            {
                var data = new Entidades.Helpers.PaginadoRelacion();
                List<Relaciones> t = new List<Relaciones>();
                using (var context = new BarandillasEntities())
                {

                    var query = (from p in context.RelacionesDeTablas
                                 group p by new { p.TablaID, p.Nombre } into i
                                 select new
                                 {
                                     TablaID = i.Key.TablaID,
                                     NombreTabla = i.Key.Nombre,
                                     Count = i.Count()
                                 })
                                 .OrderBy(i => i.TablaID)
                                .Skip(skip)
                                .Take(take)
                                .ToList().Distinct();
                    foreach (var i in query)
                    {
                        t.Add(new Relaciones { TablaID = i.TablaID, NombreTabla = i.NombreTabla, Count = i.Count});
                    }

                    data.Customers = t;
                    data.TotalRecords = t.Count();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<int> tablasRelacionadas(int id)
        {
            try
            {
                List<int> data = new List<int>();
                using (var context = new BarandillasEntities())
                {
                    var query = (from i in context.RelacionesDeTablas
                                 where i.TablaID == id
                                 select i.TablaRelacionada).ToList();
                    if (query.Count > 0)
                    {
                        foreach (var i in query)
                        {
                            data.Add(i);
                        }
                    }
                    else
                    {
                        data.Add(0);
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
