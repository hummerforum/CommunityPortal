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

        public DbSet<ReceivedPrivateMessage> ReceivedPrivateMessages { get; set; }
        public DbSet<SentPrivateMessage> SentPrivateMessages { get; set; }
        public DbSet<DiscussionPost> DiscussionPosts { get; set; }
        public DbSet<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }
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
                PasswordHash = passwordHasher.HashPassword(null, "123456"),
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = accountId, RoleId = userRoleId });

            string userU1Id = Guid.NewGuid().ToString();
            CommunityUser communityUser1 = new CommunityUser
            {
                Id = userU1Id,
                Email = "userU1@user.com",
                NormalizedEmail = "USERU1@USER.COM",
                UserName = "userU1@user.com",
                NormalizedUserName = "USERU1@USER.COM",
                PasswordHash = passwordHasher.HashPassword(null, "userU1@user.com"),
            };
            modelBuilder.Entity<CommunityUser>().HasData(communityUser1);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = userU1Id, RoleId = userRoleId });

            string userU2Id = Guid.NewGuid().ToString();
            CommunityUser communityUser2 = new CommunityUser
            {
                Id = userU2Id,
                Email = "userU2@user.com",
                NormalizedEmail = "USERU2@USER.COM",
                UserName = "userU2@user.com",
                NormalizedUserName = "USERU2@USER.COM",
                PasswordHash = passwordHasher.HashPassword(null, "userU2@user.com"),
            };
            modelBuilder.Entity<CommunityUser>().HasData(communityUser2);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = userU2Id, RoleId = userRoleId });

            string userU3Id = Guid.NewGuid().ToString();
            CommunityUser communityUser3 = new CommunityUser
            {
                Id = userU3Id,
                Email = "userU3@user.com",
                NormalizedEmail = "USERU3@USER.COM",
                UserName = "userU3@user.com",
                NormalizedUserName = "USERU3@USER.COM",
                PasswordHash = passwordHasher.HashPassword(null, "userU3@user.com"),
            };
            modelBuilder.Entity<CommunityUser>().HasData(communityUser3);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = userU3Id, RoleId = userRoleId });


        }
    }
}
