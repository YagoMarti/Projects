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
    public class JefeSolicitanteController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();
        // GET: /JefeSolicitante/
        // lista de solicitantes
        public ActionResult Index(int? idUsuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    ViewBag.dato = "nada";
                    if (idUsuario != null)
                    {
                        var usuarios = db.usuarios.Where(x => x.idUsuario == idUsuario && x.activo == true).Include(u => u.localidades).Include(u => u.roles).Include(u => u.usuarios1);
                        ViewBag.dato = "uno";
                        return View(usuarios.ToList());
                    }
                    else
                    {
                        var usuarios = db.usuarios.Where(u => u.idSupervisor == uslog.idUsuario && u.activo == true).Include(u => u.localidades).Include(u => u.roles).Include(u => u.usuarios1);
                        return View(usuarios.ToList());
                    }
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
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    var reposiciones = db.reposiciones.Where(r => r.idEstado != 3).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.solicitantes).Include(r => r.operarios).Where(r => r.solicitantes.idSupervisor == uslog.idUsuario).OrderByDescending(r => r.horaCreacion);

                    return View(reposiciones.ToList());
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }


        // GET: /JefeSolicitante/Edit/5
        public ActionResult Edit(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    usuarios usuario = db.usuarios.Find(id);
                    if (usuario == null)
                    {
                        return RedirectToAction("Index");
                    }
                    if (usuario.idSupervisor == uslog.idUsuario)
                    {
                        ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad", usuario.idLocalidad);
                        return View(usuario);
                    }
                    return RedirectToAction("Reacomodar", "Usuario");
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // POST: /JefeSolicitante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,nombre,apellido,telefono,idLocalidad,username,idSupervisor")] usuarios usuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    try
                    {
                        usuarios user = db.usuarios.Single(p => p.idUsuario == usuario.idUsuario);
                        if (user != null && user.idSupervisor == uslog.idUsuario)
                        {
                            user.nombre = usuario.nombre;
                            user.apellido = usuario.apellido;
                            user.telefono = usuario.telefono;
                            user.idLocalidad = usuario.idLocalidad;
                            user.username = usuario.username;
                            db.SaveChanges();
                            return RedirectToAction("Index", new { idUsuario = user.idUsuario });
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

        // Solicitante/ReiniciarClave(GET): Recibe información de formulario de Registro (Usuario.SolicitanteCargar)
        // Confirmar el cambio    
        public ActionResult ReiniciarClave(int id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    try
                    {
                        usuarios usuario = db.usuarios.Single(p => p.idUsuario == id);
                        if (usuario != null && uslog.idUsuario == usuario.idSupervisor)
                        {
                            usuario.claveacc = usuario.username;
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
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    reposiciones reposicione = db.reposiciones.Find(id);
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
        public ActionResult Index3()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idSolicitante = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            if (uslog != null)
            {
                /*if (uslog.idRol == "jso" && id != null)
                {
                    var reposiciones = db.reposiciones.Where(r => r.solicitantes.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idSolicitante == id).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.solicitantes).Include(r => r.operarios).OrderByDescending(r => r.horaCreacion);
                    ViewBag.idSolicitante = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                    return View(reposiciones.ToList());
                }
                else
                { return View(); } */
                return View();
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // POST
        // index3
        [HttpPost]
        public ActionResult Index3(int? idSolicitante)
           // ([Bind(Include = "idSolicitante")] reposiciones reposicione)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            //int? id = reposicione.idSolicitante;
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    try
                    {
                        var reposiciones = db.reposiciones.Where(r => r.solicitantes.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idSolicitante == idSolicitante).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.solicitantes).Include(r => r.operarios).OrderByDescending(r => r.horaCreacion);
                        ViewBag.idSolicitante = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                        ViewBag.sol = idSolicitante;
                        return View(reposiciones.ToList());
                       // return RedirectToAction("Index3/" + id);
                    }
                    catch (Exception e) { return View(); }

                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // GET: /JefeSolicitante/Index7
        // busca una solicitud por ID
        public ActionResult Index7(int? idReposicion)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    if (idReposicion == null)
                    {
                        return View();
                    }

                    reposiciones reposicione = db.reposiciones.Find(idReposicion);
                    if (reposicione == null)
                    {
                        return View();
                    }
                    ViewBag.sol = idReposicion;
                    return View(reposicione);
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
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jso")
                {
                    if (id == null)
                    {
                        return View();
                    }
                    reposiciones reposicione = db.reposiciones.Find(id);
                    if (reposicione == null)
                    {
                        return View();
                    }
                    ViewBag.sol = id;
                    return View(reposicione);
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }


        // SECCION IMPRESIONES

        // Vista de impresión index 1 -- Index1Print
        public ActionResult Index1Print()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {

                if (uslog.idRol == "jso")
                {
                    ViewBag.sup = uslog.username;
                    var usuarios = db.usuarios.Where(u => u.idSupervisor == uslog.idUsuario && u.activo == true).Include(u => u.localidades).Include(u => u.roles).Include(u => u.usuarios1);
                    return View(usuarios.ToList());
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // Vista de impresión index 3 -- Index3Print

        public ActionResult Index3Print(int idSolicitante)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {

                if (uslog.idRol == "jso")
                {
                        var reposiciones = db.reposiciones.Where(r => r.solicitantes.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idSolicitante == idSolicitante).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.solicitantes).Include(r => r.operarios).OrderByDescending(r => r.horaCreacion);
                        var solicitante = db.usuarios.Where(x => x.idUsuario == idSolicitante).First() ;
                        ViewBag.solicitante = solicitante.username;
                        return View(reposiciones.ToList());
                       // return RedirectToAction("Index3/" + id);
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // Vista de impresión Detalles -- Index7Print
        public ActionResult Index7Print(int idReposicion)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {

                if (uslog.idRol == "jso")
                {
                    reposiciones reposicione = db.reposiciones.Find(idReposicion);
                    if (reposicione == null)
                    {
                        return View();
                    }
                    ViewBag.sol = idReposicion;
                    return View(reposicione);
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }


    }
}
