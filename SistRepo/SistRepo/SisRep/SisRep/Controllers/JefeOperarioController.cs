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
    public class JefeOperarioController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();
        // GET: /JefeOperario/
        // Muestra la lista de usuarios para cambiar la Clave
        public ActionResult Index()
        {
        var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jop")
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
        // GET: /JefeOperario/Index2
        // muestra solicitudes activas 
        public ActionResult Index2()
        {
        var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jop")
{
            var reposiciones = db.reposiciones.Where(r => r.idEstado != 3).Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1);
            ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            return View(reposiciones.ToList());
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}
        }


        



        // GET: /JefeOperario/Edit/5
        public ActionResult Edit(int? id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jop")
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
    if (usuario.idSupervisor == uslog.idUsuario)
    { 
            ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad", usuario.idLocalidad);
            ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
            ViewBag.idSupervisor = new SelectList(db.usuarios, "idUsuario", "nombre", usuario.idSupervisor);
            return View(usuario);
    }
    else { return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}
        }

        // POST: /JefeOperario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,nombre,apellido,telefono,idLocalidad,username,idSupervisor")] usuario usuario)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jop")
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
                else
                { return RedirectToAction("Index", "JefeOperario"); }
            }
            catch (Exception e)
            { return RedirectToAction("Index", "JefeOperario"); }
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}
        }
        
        // Operario/ReiniciarClave(GET): Recibe información de formulario de Registro (Usuario.SolicitanteCargar)
        // Confirmar el cambio    
        public ActionResult ReiniciarClave(int id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if(uslog.idRol == "jop")
{
            try
            {
                usuario usuario = db.usuarios.Single(p => p.idUsuario == id);
                if (usuario != null && usuario.idSupervisor == uslog.idUsuario)
                {
                    usuario.claveacc = usuario.nombre + usuario.apellido;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                { return RedirectToAction("Index", "JefeOperario"); }
            }
            catch (Exception e)
            { return RedirectToAction("Index", "JefeOperario"); }
}
else
{return RedirectToAction("Reacomodar","Usuario");}
}
else
{ return RedirectToAction("Index", "LogIn");}        

        }

        // GET
        // REASIGNAR SOLICITUDES
        public ActionResult Reasignar(int? id)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
{
if (uslog.idRol == "jop")
{
                    try
                    {
                        if (id == null)
                        {
                            return RedirectToAction("Index2");
                        }
                        reposicione reposicione = db.reposiciones.Find(id);
                        if (reposicione == null)
                        {
                            return RedirectToAction("Index2");
                        }
                        ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                        return View(reposicione);
                    }
                    catch (Exception e)
                    { return RedirectToAction("Index2", "JefeOperario"); }
}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }

        // POST
        // REASIGNAR SOLICITUDES
        [HttpPost]
        public ActionResult Reasignar([Bind(Include = "idReposicion,idOperario")] reposicione reposicione)
        {
var uslog = (usuario)Session["UsuarioLogueado"];
if (uslog != null)
            {
if (uslog.idRol == "jop")
{
    try
    {
        reposicione rep = db.reposiciones.Single(p => p.idReposicion == reposicione.idReposicion);
        if(rep != null)
        {
            rep.idOperario = reposicione.idOperario;
            rep.idEstado = 2;
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        else { return RedirectToAction("Index2"); }
    }
    catch (Exception e) { return View(); }

}
else
{ return RedirectToAction("Reacomodar", "Usuario"); }
}
else
{ return RedirectToAction("Index", "LogIn"); }
        }












        // GET: /JefeOperario/Index3
        // muestra solicitudes finalizadas por operario 
        public ActionResult Index3(int? id)
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            if (uslog != null)
            {
                if (uslog.idRol == "jop" && id != null)
                {
                    var reposiciones = db.reposiciones.Where(r => r.usuario.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idOperario == id).Include(r => r.herramienta).Include(r => r.puesto).Include(r => r.usuario).Include(r => r.usuario1);
                    ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
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
        public ActionResult Index3([Bind(Include = "idOperario")] reposicione reposicione)
        {
            var uslog = (usuario)Session["UsuarioLogueado"];
            int? id = reposicione.idOperario;
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    try
                    {
                        return RedirectToAction("Index3/"+id);
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
