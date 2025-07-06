using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puskemas.Models; // Sesuaikan dengan namespace model Dokter kamu
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Puskemas.Controllers
{
    [Authorize(Roles = "Admin")] // ⬅️ Optional: batasi hanya admin yang boleh kelola dokter
    public class DoktersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DoktersController(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Dokters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dokters.ToListAsync());
        }

        // GET: Dokters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var dokter = await _context.Dokters
                .FirstOrDefaultAsync(m => m.IdDokter == id);
            if (dokter == null) return NotFound();

            return View(dokter);
        }

        // GET: Dokters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dokters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama,Email,Password,Role,JenisKelamin,Alamat,Spesialis")] Dokter dokter)
        {
            if (ModelState.IsValid)
            {
                // 1. Simpan data dokter ke DB
                _context.Add(dokter);
                await _context.SaveChangesAsync();

                // 2. Cek apakah user dengan email ini sudah ada di Identity
                var existingUser = await _userManager.FindByEmailAsync(dokter.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email ini sudah digunakan.");
                    return View(dokter);
                }

                // 3. Buat akun login
                var user = new IdentityUser
                {
                    UserName = dokter.Email,
                    Email = dokter.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, dokter.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Gagal membuat akun login: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    return View(dokter);
                }

                // 4. Pastikan role "Dokter" sudah tersedia
                if (!await _roleManager.RoleExistsAsync("Dokter"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Dokter"));
                }

                // 5. Tambahkan role ke user
                await _userManager.AddToRoleAsync(user, "Dokter");

                return RedirectToAction(nameof(Index));
            }

            return View(dokter);
        }

        // GET: Dokters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dokter = await _context.Dokters.FindAsync(id);
            if (dokter == null) return NotFound();

            return View(dokter);
        }

        // POST: Dokters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDokter,Nama,Email,Password,Role,JenisKelamin,Alamat,Spesialis")] Dokter dokter)
        {
            if (id != dokter.IdDokter) return NotFound();

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
            if (id == null) return NotFound();

            var dokter = await _context.Dokters
                .FirstOrDefaultAsync(m => m.IdDokter == id);
            if (dokter == null) return NotFound();

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
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DokterExists(int id)
        {
            return _context.Dokters.Any(e => e.IdDokter == id);
        }
    }
}
