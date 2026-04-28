using KSHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KSHOP.DAL.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet <Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoriesTranslation { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductsTranslation { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandTranslation> BrandsTranslation { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor=httpContextAccessor;

        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CategoryTranslation>()
            .ToTable("CategoriesTranslation");  // مهم جدًا

            modelBuilder.Entity<Category>()
                .HasOne(t=>t.UpdatedBy)
                .WithMany()
                .HasForeignKey(t=>t.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cart>()
    .Property(c => c.UserId)
    .HasMaxLength(450); // 👈 مهم

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_httpContextAccessor.HttpContext != null)
            {

                var entries = ChangeTracker.Entries<AuditableEntity>();
                var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
             

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(x => x.CreatedById).CurrentValue = currentUserId;
                        entry.Property(x => x.CreatedOn).CurrentValue = DateTime.UtcNow;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                        entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
