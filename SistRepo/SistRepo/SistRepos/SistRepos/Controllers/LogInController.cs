using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisRep.Controllers
{
    public class LogInController : Controller
    {
        //
        // GET: /LogIn/
        public ActionResult Index()
        {
            if (TempData["log"] != null )
            { ViewBag.log = TempData["log"]; }
            return View();
        }

        public ActionResult Empty()
        {
            return View();
        }


        // permisos de usuarios  hecho
        // impresion de las listas y reportes

    }
}