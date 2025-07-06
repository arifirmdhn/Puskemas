using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Jadwal
{
    [Key]
    public int IdJadwal { get; set; }
    public int IdDokter { get; set; }
    public string Hari { get; set; }
    public TimeSpan JamMulai { get; set; }
    public TimeSpan JamSelesai { get; set; }
    public DateTime Tanggal { get; set; }
    public string StatusJadwal { get; set; }

    public Dokter? Dokter { get; set; }
    public ICollection<Reservasi>? Reservasis { get; set; }
}
