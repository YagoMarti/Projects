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
    public class UsuarioController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();

        // GET: /Usuario/
        public ActionResult Index(int? idSupervisor, int? idUsuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
                {
                    ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.activo == true), "idUsuario", "username");
                    ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" || r.idRol == "jso" && r.activo == true), "idUsuario", "username");

                    if (idUsuario != null)
                    {
                        var usuarios = db.usuarios.Where(x => x.idUsuario == idUsuario && x.activo == true).Include(u => u.localidades).Include(u => u.roles).Include(u => u.usuarios1);
                        return View(usuarios.ToList());
                    }

                    return View();
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

                // Post: /Usuario/
        [HttpPost]
        public ActionResult Index(int? idSupervisor, int? idUsuario,string boton)
        {
            ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.activo == true), "idUsuario", "username");
            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" || r.idRol == "jso" && r.activo == true), "idUsuario", "username");
            switch(boton)
            {
                case "Consultar": { 
                    if (idSupervisor != null)
                    {
                        var usuarios = db.usuarios.Where(x => x.idSupervisor == idSupervisor && x.activo == true).Include(u => u.localidades).Include(u => u.roles).Include(u => u.usuarios1);
                        // ViewBag.idUsuario = new SelectList(db.usuarios, "idUsuario", "username");
                        // ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" || r.idRol == "jso"), "idUsuario", "username");
                        return View(usuarios.ToList());
                    }
                    if (idUsuario != null)
                    {
                        var usuarios = db.usuarios.Where(x => x.idUsuario == idUsuario).Include(u => u.localidades).Include(u => u.roles).Include(u => u.usuarios1);
                        // ViewBag.idUsuario = new SelectList(db.usuarios, "idUsuario", "username");
                        // ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" || r.idRol == "jso"), "idUsuario", "username");
                        return View(usuarios.ToList());
                    }                };break;
                case "Editar": {
                    if (idUsuario != null) { 
                    return RedirectToAction("Edit", new { id = idUsuario }); } };break;
                case "Eliminar": {
                    if (idUsuario != null) {
                        return RedirectToAction("Delete", new { id = idUsuario });}        } break;
                case "Cambiar Permisos":
                    {
                        if (idUsuario != null)
                        {
                            return RedirectToAction("CambiarPermisos", new { id = idUsuario });
                        }
                    } break; 
                case "ReiniciarClave": {
                    if (idUsuario != null) {
                        return RedirectToAction("ReiniciarClave", new { id = idUsuario });}        } break;
            }
            return View();
        }
        // POST : /Usuario/IniciarSesion
        [HttpPost]
        public ActionResult IniciarSesion(string username, string claveacc)
        {
            // NO VALIDAR SESSION en INICIO DE SESION!
            try
            {
                usuarios usuario = (from u in db.usuarios where u.username == username && u.claveacc == claveacc && u.activo == true select u).FirstOrDefault();
                if (usuario != null)
                {
                    Session["UsuarioLogueado"] = usuario;
                    var uslog = (usuarios)Session["UsuarioLogueado"];
                    switch (uslog.idRol)
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
                    return RedirectToAction("Reacomodar");
                }
                else
                {
                    TempData["log"] = "false";
                    return RedirectToAction("Index", "LogIn");
                }
            }
            catch (Exception e)
            { return RedirectToAction("Index", "LogIn"); }
        }
        // GET: /Usuario/Create
        public ActionResult Create()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
                {
                    ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad");
                    ViewBag.idRol = new SelectList(db.roles.Where(x => x.idRol != "adm"), "idRol", "rol");
                    ViewBag.idSupervisor = null;//new SelectList(db.usuarios.Where(x => x.idRol == "jop" || x.idRol == "jso" && x.activo == true), "idUsuario", "username");
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
        public ActionResult Create([Bind(Include = "idUsuario,nombre,apellido,telefono,fechaNacimiento,fechaContratacion,idRol,idLocalidad,username,claveacc,idSupervisor")] usuarios usuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
                {
                    if (ModelState.IsValid)
                    {
                        usuario.claveacc = usuario.username;
                        usuario.activo = true;
                        db.usuarios.Add(usuario);
                        db.SaveChanges();
                        ViewBag.idUsuario = new SelectList(db.usuarios, "idUsuario", "username");
                        ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" || r.idRol == "jso"), "idUsuario", "username");
                        usuarios usu = db.usuarios.Where(r => r.username == usuario.username).First();
                        return RedirectToAction("Index", new { idUsuario = usu.idUsuario });
                    }

                    ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad", usuario.idLocalidad);
                    ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
                    ViewBag.idSupervisor = new SelectList(db.usuarios.Where(x => x.idRol == "jop" || x.idRol == "jso"), "idUsuario", "username");
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
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
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
                    ViewBag.idLocalidad = new SelectList(db.localidades, "idLocalidad", "localidad", usuario.idLocalidad);
                    ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
                    ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.activo == true), "idUsuario", "username", usuario.idSupervisor);
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
        public ActionResult Edit([Bind(Include = "idUsuario,nombre,apellido,telefono,fechaNacimiento,fechaContratacion,idRol,idLocalidad,username,claveacc,idSupervisor")] usuarios usuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                ViewBag.idUsuario = new SelectList(db.usuarios, "idUsuario", "username");
                    ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" || r.idRol == "jso"), "idUsuario", "username");    
                if (uslog.idRol == "usr")
                {
                    try
                    {
                        // Debe tomar el ID del usuario
                        usuarios user = db.usuarios.Single(p => p.idUsuario == usuario.idUsuario);
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


        // GET: /Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
                {
                    usuarios usuario = db.usuarios.Find(id);
                    usuario.activo = false;
                    usuario.idSupervisor = null;
                    db.SaveChanges();
                    foreach ( var a in db.reposiciones.Where(x => x.idOperario == usuario.idUsuario) )
                    {
                        if(a.idEstado != 3)
                        a.idOperario = null;
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

        // Operario/ReiniciarClave(GET): Recibe información de formulario de Registro (Usuario.SolicitanteCargar)
        // Verificar que el Usuario esté a cargo del supervisor que reinicia la clave
        // Confirmar el cambio    
        public ActionResult ReiniciarClave(int id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
                {
                    try
                    {
                        // Debe tomar el ID del usuario
                        usuarios usuario = db.usuarios.Single(p => p.idUsuario == id);
                        if (usuario != null)
                        {
                            usuario.claveacc = usuario.username;
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

        public ActionResult Reacomodar()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            try
            {
                if (uslog != null)
                {
                    if (uslog.idRol == "adm")
                    { return RedirectToAction("Index", "Random"); }
                    else
                    { return RedirectToAction("Empty", "LogIn"); }
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            catch (Exception e)
            { return RedirectToAction("Index", "LogIn"); }
        }
        // POST : /Usuario/Reacomodar
        //    [HttpPost]
        public ActionResult Reacomodar2()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
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
                var uslog = (usuarios)Session["UsuarioLogueado"];
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
            var ulog = (usuarios)Session["UsuarioLogueado"];
            usuarios usuario = db.usuarios.Single(p => p.idUsuario == ulog.idUsuario);
            if (ClaveNueva != Confirmar)
            {
                ViewBag.aviso = "La clave nueva y su confirmación deben coincidir";
                return View();
            }
            if (ulog.claveacc != ClaveActual)
            {
                ViewBag.aviso = "La clave ingresada no es válida";
                return View();
            }
            if (usuario != null && ulog != null && ulog.claveacc == ClaveActual && ClaveNueva == Confirmar && Confirmar != null)
            {
                usuario.claveacc = ClaveNueva;
                db.SaveChanges();
                ViewBag.aviso = "La clave fue cambiada";
                return View();
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




        // GET: /Usuario/Edit/5
        public ActionResult CambiarPermisos(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
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
                    ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
                    switch (usuario.idRol)
                    {
                        case "ope":
                            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(x => x.idRol == "jop" && x.activo == true), "idUsuario", "username",usuario.idSupervisor);
                            break;
                        case "sol":
                            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(x => x.idRol == "jso" && x.activo == true), "idUsuario", "username",usuario.idSupervisor);
                            break;
                       default:
                            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(x => x.idRol == "asd" && x.activo == true), "idUsuario", "username");
                            break;
                    }
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
        public ActionResult CambiarPermisos([Bind(Include = "idUsuario,idRol,idSupervisor")] usuarios usuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "usr")
                {
                    try
                    {
                        // Debe tomar el ID del usuario
                        usuarios user = db.usuarios.Single(p => p.idUsuario == usuario.idUsuario);
                        if (user != null)
                        {
                            user.idRol = usuario.idRol;
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



        [HttpPost]
        public JsonResult getRoles(string idRol)
        {
            var query = from d in db.usuarios select d;
            switch (idRol)
            {
                case "ope":
                    query = from d in db.usuarios where d.idRol == "jop" where d.activo == true select d;
                    break;
                case "sol":
                    query = from d in db.usuarios where d.idRol == "jso" where d.activo == true select d;
                    break;
                default:
                    query = from d in db.usuarios where d.idRol == "asd" select d;
                    break;
            }
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (usuarios u in query)
            {
                lista.Add(new SelectListItem { Text = u.username, Value = Convert.ToString(u.idUsuario) });
            }
            return Json(new SelectList(lista, "Value", "Text"));
        }


        [HttpPost]
        public JsonResult getUsers(string user)
        {
            usuarios u = null;
            try { u = db.usuarios.Where(x => x.username == user).First(); }
            catch (Exception e) { };
            if ( u != null)
            { return Json(new { success = false }); }
            return Json(new { success = true });
        }


































    }




}