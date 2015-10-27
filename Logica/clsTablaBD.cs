using System;
using System.Collections.Generic;
using Tablas_BD = Entidades.ConexionBD.Tablas_BD;
using DataLayer = Datos.clsTablasBD;

namespace Logica
{
    public class clsTablaBD
    {
        public static List<Tablas_BD> SelectAll()
        {
            try
            {
                //return Datos.clsTablasBD.SelectAll();
                var procedimiento = new DataLayer();
                return procedimiento.SelectAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static string Insert(Tablas_BD tablas)
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.Insert(tablas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string Update(Tablas_BD tablas)
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.Update(tablas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string Delete(int id)
        {
            try
            {
                var procedimiento = new DataLayer();
                return procedimiento.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
