using Microsoft.CodeAnalysis.Editing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Dokter
{
    [Key]
    public int IdDokter { get; set; }
    public string Nama { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string JenisKelamin { get; set; }
    public string Alamat { get; set; }
    public string Spesialis { get; set; }

    public ICollection<Jadwal>? Jadwals { get; set; }
}
