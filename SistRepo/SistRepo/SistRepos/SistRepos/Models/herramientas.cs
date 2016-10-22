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
    
    public partial class herramientas
    {
        public herramientas()
        {
            this.reposiciones = new HashSet<reposiciones>();
        }
        
        public int idHerramienta { get; set; }

        [Required(ErrorMessage = "Indique el nombre")]
        [Display(Name = "Herramienta")]
        public string herramienta { get; set; }

               [Required(ErrorMessage = "Indique el stock")]
        [Display(Name = "Stock")]
        [Range(1, 500)]
        public Nullable<int> stock { get; set; }

               [Required(ErrorMessage = "Indique el tiempo estimado")]
        [Display(Name = "Tiempo Estimado")]
        [Range(1, 60)]
        public Nullable<int> estimado { get; set; }
    
        public virtual ICollection<reposiciones> reposiciones { get; set; }
    }
}