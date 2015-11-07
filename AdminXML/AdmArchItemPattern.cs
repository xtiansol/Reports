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

    public class AdmArchItemPattern
    {

        private AdminXML admXML = new AdminXML();

        private AdminArch.AdminArch admArc = new AdminArch.AdminArch();

        private ArrayList colHeader;

        private ArrayList colHeader2;

        private ArrayList colBody;

        private ArrayList colFooter;

        private ArrayList colGen;

        public AdmArchItemPattern()
        {
        }

        public AdmArchItemPattern(string pathXML)
        {
            this.setArchPattern(pathXML);
        }

        public ArrayList getHeader()
        {
            return colHeader;
        }

        public ArrayList getHeader2()
        {
            return colHeader2;
        }

        public ArrayList getBody()
        {
            return colBody;
        }

        public ArrayList getFooter()
        {
            return colFooter;
        }

        public bool setArchPattern(string pathXML)
        {
            if (admXML.abrirXML(pathXML))
            {
                colHeader = admXML.getItemsHeader();
                colHeader2 = admXML.getItemsHeader2();
                colBody = admXML.getItemsBody();
                colFooter = admXML.getItemsFooter();
                return true;
            }

            return false;
        }

        public bool generaArchTXT(string pathTXT, ArrayList colCom)
        {
            if (admArc.crearArchivo(pathTXT))
            {
                foreach (ArrayList colElem in colCom)
                {
                    admArc.escribirLinea(this.generaStrOfColItemPattern(colElem));
                }

                admArc.cerrarArchivo();
                return true;
            }

            return false;
        }

        public ArrayList generaColRegistros(string pathTXT)
        {
            colGen = new ArrayList();
            if (admArc.abrirArchivo(pathTXT))
            {
                string str = "";
                int cont = 1;
                int nLin = 0;
                int indic = 0;
                nLin = (admArc.getNumLineas() - 1);
                str = admArc.leerLinea();
                while ((str != null))
                {
                    if ((cont == 1))
                    {
                        indic = 1;
                    }
                    else if ((cont == 2))
                    {
                        indic = 2;
                    }
                    else if ((cont == nLin))
                    {
                        indic = 4;
                    }
                    else
                    {
                        indic = 3;
                    }

                    colGen.Add(this.generaColItemPatternOfStr(str, indic, 0));
                    str = admArc.leerLinea();
                    cont = (cont + 1);
                }

                admArc.cerrarArchivo();
            }

            return colGen;
        }

        // tipo 1= header , 2= body, 3, footer
        private ArrayList generaColItemPatternOfStr(string strArc, int tipo, int tipoDefa)
        {
            ArrayList coll = colBody;
            // Warning!!! Optional parameters not supported
            ArrayList newColIt = new ArrayList();
            switch (tipo)
            {
                case 1:
                    if ((colHeader.Count > 0))
                    {
                        coll = colHeader;
                    }

                    break;
                case 2:
                    if ((colHeader2.Count > 0))
                    {
                        coll = colHeader2;
                    }

                    break;
                case 3:
                    if ((colBody.Count > 0))
                    {
                        coll = colBody;
                    }

                    break;
                case 4:
                    if ((colFooter.Count > 0))
                    {
                        coll = colFooter;
                    }

                    break;
            }
            foreach (ItemPattern itPat in coll)
            {
                ItemPattern newItPat = itPat.Clone();
                try
                {
                    newItPat.value = strArc.Substring(Int32.Parse(newItPat.position), Int32.Parse(newItPat.size));
                }
                catch (Exception e)
                {
                }

                newColIt.Add(newItPat);
            }

            return newColIt;
        }

        private string generaStrOfColItemPattern(ArrayList coll)
        {
            string newStr = "";
            foreach (ItemPattern itPat in coll)
            {
                newStr = (newStr + itPat.value);
            }

            return newStr;
        }
    }
}
