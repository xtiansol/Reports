using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminXML;
using System.Reflection;

namespace AdminReflex
{

    public class AdminReflex
    {

        private string _xmlPattern;

        private ArrayList _header;

        private ArrayList _body;

        private ArrayList _footer;

        private ArrayList _colFinal = new ArrayList();

        public AdminXML.AdminXML _adminXML;

        public AdminReflex()
        {
        }

        public ArrayList getColFinal()
        {
            return _colFinal;
        }

        public void addValueToColItemPattern(object value, ArrayList colPattern)
        {
            _colFinal.Add(this.reflexValueToCol(value, colPattern));
        }

        public object getValueFromColItemPattern(Type value, ArrayList colItemPat)
        {
            return this.reflexColToValue(value, colItemPat);
        }


        private object reflexColToValue(Type value, ArrayList colItem)
        {
            BindingFlags flags = (BindingFlags.Instance
                        | (BindingFlags.Public
                        | (BindingFlags.DeclaredOnly
                        | (BindingFlags.Static
                        | (BindingFlags.NonPublic
                        | (BindingFlags.Instance | BindingFlags.CreateInstance))))));
            object newVal = value.InvokeMember(null, flags, null, null, null);
            foreach (ItemConf itmPat in colItem)
            {
                // Busca el nombre de la propiedad en el value
                PropertyInfo[] ppr = value.GetProperties(flags);
                foreach (PropertyInfo prop in ppr)
                {
                    if ((itmPat.name == prop.Name))
                    {
                        prop.SetValue(newVal, itmPat.value, null);
                        break;
                    }

                }

            }

            return newVal;
        }


        private ArrayList reflexValueToCol(object value, ArrayList colItem)
        {
            BindingFlags flags = (BindingFlags.Instance
                        | (BindingFlags.Public
                        | (BindingFlags.DeclaredOnly | BindingFlags.Static)));
            Type typ = value.GetType();
            ArrayList colPatNew = new ArrayList();
            foreach (ItemPattern itmPat in colItem)
            {
                // Busca el nombre de la propiedad en el value
                PropertyInfo[] pr = typ.GetProperties(flags);
                ItemPattern newItmPat = itmPat.Clone();
                foreach (PropertyInfo prop in pr)
                {
                    if ((newItmPat.name == prop.Name))
                    {
                        newItmPat.value = (String)prop.GetValue(value, null);
                        break;
                    }

                }

                colPatNew.Add(newItmPat);
            }

            return colPatNew;
        }

        public ArrayList identificaCampVO(object obj1, object obj2)
        {
            BindingFlags flags = (BindingFlags.Instance
                        | (BindingFlags.Public
                        | (BindingFlags.DeclaredOnly | BindingFlags.Static)));
            Type typ = obj1.GetType();
            Type typ2 = obj2.GetType();
            ArrayList col = new ArrayList();
            PropertyInfo[] pr1 = typ.GetProperties(flags);
            foreach (PropertyInfo prop1 in pr1)
            {
                // Busca el nombre de la propiedad en el value
                PropertyInfo[] pr2 = typ.GetProperties(flags);
                foreach (PropertyInfo prop2 in pr2)
                {
                    if ((prop1.Name == prop2.Name))
                    {
                        if ((prop1.GetValue(obj1, null) != prop2.GetValue(obj2, null)))
                        {
                            col.Add(("Campo Actualizado: ["
                                            + (prop1.Name + ("] Valor Anterior: ["
                                            + (prop2.GetValue(obj2, null) + ("] Valor Nuevo: ["
                                            + (prop1.GetValue(obj1, null) + "]")))))));
                            break;
                        }
                        else
                        {
                            break;
                        }

                    }

                }

            }

            return col;
        }
    }
}
