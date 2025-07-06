using System;
using System.ComponentModel.DataAnnotations;

public class Reservasi
{
    [Key]
    public int IdReservasi { get; set; }

    [Required(ErrorMessage = "Pasien harus dipilih")]
    public int IdPasien { get; set; }

    [Required(ErrorMessage = "Jadwal harus dipilih")]
    public int IdJadwal { get; set; }

    [Required(ErrorMessage = "Status reservasi harus diisi")]
    [StringLength(50, ErrorMessage = "Status reservasi maksimal 50 karakter")]
    public string StatusReservasi { get; set; }

    // Boleh kosong, gunakan TimeSpan? (nullable)
    [Required(ErrorMessage = "JamSelesai harus diisi")]
    public TimeSpan JamSelesai { get; set; }

    [Required(ErrorMessage = "Nomor antrian harus diisi")]
    public int NomorAntrian { get; set; }

    public Pasien? Pasien { get; set; }
    public Jadwal? Jadwal { get; set; }
}
