using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("SanPham")]
[Index("TenSP_Norm", Name = "UX_SanPham_TenSP_Norm", IsUnique = true)]
public partial class SanPham
{
    [Key]
    public int MaSP { get; set; }

    public int MaKho { get; set; }

    public int MaLSP { get; set; }

    [StringLength(30)]
    public string TenSP { get; set; } = null!;

    [StringLength(30)]
    public string? TenSP_Norm { get; set; }

    [Column(TypeName = "money")]
    public decimal DonGiaNhap { get; set; }

    [Column(TypeName = "money")]
    public decimal DonGiaXuat { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime HSD { get; set; }

    [InverseProperty("MaSPNavigation")]
    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    [InverseProperty("MaSPNavigation")]
    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    [InverseProperty("MaSPNavigation")]
    public virtual ICollection<DVT_SanPham> DVT_SanPhams { get; set; } = new List<DVT_SanPham>();

    [ForeignKey("MaKho")]
    [InverseProperty("SanPhams")]
    public virtual Kho MaKhoNavigation { get; set; } = null!;

    [ForeignKey("MaLSP")]
    [InverseProperty("SanPhams")]
    public virtual LoaiSanPham MaLSPNavigation { get; set; } = null!;
}
