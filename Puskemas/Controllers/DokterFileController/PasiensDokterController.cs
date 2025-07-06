
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puskemas.Data;
using Puskemas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Puskemas.Controllers.DokterFileController
{
    [Authorize(Roles = "Dokter")]
    public class PasiensDokterController : Controller
    {
        private readonly AppDbContext _context;

        public PasiensDokterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PasiensDokter
        public async Task<IActionResult> Index()
        {
            var pasienList = await _context.Pasiens.ToListAsync();
            return View(pasienList);
        }

        // GET: PasiensDokter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pasien = await _context.Pasiens
                .FirstOrDefaultAsync(m => m.IdPasien == id);

            if (pasien == null) return NotFound();

            return View(pasien);
        }
    }
}