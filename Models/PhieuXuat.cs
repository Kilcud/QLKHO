using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("PhieuXuat")]
public partial class PhieuXuat
{
    [Key]
    public int MaPX { get; set; }

    public int MaKho { get; set; }

    public int MaNV { get; set; }

    public int MaCH { get; set; }

    [Column(TypeName = "money")]
    public decimal GiaXuat { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime NgayXuat { get; set; }

    [InverseProperty("MaPXNavigation")]
    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    [ForeignKey("MaCH")]
    [InverseProperty("PhieuXuats")]
    public virtual CuaHang MaCHNavigation { get; set; } = null!;

    [ForeignKey("MaKho")]
    [InverseProperty("PhieuXuats")]
    public virtual Kho MaKhoNavigation { get; set; } = null!;

    [ForeignKey("MaNV")]
    [InverseProperty("PhieuXuats")]
    public virtual NhanVien MaNVNavigation { get; set; } = null!;
}
