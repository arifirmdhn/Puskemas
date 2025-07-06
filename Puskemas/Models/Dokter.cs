using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Dokter
{
    [Key]
    public int IdDokter { get; set; }

    [Required(ErrorMessage = "Nama wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter")]
    public string Nama { get; set; }

    [Required(ErrorMessage = "Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email tidak valid")]
    [StringLength(100, ErrorMessage = "Email maksimal 100 karakter")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password wajib diisi")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password minimal 6 karakter")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Role wajib dipilih")]
    [RegularExpression("^(Admin|Dokter)$", ErrorMessage = "Role hanya boleh Admin atau Dokter")]
    public string Role { get; set; }

    [Required(ErrorMessage = "Jenis Kelamin wajib dipilih")]
    [RegularExpression("^(Laki-Laki|Perempuan)$", ErrorMessage = "Jenis Kelamin hanya boleh Laki-Laki atau Perempuan")]
    public string JenisKelamin { get; set; }

    [Required(ErrorMessage = "Alamat wajib diisi")]
    [StringLength(250, ErrorMessage = "Alamat maksimal 250 karakter")]
    public string Alamat { get; set; }

    [Required(ErrorMessage = "Spesialis wajib dipilih")]
    [StringLength(50, ErrorMessage = "Spesialis maksimal 50 karakter")]
    public string Spesialis { get; set; }

    public ICollection<Jadwal>? Jadwals { get; set; }
}
