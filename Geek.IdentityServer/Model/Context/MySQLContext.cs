using Geek.IdentityServer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Geek.IdentityServer.Model.Context;

public class MySQLContext : IdentityDbContext<ApplicationUser>
{
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
    {

    }
}
