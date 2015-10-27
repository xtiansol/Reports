using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.ConexionBD;

namespace Datos
{
    public class clsTablasBD
    {
        public  List<Tablas_BD> SelectAll()
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    return context.Tablas_BD.Where(r => r.Estatus == true).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public  string Insert(Tablas_BD tablas)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    context.Tablas_BD.Add(tablas);
                    context.SaveChanges();
                    return tablas.NombreTabla;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public  string Update(Tablas_BD tablas)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    context.Entry(tablas).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return tablas.NombreTabla;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public  string Delete(int id)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    Tablas_BD tabla = context.Tablas_BD.Find(id);
                    tabla.Estatus = false;
                    context.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return tabla.NombreTabla;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

