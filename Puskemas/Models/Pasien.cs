using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Pasien
{
    [Key]
    public int IdPasien { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public DateTime TanggalLahir { get; set; }
    public string NomorHp { get; set; }

    public ICollection<Reservasi> Reservasis { get; set; }
    public ICollection<RekamMedis> RekamMedis { get; set; }
}
