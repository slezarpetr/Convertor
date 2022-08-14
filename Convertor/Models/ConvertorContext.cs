using Microsoft.EntityFrameworkCore;

namespace Convertor.Models
{
    public class ConvertorContext : DbContext
    {
        public ConvertorContext(DbContextOptions<ConvertorContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<XmlDoc>();
            builder.Entity<JsonDoc>();

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
