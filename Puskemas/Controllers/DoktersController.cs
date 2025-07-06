using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Puskemas.Controllers
{
    public class DoktersController : Controller
    {
        private readonly AppDbContext _context;

        public DoktersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dokters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dokters.ToListAsync());
        }

        // GET: Dokters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokter = await _context.Dokters
                .FirstOrDefaultAsync(m => m.IdDokter == id);
            if (dokter == null)
            {
                return NotFound();
            }

            return View(dokter);
        }

        // GET: Dokters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dokters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama,Email,Password," +
            "Role,JenisKelamin,Alamat,Spesialis")] Dokter dokter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dokter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dokter);
        }

        // GET: Dokters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokter = await _context.Dokters.FindAsync(id);
            if (dokter == null)
            {
                return NotFound();
            }
            return View(dokter);
        }

        // POST: Dokters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDokter,Nama,Email,Password,Role,JenisKelamin,Alamat,Spesialis")] Dokter dokter)
        {
            if (id != dokter.IdDokter)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dokter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DokterExists(dokter.IdDokter))
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
            return View(dokter);
        }

        // GET: Dokters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokter = await _context.Dokters
                .FirstOrDefaultAsync(m => m.IdDokter == id);
            if (dokter == null)
            {
                return NotFound();
            }

            return View(dokter);
        }

        // POST: Dokters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dokter = await _context.Dokters.FindAsync(id);
            if (dokter != null)
            {
                _context.Dokters.Remove(dokter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DokterExists(int id)
        {
            return _context.Dokters.Any(e => e.IdDokter == id);
        }
    }
}
