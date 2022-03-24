using CommunityPortal.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace CommunityPortal.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<CommunityUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }

        public DbSet<CommunityUser> CommunityUsers { get; set; }

        public DbSet<DiscussionPost> DiscussionPosts { get; set; }
        public DbSet<DiscussionGroup> DiscussionGroups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsPost> NewsPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string adminRoleId = Guid.NewGuid().ToString();
            string moderatorRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();

            string accountId = Guid.NewGuid().ToString();


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = moderatorRoleId, Name = "Moderator", NormalizedName = "MODERATOR" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" });

            PasswordHasher<CommunityUser> passwordHasher = new PasswordHasher<CommunityUser>();

            modelBuilder.Entity<CommunityUser>().HasData(new CommunityUser
            {
                Id = accountId,
                Email = "a@b.com",
                NormalizedEmail = "A@B.COM",
                UserName = "a@b.com",
                NormalizedUserName = "A@B.COM",
                PasswordHash = passwordHasher.HashPassword(null, "1234"),
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = accountId, RoleId = userRoleId });
        }
    }
}
