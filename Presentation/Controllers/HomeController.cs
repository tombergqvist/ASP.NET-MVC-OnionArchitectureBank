using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Application.Queries.Customers;
using Application.Interfaces;
using Application.Queries.Statistics;
using Persistence;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private IBankDbContext _context;

        public HomeController(IBankDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new StatisticsQuery().GetStatistics(_context);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
