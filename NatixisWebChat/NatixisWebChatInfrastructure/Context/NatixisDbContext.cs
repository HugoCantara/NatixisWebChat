namespace NatixisWebChatInfrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using NatixisWebChatDomain.AppEntities;

    public class NatixisDbContext : DbContext
    {
        private readonly string? _connectionString;

        public NatixisDbContext(DbContextOptions<NatixisDbContext> options, IConfiguration configuration) : base(options) 
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<GroupMemberEntity> GroupMembers { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<StockCodeEntity> StockCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, b => b.MigrationsAssembly("NatixisWebChatInfrastructure"));
            }
        }
    }
}
