using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puskemas.Data;
using Puskemas.Models;
using System.Diagnostics;

namespace Puskemas.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
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

        public IActionResult Service()
        {
            var viewModel = new ServiceDataViewModel
            {
                ReservasiList = _context.Reservasis
                    .Include(r => r.Pasien)
                    .Include(r => r.Jadwal)
                    .ToList(),

                RekamMedisList = _context.RekamMedis
                    .Include(r => r.Pasien)
                    .ToList(),

                JadwalList = _context.Jadwals
                    .Include(j => j.Dokter)
                    .ToList()
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
