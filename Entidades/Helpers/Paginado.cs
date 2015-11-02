using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Helpers
{
    public class Paginado
    {
        public List<Tablas> Customers { get; set; }
        public int TotalRecords { get; set; }
    }
    public class PaginadoRelacion
    {
        public List<Relaciones> Customers { get; set; }
        public int TotalRecords { get; set; }
        public int Count { get; set; }
    }
}
