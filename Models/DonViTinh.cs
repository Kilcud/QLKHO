using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("DonViTinh")]
[Index("TenDVT_Norm", Name = "UX_DonViTinh_TenDVT_Norm", IsUnique = true)]
public partial class DonViTinh
{
    [Key]
    public int MaDVT { get; set; }

    [StringLength(10)]
    public string TenDVT { get; set; } = null!;

    [StringLength(10)]
    public string? TenDVT_Norm { get; set; }

    public int STT { get; set; }

    [InverseProperty("MaDVTNavigation")]
    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    [InverseProperty("MaDVTNavigation")]
    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    [InverseProperty("MaDVTNavigation")]
    public virtual ICollection<DVT_SanPham> DVT_SanPhams { get; set; } = new List<DVT_SanPham>();
}
