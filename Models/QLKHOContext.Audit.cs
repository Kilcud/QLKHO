using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;
using System.Text.Json;

namespace QLKHO.Models
{
    public partial class QLKHOContext
    {
        /* ctor duy nhất – DI chọn */
        [ActivatorUtilitiesConstructor]
        public QLKHOContext(DbContextOptions<QLKHOContext> opt) : base(opt) { }

        /* -------- override SaveChanges -------- */
        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            (int? maNV, string? tenNV) = await GetCurrentNhanVienAsync();

            var audits = BeforeSaveChanges(maNV, tenNV);
            var result = await base.SaveChangesAsync(ct);

            if (audits.Count > 0)
                await AfterSaveChangesAsync(audits, ct);

            return result;
        }

        /* -------- Lấy Id & tên nhân viên hiện hành -------- */
        private async Task<(int? maNV, string? tenNV)> GetCurrentNhanVienAsync()
        {
            var authProv = this.GetService<AuthenticationStateProvider>();
            if (authProv == null) return (null, null);

            var user = (await authProv.GetAuthenticationStateAsync()).User;
            if (!int.TryParse(user.FindFirstValue("IdNhanVien"), out var idNV))
                return (null, null);

            string? tenNV = await NhanViens                // DbSet<NhanVien>
                                .AsNoTracking()
                                .Where(x => x.MaNV == idNV)
                                .Select(x => x.TenNV)
                                .FirstOrDefaultAsync();

            return (idNV, tenNV);
        }

        /* -------- gom thay đổi trước khi Save -------- */
        private List<AuditEntry> BeforeSaveChanges(int? maNV, string? tenNV)
        {
            ChangeTracker.DetectChanges();
            var list = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog
                    || entry.State is EntityState.Detached
                    || entry.State is EntityState.Unchanged)
                    continue;

                var ae = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName() ?? "",
                    UserId    = maNV,
                    TenNV  = tenNV
                };

                foreach (var p in entry.Properties)
                {
                    string col = p.Metadata.Name;

                    if (p.IsTemporary) { ae.TempProps.Add(p); continue; }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            ae.NewValues[col] = p.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            ae.OldValues[col] = p.OriginalValue;
                            break;
                        case EntityState.Modified when p.IsModified:
                            ae.OldValues[col] = p.OriginalValue;
                            ae.NewValues[col] = p.CurrentValue;
                            break;
                    }
                }

                ae.Action = entry.State switch
                {
                    EntityState.Added    => "Insert",
                    EntityState.Deleted  => "Delete",
                    EntityState.Modified => "Update",
                    _                    => ""
                };

                list.Add(ae);
            }
            return list;
        }

        /* -------- ghi log sau khi SaveChanges gốc thành công -------- */
        private async Task AfterSaveChangesAsync(List<AuditEntry> audits, CancellationToken ct)
        {
            foreach (var ae in audits)
            {
                foreach (var p in ae.TempProps)
                {
                    if (p.Metadata.IsPrimaryKey())
                        ae.KeyValues[p.Metadata.Name] = p.CurrentValue;
                    else
                        ae.NewValues[p.Metadata.Name] = p.CurrentValue;
                }
                AuditLogs.Add(ae.ToAuditLog());
            }
            await base.SaveChangesAsync(ct);
        }
    }

    /* ---------- lớp tạm ---------- */
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
