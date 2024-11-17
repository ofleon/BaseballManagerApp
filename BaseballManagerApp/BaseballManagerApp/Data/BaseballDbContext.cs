using BaseballManagerApp.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseballManagerApp.Data
{
    public class BaseballDbContext : IdentityDbContext<ApplicationUser>
    {
        public BaseballDbContext(DbContextOptions<BaseballDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
