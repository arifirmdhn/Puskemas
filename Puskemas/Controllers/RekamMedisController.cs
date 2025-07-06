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
    public class RekamMedisController : Controller
    {
        private readonly AppDbContext _context;

        public RekamMedisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RekamMedis
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RekamMedis.Include(r => r.Pasien);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RekamMedis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rekamMedis = await _context.RekamMedis
                .Include(r => r.Pasien)
                .FirstOrDefaultAsync(m => m.IdRekam == id);
            if (rekamMedis == null)
            {
                return NotFound();
            }

            return View(rekamMedis);
        }

        // GET: RekamMedis/Create
        public IActionResult Create()
        {
            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "Nama");
            return View();
        }

        // POST: RekamMedis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPasien,TanggalCheckup,Diagnosa,Pengobatan,Obat,Anamnesis,PemeriksaanFisik,Penatalaksanaan")] RekamMedis rekamMedis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rekamMedis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "IdPasien", rekamMedis.IdPasien);
            return View(rekamMedis);
        }

        // GET: RekamMedis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rekamMedis = await _context.RekamMedis.FindAsync(id);
            if (rekamMedis == null)
            {
                return NotFound();
            }
            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "Nama", rekamMedis.IdPasien);
            return View(rekamMedis);
        }

        // POST: RekamMedis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRekam,IdPasien,TanggalCheckup,Diagnosa,Pengobatan,Obat,Anamnesis,PemeriksaanFisik,Penatalaksanaan")] RekamMedis rekamMedis)
        {
            if (id != rekamMedis.IdRekam)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rekamMedis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RekamMedisExists(rekamMedis.IdRekam))
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
            ViewData["IdPasien"] = new SelectList(_context.Pasiens, "IdPasien", "IdPasien", rekamMedis.IdPasien);
            return View(rekamMedis);
        }

        // GET: RekamMedis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rekamMedis = await _context.RekamMedis
                .Include(r => r.Pasien)
                .FirstOrDefaultAsync(m => m.IdRekam == id);
            if (rekamMedis == null)
            {
                return NotFound();
            }

            return View(rekamMedis);
        }

        // POST: RekamMedis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rekamMedis = await _context.RekamMedis.FindAsync(id);
            if (rekamMedis != null)
            {
                _context.RekamMedis.Remove(rekamMedis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RekamMedisExists(int id)
        {
            return _context.RekamMedis.Any(e => e.IdRekam == id);
        }
    }
}
