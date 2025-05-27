using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[PrimaryKey("MaPX", "MaSP")]
[Table("ChiTietPhieuXuat")]
public partial class ChiTietPhieuXuat
{
    [Key]
    public int MaPX { get; set; }

    [Key]
    public int MaSP { get; set; }

    public int MaDVT { get; set; }

    public int SoLuongXuat { get; set; }

    [ForeignKey("MaDVT")]
    [InverseProperty("ChiTietPhieuXuats")]
    public virtual DonViTinh MaDVTNavigation { get; set; } = null!;

    [ForeignKey("MaPX")]
    [InverseProperty("ChiTietPhieuXuats")]
    public virtual PhieuXuat MaPXNavigation { get; set; } = null!;

    [ForeignKey("MaSP")]
    [InverseProperty("ChiTietPhieuXuats")]
    public virtual SanPham MaSPNavigation { get; set; } = null!;
}
