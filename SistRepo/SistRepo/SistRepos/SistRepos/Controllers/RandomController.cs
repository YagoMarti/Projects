using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistRepos.Models;

namespace SistRepos.Controllers
{
    public class RandomController : Controller
    {
        private ReposicionesEntities db = new ReposicionesEntities();



        Random rnd = new Random();
        
        // GET: /Random/Create/Herramientas
        public ActionResult h()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog.idRol == "adm")
            {
                String[] herramientasArray = { "Auricular", "Microfono", "CPU", "Monitor", "Silla", "Teclado", "Mouse" };
                herramientas h = new herramientas();
                for (int i = 0; i < herramientasArray.Count(); i++)
                {
                    h.herramienta = herramientasArray[i];
                    h.stock = 300;
                    db.herramientas.Add(h);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        // GET: /Random/Create/Estados
        public ActionResult e()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog.idRol == "adm")
            {
                String[] estadosArray = { "En espera", "En tratamiento", "Resuelta" };
                estados e = new estados();
                for (int i = 0; i < estadosArray.Count(); i++)
                {
                    e.estado = estadosArray[i];
                    db.estados.Add(e);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        // GET: /Random/Create/Usuarios
        public ActionResult cu()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog.idRol == "adm")
            {

                String[] nombreArray = { "Juan", "Constanza", "Osvaldo", "Lucia", "Maria", "Damian", "Pedro", "Pablo", "Emiliano", "Aylen", "Manuel", "Facundo", "Lucas", "Martín", "Gisella", "Jessica", "Sofia", "Julio", "Marina", "Federico", "Santiago" };
                String[] apellidoArray = { "Dans", "Blasco", "Nicolini", "Rodriguez", "Bolloqui", "Gaunes", "Pizzini", "Campos", "Bonelli", "Ceratti", "Benven", "Taberna", "Clavero", "Lopez", "Juarez", "Diaz", "Carra", "Suse", "Despasiane" };
                String[] rolesArray = { "adm", "ger", "jop", "jso", "ope", "sol", "usr" };
                usuarios u = new usuarios();
                int maxid = db.usuarios.Max(c => c.idUsuario);

                for (int i = 0; i < 2; i++)
                { // cargar 2 Gerentes
                    int n = rnd.Next(1, nombreArray.Count());
                    u.nombre = nombreArray[n];
                    int a = rnd.Next(1, apellidoArray.Count());
                    u.apellido = apellidoArray[a];
                    u.telefono = rnd.Next(155000000, 160000000).ToString();
                    u.fechaContratacion = new DateTime(2014, rnd.Next(7, 12), rnd.Next(1, 31));
                    u.fechaNacimiento = new DateTime(rnd.Next(1960, 1990), rnd.Next(1, 13), rnd.Next(1, 28));
                    u.idRol = "ger";
                    u.idLocalidad = rnd.Next(1, 97);
                    u.username = "Gerencia" + (i + 1 + maxid);
                    u.claveacc = u.username;
                    u.activo = true;
                    db.usuarios.Add(u);
                    db.SaveChanges();
                }
                maxid = db.usuarios.Max(c => c.idUsuario);

                for (int i = 0; i < 3; i++)
                { // cargar 3 Admin de Usuarios
                    int n = rnd.Next(1, nombreArray.Count());
                    u.nombre = nombreArray[n];
                    int a = rnd.Next(1, apellidoArray.Count());
                    u.apellido = apellidoArray[a];
                    u.telefono = rnd.Next(155000000, 160000000).ToString();
                    u.fechaContratacion = new DateTime(2014, rnd.Next(7, 12), rnd.Next(1, 31));
                    u.fechaNacimiento = new DateTime(rnd.Next(1960, 1990), rnd.Next(1, 13), rnd.Next(1, 28));
                    u.idRol = "usr";
                    u.idLocalidad = rnd.Next(1, 97);
                    u.username = "Usuario" + (i + 1 + maxid);
                    u.claveacc = u.username;
                    db.usuarios.Add(u);
                    db.SaveChanges();
                }
                maxid = db.usuarios.Max(c => c.idUsuario);

                for (int i = 0; i < 8; i++)
                { // cargar 8 jefes de Solicitantes
                    int n = rnd.Next(1, nombreArray.Count());
                    u.nombre = nombreArray[n];
                    int a = rnd.Next(1, apellidoArray.Count());
                    u.apellido = apellidoArray[a];
                    u.telefono = rnd.Next(155000000, 160000000).ToString();
                    u.fechaContratacion = new DateTime(2014, rnd.Next(7, 12), rnd.Next(1, 31));
                    u.fechaNacimiento = new DateTime(rnd.Next(1960, 1990), rnd.Next(1, 13), rnd.Next(1, 28));
                    u.idRol = "jso";
                    u.idLocalidad = rnd.Next(1, 97);
                    u.username = "JefeSolicitante" + (i + 1 + maxid);
                    u.claveacc = u.username;
                    db.usuarios.Add(u);
                    db.SaveChanges();
                }
                maxid = db.usuarios.Max(c => c.idUsuario);

                for (int i = 0; i < 2; i++)
                { // cargar 2 jefes de Operarios
                    int n = rnd.Next(1, nombreArray.Count());
                    u.nombre = nombreArray[n];
                    int a = rnd.Next(1, apellidoArray.Count());
                    u.apellido = apellidoArray[a];
                    u.telefono = rnd.Next(155000000, 160000000).ToString();
                    u.fechaContratacion = new DateTime(2014, rnd.Next(7, 12), rnd.Next(1, 31));
                    u.fechaNacimiento = new DateTime(rnd.Next(1960, 1990), rnd.Next(1, 13), rnd.Next(1, 28));
                    u.idRol = "jop";
                    u.idLocalidad = rnd.Next(1, 97);
                    u.username = "JefeOperario" + (i + 1 + maxid);
                    u.claveacc = u.username;
                    db.usuarios.Add(u);
                    db.SaveChanges();
                }

                var jsol = db.usuarios.Where(x => x.idRol == "jso");
                foreach (usuarios gf in jsol)
                {
                    maxid = db.usuarios.Max(c => c.idUsuario);
                    for (int i = 0; i < rnd.Next(12, 16); i++)
                    { // cargar 6 jefes de Operarios
                        int n = rnd.Next(1, nombreArray.Count());
                        u.nombre = nombreArray[n];
                        int a = rnd.Next(1, apellidoArray.Count());
                        u.apellido = apellidoArray[a];
                        u.telefono = rnd.Next(155000000, 160000000).ToString();
                        u.fechaContratacion = new DateTime(2014, rnd.Next(7, 12), rnd.Next(1, 31));
                        u.fechaNacimiento = new DateTime(rnd.Next(1960, 1990), rnd.Next(1, 13), rnd.Next(1, 28));
                        u.idRol = "sol";
                        u.idLocalidad = rnd.Next(1, 97);
                        u.username = "Solicitante" + (i + 1 + maxid);
                        u.claveacc = u.username;
                        u.idSupervisor = gf.idUsuario;
                        db.usuarios.Add(u);
                        db.SaveChanges();
                    }
                }
                var jops = db.usuarios.Where(x => x.idRol == "jop");
                foreach (usuarios gf in jops)
                {
                    maxid = db.usuarios.Max(c => c.idUsuario);
                    for (int i = 0; i < rnd.Next(8, 11); i++)
                    { // cargar operarios jefes de Operarios
                        int n = rnd.Next(1, nombreArray.Count());
                        u.nombre = nombreArray[n];
                        int a = rnd.Next(1, apellidoArray.Count());
                        u.apellido = apellidoArray[a];
                        u.telefono = rnd.Next(155000000, 160000000).ToString();
                        u.fechaContratacion = new DateTime(2014, rnd.Next(7, 12), rnd.Next(1, 31));
                        u.fechaNacimiento = new DateTime(rnd.Next(1960, 1990), rnd.Next(1, 13), rnd.Next(1, 28));
                        u.idRol = "ope";
                        u.idLocalidad = rnd.Next(1, 97);
                        u.username = "Operario" + (i + 1 + maxid);
                        u.claveacc = u.username;
                        u.idSupervisor = gf.idUsuario;
                        db.usuarios.Add(u);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: /Random/Create/400Solis
        public ActionResult r400()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog.idRol == "adm")
            {

                var oper = db.usuarios.Where(x => x.idRol == "ope" && x.activo == true);
                List<usuarios> listOper = oper.ToList();
                int op = listOper.Count() - 1;
                var soli = db.usuarios.Where(x => x.idRol == "sol" && x.activo == true);
                usuarios[] arraySol = soli.ToArray();

                var pues = db.puestos;
                List<puestos> listP = pues.ToList();

                var her = db.herramientas;
                List<herramientas> listH = her.ToList();

                reposiciones r = new reposiciones();
                for (int i = 0; i < 400; i++)
                {
                    r.idOperario = listOper[rnd.Next(0, op)].idUsuario;

                    r.idSolicitante = arraySol[rnd.Next(0, arraySol.Count())].idUsuario;
                    r.idPuesto = listP[rnd.Next(0, listP.Count())].idPuesto;
                    r.idHerramienta = listH[rnd.Next(0, listH.Count())].idHerramienta; ;
                    
                    int mes = 0;
                    mes = rnd.Next(6, 13);
                    int dia = 0;
                    dia = rnd.Next(1, 30);
                    if (mes == 12) { dia = rnd.Next(1, 19); }
                    int hora = 0;
                    hora = rnd.Next(0, 24);
                    int minuto = 0;
                    minuto = rnd.Next(0, 59);
                    int segundo = 0;
                    segundo = rnd.Next(0, 59);
                    r.horaCreacion = new DateTime(2014, mes, dia, hora, minuto, segundo);
                    segundo += rnd.Next(0, 59);
                    if (segundo > 59)
                    {
                        segundo -= 59;
                        minuto++;
                    }
                    minuto += rnd.Next(0, 59);
                    if (minuto > 59)
                    {
                        minuto -= 59;
                        hora++;
                    }
                    int dd = rnd.Next(0, 1000);
                    if (dd > 998) { hora++; }
                    dd = rnd.Next(0, 1000);
                    if (dd > 998) { hora++; }
                    if (hora > 23)
                    {
                        hora -= 23;
                        dia++;
                    }
                    if (dia > 30)
                    {
                        dia -= 30;
                        mes++;
                    }
                    r.comentario = "Se rompió";
                    if (segundo > 20) { r.comentario = "se corto el cable"; }
                    if (segundo > 30) { r.comentario = "dejó de funcionar"; }
                    if (segundo > 50) { r.comentario = "no toma corriente"; }
                    r.horaAsignacion = new DateTime(2014, mes, dia, hora, minuto, segundo);
                    segundo += rnd.Next(0, 59);
                    if (segundo > 59)
                    {
                        segundo -= 59;
                        minuto++;
                    }
                    minuto += rnd.Next(0, 59);
                    if (minuto > 59)
                    {
                        minuto -= 59;
                        hora++;
                    }
                    dd = rnd.Next(0, 1000);
                    if (dd > 998) { hora++; }
                    dd = rnd.Next(0, 1000);
                    if (dd > 998) { hora++; }
                    if (hora > 23)
                    {
                        hora -= 23;
                        dia++;
                    }
                    if (dia > 30)
                    {
                        dia -= 30;
                        mes++;
                    }
                    r.horaResolucion = new DateTime(2014, mes, dia, hora, minuto, segundo);
                    r.idEstado = 3;


                    db.reposiciones.Add(r);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: /Random/Create/15 solis activas y asignadas
        public ActionResult a30()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog.idRol == "adm")
            {

                var oper = db.usuarios.Where(x => x.idRol == "ope" && x.activo == true);
                List<usuarios> listOper = oper.ToList();
                int op = listOper.Count();
                var soli = db.usuarios.Where(x => x.idRol == "sol" && x.activo == true);
                usuarios[] arraySol = soli.ToArray();

                var pues = db.puestos;
                List<puestos> listP = pues.ToList();

                var her = db.herramientas;
                List<herramientas> listH = her.ToList();

                reposiciones r = new reposiciones();
                for (int i = 0; i < 10; i++)
                {
                    r.idOperario = listOper[rnd.Next(0, op)].idUsuario;

                    r.idSolicitante = arraySol[rnd.Next(0, arraySol.Count())].idUsuario;
                    r.idPuesto = listP[rnd.Next(0, listP.Count())].idPuesto;
                    r.idHerramienta = listH[rnd.Next(0, listH.Count())].idHerramienta; ;
                    r.comentario = "a";
                    int mes = 12;
                    int dia = 19;
                    int hora = 0;
                    hora = rnd.Next(8, 16);
                    int minuto = 0;
                    minuto = rnd.Next(0, 59);
                    int segundo = 0;
                    segundo = rnd.Next(0, 59);
                    r.horaCreacion = new DateTime(2014, mes, dia, hora, minuto, segundo);
                    segundo += rnd.Next(0, 59);
                    if (segundo > 59)
                    {
                        segundo -= 59;
                        minuto++;
                    }
                    minuto += rnd.Next(0, 59);
                    if (minuto > 59)
                    {
                        minuto -= 59;
                        hora++;
                    }
                    int dd = rnd.Next(0, 1000);
                    if (dd > 998) { hora++; }
                    dd = rnd.Next(0, 1000);
                    if (dd > 998) { hora++; }
                    if (hora > 23)
                    {
                        hora -= 23;
                        dia++;
                    }
                    if (dia > 30)
                    {
                        dia -= 30;
                        mes++;
                    }

                    r.horaAsignacion = new DateTime(2014, mes, dia, hora, minuto, segundo);
                    r.horaResolucion = null;
                    r.idEstado = 2;


                    db.reposiciones.Add(r);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: /Random/Create/15 solis activas no asignadas
        public ActionResult b30()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog.idRol == "adm")
            {

                var oper = db.usuarios.Where(x => x.idRol == "ope" && x.activo == true);
                List<usuarios> listOper = oper.ToList();
                int op = listOper.Count() - 1;
                var soli = db.usuarios.Where(x => x.idRol == "sol" && x.activo == true);
                usuarios[] arraySol = soli.ToArray();

                var pues = db.puestos;
                List<puestos> listP = pues.ToList();

                var her = db.herramientas;
                List<herramientas> listH = her.ToList();

                reposiciones r = new reposiciones();
                for (int i = 0; i < 10; i++)
                {
                    r.idOperario = null;

                    r.idSolicitante = arraySol[rnd.Next(0, arraySol.Count())].idUsuario;
                    r.idPuesto = listP[rnd.Next(0, listP.Count())].idPuesto;
                    r.idHerramienta = listH[rnd.Next(0, listH.Count())].idHerramienta; ;
                    r.comentario = "a";
                    int mes = 12;
                    int dia = 19;
                    int hora = 0;
                    hora = rnd.Next(8, 16);
                    int minuto = 0;
                    minuto = rnd.Next(0, 59);
                    int segundo = 0;
                    segundo = rnd.Next(0, 59);
                    r.horaCreacion = new DateTime(2014, mes, dia, hora, minuto, segundo);

                    r.horaAsignacion = null;
                    r.horaResolucion = null;
                    r.idEstado = 1;


                    db.reposiciones.Add(r);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        // GET: /Random/
        public ActionResult Index()
        {
            var uslog = (usuarios)Session["UsuarioLogueado"];
            if (uslog.idRol == "adm") { return View(); }
            else return RedirectToAction("Reacomodar", "Usuario");
        }



 
    }
}
