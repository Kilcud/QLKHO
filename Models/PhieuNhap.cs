using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("PhieuNhap")]
public partial class PhieuNhap
{
    [Key]
    public int MaPN { get; set; }

    public int MaKho { get; set; }

    public int MaNV { get; set; }

    public int MaNCC { get; set; }

    [Column(TypeName = "money")]
    public decimal GiaNhap { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime NgayNhap { get; set; }

    [InverseProperty("MaPNNavigation")]
    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    [ForeignKey("MaKho")]
    [InverseProperty("PhieuNhaps")]
    public virtual Kho MaKhoNavigation { get; set; } = null!;

    [ForeignKey("MaNCC")]
    [InverseProperty("PhieuNhaps")]
    public virtual NhaCungCap MaNCCNavigation { get; set; } = null!;

    [ForeignKey("MaNV")]
    [InverseProperty("PhieuNhaps")]
    public virtual NhanVien MaNVNavigation { get; set; } = null!;
}
