using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("Kho")]
[Index("TenKho_Norm", Name = "UX_Kho_TenKho_Norm", IsUnique = true)]
public partial class Kho
{
    [Key]
    public int MaKho { get; set; }

    [StringLength(10)]
    public string TenKho { get; set; } = null!;

    [StringLength(10)]
    public string? TenKho_Norm { get; set; }

    public int? TrgKho { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string SDTKho { get; set; } = null!;

    [StringLength(50)]
    public string DiaChiKho { get; set; } = null!;

    [InverseProperty("MaKhoNavigation")]
    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();

    [InverseProperty("MaKhoNavigation")]
    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    [InverseProperty("MaKhoNavigation")]
    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();

    [InverseProperty("MaKhoNavigation")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();

    [ForeignKey("TrgKho")]
    [InverseProperty("Khos")]
    public virtual NhanVien? TrgKhoNavigation { get; set; }
}
