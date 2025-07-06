using System.ComponentModel.DataAnnotations;

public class Reservasi
{
    [Key]
    public int IdReservasi { get; set; }
    public int IdPasien { get; set; }
    public int IdJadwal { get; set; }
    public string StatusReservasi { get; set; }
    public TimeSpan JamSelesai { get; set; } // Kolom baru

    public int NomorAntrian { get; set; } // Kolom baru

    public Pasien? Pasien { get; set; }
    public Jadwal? Jadwal { get; set; }
}
