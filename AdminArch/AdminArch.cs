using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdminArch
{
    public class AdminArch
    {

        private StreamWriter newFileName;

        private StreamReader reader;

        private FileStream flujoopen;

        public bool crearArchivo(string pathNom)
        {
            try
            {
                newFileName = new StreamWriter(pathNom);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public string creaRenombraArchivo(string pathNom, string cadAdicio)
        {
            DateTime fecha = new DateTime();
            // Warning!!! Optional parameters not supported
            if (this.existArch(pathNom))
            {
                if ((cadAdicio != ""))
                {
                    pathNom = (pathNom + cadAdicio);
                }
                else
                {
                    pathNom = pathNom + fecha;
                }

            }

            if (this.crearArchivo(pathNom))
            {
                return pathNom;
            }

            return null;
        }

        // 
        //  Saber si existe un directorio
        // 
        private bool existDir(string elDirectorio)
        {
            return Directory.Exists(elDirectorio);
        }

        private bool existeDirInfo(string elDirectorio)
        {
            DirectoryInfo di = new DirectoryInfo(elDirectorio);
            return di.Exists;
        }

        //  
        //  Los ficheros de un directorio
        // extension *.txt, *.*, etc.
        // 
        public string[] filesDir(string elDirectorio, string extension)
        {
            return Directory.GetFiles(elDirectorio, extension);
        }

        //  
        //  Los ficheros de un directorio
        // extension *.txt, *.*, etc.
        // 
        public FileInfo[] filesDirInfo(string elDirectorio, string extension)
        {
            DirectoryInfo di = new DirectoryInfo(elDirectorio);
            return di.GetFiles(extension);
        }

        // Existe el Archivo 
        // pathArch: Ruta del Archivo
        private bool existArch(string pathArch)
        {
            return File.Exists(pathArch);
        }

        public bool escribirLinea(string contenido)
        {
            try
            {
                newFileName.WriteLine(contenido);
                return true;
            }
            catch (Exception e)
            {
            }

            return false;
        }

        public bool cerrarArchivo()
        {
            try
            {
                newFileName.Close();
            }
            catch (Exception e)
            {
                return false;
            }

            try
            {
                flujoopen.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }

        public bool abrirArchivo(string pathNom)
        {
            try
            {
                flujoopen = new FileStream(pathNom, FileMode.Open, FileAccess.ReadWrite);
                reader = new StreamReader(flujoopen);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public string leerLinea()
        {
            string str = reader.ReadLine();
            if ((reader.Peek() > -1))
            {
                // si existe texto
                return str;
            }

            if (((str != null)
                        || (str != "")))
            {
                return str;
            }

            return null;
        }

        public int getNumLineas()
        {
            int lin = 1;
            flujoopen.Seek(0, SeekOrigin.Begin);
            reader = new StreamReader(flujoopen);
            while ((reader.Peek() > -1))
            {
                // si existe texto
                lin = (lin + 1);
                reader.ReadLine();
            }

            flujoopen.Seek(0, SeekOrigin.Begin);
            return lin;
        }
    }
}