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
    
    public partial class puestos
    {
        public puestos()
        {
            this.reposiciones = new HashSet<reposiciones>();
        }
    
        public int idPuesto { get; set; }
        public Nullable<int> idPiso { get; set; }
        [Display(Name = "Puesto")]
        public string puesto { get; set; }
    
        public virtual pisos pisos { get; set; }
        public virtual ICollection<reposiciones> reposiciones { get; set; }
    }
}