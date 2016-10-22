using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SisRep.Models;

namespace SisRep.Controllers
{
    public class OperarioController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();
        // GET: /Operario/
        // lista de solicitudes activas asignadas al operario. 
        public ActionResult Index()
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "ope")
{ 
            var reposiciones = db.reposiciones.Where(r => r.idOperario == uslog.idUsuario && r.idEstado != 3).Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1); // .Where(r => r.idSolicitante == 1);
            return View(reposiciones.ToList());
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}
        }


        // GET: /Operario/Details/5
        public ActionResult Details(int? id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "ope")
{
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            reposicione reposicione = db.reposiciones.Find(id);
            if (reposicione == null)
            {
                return RedirectToAction("Index");
            }
            if (uslog.idUsuario == reposicione.idOperario) { return View(reposicione); }
            return RedirectToAction("Reacomodar", "Usuario");
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // GET
        // Operario/Finalizar
        public ActionResult Finalizar(int? id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if (uslog.idRol == "ope")
                {
                    reposicione reposicione = db.reposiciones.Single(x => x.idReposicion == id);
                    if (uslog.idUsuario == reposicione.idOperario)
                    {
                        reposicione.idEstado = 3;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }


    }
}
