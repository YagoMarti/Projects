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
    
    public partial class sucursales
    {
        public sucursales()
        {
            this.pisos = new HashSet<pisos>();
        }
    
        public int idSucursal { get; set; }
        [Display(Name = "Sucursal")]
        public string sucursal { get; set; }
    
        public virtual ICollection<pisos> pisos { get; set; }
    }
}
