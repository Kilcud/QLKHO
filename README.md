`# Get Database

Scaffold-DbContext "Data Source=localhost; Initial Catalogs=QLKHO; User ID=sa;Password=Your_password123!" Microsoft.EntityFrameworkCore.SqlServer -OutputDir tmp -DataAnnotations -f -UseDatabaseNames

dotnet ef dbcontext scaffold "data source=localhost;initial catalog=QLKHO;user id=sa;password=Your_password123!;trustservercertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Data --context ApplicationDbContext --force

HAY DÙNG LỆNH NÀY
dotnet ef dbcontext scaffold "data source=localhost;initial catalog=QLKHO;user id=sa;password=Your_password123!;trustservercertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models --use-database-names --data-annotations --force

dotnet ef dbcontext scaffold \
 "Server=localhost;Database=QLKHO;User Id=sa;Password=Your_password123!;TrustServerCertificate=True" \
 Microsoft.EntityFrameworkCore.SqlServer \
 -o Models --use-database-names --data-annotations --force \
 --table Nhom_NguoiDung \
 --table Nhom_ChucNang


protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
if (!optionsBuilder.IsConfigured)
{
IConfigurationRoot configuration = new ConfigurationBuilder()
.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
.AddJsonFile("appsettings.json")
.Build();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBConnection"));
}
}
