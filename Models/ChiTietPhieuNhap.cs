using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[PrimaryKey("MaPN", "MaSP")]
[Table("ChiTietPhieuNhap")]
public partial class ChiTietPhieuNhap
{
    [Key]
    public int MaPN { get; set; }

    [Key]
    public int MaSP { get; set; }

    public int MaDVT { get; set; }

    public int SoLuongNhap { get; set; }

    [ForeignKey("MaDVT")]
    [InverseProperty("ChiTietPhieuNhaps")]
    public virtual DonViTinh MaDVTNavigation { get; set; } = null!;

    [ForeignKey("MaPN")]
    [InverseProperty("ChiTietPhieuNhaps")]
    public virtual PhieuNhap MaPNNavigation { get; set; } = null!;

    [ForeignKey("MaSP")]
    [InverseProperty("ChiTietPhieuNhaps")]
    public virtual SanPham MaSPNavigation { get; set; } = null!;
}
