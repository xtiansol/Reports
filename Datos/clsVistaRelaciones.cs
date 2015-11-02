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
                                 group p by new { p.TablaID, p.NombreTabla } into i
                                 select new
                                 {
                                     TablaID = i.Key.TablaID,
                                     NombreTabla = i.Key.NombreTabla,
                                     Count = i.Count()
                                 })
                                 .OrderBy(i => i.TablaID)
                                .Skip(skip)
                                .Take(take)
                                .ToList().Distinct();
                    //var blogs = context.RelacionesDeTablas.SqlQuery("Select distinct(TablaID),NombreTabla, COUNT(TablaID) as Relaciones,r.RelacionID, r.TablaRelacionada, r.Descripcion from RelacionesDeTablas group by TablaID, NombreTabla").ToList();

                    //var query2 = context.RelacionesDeTablas.GroupBy(t => new { t.TablaID, t.NombreTabla}).Select(g => new { g.Key.TablaID, g.Key.NombreTabla, count = g.Select(l => l.RelacionID).Distinct().Count() });
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
    }
}
