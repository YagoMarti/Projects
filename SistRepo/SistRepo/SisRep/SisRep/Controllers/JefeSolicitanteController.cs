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
    public class JefeSolicitanteController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();
        // GET: /JefeSolicitante/
        // lista de solicitantes
        public ActionResult Index()
        {
        var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jso")
{
            var usuarios = db.usuarios.Where(u => u.idSupervisor == uslog.idUsuario).Include(u => u.localidade).Include(u => u.role).Include(u => u.usuario1); 
            return View(usuarios.ToList());
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /JefeSolicitante/Index2
        // muestra solicitudes activas de solicitantes a su cargo
        // Todavia no puede modificar solicitudes 
        public ActionResult Index2()
        {
        var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jso")
{
            var reposiciones = db.reposiciones.Where(r => r.idEstado != 3).Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1).Where(r => r.usuario1.idSupervisor == uslog.idUsuario);
            return View(reposiciones.ToList());
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}
        }


        // GET: /JefeSolicitante/Edit/5
        public ActionResult Edit(int? id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jso")
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
            if (usuario.idSupervisor == uslog.idSupervisor) { 
            ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad", usuario.idLocalidad);
            ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
            ViewBag.idSupervisor = new SelectList(db.usuarios, "idUsuario", "nombre", usuario.idSupervisor);
            return View(usuario);
            }
            return RedirectToAction("Reacomodar", "Usuario");
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}
        }

        // POST: /JefeSolicitante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,nombre,apellido,telefono,idLocalidad,username,idSupervisor")] usuario usuario)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jso")
{
            try
            {
                usuario user = db.usuarios.Single(p => p.idUsuario == usuario.idUsuario);
                if (user != null && user.idSupervisor == uslog.idUsuario)
                {
                    user.nombre = usuario.nombre;
                    user.apellido = usuario.apellido;
                    user.telefono = usuario.telefono;
                    user.idLocalidad = usuario.idLocalidad;
                    user.username = usuario.username;
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
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}
        }

        // Solicitante/ReiniciarClave(GET): Recibe información de formulario de Registro (Usuario.SolicitanteCargar)
        // Confirmar el cambio    
        public ActionResult ReiniciarClave(int id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jso")
{
            try
            {
                usuario usuario = db.usuarios.Single(p => p.idUsuario == id);
                if (usuario != null && uslog.idUsuario == usuario.idSupervisor)
                {
                    usuario.claveacc = usuario.nombre + usuario.apellido;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                { return RedirectToAction("Index"); }
            }
            catch (Exception e)
            { return RedirectToAction("Index"); }
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}        
        }

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    reposicione reposicione = db.reposiciones.Find(id);
                    db.reposiciones.Remove(reposicione);
                    db.SaveChanges();
                    return RedirectToAction("Index2");
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }







        // GET: /JefeSolicitante/Index3
        // muestra solicitudes finalizadas por operario 
        public ActionResult Index3(int? id)
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            ViewBag.idSolicitante = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            if (uslog != null)
            {
                if (uslog.idRol == "jso" && id != null)
                {
                    var reposiciones = db.reposiciones.Where(r => r.usuario1.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idSolicitante == id).Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1);
                    ViewBag.idSolicitante = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                    return View(reposiciones.ToList());
                }
                else
                { return View(); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // POST
        // index3
        [HttpPost]
        public ActionResult Index3([Bind(Include = "idSolicitante")] reposicione reposicione)
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            int? id = reposicione.idSolicitante;
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    try
                    {
                        return RedirectToAction("Index3/" + id);
                    }
                    catch (Exception e) { return View(); }

                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }    









    }
}
