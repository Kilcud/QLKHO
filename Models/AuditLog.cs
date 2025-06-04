
namespace QLKHO.Models
{
    /// <summary>
    /// Bảng ghi lịch sử thêm / sửa / xoá – lưu JSON before/after.
    /// </summary>
    public class AuditLog
    {
        public int      Id         { get; set; }
        public string   TableName  { get; set; } = null!;
        public string?  KeyValues  { get; set; }     // JSON {MaPN:1}
        public string?  OldValues  { get; set; }     // JSON field:value
        public string?  NewValues  { get; set; }
        public string   Action     { get; set; } = null!;   // Insert | Update | Delete
        public int?     UserId     { get; set; }
        public string?  TenNV   { get; set; }
        public DateTime TimeStamp  { get; set; } = DateTime.UtcNow;
    }
}