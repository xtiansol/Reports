//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades.ConexionBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class RelacionCamposTablas_BD
    {
        public int RelacionCampoID { get; set; }
        public int RelacionID { get; set; }
        public string CampoTB { get; set; }
        public string CampoTR { get; set; }
    
        public virtual RelacionesTablas_BD RelacionesTablas_BD { get; set; }
    }
}
