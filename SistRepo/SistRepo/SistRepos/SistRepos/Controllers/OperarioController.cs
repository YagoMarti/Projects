using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistRepos.Models;

namespace SisRep.Controllers
{
    public class OperarioController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();
        // GET: /Operario/
        // lista de solicitudes del mes
        public ActionResult Index()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ope")
                {
                    DateTime actual = new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
                    var reposiciones = db.reposiciones.Where(r => r.idOperario == uslog.idUsuario && r.horaCreacion >= actual).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.solicitantes).Include(r => r.operarios);
                    if (reposiciones.Count() > 0) { return View(reposiciones.ToList()); }
                    else { return View(); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }
        // GET / OPERARIO INDEX 3
        // Solicitudes activas. 
        public ActionResult Index3()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                
                if (uslog.idRol == "ope")
                {
                    var reposiciones = db.reposiciones.Where(r => r.idOperario == uslog.idUsuario && r.idEstado != 3).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.solicitantes).Include(r => r.operarios).OrderBy(r => r.horaAsignacion).Take(15);
                    foreach(reposiciones r in reposiciones)
                    {
                        var medio = new TimeSpan(0,(int)r.herramientas.estimado,0);
                        r.comentario = "bajo";
                        r.horaResolucion = r.horaAsignacion + medio;
                        if ((DateTime.Today - r.horaAsignacion) > medio) { r.comentario = "medio"; }
                        medio = medio + medio + medio;
                        if ((DateTime.Today - r.horaAsignacion) > medio) { r.comentario = "alto"; }
                        
                    }
                    if (reposiciones.Count() > 0) { return View(reposiciones.ToList()); }
                    else { return View(); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /Operario2/
         
        public ActionResult Index2(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ope")
                {
                    List<reposiciones> mostrar15 = new List<reposiciones>();
                    if (id == null)
                    { id = 0; }
                    ViewBag.c = (int)id;
                    ViewBag.caso = 1;
                    ViewBag.volver = (int)id - 15;
                    ViewBag.sig = (int)id + 15;
                    int posicion = 0;
                    var reposiciones = db.reposiciones.Where(r => r.idOperario == uslog.idUsuario && r.idEstado == 3).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.solicitantes).Include(r => r.operarios).OrderByDescending(r => r.horaResolucion);
                    foreach (reposiciones r in reposiciones)
                    {
                        if (posicion >= Convert.ToInt32(id)  && posicion <= Convert.ToInt32(id) + 15)
                        { 
                            var medio = new TimeSpan(0, (int)r.herramientas.estimado, 0);
                            r.comentario = "bajo";

                            if ((r.horaResolucion - r.horaAsignacion) > medio) { r.comentario = "medio"; }
                            medio = medio + medio + medio;
                            if ((r.horaResolucion - r.horaAsignacion) > medio) { r.comentario = "alto"; }
                            mostrar15.Add(r);
                        }
                        posicion++;
                    }
                    if (posicion <= id + 15) { ViewBag.caso = 2; }
                    if (id == 0) { ViewBag.caso = 0; }
                    if (posicion <= 16) { ViewBag.caso = 3; }
                    if (mostrar15.Count() > 0) { return View(mostrar15.ToList()); }
                    
                    else { return View(); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /Operario/Details/5
        public ActionResult Details(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ope")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    reposiciones reposicione = db.reposiciones.Find(id);
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
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ope")
                {
                    reposiciones reposicione = db.reposiciones.Single(x => x.idReposicion == id);
                    if (uslog.idUsuario == reposicione.idOperario)
                    {
                        reposicione.idEstado = 3;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index3");
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }


    }
}
