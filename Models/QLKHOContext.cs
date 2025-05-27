using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

public partial class QLKHOContext : DbContext
{
    public QLKHOContext()
    {
    }

    public QLKHOContext(DbContextOptions<QLKHOContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    public virtual DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }

    public virtual DbSet<ChucNang> ChucNangs { get; set; }

    public virtual DbSet<CuaHang> CuaHangs { get; set; }

    public virtual DbSet<DVT_SanPham> DVT_SanPhams { get; set; }

    public virtual DbSet<DonViTinh> DonViTinhs { get; set; }

    public virtual DbSet<Kho> Khos { get; set; }

    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Nhom> Nhoms { get; set; }

    public virtual DbSet<Nhom_ChucNang> Nhom_ChucNangs { get; set; }

    public virtual DbSet<Nhom_NguoiDung> Nhom_NguoiDungs { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
        {
            entity.HasKey(e => new { e.MaPN, e.MaSP }).HasName("PK_CTPN");

            entity.HasOne(d => d.MaDVTNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPN_DVT");

            entity.HasOne(d => d.MaPNNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPN_PN");

            entity.HasOne(d => d.MaSPNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPN_SP");
        });

        modelBuilder.Entity<ChiTietPhieuXuat>(entity =>
        {
            entity.HasKey(e => new { e.MaPX, e.MaSP }).HasName("PK_CTPX");

            entity.HasOne(d => d.MaDVTNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPX_DVT");

            entity.HasOne(d => d.MaPXNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPX_PX");

            entity.HasOne(d => d.MaSPNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPX_SP");
        });

        modelBuilder.Entity<ChucNang>(entity =>
        {
            entity.HasKey(e => e.IdChucNang).HasName("PK__ChucNang__43B2811B711546B5");

            entity.Property(e => e.STT).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.IdChucNangChaNavigation).WithMany(p => p.InverseIdChucNangChaNavigation).HasConstraintName("FK__ChucNang__IdChuc__693CA210");
        });

        modelBuilder.Entity<CuaHang>(entity =>
        {
            entity.Property(e => e.SDTCH).IsFixedLength();
            entity.Property(e => e.TenCH_Norm).HasComputedColumnSql("(lower(Trim([TenCH])))", true);
        });

        modelBuilder.Entity<DVT_SanPham>(entity =>
        {
            entity.HasOne(d => d.MaDVTNavigation).WithMany(p => p.DVT_SanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DVTSanPham_DVT");

            entity.HasOne(d => d.MaSPNavigation).WithMany(p => p.DVT_SanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DVTSanPham_SanPham");
        });

        modelBuilder.Entity<DonViTinh>(entity =>
        {
            entity.Property(e => e.TenDVT_Norm).HasComputedColumnSql("(lower(Trim([TenDVT])))", true);
        });

        modelBuilder.Entity<Kho>(entity =>
        {
            entity.Property(e => e.SDTKho).IsFixedLength();
            entity.Property(e => e.TenKho_Norm).HasComputedColumnSql("(lower(Trim([TenKho])))", true);

            entity.HasOne(d => d.TrgKhoNavigation).WithMany(p => p.Khos).HasConstraintName("FK_Kho_TrgKho");
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.Property(e => e.TenLSP_Norm).HasComputedColumnSql("(lower(Trim([TenLSP])))", true);
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.IdNguoiDung).HasName("PK__NguoiDun__FEE82D404D2134EE");

            entity.HasOne(d => d.MaNVNavigation).WithOne(p => p.NguoiDung).HasConstraintName("FK__NguoiDung__MaNV__656C112C");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.Property(e => e.SDTNCC).IsFixedLength();
            entity.Property(e => e.TenNCC_Norm).HasComputedColumnSql("(lower(Trim([TenNCC])))", true);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.Property(e => e.SDTNV).IsFixedLength();

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.NhanViens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanVien_Kho");
        });

        modelBuilder.Entity<Nhom>(entity =>
        {
            entity.HasKey(e => e.IdNhom).HasName("PK__Nhom__4099C03552CC7FD3");
        });

        modelBuilder.Entity<Nhom_ChucNang>(entity =>
        {
            entity.HasKey(e => new { e.IdNhom, e.IdChucNang }).HasName("PK__Nhom_Chu__E4A2E8246A2FCB24");

            entity.HasOne(d => d.IdChucNangNavigation).WithMany(p => p.Nhom_ChucNangs).HasConstraintName("FK__Nhom_Chuc__IdChu__778AC167");

            entity.HasOne(d => d.IdNhomNavigation).WithMany(p => p.Nhom_ChucNangs).HasConstraintName("FK__Nhom_Chuc__IdNho__76969D2E");
        });

        modelBuilder.Entity<Nhom_NguoiDung>(entity =>
        {
            entity.HasKey(e => new { e.IdNguoiDung, e.IdNhom }).HasName("PK__Nhom_Ngu__BAE1B143533D03E1");

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdNguoiDungNavigation).WithMany(p => p.Nhom_NguoiDungs).HasConstraintName("FK__Nhom_Nguo__IdNgu__6EF57B66");

            entity.HasOne(d => d.IdNhomNavigation).WithMany(p => p.Nhom_NguoiDungs).HasConstraintName("FK__Nhom_Nguo__IdNho__6FE99F9F");
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.PhieuNhaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PN_Kho");

            entity.HasOne(d => d.MaNCCNavigation).WithMany(p => p.PhieuNhaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PN_NCC");

            entity.HasOne(d => d.MaNVNavigation).WithMany(p => p.PhieuNhaps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PN_NV");
        });

        modelBuilder.Entity<PhieuXuat>(entity =>
        {
            entity.HasOne(d => d.MaCHNavigation).WithMany(p => p.PhieuXuats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PX_CH");

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.PhieuXuats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PX_Kho");

            entity.HasOne(d => d.MaNVNavigation).WithMany(p => p.PhieuXuats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PX_NV");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.Property(e => e.TenSP_Norm).HasComputedColumnSql("(lower(Trim([TenSP])))", true);

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.SanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_Kho");

            entity.HasOne(d => d.MaLSPNavigation).WithMany(p => p.SanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_Loai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
