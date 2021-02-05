using Microsoft.EntityFrameworkCore;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Infra.Data.Context
{
    public class YourFinacesContext : DbContext
    {
        public YourFinacesContext(DbContextOptions<YourFinacesContext> options)
            : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Session> Session { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
