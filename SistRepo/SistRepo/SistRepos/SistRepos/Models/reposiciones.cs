//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistRepos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class reposiciones
    {
        [Display(Name = "Reposicion")]
        public int idReposicion { get; set; }

        public Nullable<int> idOperario { get; set; }

        public Nullable<int> idSolicitante { get; set; }

        [Required(ErrorMessage = "Indique el puesto de trabajo")]
        public Nullable<int> idPuesto { get; set; }

        [Required(ErrorMessage = "Seleccione una herramienta")]
        public Nullable<int> idHerramienta { get; set; }

        [Required(ErrorMessage = "Es necesario un detalle del problema")]
        [Display(Name = "Comentario")]
        public string comentario { get; set; }

        [Display(Name = "Creacion")]
        public Nullable<System.DateTime> horaCreacion { get; set; }

        [Display(Name = "Asignacion")]
        public Nullable<System.DateTime> horaAsignacion { get; set; }

        [Display(Name = "Resolucion")]
        public Nullable<System.DateTime> horaResolucion { get; set; }

        public int idEstado { get; set; }
    
        public virtual estados estados { get; set; }
        public virtual herramientas herramientas { get; set; }
        public virtual puestos puestos { get; set; }
        public virtual usuarios operarios { get; set; }
        public virtual usuarios solicitantes { get; set; }
    }
}
