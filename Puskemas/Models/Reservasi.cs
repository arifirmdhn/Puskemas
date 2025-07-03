using System.ComponentModel.DataAnnotations;

public class Reservasi
{
    [Key]
    public int IdReservasi { get; set; }
    public int IdPasien { get; set; }
    public int IdJadwal { get; set; }
    public string StatusReservasi { get; set; }

    public Pasien Pasien { get; set; }
    public Jadwal Jadwal { get; set; }
}
