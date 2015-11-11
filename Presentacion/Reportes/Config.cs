using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminXML;
using AdminReflex;
using System.IO;

namespace Presentacion.Reportes
{
    public class Config
    {
        private string ruta = (System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\" + "config.xml");

        private AdminXML.AdminXMLConf admXMLConf = new AdminXML.AdminXMLConf();

        private AdminReflex.AdminReflex admReflex = new AdminReflex.AdminReflex();

        public Config()
        {
        }

        public void getConfiguraciones()
        {
            admXMLConf.abrirXML(ruta);
        }

        public ConfBD getConfBD()
        {
            return (ConfBD)this.generaValue("BD", admXMLConf.getItemsBD());
        }

        public bool setConfBD(string nom, string servidor, string usuario, string pwd, string tipoBD)
        {
            return admXMLConf.setItemsBD(nom, servidor, usuario, pwd, tipoBD);
        }

        public ConfMail getConfMail()
        {
            return (ConfMail)this.generaValue("Mail", admXMLConf.getItemsMail());
        }

        public bool setConfMail(string Cta_Envio, string Usuario, string Pwd, string Host, string PuertoSalida, string Destinatarios, string Asunto, string Mensaje, string Envia)
        {
            return admXMLConf.setItemsMail(Cta_Envio, Usuario, Pwd, Host, PuertoSalida, Destinatarios, Asunto, Mensaje, Envia);
        }

        public bool setConfArchSAP(string RutaArchSAPTrabajadores, string RutaArchSAPTrabajadoresGrales, string RutaArchSAPRel_Trab_Ins_Dep, string RutaArchSAPHistorico_Sueldo, string RutaArchSAPRel_Trab_Agr, string RutaArchSAPRel_Trab_Coleccion, string RutaArchPatSAPTrabajadores, string RutaArchPatSAPTrabajadoresGrales, string RutaArchPatSAPRel_Trab_Ins_Dep, string RutaArchPatSAPHistorico_Sueldo, string RutaArchPatSAPRel_Trab_Agr, string RutaArchPatSAPRel_Trab_Coleccion)
        {
            return admXMLConf.setItemsRutasSAP(RutaArchSAPTrabajadores, RutaArchSAPTrabajadoresGrales, RutaArchSAPRel_Trab_Ins_Dep, RutaArchSAPHistorico_Sueldo, RutaArchSAPRel_Trab_Agr, RutaArchSAPRel_Trab_Coleccion, RutaArchPatSAPTrabajadores, RutaArchPatSAPTrabajadoresGrales, RutaArchPatSAPRel_Trab_Ins_Dep, RutaArchPatSAPHistorico_Sueldo, RutaArchPatSAPRel_Trab_Agr, RutaArchPatSAPRel_Trab_Coleccion);
        }


        public bool setConfArchFondoAhorro(string RutaArchPatFondoAhorroAltaEmpleados, string RutaArchFondoAhorroAltaEmpleados, string RutaArchPatFondoAhorroAportaciones, string RutaArchFondoAhorroAportaciones, string RutaArchPatFondoAhorroPagoPrestamos, string RutaArchFondoAhorroPagoPrestamos, string RutaArchPatFondoAhorroPrestamos, string RutaArchFondoAhorroPrestamos)
        {
            return admXMLConf.setItemsRutasFondoAhorro(RutaArchPatFondoAhorroAltaEmpleados, RutaArchFondoAhorroAltaEmpleados, RutaArchPatFondoAhorroAportaciones, RutaArchFondoAhorroAportaciones, RutaArchPatFondoAhorroPagoPrestamos, RutaArchFondoAhorroPagoPrestamos, RutaArchPatFondoAhorroPrestamos, RutaArchFondoAhorroPrestamos);
        }

        public bool setConfArchIPE(string RutaArchPatIPEAltaEmpleados, string RutaArchIPEAltaEmpleados, string RutaArchPatIPEAltaMezclas, string RutaArchIPEAltaMezclas, string RutaArchPatIPEAportaciones, string RutaArchIPEAportaciones)
        {
            return admXMLConf.setItemsRutasIPE(RutaArchPatIPEAltaEmpleados, RutaArchIPEAltaEmpleados, RutaArchPatIPEAltaMezclas, RutaArchIPEAltaMezclas, RutaArchPatIPEAportaciones, RutaArchIPEAportaciones);
        }

        public bool setConfPolizaContSAP(string RutaArchPatPolizaContSAP, string RutaArchPolizaContSAP)
        {
            return admXMLConf.setItemsRutasPolizaContSAP(RutaArchPatPolizaContSAP, RutaArchPolizaContSAP);
        }

        public bool setConfDispBanc(string RutaArchPatDispBanc, string RutaArchDispBanc)
        {
            return admXMLConf.setItemsRutasDispBanc(RutaArchPatDispBanc, RutaArchDispBanc);
        }

        public ConfArch getConfArch()
        {
            return (ConfArch)this.generaValue("Archs", admXMLConf.getItemsRutasArch());
        }

        public bool setConfArch(string RutaArchXMLLog4Net, string RutaArchLog4Net)
        {
            return admXMLConf.setItemsRutasArch(RutaArchXMLLog4Net, RutaArchLog4Net);
        }
        public bool setConfServ(string Hour, string Minute, string Second, string Intervalo)
        {
            return admXMLConf.setItemsServicio(Hour, Minute, Second, Intervalo);
        }

        private object generaValue(string nom, ArrayList col)
        {
            object objTem = null;
            Type tipo = null;
            switch (nom)
            {
                case "BD":
                    tipo = (new ConfBD()).GetType();
                    objTem = this.setColeValueOrig(ref tipo, ref col);
                    break;
                case "Mail":
                    tipo = (new ConfMail()).GetType();
                    objTem = this.setColeValueOrig(ref tipo, ref col);

                    break;
                case "Archs":
                    tipo = (new ConfArch()).GetType();
                    objTem = this.setColeValueOrig(ref tipo, ref col);

                    break;
            }
            return objTem;
        }

        private object setColeValueOrig(ref Type obj, ref ArrayList colDat)
        {
            return admReflex.getValueFromColItemPattern(obj, colDat);
        }
    }
}