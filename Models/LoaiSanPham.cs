using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("LoaiSanPham")]
[Index("TenLSP_Norm", Name = "UX_LoaiSanPham_TenLSP_Norm", IsUnique = true)]
public partial class LoaiSanPham
{
    [Key]
    public int MaLSP { get; set; }

    [StringLength(30)]
    public string TenLSP { get; set; } = null!;

    [StringLength(30)]
    public string? TenLSP_Norm { get; set; }

    public int STT { get; set; }

    [InverseProperty("MaLSPNavigation")]
    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
