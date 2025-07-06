using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puskemas.Data;
using Puskemas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Puskemas.Controllers
{
    public class ReservasisDokterController : Controller
    {
        private readonly AppDbContext _context;

        public ReservasisDokterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReservasisDokter
        public async Task<IActionResult> Index()
        {
            var reservasiList = await _context.Reservasis
                .Include(r => r.Jadwal)
                .Include(r => r.Pasien)
                .ToListAsync();

            return View(reservasiList);
        }

        // GET: ReservasisDokter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var reservasi = await _context.Reservasis
                .Include(r => r.Jadwal)
                .Include(r => r.Pasien)
                .FirstOrDefaultAsync(r => r.IdReservasi == id);

            if (reservasi == null)
                return NotFound();

            return View(reservasi);
        }

        // POST: ReservasisDokter/Konfirmasi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Konfirmasi(int id, string status)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var reservasi = _context.Reservasis.FirstOrDefault(r => r.IdReservasi == id);
            if (reservasi == null)
                return NotFound();

            // Ubah status
            reservasi.StatusReservasi = status;

            // Jika status jadi "Selesai", isi JamSelesai otomatis
            if (status == "Selesai")
            {
                reservasi.JamSelesai = DateTime.Now.TimeOfDay;
            }

            _context.SaveChanges();

            TempData["Success"] = $"Status reservasi berhasil diubah menjadi \"{status}\".";

            return RedirectToAction("Index");
        }
    }
}
