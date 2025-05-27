using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("NhaCungCap")]
[Index("TenNCC_Norm", Name = "UX_NhaCungCap_TenNCC_Norm", IsUnique = true)]
public partial class NhaCungCap
{
    [Key]
    public int MaNCC { get; set; }

    [StringLength(20)]
    public string TenNCC { get; set; } = null!;

    [StringLength(20)]
    public string? TenNCC_Norm { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string SDTNCC { get; set; } = null!;

    [StringLength(40)]
    public string DiaChiNCC { get; set; } = null!;

    [InverseProperty("MaNCCNavigation")]
    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();
}
