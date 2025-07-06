using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<IdentityUser>

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Dokter> Dokters { get; set; }
    public DbSet<Pasien> Pasiens { get; set; }
    public DbSet<Jadwal> Jadwals { get; set; }
    public DbSet<Reservasi> Reservasis { get; set; }
    public DbSet<RekamMedis> RekamMedis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Config Relasi: Pasien-Reservasi
        modelBuilder.Entity<Reservasi>()
            .HasOne(r => r.Pasien)
            .WithMany(p => p.Reservasis)
            .HasForeignKey(r => r.IdPasien)
            .OnDelete(DeleteBehavior.Restrict);

        // Config Relasi: Jadwal-Reservasi
        modelBuilder.Entity<Reservasi>()
            .HasOne(r => r.Jadwal)
            .WithOne(j => j.Reservasi)
            .HasForeignKey<Reservasi>(r => r.IdJadwal)
            .OnDelete(DeleteBehavior.Restrict);

        // Config Relasi: Dokter-Jadwal
        modelBuilder.Entity<Jadwal>()
            .HasOne(j => j.Dokter)
            .WithMany(d => d.Jadwals)
            .HasForeignKey(j => j.IdDokter)
            .OnDelete(DeleteBehavior.Restrict);

        // Config Relasi: Pasien-RekamMedis
        modelBuilder.Entity<RekamMedis>()
            .HasOne(rm => rm.Pasien)
            .WithMany(p => p.RekamMedis)
            .HasForeignKey(rm => rm.IdPasien)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
