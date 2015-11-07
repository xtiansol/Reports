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
        private string ruta = (System.AppDomain.CurrentDomain.BaseDirectory +"Reportes\\"+ "config.xml");

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

        // Public Function getConfArchSAP() As ConfArchSAP
        //     Return generaValue("ArchSAP", admXMLConf.getItemsRutasSAP())
        // End Function
        public bool setConfArchSAP(string RutaArchSAPTrabajadores, string RutaArchSAPTrabajadoresGrales, string RutaArchSAPRel_Trab_Ins_Dep, string RutaArchSAPHistorico_Sueldo, string RutaArchSAPRel_Trab_Agr, string RutaArchSAPRel_Trab_Coleccion, string RutaArchPatSAPTrabajadores, string RutaArchPatSAPTrabajadoresGrales, string RutaArchPatSAPRel_Trab_Ins_Dep, string RutaArchPatSAPHistorico_Sueldo, string RutaArchPatSAPRel_Trab_Agr, string RutaArchPatSAPRel_Trab_Coleccion)
        {
            return admXMLConf.setItemsRutasSAP(RutaArchSAPTrabajadores, RutaArchSAPTrabajadoresGrales, RutaArchSAPRel_Trab_Ins_Dep, RutaArchSAPHistorico_Sueldo, RutaArchSAPRel_Trab_Agr, RutaArchSAPRel_Trab_Coleccion, RutaArchPatSAPTrabajadores, RutaArchPatSAPTrabajadoresGrales, RutaArchPatSAPRel_Trab_Ins_Dep, RutaArchPatSAPHistorico_Sueldo, RutaArchPatSAPRel_Trab_Agr, RutaArchPatSAPRel_Trab_Coleccion);
        }

        // Public Function getConfArchFondoAhorro() As ConfFondoAhorro
        //     Return generaValue("ArchFondoAhorro", admXMLConf.getItemsRutasFondoAhorro())
        // End Function
        public bool setConfArchFondoAhorro(string RutaArchPatFondoAhorroAltaEmpleados, string RutaArchFondoAhorroAltaEmpleados, string RutaArchPatFondoAhorroAportaciones, string RutaArchFondoAhorroAportaciones, string RutaArchPatFondoAhorroPagoPrestamos, string RutaArchFondoAhorroPagoPrestamos, string RutaArchPatFondoAhorroPrestamos, string RutaArchFondoAhorroPrestamos)
        {
            return admXMLConf.setItemsRutasFondoAhorro(RutaArchPatFondoAhorroAltaEmpleados, RutaArchFondoAhorroAltaEmpleados, RutaArchPatFondoAhorroAportaciones, RutaArchFondoAhorroAportaciones, RutaArchPatFondoAhorroPagoPrestamos, RutaArchFondoAhorroPagoPrestamos, RutaArchPatFondoAhorroPrestamos, RutaArchFondoAhorroPrestamos);
        }

        // Public Function getConfArchIPE() As ConfIPE
        //     Return generaValue("ArchIPE", admXMLConf.getItemsRutasIPE())
        // End Function
        public bool setConfArchIPE(string RutaArchPatIPEAltaEmpleados, string RutaArchIPEAltaEmpleados, string RutaArchPatIPEAltaMezclas, string RutaArchIPEAltaMezclas, string RutaArchPatIPEAportaciones, string RutaArchIPEAportaciones)
        {
            return admXMLConf.setItemsRutasIPE(RutaArchPatIPEAltaEmpleados, RutaArchIPEAltaEmpleados, RutaArchPatIPEAltaMezclas, RutaArchIPEAltaMezclas, RutaArchPatIPEAportaciones, RutaArchIPEAportaciones);
        }

        // Public Function getConfPolizaContSAP() As ConfPolizaContableSAP
        //     Return generaValue("ArchPolizaContableSAP", admXMLConf.getItemsRutasPolizaContSAP())
        // End Function
        public bool setConfPolizaContSAP(string RutaArchPatPolizaContSAP, string RutaArchPolizaContSAP)
        {
            return admXMLConf.setItemsRutasPolizaContSAP(RutaArchPatPolizaContSAP, RutaArchPolizaContSAP);
        }

        // Public Function getConfDispBanc() As ConfDispBanc
        //     Return generaValue("ArchDispBanc", admXMLConf.getItemsRutasDispBanc())
        // End Function
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

        // Public Function getConfServ() As ConfServicio
        //     Return generaValue("Serv", admXMLConf.getItemsServicio())
        // End Function
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
                    // Case "ArchSAP"
                    //     objTem = setColeValueOrig((New ConfArchSAP).GetType, col)
                    // Case "ArchFondoAhorro"
                    //     objTem = setColeValueOrig((New ConfFondoAhorro).GetType, col)
                    // Case "ArchIPE"
                    //     objTem = setColeValueOrig((New ConfIPE).GetType, col)
                    // Case "ArchPolizaContableSAP"
                    //     objTem = setColeValueOrig((New ConfPolizaContableSAP).GetType, col)
                    // Case "ArchDispBanc"
                    //     objTem = setColeValueOrig((New ConfDispBanc).GetType, col)
                    break;
                case "Archs":
                    tipo = (new ConfArch()).GetType();
                    objTem = this.setColeValueOrig(ref tipo, ref col);
                    // Case "Serv"
                    //     objTem = setColeValueOrig((New ConfServicio).GetType, col)
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