using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Jadwal
{
    [Key]
    public int IdJadwal { get; set; }

    [Required(ErrorMessage = "Nama Dokter wajib diisi")]
    public int IdDokter { get; set; }

    [Required(ErrorMessage = "Hari wajib dipilih")]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Hari hanya boleh berisi huruf")]
    public string Hari { get; set; }

    [Required(ErrorMessage = "Jam Mulai wajib diisi")]
    public TimeSpan JamMulai { get; set; }

    [Required(ErrorMessage = "Jam Selesai wajib diisi")]
    public TimeSpan JamSelesai { get; set; }

    [Required(ErrorMessage = "Tanggal wajib diisi")]
    public DateTime Tanggal { get; set; }

    [Required(ErrorMessage = "Status Jadwal wajib diisi")]
    public string StatusJadwal { get; set; }

    public Dokter? Dokter { get; set; }
    public ICollection<Reservasi>? Reservasis { get; set; }
}
