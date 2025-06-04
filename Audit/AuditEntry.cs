using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Text.Json;
using QLKHO.Models;

namespace QLKHO.Infrastructure.Audit
{
    /// <summary>
    /// Lớp tạm giữ dữ liệu thay đổi – KHÔNG map DB.
    /// </summary>
    internal sealed class AuditEntry
    {
        public AuditEntry(EntityEntry entry) => Entry = entry;

        public EntityEntry Entry { get; }
        public string TableName = "";
        public Dictionary<string, object?> KeyValues = new();
        public Dictionary<string, object?> OldValues = new();
        public Dictionary<string, object?> NewValues = new();
        public List<PropertyEntry> TempProps = new();
        public string Action = "";
        public int?    UserId;
        public string? TenNV;

        public AuditLog ToAuditLog() => new()
        {
            TableName = TableName,
            KeyValues = JsonSerializer.Serialize(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
            Action    = Action,
            UserId    = UserId,
            TenNV  = TenNV,
            TimeStamp = DateTime.UtcNow
        };
    }
}
