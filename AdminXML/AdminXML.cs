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

    public class AdminXML
    {

        private ArrayList colBody;

        private ArrayList colHead;

        private ArrayList colFoot;

        private string _pathXML;

        private XmlNodeList nodelistPrincipal;

        // Contiene todos los nodos correspondientes al patron
        private XmlNodeList nodelistHeader;

        // Contiene todos los nodos correspondientes al header
        public String pathXML
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

        public bool abrirXML(string pathXML)
        {
            XmlDocument m_xmld = new XmlDocument();
            // Warning!!! Optional parameters not supported
            if ((pathXML == ""))
            {
                this.pathXML = pathXML;
            }

            //         m_xmld.Load(System.AppDomain.CurrentDomain.BaseDirectory & "Url-Extensions.xml")
            try
            {
                m_xmld.Load(pathXML);
                nodelistPrincipal = m_xmld.SelectNodes("//pattern");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public ArrayList getItemsHeader()
        {
            return this.geElementos("header/*");
        }

        public ArrayList getItemsHeader2()
        {
            return this.geElementos("header2/*");
        }

        public ArrayList getItemsBody()
        {
            return this.geElementos("body/*");
        }

        public ArrayList getItemsFooter()
        {
            return this.geElementos("footer/*");
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

        private ItemPattern generaItemPattern(XmlNode node)
        {
            ItemPattern itemPat = this.generaItemPatern(node.Attributes);
            itemPat.valuesDefault = this.setValuesDefault(node);
            return itemPat;
        }

        private ItemPattern generaItemPatern(XmlAttributeCollection atributos)
        {
            return this.setValorItemPatern(atributos);
        }

        private ItemPattern setValorItemPatern(XmlAttributeCollection atributos)
        {
            ItemPattern itemPat = new ItemPattern();
            itemPat.setValuesByAttribute(atributos);
            return itemPat;
        }

        // Public Sub LeerXml(ByVal S As Stream)
        //     Dim reader As New XmlTextReader(S)
        //     'no he probrado codigo aun toy apuradito mi veija ta que 
        //     reader.WhitespaceHandling = WhitespaceHandling.None
        //     Dim num As Integer = 0
        //     'mientras haya que leer
        //     While reader.Read()
        //         Select Case reader.NodeType
        //             'leer elementos
        //             Case XmlNodeType.Element
        //                 num += 1
        //                 Me.WriteRich("<" & reader.Name & ">", num)
        //             Case XmlNodeType.Text      'leer texto
        //                 Me.WriteRich(reader.Value, (num + 1))
        //             Case XmlNodeType.CDATA     'seccion CDATA
        //                 Me.WriteRich("<![CDATA[" & reader.Value & "]]>")
        //             Case XmlNodeType.ProcessingInstruction    'instruccion de procesamiento
        //                 Me.WriteRich("<?" & reader.Name & reader.Value & "?>")
        //             Case XmlNodeType.Comment
        //                 Me.WriteRich("<!--reader.Value-->")
        //             Case XmlNodeType.XmlDeclaration
        //                 Me.WriteRich("<?xml version='1.0'?>")
        //             Case XmlNodeType.Document
        //             Case XmlNodeType.DocumentType
        //                 Me.WriteRich("<!DOCTYPE " & reader.Name & " [" & reader.Value & "]")
        //             Case XmlNodeType.EntityReference
        //                 Me.WriteRich(reader.Name)
        //             Case XmlNodeType.EndElement
        //                 Me.WriteRich("</" & reader.Name & ">", num)
        //                 num -= 1
        //         End Select
        //     End While
        // End Sub
        public AdminXML(string pathXML)
        {
            this.abrirXML(pathXML);
        }

        public AdminXML()
        {
        }
    }
}
