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
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<CommunityUser> CommunityUsers { get; set; }

        public DbSet<ReceivedPrivateMessage> ReceivedPrivateMessages { get; set; }
        public DbSet<SentPrivateMessage> SentPrivateMessages { get; set; }

        // forum
        public DbSet<DiscussionCategory> DiscussionCategories { get; set; }
        public DbSet<DiscussionForum> DiscussionForums { get; set; }
        public DbSet<DiscussionTopic> DiscussionTopics { get; set; }
        public DbSet<DiscussionReply> DiscussionReplies { get; set; }
        public DbSet<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsPost> NewsPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<PrivateMessage>()
                .HasOne(e => e.SenderCommunityUser)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PrivateMessage>()
               .HasOne(e => e.ReceiverCommunityUser)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);*/


            string adminRoleId = Guid.NewGuid().ToString();
            string moderatorRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();

            string accountId = Guid.NewGuid().ToString();
            string accountId2 = Guid.NewGuid().ToString();
            string accountId3 = Guid.NewGuid().ToString();


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN"});
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {Id = moderatorRoleId, Name = "Moderator", NormalizedName = "MODERATOR"});
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {Id = userRoleId, Name = "User", NormalizedName = "USER"});

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
                new IdentityUserRole<string> {UserId = accountId, RoleId = userRoleId});

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> {UserId = accountId2, RoleId = moderatorRoleId});

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> {UserId = accountId3, RoleId = adminRoleId});


            // News  post and categories

            Category ca1 = new Category() { CategoryId = 1, Description = "News covering all the world", Title = "World News" };
            Category ca2 = new Category() { CategoryId = 2, Description = "New covering fishing", Title = "Fishing News" };
            Category ca3 = new Category() { CategoryId = 3, Description = "The wors possible jokes in the world", Title = "Joke News" };

            modelBuilder.Entity<Category>().HasData(ca1);
            modelBuilder.Entity<Category>().HasData(ca2);
            modelBuilder.Entity<Category>().HasData(ca3);


            DateTime d = new DateTime(2000, 1, 30);
            DateTime d2 = new DateTime(2010, 2, 15);
            DateTime d3 = new DateTime(2020, 10, 1);


            NewsPost np1 = new NewsPost()
            {
                NewsPostId = 1,
                IsEvent = true,
                CategoryId = 1,
                UserName = accountId,
                CreatedDate = d,
                UpdatedDate = DateTime.Now,
                Tag = "Tag 1",
                Description = "Biggest looser competition",
                Heading = "Biggest looser",
                Information = "info"
            };

            NewsPost np2 = new NewsPost()
            {
                NewsPostId = 2,
                IsEvent = true,
                CategoryId = 2,
                UserName = accountId2,
                CreatedDate = d2,
                UpdatedDate = d2,
                Tag = "Tag 2",
                Description = "To mäny pots baby",
                Heading = "Pouching",
                Information = "info2"
            };

            NewsPost np3 = new NewsPost()
            {
                NewsPostId = 3,
                IsEvent = false,
                CategoryId = 3,
                UserName = accountId3,
                CreatedDate = d3,
                UpdatedDate = d3,
                Tag = "Tag 3",
                Description = "what is grey and comes in pints?",
                Heading = "Bad Joke1",
                Information = "info2"
            };

            modelBuilder.Entity<NewsPost>().HasData(np1);
            modelBuilder.Entity<NewsPost>().HasData(np2);
            modelBuilder.Entity<NewsPost>().HasData(np3);

            // set up forum
            // fishing forum sections
            DiscussionCategory c = new()
                {DiscussionCategoryId = 1, Name = "Fishing", Description = "A forum for fishing."};
            modelBuilder.Entity<DiscussionCategory>().HasData(c);
            DiscussionForum dgF1 = new()
            {
                Name = "Lobster", DiscussionForumId = 1, DiscussionCategoryId = 1,
                Description = "Everything about lobsters."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgF1);
            DiscussionForum dgF2 = new()
            {
                Name = "Cod", DiscussionForumId = 2, DiscussionCategoryId = 1,
                Description = "Everyone's favourite white fish."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgF2);
            DiscussionForum dgF3 = new()
            {
                Name = "Anchovy", DiscussionForumId = 3, DiscussionCategoryId = 1,
                Description = "A must on the christmas table."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgF3);

            // cars forum sections
            DiscussionCategory c2 = new() {DiscussionCategoryId = 2, Name = "Cars", Description = "A forum for cars."};
            modelBuilder.Entity<DiscussionCategory>().HasData(c2);
            DiscussionForum dgC1 = new()
                {Name = "Volvo", DiscussionForumId = 4, DiscussionCategoryId = 2, Description = "I roll."};
            modelBuilder.Entity<DiscussionForum>().HasData(dgC1);
            DiscussionForum dgC2 = new()
            {
                Name = "SAAB", DiscussionForumId = 5, DiscussionCategoryId = 2,
                Description = "They also make military aircrafts."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgC2);
            DiscussionForum dgC3 = new()
                {Name = "BMW", DiscussionForumId = 6, DiscussionCategoryId = 2, Description = "German Engineering."};
            modelBuilder.Entity<DiscussionForum>().HasData(dgC3);

            // create an example topic to the Lobster forum in the Fishing category.
            DiscussionTopic dt = new()
            {
                DiscussionTopicId = 1,
                DiscussionForumId = dgF1.DiscussionForumId,
                AuthorId = cu.Id,
                Subject = "My finest lobster jokes.",
                Content = "Have you found your lost lobster yet? No, it's just a lost claws now.",
                Time = Convert.ToDateTime("04 April 2022 7:00:00 AM"),
            };
            modelBuilder.Entity<DiscussionTopic>().HasData(dt);

            // create replies to example topic
            DiscussionReply dr1 = new()
            {
                DiscussionReplyId = 1,
                TopicId = dt.DiscussionTopicId,
                AuthorId = cu2.Id,
                Content = "This is the worst joke I've ever heard in my whole life.",
                Time = Convert.ToDateTime("04 April 2022 7:10:35 AM"),
            };
            modelBuilder.Entity<DiscussionReply>().HasData(dr1);
            DiscussionReply dr2 = new()
            {
                DiscussionReplyId = 2,
                TopicId = dt.DiscussionTopicId,
                AuthorId = cu3.Id,
                Content = "As an admin, I'm banning you for this awful joke.",
                Time = Convert.ToDateTime("04 April 2022 7:20:35 AM"),
            };
            modelBuilder.Entity<DiscussionReply>().HasData(dr2);


            // create secret category only meant for people in a group
            DiscussionCategory c3 = new()
                {DiscussionCategoryId = 3, Name = "Secret forum", Description = "A secret forum"};
            modelBuilder.Entity<DiscussionCategory>().HasData(c3);
            DiscussionForum dgS1 = new()
            {
                Name = "Secret", DiscussionForumId = 7, DiscussionCategoryId = 3,
                Description = "If you see this, you're cool."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgS1);
            DiscussionTopic dt2 = new()
            {
                DiscussionTopicId = 2,
                DiscussionForumId = dgS1.DiscussionForumId,
                AuthorId = cu2.Id,
                Subject = "Hello I'm a lonely moderator.",
                Content = "Is anyone here? hello hello hello *echo*",
                Time = Convert.ToDateTime("04 April 2022 7:00:00 AM"),
            };
            modelBuilder.Entity<DiscussionTopic>().HasData(dt2);

            // create replies to example topic
            DiscussionReply dr3 = new()
            {
                DiscussionReplyId = 3,
                TopicId = dt2.DiscussionTopicId,
                AuthorId = cu2.Id,
                Content = "I guess I'm alone.",
                Time = Convert.ToDateTime("04 April 2022 7:10:35 AM"),
            };
            modelBuilder.Entity<DiscussionReply>().HasData(dr3);

            // create group meant to read our secret forum
            DiscussionGroup dg1 = new()
            {
                DiscussionGroupId = 1,
                DiscussionGroupName = "The secret society."
            };
            modelBuilder.Entity<DiscussionGroup>().HasData(dg1);

            // link the group to the category
            modelBuilder.Entity<DiscussionGroupCategory>()
                .HasKey(x => new {x.DiscussionGroupId, x.DiscussionCategoryId});
            DiscussionGroupCategory dgc1 = new()
            {
                DiscussionGroupCategoryId = 1,
                DiscussionGroupId = dg1.DiscussionGroupId,
                DiscussionCategoryId = c3.DiscussionCategoryId
            };
            modelBuilder.Entity<DiscussionGroupCategory>().HasData(dgc1);
            // add user to group
            modelBuilder.Entity<DiscussionGroupMembership>()
                .HasKey(t => new {t.CommunityUserId, t.DiscussionGroupId});

            modelBuilder.Entity<DiscussionGroupMembership>()
                .HasOne(pt => pt.CommunityUser)
                .WithMany(p => p.DiscussionGroupMembership)
                .HasForeignKey(pt => pt.CommunityUserId);

            modelBuilder.Entity<DiscussionGroupMembership>()
                .HasOne(pt => pt.DiscussionGroup)
                .WithMany(t => t.DiscussionGroupMembership)
                .HasForeignKey(pt => pt.DiscussionGroupId);
            DiscussionGroupMembership dgcm1 = new()
            {
                CommunityUserId = cu2.Id,
                DiscussionGroupId = dg1.DiscussionGroupId
            };
            modelBuilder.Entity<DiscussionGroupMembership>().HasData(dgcm1);
        }
    }
}