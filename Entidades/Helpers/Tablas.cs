using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Helpers
{
    public class Tablas
    {
        public int TablaID { get; set; }
        public string NombreTabla { get; set; }
        public string Descripcion { get; set; }
        public string TipoTabla { get; set; }
        public Nullable<bool> Estatus { get; set; }
    }
}
