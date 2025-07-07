using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puskemas.Data;
using Puskemas.Models;
using System.Threading.Tasks;

namespace Puskemas.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PasiensController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PasiensController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pasiens
        public async Task<IActionResult> Index()
        {
            var data = await _context.Pasiens.ToListAsync();
            return View(data);
        }

        // GET: Pasiens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pasien = await _context.Pasiens.FirstOrDefaultAsync(m => m.IdPasien == id);
            if (pasien == null) return NotFound();

            return View(pasien);
        }

        // GET: Pasiens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pasiens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama,Alamat,TanggalLahir,NomorHp,StatusPernikahan,NomorKeluarga,TanggalTerdaftar,PasswordPasien")] Pasien pasien)
        {
            if (ModelState.IsValid)
            {
                // 1. Simpan ke tabel pasien
                _context.Add(pasien);
                await _context.SaveChangesAsync();

                // 2. Buat akun Identity
                var generatedEmail = pasien.Nama.Replace(" ", "").ToLower() + "@pasien.com";
                var user = new IdentityUser
                {
                    UserName = pasien.Nama,
                    Email = generatedEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, pasien.PasswordPasien);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Pasien");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(pasien);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(pasien);
        }


        // GET: Pasiens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pasien = await _context.Pasiens.FindAsync(id);
            if (pasien == null) return NotFound();

            return View(pasien);
        }

        // POST: Pasiens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPasien,Nama,Alamat,TanggalLahir,NomorHp,StatusPernikahan,NomorKeluarga,TanggalTerdaftar,PasswordPasien")] Pasien pasien)
        {
            if (id != pasien.IdPasien) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasienExists(pasien.IdPasien)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pasien);
        }

        // GET: Pasiens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pasien = await _context.Pasiens.FirstOrDefaultAsync(m => m.IdPasien == id);
            if (pasien == null) return NotFound();

            return View(pasien);
        }

        // POST: Pasiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasien = await _context.Pasiens.FindAsync(id);
            if (pasien != null)
            {
                _context.Pasiens.Remove(pasien);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PasienExists(int id)
        {
            return _context.Pasiens.Any(e => e.IdPasien == id);
        }
    }
}
