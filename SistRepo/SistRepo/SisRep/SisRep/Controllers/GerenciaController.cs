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
    public class GerenciaController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();
        // faltan los reportes
        // GET: /Gerencia/
        public ActionResult Index()
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "ger")
{
            return View();
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // get herramientas
        public ActionResult Herr()
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    var herramientas = db.herramientas;
                    return View(herramientas.ToList());
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }

        }

        // get herramientas
        public ActionResult EditH(int? id)
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    herramienta herramienta = db.herramientas.Find(id);
                    if (herramienta == null)
                    {
                        return RedirectToAction("Herr");
                    }
                    return View(herramienta);
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }

        }


        // POST:// get herramientas
        [HttpPost]
        public ActionResult EditH([Bind(Include = "idHerramienta,stock")] herramienta herramienta)
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    try
                    {
                        // Debe tomar el ID del usuario
                        herramienta herr = db.herramientas.Single(p => p.idHerramienta == herramienta.idHerramienta);
                        if (herr != null)
                        {
                            //herr.stock = herramienta.stock;
                            db.SaveChanges();
                            return RedirectToAction("Herr");
                        }
                        //Redirige a la lista de solicitudes activas y cambio de clave.
                        else
                        { return RedirectToAction("Herr"); }
                    }
                    catch (Exception e)
                    { return RedirectToAction("Herr"); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // reportes
        // permisos de usuarios
        // parte de consumos
        // impresion de las listas y reportes

	}
}