using System;
using System.ComponentModel.DataAnnotations;

public class RekamMedis
{
    [Key]
    public int IdRekam { get; set; }
    public int IdPasien { get; set; }
    public DateTime TanggalCheckup { get; set; }
    public string Diagnosa { get; set; }
    public string Pengobatan { get; set; }
    public string Obat { get; set; }

    public Pasien Pasien { get; set; }
}
