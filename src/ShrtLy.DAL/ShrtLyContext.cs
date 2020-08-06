using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShrtLy.DAL
{
    public class ShrtLyContext : IdentityDbContext
    {
        public ShrtLyContext(DbContextOptions<ShrtLyContext> options)
            : base(options)
        {
        }

        public DbSet<LinkEntity> Links { get; set; }
    }
}
