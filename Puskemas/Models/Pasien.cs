using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Pasien
{
    [Key]
    public int IdPasien { get; set; }

    [Required(ErrorMessage = "Nama wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter")]
    public string Nama { get; set; }

    [Required(ErrorMessage = "Alamat wajib diisi")]
    [StringLength(250, ErrorMessage = "Alamat maksimal 250 karakter")]
    public string Alamat { get; set; }

    [Required(ErrorMessage = "Tanggal Lahir wajib diisi")]
    [DataType(DataType.Date, ErrorMessage = "Format tanggal tidak valid")]
    public DateTime TanggalLahir { get; set; }

    [Required(ErrorMessage = "Nomor HP wajib diisi")]
    [Phone(ErrorMessage = "Format nomor HP tidak valid")]
    [StringLength(20, ErrorMessage = "Nomor HP maksimal 20 digit")]
    public string NomorHp { get; set; }

    [Required(ErrorMessage = "Status Pernikahan wajib dipilih")]
    [RegularExpression("^(Belum Menikah|Sudah Menikah)$", ErrorMessage = "Status hanya Belum Menikah atau Sudah Menikah")]
    public string StatusPernikahan { get; set; }

    [Required(ErrorMessage = "Nomor Keluarga wajib diisi")]
    [StringLength(20, ErrorMessage = "Nomor Keluarga maksimal 20 karakter")]
    public string NomorKeluarga { get; set; }

    [Required(ErrorMessage = "Tanggal Terdaftar wajib diisi")]
    [DataType(DataType.Date, ErrorMessage = "Format tanggal tidak valid")]
    public DateTime TanggalTerdaftar { get; set; }

    [Required(ErrorMessage = "Password wajib diisi")]
    [StringLength(100, ErrorMessage = "Password maksimal 100 karakter", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string PasswordPasien { get; set; }

    public ICollection<Reservasi>? Reservasis { get; set; }
    public ICollection<RekamMedis>? RekamMedis { get; set; }
}
