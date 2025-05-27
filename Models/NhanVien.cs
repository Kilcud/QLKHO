using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("NhanVien")]
public partial class NhanVien
{
    [Key]
    public int MaNV { get; set; }

    public int MaKho { get; set; }

    [StringLength(40)]
    public string TenNV { get; set; } = null!;

    public bool GioiTinh { get; set; }

    [StringLength(40)]
    public string DiaChiNV { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string SDTNV { get; set; } = null!;

    [InverseProperty("TrgKhoNavigation")]
    public virtual ICollection<Kho> Khos { get; set; } = new List<Kho>();

    [ForeignKey("MaKho")]
    [InverseProperty("NhanViens")]
    public virtual Kho MaKhoNavigation { get; set; } = null!;

    [InverseProperty("MaNVNavigation")]
    public virtual NguoiDung? NguoiDung { get; set; }

    [InverseProperty("MaNVNavigation")]
    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    [InverseProperty("MaNVNavigation")]
    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
}
