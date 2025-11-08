using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AddplayApp.Api.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
    }
}
