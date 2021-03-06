﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.ConexionBD;

namespace Datos
{
    public class clsTablasBD
    {
        public Entidades.Helpers.Paginado SelectAll(int skip, int take)
        {
            try
            {
                var data = new Entidades.Helpers.Paginado();
                List<Entidades.Helpers.Tablas> t = new List<Entidades.Helpers.Tablas>();
                using (var context = new BarandillasEntities())
                {

                    var query = (from p in context.Tablas_BD where p.Estatus == true
                                 orderby p.TablaID
                                 select p)
                                .Skip(skip)
                                .Take(take)
                                .ToList();
                    foreach (var i in query)
                    {
                        t.Add(new Entidades.Helpers.Tablas { TablaID = i.TablaID, NombreTabla = i.Nombre, Descripcion = i.Descripcion, TipoTabla = i.TipoTabla, Estatus = i.Estatus });
                    }
                    
                    data.Customers = t;
                    data.TotalRecords = (from p in context.Tablas_BD
                                         select p).Count();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public string Insert(Tablas_BD tablas)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    context.Tablas_BD.Add(tablas);
                    context.SaveChanges();
                    return tablas.Nombre;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Update(Tablas_BD tablas)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    context.Entry(tablas).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return tablas.Nombre;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Delete(int id)
        {
            try
            {
                using (var context = new BarandillasEntities())
                {
                    Tablas_BD tabla = context.Tablas_BD.Find(id);
                    tabla.Estatus = false;
                    context.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return tabla.Nombre;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

