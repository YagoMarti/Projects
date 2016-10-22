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
    public class SolicitanteController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();

        // GET: /Solicitante/
        // lista de solicitudes por solicitante
        public ActionResult Index()
        { 
            var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "sol")
{
            var reposiciones = db.reposiciones.Where(r => r.idSolicitante == uslog.idUsuario && r.idEstado != 3).Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1);
            return View(reposiciones.ToList());
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /Solicitante/Index2
        // lista de solicitudes finalizadas por solicitante
        public ActionResult Index2()
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "sol")
                {
                    var reposiciones = db.reposiciones.Where(r => r.idSolicitante == uslog.idUsuario && r.idEstado == 3).Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1);
                    return View(reposiciones.ToList());
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /Solicitante/Details/5
        public ActionResult Details(int? id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "sol")
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
            return View(reposicione);
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /Solicitante/Create
        public ActionResult Create()
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "sol")
{
            ViewBag.idHerramienta = new SelectList(db.herramientas, "idHerramienta", "herramienta1");
            ViewBag.idPuesto = new SelectList(db.puestos, "idPuesto", "puesto1");
            return View();
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // POST: /Operario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHerramienta,idPuesto,comentario")] reposicione reposicione)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "sol")
{
            if (ModelState.IsValid)
            {
                reposicione.idSolicitante = uslog.idUsuario;
                reposicione.idEstado = 1;
                db.reposiciones.Add(reposicione);
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
