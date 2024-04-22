namespace NatixisWebChatInfrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using NatixisWebChatDomain.AppEntities;

    public class NatixisDbContext : DbContext
    {
        private const string connection = "Server=LAPTOP-52C0HP7B\\SQLEXPRESS2019;Database=NatixisWebChat;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";

        public NatixisDbContext(DbContextOptions<NatixisDbContext> options) : base(options) { }

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
                optionsBuilder.UseSqlServer(connection, b => b.MigrationsAssembly("NatixisWebChatInfrastructure"));
            }
        }
    }
}
