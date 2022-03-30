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

        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<DiscussionPost> DiscussionPosts { get; set; }
        public DbSet<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }
        public DbSet<DiscussionGroup> DiscussionGroups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsPost> NewsPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(e => e.SenderCommunityUser)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PrivateMessage>()
               .HasOne(e => e.ReceiverCommunityUser)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);


            string adminRoleId = Guid.NewGuid().ToString();
            string moderatorRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();

            string accountId = Guid.NewGuid().ToString();
            string accountId2 = Guid.NewGuid().ToString();
            string accountId3 = Guid.NewGuid().ToString();


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = moderatorRoleId, Name = "Moderator", NormalizedName = "MODERATOR" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" });

            PasswordHasher<CommunityUser> passwordHasher = new PasswordHasher<CommunityUser>();

            CommunityUser cu = new CommunityUser
            {
                Id = accountId,
                Email = "user@b.com",
                NormalizedEmail = "USER@B.COM",
                UserName = "user@b.com",
                NormalizedUserName = "USER@B.COM",
                PasswordHash = passwordHasher.HashPassword(null, "123456"),
            };
            modelBuilder.Entity<CommunityUser>().HasData(cu);

            CommunityUser cu2 = new CommunityUser
            {
                Id = accountId2,
                Email = "moderator@b.com",
                NormalizedEmail = "MODERATOR@B.COM",
                UserName = "moderator@b.com",
                NormalizedUserName = "MODERATOR@B.COM",
                PasswordHash = passwordHasher.HashPassword(null, "123456"),
            };

            modelBuilder.Entity<CommunityUser>().HasData(cu2);


            CommunityUser cu3 = new CommunityUser
            {
                Id = accountId3,
                Email = "admin@b.com",
                NormalizedEmail = "ADMIN@B.COM",
                UserName = "admin@b.com",
                NormalizedUserName = "ADMIN@B.COM",
                PasswordHash = passwordHasher.HashPassword(null, "123456"),
            };

            modelBuilder.Entity<CommunityUser>().HasData(cu3);

            // ROLES
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId, RoleId = userRoleId });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId2, RoleId = moderatorRoleId });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId3, RoleId = adminRoleId });




            DiscussionGroup dg = new DiscussionGroup() 
                    { Name = "Grupp1", DiscussionGroupId = 1, Description = "Text för discusionsgrupp1" };

            DiscussionGroup dg2 = new DiscussionGroup()
            { Name = "Grupp2", DiscussionGroupId = 2, Description = "Text för discusionsgrupp2" };


            DiscussionGroup dg3 = new DiscussionGroup()
            { Name = "Grupp3", DiscussionGroupId = 3, Description = "Text för discusionsgrupp3" };


            modelBuilder.Entity<DiscussionGroup>().HasData(dg);
            modelBuilder.Entity<DiscussionGroup>().HasData(dg2);
            modelBuilder.Entity<DiscussionGroup>().HasData(dg3);

            Category c = new Category() {CategoryId=1,Title="Kategori 1",Description="Text för grupp1 " };
            Category c2 = new Category() {CategoryId=2,Title="Kategori 2",Description="Text för grupp2 " };
            Category c3 = new Category() {CategoryId=3,Title="Kategori 3",Description="Text för grupp3 " };

            modelBuilder.Entity<Category>().HasData(c);
            modelBuilder.Entity<Category>().HasData(c2);
            modelBuilder.Entity<Category>().HasData(c3);


            DiscussionPost dp = new DiscussionPost()
            {
                DiscussionPostId = 1,
                Heading = "Heading dp1",
                Content = "content dp1",
                Time = DateTime.Now,
                CommunityUserId = cu.Id,
                CategoryId= 1 
             };

            DiscussionPost dp2 = new DiscussionPost()
            {
                DiscussionPostId = 2,
                Heading = "Heading dp2",
                Content = "content dp2",
                Time = DateTime.Now,
                CommunityUserId = cu2.Id,
                CategoryId = 2
            };

            DiscussionPost dp3 = new DiscussionPost()
            {
                DiscussionPostId = 3,
                Heading = "Heading dp3",
                Content = "content dp3",
                Time = DateTime.Now,
                CommunityUserId = cu3.Id,
                CategoryId = 3
            };

            modelBuilder.Entity<DiscussionPost>().HasData(dp);
            modelBuilder.Entity<DiscussionPost>().HasData(dp2);
            modelBuilder.Entity<DiscussionPost>().HasData(dp3);


            DiscussionGroupMembership dgm = new DiscussionGroupMembership(1, dg.DiscussionGroupId);
            DiscussionGroupMembership dgm2 = new DiscussionGroupMembership(2, dg2.DiscussionGroupId);
            DiscussionGroupMembership dgm3 = new DiscussionGroupMembership(3, dg3.DiscussionGroupId);

        }
    }
}
