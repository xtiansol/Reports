using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Helpers
{
    public class Paginado
    {
        public List<ConexionBD.Tablas_BD> Customers { get; set; }
        public int TotalRecords { get; set; }
    }
}
