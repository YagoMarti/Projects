﻿using System;
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
            return View();
        }

        public ActionResult Empty()
        {
            return View();
        }
	}
}