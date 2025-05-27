using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[PrimaryKey("IdNguoiDung", "IdNhom")]
[Table("Nhom_NguoiDung")]
public partial class Nhom_NguoiDung
{
    [Key]
    public int IdNguoiDung { get; set; }

    [Key]
    public int IdNhom { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [ForeignKey("IdNguoiDung")]
    [InverseProperty("Nhom_NguoiDungs")]
    public virtual NguoiDung IdNguoiDungNavigation { get; set; } = null!;

    [ForeignKey("IdNhom")]
    [InverseProperty("Nhom_NguoiDungs")]
    public virtual Nhom IdNhomNavigation { get; set; } = null!;
}
