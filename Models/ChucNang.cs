using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("ChucNang")]
public partial class ChucNang
{
    [Key]
    public int IdChucNang { get; set; }

    [StringLength(100)]
    public string TenChucNang { get; set; } = null!;

    [StringLength(200)]
    public string? DuongDan { get; set; }

    [StringLength(50)]
    public string? Icon { get; set; }

    public int STT { get; set; }

    public int? IdChucNangCha { get; set; }

    [ForeignKey("IdChucNangCha")]
    [InverseProperty("InverseIdChucNangChaNavigation")]
    public virtual ChucNang? IdChucNangChaNavigation { get; set; }

    [InverseProperty("IdChucNangChaNavigation")]
    public virtual ICollection<ChucNang> InverseIdChucNangChaNavigation { get; set; } = new List<ChucNang>();

    [InverseProperty("IdChucNangNavigation")]
    public virtual ICollection<Nhom_ChucNang> Nhom_ChucNangs { get; set; } = new List<Nhom_ChucNang>();
}
