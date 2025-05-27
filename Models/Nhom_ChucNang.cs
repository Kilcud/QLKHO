using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[PrimaryKey("IdNhom", "IdChucNang")]
[Table("Nhom_ChucNang")]
public partial class Nhom_ChucNang
{
    [Key]
    public int IdNhom { get; set; }

    [Key]
    public int IdChucNang { get; set; }

    public bool AllowView { get; set; }

    public bool AllowAdd { get; set; }

    public bool AllowEdit { get; set; }

    public bool AllowDelete { get; set; }

    [ForeignKey("IdChucNang")]
    [InverseProperty("Nhom_ChucNangs")]
    public virtual ChucNang IdChucNangNavigation { get; set; } = null!;

    [ForeignKey("IdNhom")]
    [InverseProperty("Nhom_ChucNangs")]
    public virtual Nhom IdNhomNavigation { get; set; } = null!;
}
