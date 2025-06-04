using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace QLKHO.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ProtectedLocalStorage _localStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, ProtectedLocalStorage localStorage)
        {
            _sessionStorage = sessionStorage;
            _localStorage = localStorage;
        }

        public async Task SignInAsync(IEnumerable<Claim> claims, bool rememberMe)
        {
            var identity  = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);

            string userId     = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;      
            string idNhanVien = claims.FirstOrDefault(c => c.Type == "IdNhanVien")?.Value ?? "0";

            if (rememberMe)
            {
                await _localStorage.SetAsync ("userId",     userId);
                await _localStorage.SetAsync("IdNhanVien",   idNhanVien);
                await _localStorage.SetAsync ("dateExpire", DateTime.Now.AddDays(7));
            }
            else
            {
                await _sessionStorage.SetAsync("userId",     userId);
                await _localStorage.SetAsync("IdNhanVien",   idNhanVien);
                await _sessionStorage.SetAsync("dateExpire", DateTime.Now.AddHours(3));
            }

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(principal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // --- LẤY userId ---
                var userIdSessionStorageResult = await _sessionStorage.GetAsync<string>("userId");
                var userIdLocalStorageResult = await _localStorage.GetAsync<string>("userId");
                string? userIdSession = userIdSessionStorageResult.Success ? userIdSessionStorageResult.Value : null;
                string? userIdLocal = userIdLocalStorageResult.Success ? userIdLocalStorageResult.Value : null;
                string? userId = userIdSession == null ? userIdLocal : userIdSession;

                // --- LẤY IdNhanVien ---
                var nvSessionResult   = await _sessionStorage.GetAsync<string>("IdNhanVien");
                var nvLocalResult     = await _localStorage.GetAsync<string>("IdNhanVien");
                string? nvSession     = nvSessionResult.Success ? nvSessionResult.Value : null;
                string? nvLocal       = nvLocalResult.Success   ? nvLocalResult.Value   : null;
                string? idNhanVienStr = nvSession ?? nvLocal;
                int     maNV          = 0;
                if (!string.IsNullOrWhiteSpace(idNhanVienStr))
                    int.TryParse(idNhanVienStr, out maNV);

                // --- LẤY dateExpire ---
                var dateExpireSessionStorageResult = await _sessionStorage.GetAsync<DateTime>("dateExpire");
                var dateExpireLocalStorageResult = await _localStorage.GetAsync<DateTime>("dateExpire");
                DateTime? dateExpireSession = dateExpireSessionStorageResult.Success ? dateExpireSessionStorageResult.Value : null;
                DateTime? dateExpireLocal = dateExpireLocalStorageResult.Success ? dateExpireLocalStorageResult.Value : null;
                DateTime? dateExpire = dateExpireSession == null ? dateExpireLocal : dateExpireSession;

                // Nếu không có userId hoặc dateExpire => ẩn danh
                if (userId == null || dateExpire == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                // Nếu token đã hết hạn
                if (dateExpire.Value < DateTime.Now)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                // --- TẠO CLAIMS từ hai giá trị đã lưu ---
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim("IdNhanVien", maNV.ToString())
                };

                // Cuối cùng tạo Identity + Principal
                var identity       = new ClaimsIdentity(claims, "CustomAuth");
                var principal      = new ClaimsPrincipal(identity);
                return new AuthenticationState(principal);

                // var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                // {
                //     new Claim(ClaimTypes.Name, userId ?? ""),

                // }, "CustomAuth"));
                // return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

        }

        public async Task<string> getTypeStorage()
        {
            try
            {
                var userIdSessionStorageResult = await _sessionStorage.GetAsync<string>("userId");
                var userIdLocalStorageResult = await _localStorage.GetAsync<string>("userId");
                string? userIdSession = userIdSessionStorageResult.Success ? userIdSessionStorageResult.Value : null;
                string? userIdLocal = userIdLocalStorageResult.Success ? userIdLocalStorageResult.Value : null;

                return userIdSession == null ? "local" : "session";

            }
            catch
            {
                return "";
            }
        }
        public async Task UpdateAuthenticationState(string? userId, string typeStorage = "session", bool addNew = false)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userId != null && typeStorage != "")
            {
                if (typeStorage == "session")
                {
                    await _sessionStorage.SetAsync("userId", userId);
                    if (!addNew)
                    {
                        await _sessionStorage.SetAsync("dateExpire", DateTime.Now.AddHours(3));
                    }
                }
                if (typeStorage == "local")
                {
                    await _localStorage.SetAsync("userId", userId);
                    if (!addNew)
                    {
                        await _localStorage.SetAsync("dateExpire", DateTime.Now.AddDays(7));
                    }

                }
                // claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                // {
                //     new Claim(ClaimTypes.Name, userId),
                // }));
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userId ?? ""),

                }, "CustomAuth"));
                
            }
            else
            {
                await _sessionStorage.DeleteAsync("userId");
                await _localStorage.DeleteAsync("userId");

                await _sessionStorage.DeleteAsync("dateExpire");
                await _localStorage.DeleteAsync("dateExpire");

                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}