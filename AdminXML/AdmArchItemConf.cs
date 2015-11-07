using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminXML;
using AdminArch;

namespace AdminXML
{

    public class AdmArchItemConf
    {

        private AdminXMLConf admXML = new AdminXMLConf();

        private AdminArch.AdminArch admArc = new AdminArch.AdminArch();

        private ArrayList colBD;

        private ArrayList colMail;

        private ArrayList colArchSAP;

        private ArrayList colArchFondoAhorro;

        private ArrayList colArchIPE;

        private ArrayList colArchPolizaContSAP;

        private ArrayList colArchDispBanc;

        private ArrayList colArch;

        private ArrayList colServ;

        private ArrayList colGen = new ArrayList();

        public AdmArchItemConf()
        {
        }

        public AdmArchItemConf(string pathXML)
        {
            this.setArchConf(pathXML);
        }

        public bool setBDConf(string nom, string servidor, string usuario, string pwd, string tipoBD)
        {
            return admXML.setItemsBD(nom, servidor, usuario, pwd, tipoBD);
        }

        public ArrayList getBDConf()
        {
            return colBD;
        }

        public bool setMailConf(string RutaArchSAPTrabajadores, string RutaArchSAPTrabajadoresGrales, string RutaArchSAPRel_Trab_Ins_Dep, string RutaArchSAPHistorico_Sueldo, string RutaArchSAPRel_Trab_Agr, string RutaArchSAPRel_Trab_Coleccion, string RutaArchPatSAPTrabajadores, string RutaArchPatSAPTrabajadoresGrales, string RutaArchPatSAPRel_Trab_Ins_Dep, string RutaArchPatSAPHistorico_Sueldo, string RutaArchPatSAPRel_Trab_Agr, string RutaArchPatSAPRel_Trab_Coleccion)
        {
            return admXML.setItemsRutasSAP(RutaArchSAPTrabajadores, RutaArchSAPTrabajadoresGrales, RutaArchSAPRel_Trab_Ins_Dep, RutaArchSAPHistorico_Sueldo, RutaArchSAPRel_Trab_Agr, RutaArchSAPRel_Trab_Coleccion, RutaArchPatSAPTrabajadores, RutaArchPatSAPTrabajadoresGrales, RutaArchPatSAPRel_Trab_Ins_Dep, RutaArchPatSAPHistorico_Sueldo, RutaArchPatSAPRel_Trab_Agr, RutaArchPatSAPRel_Trab_Coleccion);
        }

        public ArrayList getMailConf()
        {
            return colMail;
        }

        public bool getMailConf(string Cta_Envio, string Usuario, string Pwd, string Host, string PuertoSalida, string Destinatarios, string Asunto, string Mensaje, string Envia)
        {
            return admXML.setItemsMail(Cta_Envio, Usuario, Pwd, Host, PuertoSalida, Destinatarios, Asunto, Mensaje, Envia);
        }

        public bool setItemsRutasSAP(string RutaArchSAPTrabajadores, string RutaArchSAPTrabajadoresGrales, string RutaArchSAPRel_Trab_Ins_Dep, string RutaArchSAPHistorico_Sueldo, string RutaArchSAPRel_Trab_Agr, string RutaArchSAPRel_Trab_Coleccion, string RutaArchPatSAPTrabajadores, string RutaArchPatSAPTrabajadoresGrales, string RutaArchPatSAPRel_Trab_Ins_Dep, string RutaArchPatSAPHistorico_Sueldo, string RutaArchPatSAPRel_Trab_Agr, string RutaArchPatSAPRel_Trab_Coleccion)
        {
            return admXML.setItemsRutasSAP(RutaArchSAPTrabajadores, RutaArchSAPTrabajadoresGrales, RutaArchSAPRel_Trab_Ins_Dep, RutaArchSAPHistorico_Sueldo, RutaArchSAPRel_Trab_Agr, RutaArchSAPRel_Trab_Coleccion, RutaArchPatSAPTrabajadores, RutaArchPatSAPTrabajadoresGrales, RutaArchPatSAPRel_Trab_Ins_Dep, RutaArchPatSAPHistorico_Sueldo, RutaArchPatSAPRel_Trab_Agr, RutaArchPatSAPRel_Trab_Coleccion);
        }

        public ArrayList getArchSAP()
        {
            return colArchSAP;
        }

        public bool setArchFondoAhorro(string RutaArchPatFondoAhorroAltaEmpleados, string RutaArchFondoAhorroAltaEmpleados, string RutaArchPatFondoAhorroAportaciones, string RutaArchFondoAhorroAportaciones, string RutaArchPatFondoAhorroPagoPrestamos, string RutaArchFondoAhorroPagoPrestamos, string RutaArchPatFondoAhorroPrestamos, string RutaArchFondoAhorroPrestamos)
        {
            return admXML.setItemsRutasFondoAhorro(RutaArchPatFondoAhorroAltaEmpleados, RutaArchFondoAhorroAltaEmpleados, RutaArchPatFondoAhorroAportaciones, RutaArchFondoAhorroAportaciones, RutaArchPatFondoAhorroPagoPrestamos, RutaArchFondoAhorroPagoPrestamos, RutaArchPatFondoAhorroPrestamos, RutaArchFondoAhorroPrestamos);
        }

        public ArrayList getArchFondoAhorro()
        {
            return colArchFondoAhorro;
        }

        public bool setArchIPE(string RutaArchPatIPEAltaEmpleados, string RutaArchIPEAltaEmpleados, string RutaArchPatIPEAltaMezclas, string RutaArchIPEAltaMezclas, string RutaArchPatIPEAportaciones, string RutaArchIPEAportaciones)
        {
            return admXML.setItemsRutasIPE(RutaArchPatIPEAltaEmpleados, RutaArchIPEAltaEmpleados, RutaArchPatIPEAltaMezclas, RutaArchIPEAltaMezclas, RutaArchPatIPEAportaciones, RutaArchIPEAportaciones);
        }

        public ArrayList getArchIPE()
        {
            return colArchIPE;
        }

        public bool setArchPolizaContSAP(string RutaArchPatPolizaContSAP, string RutaArchPolizaContSAP)
        {
            return admXML.setItemsRutasPolizaContSAP(RutaArchPatPolizaContSAP, RutaArchPolizaContSAP);
        }

        public ArrayList getArchPolizaContSAP()
        {
            return colArchPolizaContSAP;
        }

        public bool setArchDispBanc(string RutaArchPatDispBanc, string RutaArchDispBanc)
        {
            return admXML.setItemsRutasDispBanc(RutaArchPatDispBanc, RutaArchDispBanc);
        }

        public ArrayList getArchDispBanc()
        {
            return colArchDispBanc;
        }

        public bool setArch(string RutaArchXMLLog4Net, string RutaArchLog4Net)
        {
            return admXML.setItemsRutasArch(RutaArchXMLLog4Net, RutaArchLog4Net);
        }

        public ArrayList getArch()
        {
            return colArch;
        }

        public bool setServ(string Hour, string Minute, string Second, string Intervalo)
        {
            return admXML.setItemsServicio(Hour, Minute, Second, Intervalo);
        }

        public ArrayList getServ()
        {
            return colServ;
        }

        public void setArchConf(string pathXML)
        {
            admXML.abrirXML(pathXML);
            colBD = admXML.getItemsBD();
            colMail = admXML.getItemsMail();
            colArchSAP = admXML.getItemsRutasSAP();
            colArch = admXML.getItemsRutasArch();
            colServ = admXML.getItemsServicio();
            colArchFondoAhorro = admXML.getItemsRutasFondoAhorro();
            colArchIPE = admXML.getItemsRutasIPE();
            colArchPolizaContSAP = admXML.getItemsRutasPolizaContSAP();
            colArchDispBanc = admXML.getItemsRutasDispBanc();
        }
    }
}
