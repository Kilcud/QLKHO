using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("Nhom")]
public partial class Nhom
{
    [Key]
    public int IdNhom { get; set; }

    [StringLength(50)]
    public string TenNhom { get; set; } = null!;

    [InverseProperty("IdNhomNavigation")]
    public virtual ICollection<Nhom_ChucNang> Nhom_ChucNangs { get; set; } = new List<Nhom_ChucNang>();

    [InverseProperty("IdNhomNavigation")]
    public virtual ICollection<Nhom_NguoiDung> Nhom_NguoiDungs { get; set; } = new List<Nhom_NguoiDung>();
}
