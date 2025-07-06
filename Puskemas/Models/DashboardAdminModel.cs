namespace Puskemas.Models
{
    public class DashboardAdminModel
    {
        public int JumlahPasien { get; set; }
        public int JumlahDokter { get; set; }
        public int JumlahRekamMedis { get; set; }
        public int JumlahReservasi { get; set; }

        public List<Reservasi> ReservasiTerbaru { get; set; } = new();
        public List<Pasien> PasienBaru { get; set; } = new();
    }
}
