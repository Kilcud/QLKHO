using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[PrimaryKey("MaSP", "MaDVT")]
[Table("DVT_SanPham")]
public partial class DVT_SanPham
{
    [Key]
    public int MaSP { get; set; }

    [Key]
    public int MaDVT { get; set; }

    public int HeSoQuyDoi { get; set; }

    public bool LaMacDinh { get; set; }

    [ForeignKey("MaDVT")]
    [InverseProperty("DVT_SanPhams")]
    public virtual DonViTinh MaDVTNavigation { get; set; } = null!;

    [ForeignKey("MaSP")]
    [InverseProperty("DVT_SanPhams")]
    public virtual SanPham MaSPNavigation { get; set; } = null!;
}
