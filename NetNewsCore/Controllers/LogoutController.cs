﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetNews.Models;

namespace NetNews.Controllers
{
    public class LogoutController : Controller
    {
        private readonly DBConnection _context;
        private readonly SessionManager _sessionManager;

        public LogoutController(SessionManager sessionManager, DBConnection context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public IActionResult Index()
        {
            //Remove sessions
            _sessionManager.ClearSessions();
            return RedirectToAction("Index", "Home");
        }
    }
}