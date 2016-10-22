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
    public class UsuarioController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();

        // GET: /Usuario/
        public ActionResult Index()
        {            
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)		
{
if(uslog.idRol == "usr")	       	 	
{
                    var usuarios = db.usuarios.Include(u => u.localidade).Include(u => u.role).Include(u => u.usuario1);
                    return View(usuarios.ToList());
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // POST : /Usuario/IniciarSesion
        [HttpPost]
        public ActionResult IniciarSesion(string username, string claveacc)
        {
            // NO VALIDAR SESSION en INICIO DE SESION!
            try
            {
                usuario usuario = (from u in db.usuarios where u.username == username && u.claveacc == claveacc select u).FirstOrDefault();
                if (usuario != null)
                {
                    Session["UsuarioLogueado"] = usuario;
                    var uslog = (usuario)Session["UsuarioLogueado"];
                    switch(uslog.idRol)
                    {
                        case "adm": { Session["Rol"] = "adm"; break; }
                        case "ger": { Session["Rol"] = "ger"; break; }
                        case "jop": { Session["Rol"] = "jop"; break; }
                        case "jso": { Session["Rol"] = "jso"; break; }
                        case "ope": { Session["Rol"] = "ope"; break; }
                        case "sol": { Session["Rol"] = "sol"; break; }
                        case "usr": { Session["Rol"] = "usr"; break; }
                        default: { return RedirectToAction("Reacomodar"); }
                    }
                    return RedirectToAction("Empty", "LogIn");
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            catch (Exception e)
            { return RedirectToAction("Index", "LogIn"); }   
        }
        // GET: /Usuario/Create
        public ActionResult Create()
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)		
{
if(uslog.idRol == "usr")	       	 	
{
            ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad");
            ViewBag.idRol = new SelectList(db.roles, "idRol", "rol");
            ViewBag.idSupervisor = new SelectList(db.usuarios, "idUsuario", "nombre");
            return View();
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // POST: /Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,nombre,apellido,telefono,fechaNacimiento,fechaContratacion,idRol,idLocalidad,username,claveacc,idSupervisor")] usuario usuario)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)		
{
if(uslog.idRol == "usr")	       	 	
{
            if (ModelState.IsValid)
            {
                usuario.claveacc = usuario.username;
                db.usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad", usuario.idLocalidad);
            ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
            ViewBag.idSupervisor = new SelectList(db.usuarios, "idUsuario", "nombre", usuario.idSupervisor);
            return View(usuario);
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)		
{
if(uslog.idRol == "usr")	       	 	
{
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad", usuario.idLocalidad);
            ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
            ViewBag.idSupervisor = new SelectList(db.usuarios, "idUsuario", "nombre", usuario.idSupervisor);
            return View(usuario);
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // POST: /Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,nombre,apellido,telefono,fechaNacimiento,fechaContratacion,idRol,idLocalidad,username,claveacc,idSupervisor")] usuario usuario)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)		
{
if(uslog.idRol == "usr")	       	 	
{
            try
            {
                // Debe tomar el ID del usuario
                usuario user = db.usuarios.Single(p => p.idUsuario == usuario.idUsuario);
                if (user != null)
                {
                    user.nombre = usuario.nombre;
                    user.apellido = usuario.apellido;
                    user.telefono = usuario.telefono;
                    user.fechaContratacion = usuario.fechaContratacion;
                    user.fechaNacimiento = usuario.fechaNacimiento;
                    user.idLocalidad = usuario.idLocalidad;
                    user.idRol = usuario.idRol;
                    user.username = usuario.username;
                    user.idSupervisor = usuario.idSupervisor;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //Redirige a la lista de solicitudes activas y cambio de clave.
                else
                { return RedirectToAction("Index"); }
            }
            catch (Exception e)
            { return RedirectToAction("Index"); }
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }


        // GET: /Usuario/Delete/5
        public ActionResult Delete(int id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)		
{
if(uslog.idRol == "usr")	       	 	
{
            usuario usuario = db.usuarios.Find(id);
            db.usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // Operario/ReiniciarClave(GET): Recibe información de formulario de Registro (Usuario.SolicitanteCargar)
        // Verificar que el Usuario esté a cargo del supervisor que reinicia la clave
        // Confirmar el cambio    
        public ActionResult ReiniciarClave(int id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)		
{
if(uslog.idRol == "usr")	       	 	
{
            try
            {
                // Debe tomar el ID del usuario
                usuario usuario = db.usuarios.Single(p => p.idUsuario == id);
                if (usuario != null)
                {
                    usuario.claveacc = usuario.nombre + usuario.apellido;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //Redirige a la lista de solicitudes activas y cambio de clave.
                else
                { return RedirectToAction("Index"); }
            }
            catch (Exception e)
            { return RedirectToAction("Index"); }
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }


        // POST : /Usuario/Reacomodar
        //    [HttpPost]
        public ActionResult Reacomodar()
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            try
            {
                if (uslog != null)
                {
                    switch (uslog.idRol)
                    {
                        case "adm": { return RedirectToAction("Index", "LogIn"); }
                        case "ger": { return RedirectToAction("Index", "Gerencia"); }
                        case "jop": { return RedirectToAction("Index", "JefeOperario"); }
                        case "jso": { return RedirectToAction("Index", "JefeSolicitante"); }
                        case "ope": { return RedirectToAction("Index", "Operario"); }
                        case "sol": { return RedirectToAction("Index", "Solicitante"); }
                        case "usr": { return RedirectToAction("Index", "Usuario"); }
                        default: { return RedirectToAction("Index", "LogIn"); }
                    }
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            catch (Exception e)
            { return RedirectToAction("Index", "LogIn"); }
        }

        // CambiarClave (GET): Formulario de cambio de clave de acceso
        public ActionResult CambiarClave()
        {
            if (Session["UsuarioLogueado"] != null)
            {
                var uslog = (usuario)Session["UsuarioLogueado"];
                ViewBag.NombreUsuario = uslog.username;
                return View();
            }
            else
            {
                return RedirectToAction("Reacomodar", "Usuario");
            }

        }

        // CambiarClave (POST): Recibe los datos del formulario de cambio de clave de acceso
        [HttpPost]
        public ActionResult CambiarClave(string ClaveActual, string ClaveNueva, string Confirmar)
        {
            var ulog = (usuario)Session["UsuarioLogueado"];
            usuario usuario = db.usuarios.Single(p => p.idUsuario == ulog.idUsuario);
            if (usuario != null && ulog != null && ulog.claveacc == ClaveActual && ClaveNueva == Confirmar && Confirmar != null)
            {
                usuario.claveacc = ClaveNueva;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return RedirectToAction("Reacomodar", "Usuario"); }
        }

        // CerrarSesion (GET): Cierra la sesión del usuario logueado 
        public ActionResult CerrarSesion()
        {
            Session["UsuarioLogueado"] = null;
            Session["Rol"] = null;
            return RedirectToAction("Reacomodar");
        }

    }
}