using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistRepos.Models;
using System.Collections;

namespace SisRep.Controllers
{
    public class GerenciaController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();






        // GET: /Gerencia/
        public ActionResult Index()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    return View();
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        //Gerencia/SolicitudesCanceladas
        public ActionResult Index2(int? yer, string print)
        {
            
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    DateTime fecha1 = new DateTime(DateTime.Today.Year, 1, 1);
                    DateTime fecha2 = new DateTime(DateTime.Today.Year + 1, 1, 1);
                    if(yer != null)
                    {
                        fecha1 = new DateTime((int)yer,1,1);
                        fecha2 = new DateTime((int)yer + 1, 1, 1);
                    }
                    else
                    {
                        return View();
                        yer = DateTime.Today.Year;
                    }
                    var canceladas = db.canceladas.Where(r => r.horaCreacion > fecha1 && r.horaCreacion < fecha2);
                    if (canceladas.Count() < 1) { return View(); }
                    var solicitudes = new List<ger5>();
                    List<herramientas> her = db.herramientas.ToList();
                    List<puestos> pu = db.puestos.ToList();
                    foreach(canceladas r in canceladas)
                    {
                        solicitudes.Add(new ger5()
                        {
                            solicitante = r.idSolicitante,
                            horaCreacion = r.horaCreacion,
                            horaCancelacion = r.horaCancelacion,
                            herramienta = her[(int)r.idHerramienta].herramienta,
                            puesto = pu[(int)r.idPuesto].puesto,
                            solicitud = r.idReposicion
                        });
                    }
                    if (print == "Imprimir")
                    {
                        TempData["yer"] = yer;
                        TempData["ind2"] = solicitudes.ToList();
                        return RedirectToAction("Index2Print", new { yer = yer }); 
                    }
                    return View(solicitudes.ToList());
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        // Index2Print
        public ActionResult Index2Print()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                ViewBag.year = TempData["yer"];
                var canc = TempData["ind2"];
                return View(canc);
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }

        //Gerencia/SolicitudesTratadasTarde
        public ActionResult Index3()
        {
            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" && r.activo == true), "idUsuario", "username");
            ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idRol == "ope" && r.activo == true), "idUsuario", "username");
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {

                return View();

            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }
        //index3//post
        [HttpPost]
        public ActionResult Index3(int? idOperario, int? idSupervisor, DateTime? fecha1, DateTime? fecha2, string print)
        {
            int cas = 0;
            if (fecha1 != null) { cas = 1; ViewBag.desde = fecha1.Value.ToString("yyyy-MM-dd"); }
            if (fecha2 != null) { cas = 2; ViewBag.hasta = fecha2.Value.ToString("yyyy-MM-dd"); }
            if (fecha1 != null && fecha2 != null) { cas = 3; }
            if (cas == 0)
            {
                fecha1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                fecha2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month));
                cas = 3;
            }


            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop" && r.activo == true), "idUsuario", "username");
            ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idRol == "ope" && r.activo == true), "idUsuario", "username");
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                var repos = db.reposiciones.Take(5);
                List<ger5> model = new List<ger5>();
                //ger5 datos = new ger5();
                ViewBag.sup = "true";
                if (idSupervisor != null)
                {
                    usuarios u = db.usuarios.Where(x => x.idUsuario == idSupervisor).First();
                    TempData["usu"] = "Supervisor: " + u.username;
                    switch (cas)
                    {
                        case 3:
                            {
                                 repos = db.reposiciones.Where(x => x.operarios.idSupervisor == idSupervisor && x.horaCreacion < fecha2 && x.horaCreacion > fecha1).OrderByDescending(x => x.idHerramienta).ThenByDescending(x => x.horaCreacion);                               
                            }break;
                        case 1:
                            {
                                 repos = db.reposiciones.Where(x => x.operarios.idSupervisor == idSupervisor && x.horaCreacion > fecha1).OrderByDescending(x => x.idHerramienta).ThenByDescending(x => x.horaCreacion);                               
                            }break;
                        case 2:
                            {
                                 repos = db.reposiciones.Where(x => x.operarios.idSupervisor == idSupervisor && x.horaCreacion < fecha2 ).OrderByDescending(x => x.idHerramienta).ThenByDescending(x => x.horaCreacion);                               
                            }break;
                        default: { }; break;
                    }
                    
                    foreach (reposiciones b in repos)
                    {
                        TimeSpan estim = new TimeSpan(0, (int)b.herramientas.estimado, 0);
                        if ((b.horaResolucion - b.horaAsignacion) > estim)
                        {
                            model.Add(new ger5() { operario = b.operarios.username, solicitud = b.idReposicion, horaCreacion = b.horaCreacion, herramienta = b.herramientas.herramienta, puesto = b.puestos.puesto, estimado = estim, tiempo = (b.horaResolucion - b.horaAsignacion) + estim });
                        }
                    }
                    if (print == "Imprimir")
                    {
   //                     
                        TempData["sup"] = ViewBag.sup;
                        TempData["ind3"] = model;
                        return RedirectToAction("Index3Print");
                    }
                    return View(model);
                }


                if (idOperario != null)
                {
                    usuarios u = db.usuarios.Where(x => x.idUsuario == idOperario).First();
                    TempData["usu"] = "Operario: " + u.username;
                    ViewBag.sup = "false";
                    switch (cas)
                    {
                        case 3:
                            {
                                repos = db.reposiciones.Where(x => x.idOperario == idOperario && x.horaCreacion < fecha2 && x.horaCreacion > fecha1).OrderByDescending(x => x.idHerramienta).ThenByDescending(x => x.horaCreacion);
                            } break;
                        case 1:
                            {
                                repos = db.reposiciones.Where(x => x.idOperario == idOperario && x.horaCreacion > fecha1).OrderByDescending(x => x.idHerramienta).ThenByDescending(x => x.horaCreacion);
                            } break;
                        case 2:
                            {
                                repos = db.reposiciones.Where(x => x.idOperario == idOperario && x.horaCreacion < fecha2).OrderByDescending(x => x.idHerramienta).ThenByDescending(x => x.horaCreacion);
                            } break;
                        default: { }; break;

                    }
                    
                    foreach (reposiciones b in repos)
                    {
                        TimeSpan estim = new TimeSpan(0, (int)b.herramientas.estimado, 0);
                        if ((b.horaResolucion - b.horaAsignacion) > estim)
                        {
                            model.Add(new ger5() { solicitud = b.idReposicion, horaCreacion = b.horaCreacion, herramienta = b.herramientas.herramienta, puesto = b.puestos.puesto, estimado = estim, tiempo = (b.horaResolucion - b.horaAsignacion) + estim });
                        }
                    }
                    if (print == "Imprimir")
                    {
//
                        TempData["sup"] = ViewBag.sup;
                        TempData["ind3"] = model;
                        return RedirectToAction("Index3Print");
                    }
                    return View(model.ToList());
                }
                return View();
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }

        // Index3Print
        public ActionResult Index3Print()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                ViewBag.sup = TempData["sup"];
                ViewBag.usu = TempData["usu"];
                var model = TempData["ind3"];
                return View(model);
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }


        //Gerencia/MayorDesvio y Porcentaje fuera de tiempo mensual por Supervisor.
        public ActionResult Index4()
        {
            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop"), "idUsuario", "username");
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {

                return View();

            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }


        //Gerencia/MayorDesvio y Porcentaje fuera de tiempo mensual por Supervisor.
        [HttpPost]
        public ActionResult Index4(int? idSupervisor, DateTime? fecha, string print)
        {

            List<ger5> model = new List<ger5>();
            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop"), "idUsuario", "username");
            if (uslog != null && uslog.idRol == "ger")
            {
                DateTime fecha1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime fecha2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                if (fecha != null)
                {
                    fecha1 = new DateTime(fecha.Value.Year, fecha.Value.Month, 1);
                    fecha2 = new DateTime(fecha.Value.Year, fecha.Value.Month, DateTime.DaysInMonth(fecha.Value.Year, fecha.Value.Month));
                }
                var opers = db.usuarios.Where(x => x.idSupervisor == idSupervisor);
                // mayor desvio y porcentaje de resoluciones fuera de tiempo por tipo de reposicion
                // por cada operario : 
                string ope = "";
                foreach (usuarios u in opers)
                {
                    var her = db.herramientas;
                    int[] tratadas = new int[her.Count()];
                    int[] excedidas = new int[her.Count()];
                    TimeSpan[] acumulado = new TimeSpan[her.Count()];
                    TimeSpan[] mayor = new TimeSpan[her.Count()];

                    for (int i = 0; i < her.Count(); i++)
                    {
                        acumulado[i] = new TimeSpan(0, 0, 0);
                        mayor[i] = new TimeSpan(0, 0, 0);
                    }

                    var reps = db.reposiciones.Where(x => x.horaCreacion > fecha1 && x.horaCreacion < fecha2 && x.operarios.idSupervisor == idSupervisor && x.idOperario == u.idUsuario && x.idEstado == 3 && x.horaResolucion != null);
                    List<herramientas> la = db.herramientas.ToList();
                    foreach (var a in reps)
                    {
                        tratadas[(int)a.idHerramienta - 1]++;
                        DateTime hora1 = (DateTime)a.horaResolucion;
                        DateTime hora2 = (DateTime)a.horaAsignacion;
                        TimeSpan diferencia = hora1 - hora2;
                        acumulado[(int)a.idHerramienta - 1] = acumulado[(int)a.idHerramienta - 1] + diferencia;
                        if (a.horaResolucion - a.horaAsignacion > mayor[(int)a.idHerramienta - 1])
                        {
                            mayor[(int)a.idHerramienta - 1] = (TimeSpan)diferencia; // verifica el mayor tiempo de solicitud. 
                        }
                        if (diferencia > new TimeSpan(0, (int)a.herramientas.estimado, 0))
                        {
                            excedidas[(int)a.idHerramienta - 1]++; // verifica la cantidad de solicitudes fuera de tiempo. 
                        }
                    }
                    double? pexceso = 0; double? porc = 0;
                    // aca pasar los datos a la lista para mostrarlos... 

                    for (int i = 1; i < her.Count() + 1; i++)
                    {

                        if (i == 1) { ope = u.username; }
                        else { ope = " "; }
                        herramientas her2 = db.herramientas.Where(x => x.idHerramienta == i).First();
                        TimeSpan estim = new TimeSpan(0, (int)her2.estimado, 0);
                        if (excedidas[i - 1] > 0) { pexceso = excedidas[i - 1] * 100 / tratadas[i - 1]; }
                        else { pexceso = 0; }
                        TimeSpan? exc = new TimeSpan?();
                        if ((mayor[i - 1] - estim) > new TimeSpan(0, 0, 0))
                        { exc = (mayor[i - 1] - estim); }
                        TimeSpan tiempoPromedio;

                        try
                        {
                            tiempoPromedio = (TimeSpan)acumulado[i - 1];
                            porc = ((tiempoPromedio.TotalMinutes / tratadas[i - 1]) - her2.estimado) * 100 / her2.estimado;
                            porc = Math.Round((double)porc, 2);
                            if (porc <= 0) { porc = 0; }
                            if (tratadas[i - 1] <= 0) { porc = 0; }
                        }
                        catch (Exception) { porc = 0; }
                        try
                        {
                            tiempoPromedio = (TimeSpan)acumulado[i - 1];
                            tiempoPromedio = new TimeSpan(0, 0, (Convert.ToInt32(tiempoPromedio.TotalSeconds / tratadas[i - 1])));

                        }
                        catch (Exception) { tiempoPromedio = new TimeSpan(0, 0, 0); }


                        model.Add(new ger5()
                        {
                            operario = ope,
                            herramienta = her2.herramienta,
                            estimado = estim,
                            exceso = exc,
                            tratadas = tratadas[i - 1],
                            excedidas = excedidas[i - 1],
                            porcExceso = pexceso,
                            tmedio = tiempoPromedio,
                            porcFuera = porc
                        });
                    }

                    model.Add(new ger5()
                    {
                        operario = "",
                        herramienta = "",
                    });

                }
                if (fecha != null)
                {
                    DateTime idFecha2 = (DateTime)fecha;
                    if (idFecha2.Month < 10) { ViewBag.date = idFecha2.Year + "-0" + idFecha2.Month; }
                    else { ViewBag.date = idFecha2.Year + "-" + idFecha2.Month; }
                    TempData["fecha"] = idFecha2.ToShortDateString();
                }
                else
                {
                    TempData["fecha"] = DateTime.Today.ToShortDateString();
                    if (DateTime.Today.Month > 9)
                    {
                        ViewBag.date = DateTime.Today.Year + "-" + DateTime.Today.Month;
                    }
                    else { ViewBag.date = DateTime.Today.Year + "-0" + DateTime.Today.Month; }
                }
                if (print == "Imprimir")
                {
                    var usu = db.usuarios.Where(x => x.idUsuario == idSupervisor).First();
                    TempData["sup"] = usu.username;
                    TempData["ind4"] = model.ToList();
                    return RedirectToAction("Index4Print");
                }
                return View(model.ToList());
            }
            return RedirectToAction("Reacomodar", "Usuario");
        }

        // Index4Print
        public ActionResult Index4Print()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                ViewBag.date = TempData["fecha"];
                ViewBag.sup = TempData["sup"];
                var ind4 = TempData["ind4"];
                return View(ind4);
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }

        //Gerencia/SolicitudesPorSolicitante/Operario
        public ActionResult Index5()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idRol == "ope" && r.activo == true), "idUsuario", "username");
                ViewBag.idSolicitante = new SelectList(db.usuarios.Where(r => r.idRol == "sol" && r.activo == true), "idUsuario", "username");
                if (uslog.idRol == "ger")
                {
                    return View();
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        [HttpPost]
        public ActionResult Index5(int? idOperario, int? idSolicitante, DateTime? fecha1, DateTime? fecha2,string print)
        {

            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idOperario = new SelectList(db.usuarios.Where(r => r.idRol == "ope" && r.activo == true), "idUsuario", "username");
            ViewBag.idSolicitante = new SelectList(db.usuarios.Where(r => r.idRol == "sol" && r.activo == true), "idUsuario", "username");
            if (uslog != null && uslog.idRol == "ger")
            {
                int? id = null;
                try
                {
                    if (idSolicitante != null) { id = idSolicitante;
                    usuarios u = db.usuarios.Where(x => x.idUsuario == idSolicitante).First();
                    TempData["usu"] = "Solicitate: " + u.username;
                    }
                    if (idOperario != null) { id = idOperario;
                    usuarios u = db.usuarios.Where(x => x.idUsuario == idOperario).First();
                    TempData["usu"] = "Operario: " + u.username;
                    }
                    {
                        if (id != null)
                        {
                            
                            int cas = 0;
                            if (fecha1 != null) { cas = 1; ViewBag.desde = fecha1.Value.ToString("yyyy-MM-dd"); }
                            if (fecha2 != null) { cas = 2; ViewBag.hasta = fecha2.Value.ToString("yyyy-MM-dd"); }
                            if (fecha1 != null && fecha2 != null) { cas = 3; }

                            switch (cas)
                            {
                                case 0:
                                    {
                                        var reposicion = db.reposiciones.Where(x => x.idOperario == id || x.idSolicitante == id).OrderByDescending(x => x.horaCreacion);
                                        if (reposicion.Count() > 1 && print != "Imprimir") { return View(reposicion.ToList()); } else { TempData["ind5"] = reposicion; break; } 
                                    }
                                case 1:
                                    {
                                        var reposicion = db.reposiciones.Where(x => x.idOperario == id || x.idSolicitante == id).Where(x => x.horaCreacion > fecha1).OrderByDescending(x => x.horaCreacion);
                                        if (reposicion.Count() > 1 && print != "Imprimir") { return View(reposicion.ToList()); } else { TempData["ind5"] = reposicion; break; } 
                                    }
                                case 2:
                                    {
                                        var reposicion = db.reposiciones.Where(x => x.idOperario == id || x.idSolicitante == id).Where(x => x.horaCreacion < fecha2).OrderByDescending(x => x.horaCreacion);
                                        if (reposicion.Count() > 1 && print != "Imprimir") { return View(reposicion.ToList()); } else { TempData["ind5"] = reposicion; break; } 
                                    }
                                case 3:
                                    {
                                        var reposicion = db.reposiciones.Where(x => x.idOperario == id || x.idSolicitante == id).Where(x => x.horaCreacion > fecha1 && x.horaCreacion < fecha2).OrderByDescending(x => x.horaCreacion);
                                        if (reposicion.Count() > 1 && print != "Imprimir") { return View(reposicion.ToList()); } else { TempData["ind5"] = reposicion; break; } 
                                    }
                                default: return View(); break;
                            }
                            if (print == "Imprimir")
                            {
                                return RedirectToAction("Index5Print");
                            }
                            return View();
                        }
                    }
                    return View();
                }
                catch (Exception e) { return View(); }

            }
            return RedirectToAction("Reacomodar", "Usuario");
        }

        // Index5Print
        public ActionResult Index5Print()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                ViewBag.usu = TempData["usu"];
                var model = TempData["ind5"];
                return View(model);
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }

        // GET: /Solicitante/Details/5
        public ActionResult Details(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index");
                    }
                    canceladas cancel = db.canceladas.Find(id);
                    reposiciones rep = new reposiciones();
                    try { usuarios user = db.usuarios.Find(Convert.ToInt32(cancel.idSolicitante));
                    rep.solicitantes = user;
                    }
                    catch (Exception ) {  }
                    if (cancel == null)
                    {
                        return RedirectToAction("Index");
                    }
                    
                    rep.horaCreacion = cancel.horaCreacion;
                    rep.idSolicitante = Convert.ToInt32(cancel.idSolicitante);
                    rep.idReposicion = cancel.idReposicion;
                    rep.idHerramienta = cancel.idHerramienta;
                    rep.idPuesto = cancel.idPuesto;
                    rep.horaAsignacion = cancel.horaCancelacion;
                    return View(rep);
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }

        //Gerencia/MayorDesvio y Porcentaje fuera de tiempo mensual por Supervisor.
        public ActionResult Index6()
        {
            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop"), "idUsuario", "username");
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {

                return View();

            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }


        //Gerencia/MayorDesvio y Porcentaje fuera de tiempo mensual por Supervisor.
        [HttpPost]
        public ActionResult Index6(int? idSupervisor, DateTime? fecha, string print)
        {
            /*
            acumulado1 = acumulado2 = acumulado3 = acumulado4 = acumulado5 = acumulado6 = acumulado7 = acumulado8 = acumulado9 = acumulado10 = new TimeSpan(0, 0, 0);
            */

            List<ger5> model = new List<ger5>();
            var uslog = (usuarios)Session["UsuarioLogueado"];
            ViewBag.idSupervisor = new SelectList(db.usuarios.Where(r => r.idRol == "jop"), "idUsuario", "username");
            if (uslog != null && uslog.idRol == "ger")
            {
                DateTime fecha1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime fecha2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                if (fecha != null)
                {
                    fecha1 = new DateTime(fecha.Value.Year, fecha.Value.Month, 1);
                    fecha2 = new DateTime(fecha.Value.Year, fecha.Value.Month, DateTime.DaysInMonth(fecha.Value.Year, fecha.Value.Month));
                }

                // mayor desvio y porcentaje de resoluciones fuera de tiempo por tipo de reposicion
                // por cada operario : 

                // si quiero meter que muestre ambos sups si no elijo alguno, acá es el lugar... 



                var reps = db.reposiciones.Where(x => x.horaCreacion > fecha1 && x.horaCreacion < fecha2 && x.operarios.idSupervisor == idSupervisor && x.idEstado == 3 && x.horaResolucion != null);
                var her = db.herramientas;
                int[] tratadas = new int[her.Count()];
                int[] excedidas = new int[her.Count()];
                TimeSpan[] acumulado = new TimeSpan[her.Count()];
                TimeSpan[] mayor = new TimeSpan[her.Count()];
                for (int i = 0; i < her.Count(); i++)
                {
                    acumulado[i] = new TimeSpan(0, 0, 0);
                    mayor[i] = new TimeSpan(0, 0, 0);
                }
                foreach (var a in reps)
                {
                    tratadas[(int)a.idHerramienta - 1]++;
                    DateTime hora1 = (DateTime)a.horaResolucion;
                    DateTime hora2 = (DateTime)a.horaAsignacion;
                    TimeSpan diferencia = hora1 - hora2;
                    acumulado[(int)a.idHerramienta - 1] = acumulado[(int)a.idHerramienta - 1] + diferencia;
                    if (a.horaResolucion - a.horaAsignacion > mayor[(int)a.idHerramienta - 1])
                    {
                        mayor[(int)a.idHerramienta - 1] = (TimeSpan)diferencia; // verifica el mayor tiempo de solicitud. 
                    }
                    if (diferencia > new TimeSpan(0, (int)a.herramientas.estimado, 0))
                    {
                        excedidas[(int)a.idHerramienta - 1]++; // verifica la cantidad de solicitudes fuera de tiempo. 
                    }
                }

                // foreach reposicion
                double? pexceso = 0; double? porc = 0;
                var sup = db.usuarios.Where(x => x.idUsuario == idSupervisor).First();
                TempData["sup"] = sup.username;


                for (int i = 1; i < her.Count() + 1; i++)
                {
                    string ss = "";
                    if (i == 1) { ss = sup.username; }
                    herramientas her2 = db.herramientas.Where(x => x.idHerramienta == i).First();
                    TimeSpan estim = new TimeSpan(0, (int)her2.estimado, 0);
                    if (tratadas[i - 1] > 0) { pexceso = excedidas[i - 1] * 100 / tratadas[i - 1]; }
                    else { pexceso = 0; }
                    TimeSpan? exc = new TimeSpan?();
                    if ((mayor[i - 1] - estim) > new TimeSpan(0, 0, 0))
                    { exc = (mayor[i - 1] - estim); }
                    TimeSpan tiempoPromedio;

                    try
                    {
                        tiempoPromedio = (TimeSpan)acumulado[i - 1];
                        porc = ((tiempoPromedio.TotalMinutes / tratadas[i - 1]) - her2.estimado) * 100 / her2.estimado;
                        porc = Math.Round((double)porc, 2);
                        if (porc <= 0) { porc = 0; }
                        if (tratadas[i - 1] <= 0) { porc = 0; }
                    }
                    catch (Exception) { porc = 0; }

                    try
                    {
                        tiempoPromedio = (TimeSpan)acumulado[i - 1];
                        tiempoPromedio = new TimeSpan(0, 0, (Convert.ToInt32(tiempoPromedio.TotalSeconds / tratadas[i - 1])));

                    }
                    catch (Exception) { tiempoPromedio = new TimeSpan(0, 0, 0); }
                    model.Add(new ger5()
                    {
                        supervisor = ss,
                        herramienta = her2.herramienta,
                        estimado = estim,
                        exceso = exc,
                        tratadas = tratadas[i - 1],
                        excedidas = excedidas[i - 1],
                        porcExceso = pexceso,
                        tmedio = tiempoPromedio,
                        porcFuera = porc
                    });
                }

                // for que carga el modelo





                if (fecha != null)
                {
                    DateTime idFecha2 = (DateTime)fecha;
                    if (idFecha2.Month < 10) { ViewBag.date = idFecha2.Year + "-0" + idFecha2.Month; }
                    else { ViewBag.date = idFecha2.Year + "-" + idFecha2.Month; }
                    TempData["fecha"] = idFecha2.ToShortDateString();
                }
                else
                {
                    TempData["fecha"] = DateTime.Today.ToShortDateString();
                    if (DateTime.Today.Month > 9)
                    {
                        ViewBag.date = DateTime.Today.Year + "-" + DateTime.Today.Month;
                    }
                    else { ViewBag.date = DateTime.Today.Year + "-0" + DateTime.Today.Month; }
                }
                if (print == "Imprimir")
                {
                    var usu = db.usuarios.Where(x => x.idUsuario == idSupervisor).First();
                    TempData["sup"] = usu.username;
                    TempData["ind6"] = model.ToList();
                    return RedirectToAction("Index6Print");
                }
                return View(model.ToList());
            }
            return RedirectToAction("Reacomodar", "Usuario");
        }

        // Index6Print
        public ActionResult Index6Print()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                ViewBag.date = TempData["fecha"];
                ViewBag.sup = TempData["sup"];
                var ind6 = TempData["ind6"];
                return View(ind6);
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }

        // get herramientas
        public ActionResult Herr()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
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
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    herramientas herramienta = db.herramientas.Find(id);
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


        // get herramientas
        public ActionResult Herr2()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
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
        public ActionResult EditH2(int? id)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    herramientas herramienta = db.herramientas.Find(id);
                    if (herramienta == null)
                    {
                        return RedirectToAction("Herr2");
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
        public ActionResult EditH([Bind(Include = "idHerramienta,stock")] herramientas herramienta)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    try
                    {
                        // Debe tomar el ID del usuario
                        herramientas herr = db.herramientas.Single(p => p.idHerramienta == herramienta.idHerramienta);
                        if (herr != null)
                        {
                            herr.stock = herramienta.stock;
                            db.SaveChanges();
                            return RedirectToAction("Herr");
                        }
                        //Redirige a la lista de solicitudes activas y cambio de clave.
                        else
                        { return RedirectToAction("Herr"); }
                    }
                    catch (Exception )
                    { return RedirectToAction("Herr"); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }


        // POST:// get herramientas
        [HttpPost]
        public ActionResult EditH2([Bind(Include = "idHerramienta,estimado")] herramientas herramienta)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null)
            {
                if (uslog.idRol == "ger")
                {
                    try
                    {
                        // Debe tomar el ID del usuario
                        herramientas herr = db.herramientas.Single(p => p.idHerramienta == herramienta.idHerramienta);
                        if (herr != null)
                        {
                            herr.estimado = herramienta.estimado;
                            db.SaveChanges();
                            return RedirectToAction("Herr2");
                        }
                        //Redirige a la lista de solicitudes activas y cambio de clave.
                        else
                        { return RedirectToAction("Herr2"); }
                    }
                    catch (Exception)
                    { return RedirectToAction("Herr2"); }
                }
                else
                { return RedirectToAction("Reacomodar", "Usuario"); }
            }
            else
            { return RedirectToAction("Index", "LogIn"); }
        }


        // get herramientas
        public ActionResult cargarH()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                return View();
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); }
        }

        // get herramientas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cargarH([Bind(Include = "herramienta,stock,estimado")] herramientas herr)
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog != null && uslog.idRol == "ger")
            {
                db.herramientas.Add(new herramientas()
                {
                    herramienta = herr.herramienta,
                    stock = herr.stock,
                    estimado = herr.estimado
                });
                    db.SaveChanges();
                    ViewBag.aviso = "Herramienta: " + herr.herramienta + " cargada exitosamente ( Stock : " + herr.stock + " TE : " + herr.estimado +  ")";
                return View();
            }
            else
            { return RedirectToAction("Reacomodar", "Usuario"); } 
        }

    }
}