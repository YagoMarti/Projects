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
    
    public partial class usuarios
    {
        public usuarios()
        {
            this.reposiciones = new HashSet<reposiciones>();
            this.reposiciones1 = new HashSet<reposiciones>();
            this.usuarios1 = new HashSet<usuarios>();
        }
    
        public int idUsuario { get; set; }
        [Required(ErrorMessage = "Indique el nombre del usuario")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Indique el apellido")]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Display(Name = "Tel�fono")]
        public string telefono { get; set; }

        [Display(Name = "Nacimiento")]
        public Nullable<System.DateTime> fechaNacimiento { get; set; }

        [Display(Name = "Contratado")]
        public Nullable<System.DateTime> fechaContratacion { get; set; }

        [Required(ErrorMessage = "Es necesario un cargo")]
        public string idRol { get; set; }

        public Nullable<int> idLocalidad { get; set; }

        [Required(ErrorMessage = "Es necesario un nombre de usuario")]
        [Display(Name = "Usuario")]
        public string username { get; set; }

        [Display(Name = "Clave")]
        public string claveacc { get; set; }

        public Nullable<int> idSupervisor { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> activo { get; set; }
    
        public virtual localidades localidades { get; set; }
        public virtual ICollection<reposiciones> reposiciones { get; set; }
        public virtual ICollection<reposiciones> reposiciones1 { get; set; }
        public virtual roles roles { get; set; }
        public virtual ICollection<usuarios> usuarios1 { get; set; }
        public virtual usuarios usuarios2 { get; set; }
    }
}