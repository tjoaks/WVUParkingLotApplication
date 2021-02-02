using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DiscussionMVCAppOaks.Models;
using DiscussionMVCAppOaks.Models.LotModel;

namespace DiscussionMVCAppOaks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILotRepo iLotRepo;

        public HomeController(ILogger<HomeController> logger, ILotRepo lotRepo)
        {
            _logger = logger;
            this.iLotRepo = lotRepo;
        }

        //generating json data for js

        public string GetSpotsForAllLots()
        {
            string JSONData = iLotRepo.GetSpotsForAllLots();
            return JSONData;
        }

        public IActionResult Index()
        {
            return View();
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
