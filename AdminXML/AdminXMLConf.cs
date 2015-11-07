using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic;
using AdminXML;

namespace AdminXML
{

    public class AdminXMLConf
    {

        private ArrayList colBody;

        private ArrayList colHead;

        private ArrayList colFoot;

        private string _pathXML;

        private XmlNodeList nodelistPrincipal;

        // Contiene todos los nodos correspondientes al patron
        private XmlNodeList nodelistHeader;

        // Contiene todos los nodos correspondientes al header
        private XmlDocument m_xmld;

        public string pathXML
        {
            get
            {
                return _pathXML;
            }
            set
            {
                _pathXML = value;
            }
        }

        public void abrirXML(string pathXML)
        {
            m_xmld = new XmlDocument();
            // Warning!!! Optional parameters not supported
            if ((pathXML != ""))
            {
                this.pathXML = pathXML;
            }

            //         m_xmld.Load(System.AppDomain.CurrentDomain.BaseDirectory & "Url-Extensions.xml")
            m_xmld.Load(pathXML);
            nodelistPrincipal = m_xmld.SelectNodes("//config");
        }

        public ArrayList getItemsBD()
        {
            return this.geElementos("BD/*");
        }

        public bool setItemsBD(string nom, string servidor, string usuario, string pwd, string tipoBD)
        {
            this.setElemento("BD/*", "NomBD", nom);
            this.setElemento("BD/*", "Servidor", servidor);
            this.setElemento("BD/*", "Us", usuario);
            this.setElemento("BD/*", "Pwd", pwd);
            this.setElemento("BD/*", "BD", tipoBD);
            return this.salvarDocumento();
        }

        public ArrayList getItemsMail()
        {
            return this.geElementos("Mail/*");
        }

        public bool setItemsMail(string Cta_Envio, string Usuario, string Pwd, string Host, string PuertoSalida, string Destinatarios, string Asunto, string Mensaje, string Envia)
        {
            this.setElemento("Mail/*", "Cta_Envio", Cta_Envio);
            this.setElemento("Mail/*", "Usuario", Usuario);
            this.setElemento("Mail/*", "Pwd", Pwd);
            this.setElemento("Mail/*", "Host", Host);
            this.setElemento("Mail/*", "PuertoSalida", PuertoSalida);
            // setElemento("Mail/*", "Destinatarios", Destinatarios)
            // setElemento("Mail/*", "Asunto", Asunto)
            // setElemento("Mail/*", "Mensaje", Mensaje)
            this.setElemento("Mail/*", "Envia", Envia);
            return this.salvarDocumento();
        }

        public ArrayList getItemsRutasSAP()
        {
            return this.geElementos("RutasArchSAP/*");
        }

        public bool setItemsRutasSAP(string RutaArchSAPTrabajadores, string RutaArchSAPTrabajadoresGrales, string RutaArchSAPRel_Trab_Ins_Dep, string RutaArchSAPHistorico_Sueldo, string RutaArchSAPRel_Trab_Agr, string RutaArchSAPRel_Trab_Coleccion, string RutaArchPatSAPTrabajadores, string RutaArchPatSAPTrabajadoresGrales, string RutaArchPatSAPRel_Trab_Ins_Dep, string RutaArchPatSAPHistorico_Sueldo, string RutaArchPatSAPRel_Trab_Agr, string RutaArchPatSAPRel_Trab_Coleccion)
        {
            this.setElemento("RutasArchSAP/*", "RutaArchSAPTrabajadores", RutaArchSAPTrabajadores);
            this.setElemento("RutasArchSAP/*", "RutaArchSAPTrabajadoresGrales", RutaArchSAPTrabajadoresGrales);
            this.setElemento("RutasArchSAP/*", "RutaArchSAPRel_Trab_Ins_Dep", RutaArchSAPRel_Trab_Ins_Dep);
            this.setElemento("RutasArchSAP/*", "RutaArchSAPHistorico_Sueldo", RutaArchSAPHistorico_Sueldo);
            this.setElemento("RutasArchSAP/*", "RutaArchSAPRel_Trab_Agr", RutaArchSAPRel_Trab_Agr);
            this.setElemento("RutasArchSAP/*", "RutaArchSAPRel_Trab_Coleccion", RutaArchSAPRel_Trab_Coleccion);
            this.setElemento("RutasArchSAP/*", "RutaArchPatSAPTrabajadores", RutaArchPatSAPTrabajadores);
            this.setElemento("RutasArchSAP/*", "RutaArchPatSAPTrabajadoresGrales", RutaArchPatSAPTrabajadoresGrales);
            this.setElemento("RutasArchSAP/*", "RutaArchPatSAPRel_Trab_Ins_Dep", RutaArchPatSAPRel_Trab_Ins_Dep);
            this.setElemento("RutasArchSAP/*", "RutaArchPatSAPHistorico_Sueldo", RutaArchPatSAPHistorico_Sueldo);
            this.setElemento("RutasArchSAP/*", "RutaArchPatSAPRel_Trab_Agr", RutaArchPatSAPRel_Trab_Agr);
            this.setElemento("RutasArchSAP/*", "RutaArchPatSAPRel_Trab_Coleccion", RutaArchPatSAPRel_Trab_Coleccion);
            return this.salvarDocumento();
        }

        public ArrayList getItemsRutasFondoAhorro()
        {
            return this.geElementos("RutasArchFondoAhorro/*");
        }

        public bool setItemsRutasFondoAhorro(string RutaArchPatFondoAhorroAltaEmpleados, string RutaArchFondoAhorroAltaEmpleados, string RutaArchPatFondoAhorroAportaciones, string RutaArchFondoAhorroAportaciones, string RutaArchPatFondoAhorroPagoPrestamos, string RutaArchFondoAhorroPagoPrestamos, string RutaArchPatFondoAhorroPrestamos, string RutaArchFondoAhorroPrestamos)
        {
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchPatFondoAhorroAltaEmpleados", RutaArchPatFondoAhorroAltaEmpleados);
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchFondoAhorroAltaEmpleados", RutaArchFondoAhorroAltaEmpleados);
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchPatFondoAhorroAportaciones", RutaArchPatFondoAhorroAportaciones);
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchFondoAhorroAportaciones", RutaArchFondoAhorroAportaciones);
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchPatFondoAhorroPagoPrestamos", RutaArchPatFondoAhorroPagoPrestamos);
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchFondoAhorroPagoPrestamos", RutaArchFondoAhorroPagoPrestamos);
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchPatFondoAhorroPrestamos", RutaArchPatFondoAhorroPrestamos);
            this.setElemento("RutasArchFondoAhorro/*", "RutaArchFondoAhorroPrestamos", RutaArchFondoAhorroPrestamos);
            return this.salvarDocumento();
        }

        public ArrayList getItemsRutasIPE()
        {
            return this.geElementos("RutasArchIPE/*");
        }

        public bool setItemsRutasIPE(string RutaArchPatIPEAltaEmpleados, string RutaArchIPEAltaEmpleados, string RutaArchPatIPEAltaMezclas, string RutaArchIPEAltaMezclas, string RutaArchPatIPEAportaciones, string RutaArchIPEAportaciones)
        {
            this.setElemento("RutasArchIPE/*", "RutaArchPatIPEAltaEmpleados", RutaArchPatIPEAltaEmpleados);
            this.setElemento("RutasArchIPE/*", "RutaArchIPEAltaEmpleados", RutaArchIPEAltaEmpleados);
            this.setElemento("RutasArchIPE/*", "RutaArchPatIPEAltaMezclas", RutaArchPatIPEAltaMezclas);
            this.setElemento("RutasArchIPE/*", "RutaArchIPEAltaMezclas", RutaArchIPEAltaMezclas);
            this.setElemento("RutasArchIPE/*", "RutaArchPatIPEAportaciones", RutaArchPatIPEAportaciones);
            this.setElemento("RutasArchIPE/*", "RutaArchIPEAportaciones", RutaArchIPEAportaciones);
            return this.salvarDocumento();
        }

        public ArrayList getItemsRutasPolizaContSAP()
        {
            return this.geElementos("RutasArchPolizaContSAP/*");
        }

        public bool setItemsRutasPolizaContSAP(string RutaArchPatPolizaContSAP, string RutaArchPolizaContSAP)
        {
            this.setElemento("RutasArchPolizaContSAP/*", "RutaArchPatPolizaContSAP", RutaArchPatPolizaContSAP);
            this.setElemento("RutasArchPolizaContSAP/*", "RutaArchPolizaContSAP", RutaArchPolizaContSAP);
            return this.salvarDocumento();
        }

        public ArrayList getItemsRutasDispBanc()
        {
            return this.geElementos("RutasArchDispBanc/*");
        }

        public bool setItemsRutasDispBanc(string RutaArchPatDispBanc, string RutaArchDispBanc)
        {
            this.setElemento("RutasArchDispBanc/*", "RutaArchPatDispBanc", RutaArchPatDispBanc);
            this.setElemento("RutasArchDispBanc/*", "RutaArchDispBanc", RutaArchDispBanc);
            return this.salvarDocumento();
        }

        public ArrayList getItemsRutasArch()
        {
            return this.geElementos("RutasArch/*");
        }

        public bool setItemsRutasArch(string RutaArchXMLLog4Net, string RutaArchLog4Net)
        {
            this.setElemento("RutasArch/*", "RutaArchXMLLog4Net", RutaArchXMLLog4Net);
            this.setElemento("RutasArch/*", "RutaArchLog4Net", RutaArchLog4Net);
            return this.salvarDocumento();
        }

        public ArrayList getItemsServicio()
        {
            return this.geElementos("Servicio/*");
        }

        public bool setItemsServicio(string Hour, string Minute, string Second, string Intervalo)
        {
            this.setElemento("Servicio/*", "Hour", Hour);
            this.setElemento("Servicio/*", "Minute", Minute);
            this.setElemento("Servicio/*", "Second", Second);
            this.setElemento("Servicio/*", "Intervalo", Intervalo);
            return this.salvarDocumento();
        }

        private ArrayList geElementos(string xpath)
        {
            ArrayList col = new ArrayList();
            XmlNodeList nodelist = nodelistPrincipal[0].SelectNodes(xpath);
            foreach (XmlNode node in nodelist)
            {
                col.Add(this.generaItemPattern(node));
            }

            return col;
        }

        private void setElemento(string path, string elem, string val)
        {
            nodelistPrincipal = m_xmld.SelectNodes("//config");
            XmlNodeList nodes = m_xmld.SelectNodes("//config");
            XmlNodeList nod2 = nodes[0].SelectNodes(path);
            foreach (XmlNode node in nod2)
            {
                // MsgBox(node.Attributes("name").Value & ":" & node.FirstChild.InnerText)
                if ((node.Attributes["name"].Value == elem))
                {
                    node.FirstChild.InnerText = val;
                }

            }

        }

        private bool salvarDocumento()
        {
            try
            {
                m_xmld.Save(_pathXML);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public ArrayList setValuesDefault(XmlNode node)
        {
            XmlNodeList nodeList = node.SelectNodes("value");
            ArrayList colValues = new ArrayList();
            foreach (XmlNode node2 in nodeList)
            {
                colValues.Add(node2.InnerText);
            }

            return colValues;
        }

        private ItemConf generaItemPattern(XmlNode node)
        {
            ItemConf itemConf = this.generaItemConf(node.Attributes);
            itemConf.valuesDefault = this.setValuesDefault(node);
            return itemConf;
        }

        private ItemConf generaItemConf(XmlAttributeCollection atributos)
        {
            return this.setValorItemConf(atributos);
        }

        private ItemConf setValorItemConf(XmlAttributeCollection atributos)
        {
            ItemConf itemConf = new ItemConf();
            itemConf.setValuesByAttribute(atributos);
            return itemConf;
        }

        public AdminXMLConf(string pathXML)
        {
            this.abrirXML(pathXML);
        }

        public AdminXMLConf()
        {
        }
    }
}
