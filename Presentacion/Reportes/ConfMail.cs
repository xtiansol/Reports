using System.Collections;

namespace Presentacion.Reportes
{
    public class ConfMail
    {

        private string p_Cta_Envio;

        private string p_Usuario;

        private string p_Pwd;

        private string p_Host;

        private string p_Destinatarios;

        private string p_PuertoSalida;

        private string p_Asunto;

        private string p_Mensaje;

        private ArrayList p_CollectionValues;

        private string p_Envia = "true";

        public string Cta_Envio
        {
            get
            {
                return p_Cta_Envio;
            }
            set
            {
                p_Cta_Envio = value;
            }
        }

        public string Usuario
        {
            get
            {
                return p_Usuario;
            }
            set
            {
                p_Usuario = value;
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

        public string Host
        {
            get
            {
                return p_Host;
            }
            set
            {
                p_Host = value;
            }
        }

        public string Destinatarios
        {
            get
            {
                return p_Destinatarios;
            }
            set
            {
                p_Destinatarios = value;
            }
        }

        public string PuertoSalida
        {
            get
            {
                return p_PuertoSalida;
            }
            set
            {
                p_PuertoSalida = value;
            }
        }

        public string Asunto
        {
            get
            {
                return p_Asunto;
            }
            set
            {
                p_Asunto = value;
            }
        }

        public string Mensaje
        {
            get
            {
                return p_Mensaje;
            }
            set
            {
                p_Mensaje = value;
            }
        }

        public string Envia
        {
            get
            {
                return p_Envia;
            }
            set
            {
                p_Envia = value;
            }
        }

        public ArrayList MailsEnvio
        {
            get
            {
                p_CollectionValues = new ArrayList();
                int coma = p_Destinatarios.IndexOf(";", 0);
                int tam = p_Destinatarios.Length;
                int cont = 0;
                while ((cont < tam))
                {
                    p_CollectionValues.Add(p_Destinatarios.Substring(cont, (coma - cont)));
                    cont = (coma + 1);
                    if ((cont < tam))
                    {
                        coma = p_Destinatarios.IndexOf(";", (cont + 1));
                    }

                }

                return p_CollectionValues;
            }
        }
    }
}