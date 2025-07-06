using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // PENTING: agar Include() bisa dipakai
using Puskemas.Data;
using Puskemas.Models;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult DashboardAdmin()
    {
        var viewModel = new DashboardAdminModel
        {
            JumlahPasien = _context.Pasiens.Count(),
            JumlahDokter = _context.Dokters.Count(),
            JumlahRekamMedis = _context.RekamMedis.Count(),
            JumlahReservasi = _context.Reservasis.Count(),

            ReservasiTerbaru = _context.Reservasis
                .Include(r => r.Pasien)
                .Include(r => r.Jadwal)
                    .ThenInclude(j => j.Dokter)
                .OrderByDescending(r => r.IdReservasi)
                .Take(5)
                .ToList(),

            PasienBaru = _context.Pasiens
                .OrderByDescending(p => p.TanggalTerdaftar)
                .Take(5)
                .ToList()
        };

        return View(viewModel);
    }
}
