using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistRepos.Models
{
    public class ger5
    {
        [Display(Name = "Operario")]
        public string operario {set;get;}
        [Display(Name = "Supervisor")]
        public string supervisor { set; get; }
        [Display(Name = "Solicitante")]
        public string solicitante { set; get; }
        [Display(Name = "Tiempo de tratamiento")]
        public TimeSpan? tiempo {set;get; }
        [Display(Name = "Reposicion")]
        public int solicitud { set; get; }
        [Display(Name = "Herramienta")]
        public string herramienta { set; get; }
        [Display(Name = "Puesto")]
        public string puesto { set; get; }
        [Display(Name = "Creacion")]
        public DateTime? horaCreacion { set; get; }
        [Display(Name = "Tiempo estimado")]
        public TimeSpan estimado { set; get; }
        [Display(Name = "Mayor exceso")]
        public TimeSpan? exceso { set; get; }
        [Display(Name = "Tratadas")]
        public int? tratadas { set; get; }
        [Display(Name = "Excedidas")]
        public int excedidas { set; get; }
        [Display(Name = "Porcentaje excedidas")]
        public double? porcExceso { set; get; }
        [Display(Name = "Tiempo promedio")]
        public TimeSpan? tmedio { set; get; }
        [Display(Name = "Porcentaje exceso")]
        public double? porcFuera { set; get; }
        [Display(Name = "Cancelacion")]
        public DateTime? horaCancelacion { set; get; }
        [Display(Name = "Creacion")]
        public string creacion { set; get; }
    }
}