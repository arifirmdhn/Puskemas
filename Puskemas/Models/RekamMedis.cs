using System;
using System.ComponentModel.DataAnnotations;

public class RekamMedis
{
    [Key]
    public int IdRekam { get; set; }

    [Required(ErrorMessage = "Pasien harus dipilih")]
    public int IdPasien { get; set; }

    [Required(ErrorMessage = "Tanggal checkup harus diisi")]
    [DataType(DataType.Date, ErrorMessage = "Format tanggal tidak valid")]
    public DateTime TanggalCheckup { get; set; }

    [Required(ErrorMessage = "Diagnosa harus diisi")]
    [StringLength(200, ErrorMessage = "Diagnosa maksimal 200 karakter")]
    public string Diagnosa { get; set; }

    [Required(ErrorMessage = "Pengobatan harus diisi")]
    [StringLength(500, ErrorMessage = "Pengobatan maksimal 500 karakter")]
    public string Pengobatan { get; set; }

    [Required(ErrorMessage = "Obat harus diisi")]
    [StringLength(300, ErrorMessage = "Obat maksimal 300 karakter")]
    public string Obat { get; set; }

    [Required(ErrorMessage = "Anamnesis harus diisi")]
    [StringLength(500, ErrorMessage = "Anamnesis maksimal 500 karakter")]
    public string Anamnesis { get; set; } // Tambahan

    [Required(ErrorMessage = "Pemeriksaan fisik harus diisi")]
    [StringLength(500, ErrorMessage = "Pemeriksaan fisik maksimal 500 karakter")]
    public string PemeriksaanFisik { get; set; } // Tambahan

    [Required(ErrorMessage = "Penatalaksanaan harus diisi")]
    [StringLength(200, ErrorMessage = "Penatalaksanaan maksimal 200 karakter")]
    public string Penatalaksanaan { get; set; } // Tambahan

    public Pasien? Pasien { get; set; }
}
