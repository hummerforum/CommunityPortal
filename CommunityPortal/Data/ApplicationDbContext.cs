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
            string accountId4 = Guid.NewGuid().ToString();
            string accountId5 = Guid.NewGuid().ToString();


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            { Id = moderatorRoleId, Name = "Moderator", NormalizedName = "MODERATOR" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            { Id = userRoleId, Name = "User", NormalizedName = "USER" });

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

            CommunityUser cu4 = new CommunityUser
            {
                Id = accountId4,
                Email = "tobias@b.com",
                NormalizedEmail = "TOBIAS@B.COM",
                UserName = "tobias@b.com",
                NormalizedUserName = "TOBIAS@B.COM",
                PasswordHash = passwordHasher.HashPassword(null, "123456"),
            };

            modelBuilder.Entity<CommunityUser>().HasData(cu4);


            CommunityUser cu5 = new CommunityUser
            {
                Id = accountId5,
                Email = "peter@b.com",
                NormalizedEmail = "PETER@B.COM",
                UserName = "peter@b.com",
                NormalizedUserName = "PETER@B.COM",
                PasswordHash = passwordHasher.HashPassword(null, "123456"),
            };
            modelBuilder.Entity<CommunityUser>().HasData(cu5);


            // ROLES
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId, RoleId = userRoleId });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId2, RoleId = moderatorRoleId });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId3, RoleId = adminRoleId });
            
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId4, RoleId = userRoleId });
            
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = accountId5, RoleId = userRoleId });


            // News  post and categories

            Category ca1 = new Category() { CategoryId = 1, Description = "News covering all the world", Title = "World News" };
            Category ca2 = new Category() { CategoryId = 2, Description = "New covering fishing", Title = "Fishing News" };
            Category ca3 = new Category() { CategoryId = 3, Description = "The wors possible jokes in the world", Title = "Joke News" };

            modelBuilder.Entity<Category>().HasData(ca1);
            modelBuilder.Entity<Category>().HasData(ca2);
            modelBuilder.Entity<Category>().HasData(ca3);


            DateTime d = new DateTime(2022, 4, 19);
            DateTime d2 = new DateTime(2022, 4, 20);
            DateTime d3 = new DateTime(2022, 4, 18);


            NewsPost np1 = new NewsPost()
            {
                NewsPostId = 1,
                IsEvent = true,
                CategoryId = 1,
                UserName = cu.UserName,
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
                UserName = cu.UserName,
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
                UserName = cu.UserName,
                CreatedDate = d3,
                UpdatedDate = d3,
                Tag = "Tag 3",
                Description = "what is grey and comes in pints?",
                Heading = "Bad Joke1",
                Information = "info2"
            };

            NewsPost np4 = new NewsPost()
            {
                NewsPostId = 4,
                IsEvent = false,
                CategoryId = 3,
                UserName = cu.UserName,
                CreatedDate = d3,
                UpdatedDate = d3,
                Tag = "Tag 3",
                Description = "What is the most common swedish name in russian uboats? Tor-Peder",
                Heading = "Bad Joke2",
                Information = "info2"
            };

            NewsPost np5 = new NewsPost()
            {
                NewsPostId = 5,
                IsEvent = false,
                CategoryId = 3,
                UserName = cu.UserName,
                CreatedDate = d3,
                UpdatedDate = d3,
                Tag = "Tag 3",
                Description = "Can a dog jump higher than a house? Well, duh. Houses can’t jump.? ",
                Heading = "Bad Joke3",
                Information = "info2"
            };

            modelBuilder.Entity<NewsPost>().HasData(np1);
            modelBuilder.Entity<NewsPost>().HasData(np2);
            modelBuilder.Entity<NewsPost>().HasData(np3);
            modelBuilder.Entity<NewsPost>().HasData(np4);
            modelBuilder.Entity<NewsPost>().HasData(np5);

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 6,
                IsEvent = false,
                CategoryId = 3,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 1),
                UpdatedDate = new DateTime(2022, 4, 1),
                Heading = "Bad Joke 4",
                Information = "Can a dog jump higher than a house? Well, duh. Houses can’t jump !",
                Tag = "Tag",
                Description =  ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 7,
                IsEvent = false,
                CategoryId = 1,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 1),
                UpdatedDate = new DateTime(2022, 4, 1),
                Heading = "What is Lorem Ipsum?",
                Information = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 8,
                IsEvent = false,
                CategoryId = 1,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 1),
                UpdatedDate = new DateTime(2022, 4, 1),
                Heading = "Where does Lorem Ipsum come from?",
                Information = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, Lorem ipsum dolor sit amet.. comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H.Rackham.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 9,
                IsEvent = false,
                CategoryId = 1,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 1),
                UpdatedDate = new DateTime(2022, 4, 1),
                Heading = "Why do we use Lorem Ipsum?",
                Information = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 10,
                IsEvent = false,
                CategoryId = 1,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 1),
                UpdatedDate = new DateTime(2022, 4, 1),
                Heading = "Where can I get some Lorem Ipsum?",
                Information = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 11,
                IsEvent = false,
                CategoryId = 2,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 2),
                UpdatedDate = new DateTime(2022, 4, 2),
                Heading = "Perch",
                Information = "Perch is a common name for fish of the genus Perca, freshwater gamefish belonging to the family Percidae. The perch, of which three species occur in different geographical areas, lend their name to a large order of vertebrates: the Perciformes, from the Greek: πέρκη (perke), simply meaning perch, and the Latin forma meaning shape. Many species of freshwater gamefish more or less resemble perch, but belong to different genera. In fact, the exclusively saltwater-dwelling red drum is often referred to as a red perch, though by definition perch are freshwater fish. Though many fish are referred to as perch as a common name, to be considered a true perch, the fish must be of the family Percidae.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 12,
                IsEvent = false,
                CategoryId = 2,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 2),
                UpdatedDate = new DateTime(2022, 4, 2),
                Heading = "Pike",
                Information = "The pike is a large fish that can grow over a metre in length. It is found in lakes and slow-flowing rivers and canals that have a lot of vegetation. It uses these plants as hiding places when hunting, bursting out with remarkable speed to catch fish, frogs, small mammals or ducklings. Young pike are called 'jack' and will eat small fish and invertebrates. Pike spawn between March and May, returning to the same place every year. A large female can produce up to 500,000 eggs.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 13,
                IsEvent = false,
                CategoryId = 2,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 3),
                UpdatedDate = new DateTime(2022, 4, 3),
                Heading = "Salmon",
                Information = "Salmon is the common name for several species of ray-finned fish in the family Salmonidae. Other fish in the same family include trout, char, grayling, and whitefish. Salmon are native to tributaries of the North Atlantic (genus Salmo) and Pacific Ocean (genus Oncorhynchus). Many species of salmon have been introduced into non-native environments such as the Great Lakes of North America and Patagonia in South America. Salmon are intensively farmed in many parts of the world.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 14,
                IsEvent = false,
                CategoryId = 2,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 3),
                UpdatedDate = new DateTime(2022, 4, 3),
                Heading = "Lobster",
                Information = "Lobsters are a family (Nephropidae, synonym Homaridae) of large marine crustaceans. Lobsters have long bodies with muscular tails, and live in crevices or burrows on the sea floor.Three of their five pairs of legs have claws including the first pair, which are usually much larger than the others.Highly prized as seafood, lobsters are economically important, and are often one of the most profitable commodities in coastal areas they populate. ",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 15,
                IsEvent = false,
                CategoryId = 2,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 4),
                UpdatedDate = new DateTime(2022, 4, 4),
                Heading = "Crab",
                Information = "Crabs are decapod crustaceans of the infraorder Brachyura, which typically have a very short projecting tail (abdomen), usually hidden entirely under the thorax. They live in all the world's oceans, in fresh water, and on land, are generally covered with a thick exoskeleton, and have a single pair of pincers. They first appeared during the Jurassic Period.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 16,
                IsEvent = false,
                CategoryId = 1,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 2),
                UpdatedDate = new DateTime(2022, 4, 2),
                Heading = "Microsoft to kill Patch Tuesday for some enterprise users",
                Information = "The company plans to launch its Windows Autopatch service for enterprise users with Windows 10 or Windows 11 Enterprise E3 licenses; it will continuously deploy patches, eliminating the need for admins to manually deploy them.",
                Tag = "Tag",
                Description = ""
            });

            modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = 17,
                IsEvent = false,
                CategoryId = 1,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 3),
                UpdatedDate = new DateTime(2022, 4, 3),
                Heading = "What will Apple announce at WWDC 2022?",
                Information = "WWDC speculation has officially begun. So, what will Apple announce at its annual developer conference in June? Macworld executive editor Michael Simon and Computerworld executive editor Ken Mingis join Juliet to discuss what to expect at WWDC this year, including updates to Apple’s operating systems like iOS and macOS and maybe even some hardware announcements.",
                Tag = "Tag",
                Description = ""
            });

            /*modelBuilder.Entity<NewsPost>().HasData(new NewsPost()
            {
                NewsPostId = ,
                IsEvent = false,
                CategoryId = 1,
                UserName = cu.UserName,
                CreatedDate = new DateTime(2022, 4, 1),
                UpdatedDate = new DateTime(2022, 4, 1),
                Heading = ".",
                Information = ".",
                Tag = "Tag",
                Description = ""
            });*/


            // set up forum
            // fishing forum sections
            DiscussionCategory c = new()
            { DiscussionCategoryId = 1, Name = "Fishing", Description = "A forum for fishing." };
            modelBuilder.Entity<DiscussionCategory>().HasData(c);
            DiscussionForum dgF1 = new()
            {
                Name = "Lobster",
                DiscussionForumId = 1,
                DiscussionCategoryId = 1,
                Description = "Everything about lobsters."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgF1);
            DiscussionForum dgF2 = new()
            {
                Name = "Cod",
                DiscussionForumId = 2,
                DiscussionCategoryId = 1,
                Description = "Everyone's favourite white fish."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgF2);
            DiscussionForum dgF3 = new()
            {
                Name = "Anchovy",
                DiscussionForumId = 3,
                DiscussionCategoryId = 1,
                Description = "A must on the christmas table."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgF3);

            // cars forum sections
            DiscussionCategory c2 = new() { DiscussionCategoryId = 2, Name = "Cars", Description = "A forum for cars." };
            modelBuilder.Entity<DiscussionCategory>().HasData(c2);
            DiscussionForum dgC1 = new()
            { Name = "Volvo", DiscussionForumId = 4, DiscussionCategoryId = 2, Description = "I roll." };
            modelBuilder.Entity<DiscussionForum>().HasData(dgC1);
            DiscussionForum dgC2 = new()
            {
                Name = "SAAB",
                DiscussionForumId = 5,
                DiscussionCategoryId = 2,
                Description = "They also make military aircrafts."
            };
            modelBuilder.Entity<DiscussionForum>().HasData(dgC2);
            DiscussionForum dgC3 = new()
            { Name = "BMW", DiscussionForumId = 6, DiscussionCategoryId = 2, Description = "German Engineering." };
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
            { DiscussionCategoryId = 3, Name = "Secret forum", Description = "A secret forum" };
            modelBuilder.Entity<DiscussionCategory>().HasData(c3);
            DiscussionForum dgS1 = new()
            {
                Name = "Secret",
                DiscussionForumId = 7,
                DiscussionCategoryId = 3,
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
                .HasKey(x => new { x.DiscussionGroupId, x.DiscussionCategoryId });
            DiscussionGroupCategory dgc1 = new()
            {
                DiscussionGroupCategoryId = 1,
                DiscussionGroupId = dg1.DiscussionGroupId,
                DiscussionCategoryId = c3.DiscussionCategoryId
            };
            modelBuilder.Entity<DiscussionGroupCategory>().HasData(dgc1);
            // add user to group
            modelBuilder.Entity<DiscussionGroupMembership>()
                .HasKey(t => new { t.CommunityUserId, t.DiscussionGroupId });

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