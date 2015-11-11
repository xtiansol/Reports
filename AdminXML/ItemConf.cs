using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdminXML
{

    public class ItemConf : ICloneable
    {

        private string _name = "";

        private string _type = "string";

        private int _size = 0;

        private int _position = 0;

        private string _align = "ninguno";

        private string _fill = "";

        private ArrayList colValues = new ArrayList();

        private string _value = "";

        public void addValue(string valor)
        {
            colValues.Add(valor);
        }

        public string getValue(int index)
        {
            return (string)colValues[index];
        }

        public ArrayList getValues()
        {
            return colValues;
        }

        public ArrayList valuesDefault
        {
            get
            {
                return colValues;
            }
            set
            {
                colValues = value;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public string size
        {
            get
            {
                return "" + _size;
            }
            set
            {
                try
                {
                    _size = int.Parse(value);
                }
                catch (Exception e)
                {
                    _size = 0;
                }

            }
        }

        public string position
        {
            get
            {
                return "" + _position;
            }
            set
            {
                try
                {
                    _position = int.Parse(value);
                }
                catch (Exception e)
                {
                    _position = 0;
                }

            }
        }

        public string align
        {
            get
            {
                return _align;
            }
            set
            {
                switch (value)
                {
                    case "izquierda":
                        _align = "izquierda";
                        break;
                    case "derecha":
                        _align = "derecha";
                        break;
                    case "centrar":
                        _align = "centrar";
                        break;
                    case "ninguno":
                        _align = "ninguno";
                        break;
                    case "normal":
                        _align = "normal";
                        break;
                    default:
                        _align = "ninguno";
                        break;
                }
            }
        }

        public string fill
        {
            get
            {
                return _fill;
            }
            set
            {
                if ((value == null))
                {
                    _fill = "";
                }
                else
                {
                    _fill = value;
                }

            }
        }

        public void setValuesByAttribute(XmlAttributeCollection attributes)
        {
            name = attributes["name"].Value;
        }

        public string value
        {
            get
            {
                _value = this.normaliza(_value);
                if (this.isValidValue(_value))
                {
                    return value;
                }
                else
                {
                    return this.normaliza((string)colValues[0]);
                }

            }
            set
            {
                _value = value;
            }
        }

        public ArrayList CollectionValues
        {
            get
            {
                return colValues;
            }
            set
            {
                colValues = value;
            }
        }

        public string getValueOriginal()
        {
            return _value;
        }

        private string normaliza(string val)
        {
            int cont = val.Length;
            // Warning!!! Optional parameters not supported
            string valueAux = val;

            if (!(_align == "ninguno"))
            {
                while ((cont < _size))
                {
                    if ((_align == "derecha"))
                    {
                        valueAux = (_fill + valueAux);
                    }
                    else if ((_align == "izquierda"))
                    {
                        valueAux = (valueAux + _fill);
                    }
                    else if ((_align == "normal"))
                    {
                        valueAux = valueAux;
                    }
                    else if ((_align == "centrar"))
                    {

                    }

                    cont = (cont + 1);
                }

            }

            return valueAux;
        }

        private bool isValidValue(string value)
        {
            int cont = 0;
            // Warning!!! Optional parameters not supported
            if (value == "")
            {
                value = this.normaliza(value);
            }

            if (colValues.Count == 0)
            {
                return true;
            }
            else
            {
                while (cont < colValues.Count)
                {
                    string str = (string)colValues[cont];
                    if ((value == str))
                    {
                        return true;
                    }

                    cont++;
                }

            }

            return false;
        }

        public object Clone()
        {
            ItemConf newItemConf = new ItemConf();
            newItemConf._name = this._name;
            newItemConf._type = this._type;
            newItemConf._size = this._size;
            newItemConf._position = this._position;
            newItemConf._align = this._align;
            newItemConf._fill = this._fill;
            newItemConf.colValues = this.colValues;
            newItemConf._value = this._value;
            return newItemConf;
        }

        public string toString()
        {
            return this.value;
        }
    }
}
