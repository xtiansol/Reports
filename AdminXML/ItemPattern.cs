using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdminXML
{

    public class ItemPattern
    {

        private string _name = "";

        private string _type = "string";

        private int _size = 0;

        private int _position = 0;

        private string _align = "normal";

        private string _fill = "";

        private ArrayList colValues = new ArrayList();

        private string _value = "";

        private string _window1252 = "true";

        private string _trim = "false";

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

        public string trim
        {
            get
            {
                if ((_trim == ""))
                {
                    return "false";
                }

                return _trim;
            }
            set
            {
                _trim = value;
            }
        }

        public string window1252
        {
            get
            {
                if ((_window1252 == ""))
                {
                    return "true";
                }

                return _window1252;
            }
            set
            {
                _window1252 = value;
            }
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
            type = attributes["type"].Value;
            size = attributes["size"].Value;
            position = attributes["position"].Value;
            align = attributes["align"].Value;
            fill = attributes["fill"].Value;
            try
            {
                window1252 = attributes["window1252"].Value;
            }
            catch (Exception e)
            {
                window1252 = "true";
            }

            try
            {
                trim = attributes["trim"].Value;
            }
            catch (Exception e)
            {
                trim = "false";
            }

        }

        public string value
        {
            get
            {
                value = this.normaliza(_value);
                if (this.isValidValue(_value))
                {
                    return value;
                }
                else
                {
                    return this.normaliza((string)colValues[1]);
                }

                //return value;
            }
            set
            {
                _value = value;
            }
        }

        public object getValueOriginal()
        {
            return _value;
        }

        private string normaliza(string val)
        {
            int cont = 0;
            // Warning!!! Optional parameters not supported
            if ((_value != null))
            {
                cont = _value.Length;
            }

            string valueAux = _value;
            if ((val != ""))
            {
                valueAux = val;
                cont = val.Length;
            }

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

            if ((this._window1252 == "true"))
            {
                valueAux = this.strToWindow1252(valueAux);
            }

            if ((this._trim == "true"))
            {
                valueAux = valueAux.Trim();
            }

            if ((valueAux != null))
            {
                if ((valueAux.Length > this._size))
                {
                    valueAux = valueAux.Substring(0, this._size);
                }

            }

            return valueAux;
        }

        private string strToWindow1252(string val)
        {
            string tem = val;
            string carTs = "�����������@�";
            int tam = carTs.Length;
            int tamVal = val.Length;
            int cont = 0;
            int numCut = 0;
            int contAux = 0;
            while ((tam > cont))
            {
                string car = carTs.Substring(contAux, 1);
                contAux = (contAux + 1);
                cont = (cont + 1);
                numCut = (numCut
                            + (tamVal - tem.Replace(car, "").Length));
            }

            if (((tamVal + numCut) > this._size))
            {
                return val.Substring(0, (tamVal - numCut));
            }
            else
            {
                return val;
            }

        }

        private bool isValidValue(string value)
        {
            int cont = 0;
            // Warning!!! Optional parameters not supported
            if ((value == ""))
            {
                value = this.normaliza(value);
            }

            if ((colValues.Count == 0))
            {
                return true;
            }
            else
            {
                while ((cont < colValues.Count))
                {
                    String str = (string)colValues[(cont + 1)];
                    if ((value == str))
                    {
                        return true;
                    }

                    cont = (cont + 1);
                }

            }

            return false;
        }

        public ItemPattern Clone()
        {
            ItemPattern newItemPattern = new ItemPattern();
            newItemPattern._name = this._name;
            newItemPattern._type = this._type;
            newItemPattern._size = this._size;
            newItemPattern._position = this._position;
            newItemPattern._align = this._align;
            newItemPattern._fill = this._fill;
            newItemPattern.colValues = this.colValues;
            newItemPattern._value = this._value;
            newItemPattern._window1252 = this._window1252;
            newItemPattern._trim = this._trim;
            return newItemPattern;
        }

        public string toString()
        {
            return this.value;
        }
    }
}
