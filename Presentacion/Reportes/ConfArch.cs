using System;

namespace Presentacion.Reportes
{
    public class ConfArch
    {

        private string p_RutaArchXMLLog4Net;

        private string p_RutaArchLog4Net;

        public String RutaArchXMLLog4Net
        {
            get
            {
                return p_RutaArchXMLLog4Net;
            }
            set
            {
                p_RutaArchXMLLog4Net = value;
            }
        }

        public String RutaArchLog4Net
        {
            get
            {
                return p_RutaArchLog4Net;
            }
            set
            {
                p_RutaArchLog4Net = value;
            }
        }
    }
}
