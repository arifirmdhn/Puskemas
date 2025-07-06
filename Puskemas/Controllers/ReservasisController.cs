using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puskemas.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReservasisController : Controller
    {
        private readonly AppDbContext _context;

        public ReservasisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reservasis
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reservasis.Include(r => r.Jadwal).Include(r => r.Pasien);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reservasis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasi = await _context.Reservasis
                .Include(r => r.Jadwal)
                .Include(r => r.Pasien)
                .FirstOrDefaultAsync(m => m.IdReservasi == id);
            if (reservasi == null)
            {
                return NotFound();
            }

            return View(reservasi);
        }

        // GET: Reservasis/Create
        public IActionResult Create()
        {
            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "Nama");
            ViewData["IdJadwal"] = new SelectList(
                _context.Jadwals.AsEnumerable().Select(j => new {
                    IdJadwal = j.IdJadwal,
                    TanggalFormatted = j.Tanggal.ToString("dd-MM-yyyy")
                }),
                "IdJadwal",
                "TanggalFormatted"
            );

            // Ambil nomor terakhir (angka murni)
            var nomorTerakhir = _context.Reservasis
                .OrderByDescending(r => r.NomorAntrian)
                .Select(r => (int?)r.NomorAntrian)
                .FirstOrDefault();

            int nomorBaru = (nomorTerakhir ?? 0) + 1;

            ViewBag.NomorAntrian = nomorBaru; // tanpa format string

            var reservasi = new Reservasi
            {
                JamSelesai = TimeSpan.Zero
            };
            return View(reservasi);
        }

        // POST: Reservasis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPasien,IdJadwal,StatusReservasi,JamSelesai")] Reservasi reservasi)
        {
            if (ModelState.IsValid)
            {
                // Nomor antrian terbaru sebagai angka
                var nomorTerakhir = _context.Reservasis
                    .OrderByDescending(r => r.NomorAntrian)
                    .Select(r => (int?)r.NomorAntrian)
                    .FirstOrDefault();

                int nomorBaru = (nomorTerakhir ?? 0) + 1;
                reservasi.NomorAntrian = nomorBaru;

                _context.Add(reservasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "Nama", reservasi.IdPasien);
            ViewData["IdJadwal"] = new SelectList(
                _context.Jadwals.AsEnumerable().Select(j => new {
                    IdJadwal = j.IdJadwal,
                    TanggalFormatted = j.Tanggal.ToString("dd-MM-yyyy")
                }),
                "IdJadwal",
                "TanggalFormatted",
                reservasi.IdJadwal
            );

            return View(reservasi);
        }

        // GET: Reservasis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasi = await _context.Reservasis.FindAsync(id);
            if (reservasi == null)
            {
                return NotFound();
            }

            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "Nama", reservasi.IdPasien);
            ViewData["IdJadwal"] = new SelectList(
                _context.Jadwals.AsEnumerable().Select(j => new {
                    IdJadwal = j.IdJadwal,
                    TanggalFormatted = j.Tanggal.ToString("dd-MM-yyyy")
                }),
                "IdJadwal",
                "TanggalFormatted",
                reservasi.IdJadwal
            );
            return View(reservasi);
        }

        // POST: Reservasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReservasi,IdPasien,IdJadwal,StatusReservasi,JamSelesai,NomorAntrian")] Reservasi reservasi)
        {
            if (id != reservasi.IdReservasi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservasiExists(reservasi.IdReservasi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "Nama", reservasi.IdPasien);
            ViewData["IdJadwal"] = new SelectList(
                _context.Jadwals.AsEnumerable().Select(j => new {
                    IdJadwal = j.IdJadwal,
                    TanggalFormatted = j.Tanggal.ToString("dd-MM-yyyy")
                }),
                "IdJadwal",
                "TanggalFormatted",
                reservasi.IdJadwal
            );
            return View(reservasi);
        }

        // GET: Reservasis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasi = await _context.Reservasis
                .Include(r => r.Jadwal)
                .Include(r => r.Pasien)
                .FirstOrDefaultAsync(m => m.IdReservasi == id);
            if (reservasi == null)
            {
                return NotFound();
            }

            return View(reservasi);
        }

        // POST: Reservasis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservasi = await _context.Reservasis.FindAsync(id);
            if (reservasi != null)
            {
                _context.Reservasis.Remove(reservasi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservasiExists(int id)
        {
            return _context.Reservasis.Any(e => e.IdReservasi == id);
        }
    }
}
