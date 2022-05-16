using MegaHockey.Data;
using MegaHockey.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace MegaHockey.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HistoryRecordContext _context;

        public HomeController(ILogger<HomeController> logger, HistoryRecordContext context)
        {
            _context = context;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        public IActionResult SearchResult(String first, String second)
        {
            ViewBag.First = first;
            ViewBag.Second = second;
            ViewBag.Context = _context;
            ViewBag.User = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        public IActionResult Search()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(id);
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}