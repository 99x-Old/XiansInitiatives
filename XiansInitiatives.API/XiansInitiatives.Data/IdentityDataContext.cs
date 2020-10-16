using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XiansInitiative.Configuration.Accessor;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data
{
    public class IdentityDataContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        private readonly IConfiguration _configuration;

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options, IConfiguration configuration, IConfigurationAccessor configurationAccessor)
            : base(options)
        {
            _configuration = configuration;
            _configurationAccessor = configurationAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<InitiativeMember>()
            .HasKey(x => new { x.InitiativeId, x.MemberId });

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configurationAccessor.GetConnectionString("IdentityDataContextConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<InitiativeYear> InitiativeYear { get; set; }
        public DbSet<Initiative> Initiative { get; set; }
        public DbSet<InitiativeMember> InitiativeMember { get; set; }
        public DbSet<InitiativeMeeting> InitiativeMeeting { get; set; }
        public DbSet<ActionItem> ActionItem { get; set; }
        public DbSet<CommentItem> CommentItem { get; set; }
        public DbSet<ItemAssignee> ItemAssignee { get; set; }
        public DbSet<ReviewCycle> ReviewCycle { get; set; }
        public DbSet<EvaluationCriteria> EvaluationCriteria { get; set; }
    }
}