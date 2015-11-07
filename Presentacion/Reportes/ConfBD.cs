using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Reportes
{
    public class ConfBD
    {
        private string p_NomBD;

        private string p_Servidor;

        private string p_Us;

        private string p_Pwd;

        private string p_BD;

        public string NomBD
        {
            get
            {
                return p_NomBD;
            }
            set
            {
                p_NomBD = value;
            }
        }

        public string Servidor
        {
            get
            {
                return p_Servidor;
            }
            set
            {
                p_Servidor = value;
            }
        }

        public string Us
        {
            get
            {
                return p_Us;
            }
            set
            {
                p_Us = value;
            }
        }

        public string Pwd
        {
            get
            {
                return p_Pwd;
            }
            set
            {
                p_Pwd = value;
            }
        }

        public string BD
        {
            get
            {
                return p_BD;
            }
            set
            {
                p_BD = value;
            }
        }
    }
}