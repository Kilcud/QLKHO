using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QLKHO.Models;

[Table("NguoiDung")]
[Index("MaNV", Name = "UQ__NguoiDun__2725D70BC7CCB0F1", IsUnique = true)]
[Index("Username", Name = "UQ__NguoiDun__536C85E4BCD02918", IsUnique = true)]
public partial class NguoiDung
{
    [Key]
    public int IdNguoiDung { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    public int? MaNV { get; set; }

    [ForeignKey("MaNV")]
    [InverseProperty("NguoiDung")]
    public virtual NhanVien? MaNVNavigation { get; set; }

    [InverseProperty("IdNguoiDungNavigation")]
    public virtual ICollection<Nhom_NguoiDung> Nhom_NguoiDungs { get; set; } = new List<Nhom_NguoiDung>();

    [NotMapped]
    public List<int> ListIdNguoiDung { get; set; } = default!;
}
