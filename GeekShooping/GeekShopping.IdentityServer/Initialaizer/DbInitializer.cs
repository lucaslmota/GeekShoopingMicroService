using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initialaizer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly SQLServerContext _sQLServerContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(SQLServerContext sQLServerContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _sQLServerContext = sQLServerContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initializer()
        {
            if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "lucas-admin",
                Email = "lucaslmota@lucas.com.br",
                EmailConfirmed = true,
                PhoneNumber = "88999222222",
                FirstName = "Lucas",
                LastName = "Admin"

            };

            _userManager.CreateAsync(admin,"Lucas123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "lucas-cliente",
                Email = "lucaslmota@lucas.com.br",
                EmailConfirmed = true,
                PhoneNumber = "88999222222",
                FirstName = "Lucas",
                LastName = "Client"
            };

            _userManager.CreateAsync(client, "Lucas123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(client,
                IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;

        }
    }
}
