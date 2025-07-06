using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Pasien")]
public class PasienController : Controller
{
    public IActionResult DashboardPasien()
    {
        ViewBag.Reservasi = new List<string> { "Reservasi A", "Reservasi B" };
        ViewBag.RekamMedis = new List<string> { "Data Medis 1", "Data Medis 2" };
        ViewBag.JadwalDokter = new List<string> { "Senin - 08.00", "Selasa - 10.00" };
        ViewBag.InformasiReservasi = new List<string> { "Detail 1", "Detail 2" };
        return View();
    }
}
