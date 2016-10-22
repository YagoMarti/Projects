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
    public class JefeOperarioController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();
        // GET: /JefeOperario/
        // Muestra la lista de usuarios 
        public ActionResult Index(int? idUsuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
               
                if (uslog.idRol == "jop")
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
        // Vista de impresión index 1
        public ActionResult Index1Print()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                
                if (uslog.idRol == "jop")
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




        // GET: /JefeOperario/Index2
        // muestra solicitudes activas 
        public ActionResult Index2(int? id)
        {
            if (id == null)
            { id = 0; }
            ViewBag.volver = (int)id - 15;
            ViewBag.sig = (int)id + 15;
            
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    List<reposiciones> mostrar15 = new List<reposiciones>();
                    int posicion = 0;
                    ViewBag.caso = 1;
                    var reposiciones = db.reposiciones.Where(r => r.idEstado == 1).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.operarios).Include(r => r.solicitantes).OrderBy(r => r.horaCreacion);
                    ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario && r.activo == true), "idUsuario", "username");
                    foreach (reposiciones r in reposiciones)
                    {
                        if (posicion >= Convert.ToInt32(id) + 1 && posicion <= Convert.ToInt32(id) + 15)
                        { // se supone que 10 mins es el tiempo de asignación, 30 mins ya es una demora crítica. 
                            TimeSpan medio = new TimeSpan(0, 10, 0);
                            r.comentario = "bajo";
                            r.horaResolucion = r.horaCreacion + medio ;
                            if ((DateTime.Today - r.horaCreacion) > medio) { r.comentario = "medio"; }
                            medio = medio + medio + medio ;
                            if ((DateTime.Today - r.horaCreacion) > medio) { r.comentario = "alto"; }
                            mostrar15.Add(r);
                        }
                        posicion++;


                    }
                    if (posicion <= id + 15) { ViewBag.caso = 2; }
                    if (id == 0) { ViewBag.caso = 0; }
                    if (posicion <= 16) { ViewBag.caso = 3; }
                    if (mostrar15.Count() > 0) { return View(mostrar15.ToList()); }
                    return View(mostrar15);
                    
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }


        // GET: /JefeOperario/Index4
        // muestra solicitudes asignadas 
        public ActionResult Index4(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    List<reposiciones> mostrar15 = new List<reposiciones>();
                    if (id == null)
                    { id = 0; }
                    ViewBag.volver = (int)id - 15;
                    ViewBag.sig = (int)id + 15;
                    int posicion = 0;
                    ViewBag.caso = 1;
                    var reposiciones = db.reposiciones.Where(r => r.idEstado == 2).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.operarios).Include(r => r.solicitantes).OrderBy(r => r.horaAsignacion);
                    ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                    foreach (reposiciones r in reposiciones)
                    {
                        if (posicion >= Convert.ToInt32(id) + 1 && posicion <= Convert.ToInt32(id) + 15)
                        { // se supone que 10 mins es el tiempo de asignación, 30 mins ya es una demora crítica. 
                            var medio = new TimeSpan(0, (int)r.herramientas.estimado, 0);
                            r.comentario = "bajo";
                            r.horaResolucion = r.horaAsignacion + medio;
                            if ((DateTime.Today - r.horaAsignacion) > medio) { r.comentario = "medio"; }
                            medio = medio + medio + medio;
                            if ((DateTime.Today - r.horaAsignacion) > medio) { r.comentario = "alto"; }
                            mostrar15.Add(r);
                        }
                        posicion++;


                    }
                    if (posicion <= id + 15) { ViewBag.caso = 2; }
                    if (id == 0) { ViewBag.caso = 0; }
                    if (posicion <= 16) { ViewBag.caso = 3; }
                    if (mostrar15.Count() > 0) { return View(mostrar15.ToList()); }
                    return View(mostrar15);
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }



        // GET: /JefeOperario/Edit/5
        public ActionResult Edit(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
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
                        ViewBag.idRol = new SelectList(db.roles, "idRol", "rol", usuario.idRol);
                        ViewBag.idSupervisor = new SelectList(db.usuarios, "idUsuario", "nombre", usuario.idSupervisor);
                        return View(usuario);
                    }
                    else { return RedirectToAction("Reacomodar", "Usuario"); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // POST: /JefeOperario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,nombre,apellido,telefono,idLocalidad,username,idSupervisor")] usuarios usuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
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
                        else
                        { return RedirectToAction("Index", "JefeOperario"); }
                    }
                    catch (Exception e)
                    { return RedirectToAction("Index", "JefeOperario"); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // Operario/ReiniciarClave(GET): Recibe información de formulario de Registro (Usuario.SolicitanteCargar)
        // Confirmar el cambio    
        public ActionResult ReiniciarClave(int id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    try
                    {
                        usuarios usuario = db.usuarios.Single(p => p.idUsuario == id);
                        if (usuario != null && usuario.idSupervisor == uslog.idUsuario)
                        {
                            usuario.claveacc = usuario.username;
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
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }

        }

        // GET
        // REASIGNAR SOLICITUDES
        public ActionResult Reasignar(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
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
                        reposiciones reposicione = db.reposiciones.Find(id);
                        if (reposicione == null)
                        {
                            return RedirectToAction("Index2");
                        }
                        ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario),"idUsuario", "username");
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
        public ActionResult Reasignar([Bind(Include = "idReposicion,idOperario")] reposiciones reposicione)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    try
                    {
                        ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                        reposiciones rep = db.reposiciones.Single(p => p.idReposicion == reposicione.idReposicion);
                        if (rep != null)
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


        // GET: /Solicitante/Details/5
        public ActionResult Details(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
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
                    ViewBag.sol = id;
                    return View(reposicione);
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }








        // GET: /JefeOperario/Index3
        // muestra solicitudes finalizadas por operario 
        public ActionResult Index3()
        {
            ViewBag.c = 3;
            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            ViewBag.date = null;
            if (uslog != null)
            { return View(); }

            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // POST
        // index3
        [HttpPost]
        public ActionResult Index3(int? idUsuario, DateTime? idFecha, string print)
        {
            if (print == "Imprimir")
            { return RedirectToAction("Index3Print", new { idOperario = idUsuario, idFecha = idFecha }); }
           
            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            ViewBag.c = 3;
            int? id = idUsuario;
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    try
                    {
                        if (uslog.idRol == "jop" && idUsuario != null)
                        {
                            ViewBag.sol = idUsuario;
                            if (idFecha != null)
                            {
                                DateTime idFecha2 = (DateTime)idFecha;
                                if (idFecha2.Month < 10) { ViewBag.date = idFecha2.Year + "-0" + idFecha2.Month; }
                                else { ViewBag.date = idFecha2.Year + "-" + idFecha2.Month; }
                                
                                var a = idFecha2.Month;
                                var b = idFecha2.Year;

                                DateTime bot = new DateTime(b, a, 1); DateTime top = new DateTime(b, a, DateTime.DaysInMonth(b,a));
                                var reposiciones2 = db.reposiciones.Where(r => r.operarios.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idOperario == idUsuario).Where(x => x.horaCreacion > bot && x.horaCreacion < top).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.operarios).Include(r => r.solicitantes).OrderByDescending(r => r.horaCreacion);
                                
                                return View(reposiciones2.ToList());
                            }
                            var reposiciones = db.reposiciones.Where(r => r.operarios.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idOperario == idUsuario).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.operarios).Include(r => r.solicitantes).OrderByDescending(r => r.horaCreacion);
                            ViewBag.date = null;
                            return View(reposiciones.ToList());
                        }
                        else
                        {
                            
                            return View(); }
                    }
                    catch (Exception e) { return View(); }

                }
                return RedirectToAction("Reacomodar", "Usuario");
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }




        // GET: /JefeOperario/Index5
        // muestra solicitudes mensuales por operario 
        public ActionResult Index5()
        {

            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            if (uslog != null && uslog.idRol == "jop")
            { return View(); }

            else
            { return RedirectToAction("Index", "LogIn"); }
        }


        // INDEX 5
        [HttpPost]
        public ActionResult Index5(int? idUsuario)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            int? id = idUsuario;
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    if (id != null)
                    {
                        DateTime bot = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        DateTime top = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
                        var reposiciones = db.reposiciones.Where(r => r.operarios.idSupervisor == uslog.idUsuario && r.idOperario == id && r.horaCreacion > bot && r.horaCreacion < top).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.operarios).Include(r => r.solicitantes);
                        ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                        return View(reposiciones.ToList());
                    }
                    else
                    {
                        ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
                        return View();
                    }
                }
                return RedirectToAction("Reacomodar", "Usuario");
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }



        // GET: /JefeSolicitante/Index7
        // busca una solicitud por ID
        // GET: /Solicitante/Details/5
        public ActionResult Index7(int? idReposicion)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "jop")
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


        // SECCION IMPRESIONES

        // Vista de impresión Detalles -- Index7Print
        public ActionResult Index7Print(int idReposicion)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {

                if (uslog.idRol == "jop")
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


                // Vista de impresión Index3 -- Index3Print
        public ActionResult Index3Print(int idOperario, DateTime? idFecha)
        {

            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idUsuario = new SelectList(db.usuarios.Where(r => r.idSupervisor == uslog.idUsuario), "idUsuario", "username");
            var ope = db.usuarios.Where(x => x.idUsuario == idOperario).First();
            ViewBag.ope = ope.username;

            if (uslog != null)
            {
                if (uslog.idRol == "jop")
                {
                    try
                    {
                        ViewBag.sol = idOperario;
                        if (idFecha != null)
                        {
                           
                            DateTime idFecha2 = (DateTime)idFecha;
                            ViewBag.date = idFecha2.ToShortDateString(); 
                            var a = idFecha2.Month;
                            var b = idFecha2.Year;
                            DateTime bot = new DateTime(b, a, 1); DateTime top = new DateTime(b, a, DateTime.DaysInMonth(b, a));
                            var reposiciones2 = db.reposiciones.Where(r => r.operarios.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idOperario == idOperario).Where(x => x.horaCreacion > bot && x.horaCreacion < top).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.operarios).Include(r => r.solicitantes).OrderByDescending(r => r.horaCreacion);
                            ViewBag.fech = idFecha.ToString();
                            return View(reposiciones2.ToList());
                        }
                        var reposiciones = db.reposiciones.Where(r => r.operarios.idSupervisor == uslog.idUsuario && r.idEstado == 3 && r.idOperario == idOperario).Include(r => r.herramientas).Include(r => r.puestos).Include(r => r.operarios).Include(r => r.solicitantes).OrderByDescending(r => r.horaCreacion);
                        ViewBag.date = null;
                        return View(reposiciones.ToList());
                    }

                    catch (Exception e) { return View(); }

                }
                return RedirectToAction("Reacomodar", "Usuario");
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }
                    












    }
}
