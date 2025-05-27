using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("CuaHang")]
[Index("TenCH_Norm", Name = "UX_CuaHang_TenCH_Norm", IsUnique = true)]
public partial class CuaHang
{
    [Key]
    public int MaCH { get; set; }

    [StringLength(20)]
    public string TenCH { get; set; } = null!;

    [StringLength(20)]
    public string? TenCH_Norm { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string SDTCH { get; set; } = null!;

    [StringLength(50)]
    public string DiaChiCH { get; set; } = null!;

    [InverseProperty("MaCHNavigation")]
    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
}
