using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Local_Theatre.Models
{
    public class DatabaseInitialiser : DropCreateDatabaseAlways<LocalTheatreDbContext>
    {
        protected override void Seed(LocalTheatreDbContext context)
        {
            base.Seed(context);

            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));


            if (!roleManager.RoleExists("SysAdmin"))
            {
                roleManager.Create(new IdentityRole("SysAdmin"));
            }
            if (!roleManager.RoleExists("Moderator"))
            {
                roleManager.Create(new IdentityRole("Moderator"));
            }
            if (!roleManager.RoleExists("Staff"))
            {
                roleManager.Create(new IdentityRole("Staff"));
            }
            if (!roleManager.RoleExists("Member"))
            {
                roleManager.Create(new IdentityRole("Member"));
            }
            if (!roleManager.RoleExists("Suspended"))
            {
                roleManager.Create(new IdentityRole("Suspended"));
            }
            context.SaveChanges();
            

            Staff sysadmin = new Staff();
            if (userManager.FindByName("admin@LocalTheatre.com") == null)
            {
                userManager.PasswordValidator = new PasswordValidator
                {
                    RequireDigit = false,
                    RequiredLength = 1,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false,
                };

                sysadmin = new Staff
                {
                    UserName = "admin@LocalTheatre.com",
                    Email = "admin@LocalTheatre.com",
                    FirstName = "Nicholas",
                    LastName = "Runolfsdottir",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1986, 05, 15),
                    RegistereredAt = new DateTime(2020, 01, 01),
                    Role = StaffRole.SYSADMIN
                };

                userManager.Create(sysadmin, "password123");
                userManager.AddToRole(sysadmin.Id, "SysAdmin");
            }

            Staff mod1 = new Staff();
            if (userManager.FindByName("leanne.graham@LocalTheatre.com") == null)
            {
                mod1 = new Staff
                {
                    UserName = "leanne.graham@LocalTheatre.com",
                    Email = "leanne.graham@LocalTheatre.com",
                    FirstName = "Leanne",
                    LastName = "Graham",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1994, 08, 04),
                    RegistereredAt = new DateTime(2020, 01, 01),
                    Role = StaffRole.MODERATOR
                };

                userManager.Create(mod1, "password123");
                userManager.AddToRole(mod1.Id, "Moderator");
            }

            Staff mod2 = new Staff();
            if (userManager.FindByName("edward.martin@LocalTheatre.com") == null)
            {
                mod2 = new Staff
                {
                    UserName = "edward.martin@LocalTheatre.com",
                    Email = "edward.martin@LocalTheatre.com",
                    FirstName = "Edward",
                    LastName = "Martin",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1977, 06, 20),
                    RegistereredAt = new DateTime(2020, 01, 01),
                    Role = StaffRole.MODERATOR
                };

                userManager.Create(mod2, "password123");
                userManager.AddToRole(mod2.Id, "Moderator");
            }

            Staff staff1 = new Staff();
            if (userManager.FindByName("ervin.howell@LocalTheatre.com") == null)
            {
                staff1 = new Staff
                {
                    UserName = "ervin.howell@LocalTheatre.com",
                    Email = "ervin.howell@LocalTheatre.com",
                    FirstName = "Ervin",
                    LastName = "Howell",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1985, 10, 20),
                    RegistereredAt = new DateTime(2020, 01, 15),
                    Role = StaffRole.STAFF
                };

                userManager.Create(staff1, "password123");
                userManager.AddToRole(staff1.Id, "Staff");
            }

            Staff staff2 = new Staff();
            if (userManager.FindByName("clementine.bauch@LocalTheatre.com") == null)
            {
                staff2 = new Staff
                {
                    UserName = "clementine.bauch@LocalTheatre.com",
                    Email = "clementine.bauch@LocalTheatre.com",
                    FirstName = "Clementine",
                    LastName = "Bauch",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1987, 08, 08),
                    RegistereredAt = new DateTime(2020, 01, 10),
                    Role = StaffRole.STAFF
                };

                userManager.Create(staff2, "password123");
                userManager.AddToRole(staff2.Id, "Staff");
            }

            Staff staff3 = new Staff();
            if (userManager.FindByName("patricia.lebsack@LocalTheatre.com") == null)
            {
                staff3 = new Staff
                {
                    UserName = "patricia.lebsack@LocalTheatre.com",
                    Email = "patricia.lebsack@LocalTheatre.com",
                    FirstName = "Patricia",
                    LastName = "Lebsack",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1995, 04, 02),
                    RegistereredAt = new DateTime(2020, 02, 07),
                    Role = StaffRole.STAFF
                };

                userManager.Create(staff3, "password123");
                userManager.AddToRole(staff3.Id, "Staff");
            }

            Staff staff4 = new Staff();
            if (userManager.FindByName("chelsey.dietrich@LocalTheatre.com") == null)
            {
                staff4 = new Staff
                {
                    UserName = "chelsey.dietrich@LocalTheatre.com",
                    Email = "chelsey.dietrich@LocalTheatre.com",
                    FirstName = "Chelsey",
                    LastName = "Dietrich",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1992, 11, 29),
                    RegistereredAt = new DateTime(2020, 02, 27),
                    Role = StaffRole.STAFF
                };

                userManager.Create(staff4, "password123");
                userManager.AddToRole(staff4.Id, "Staff");
            }

            Staff staff5 = new Staff();
            if (userManager.FindByName("tonya.oliverr@LocalTheatre.com") == null)
            {
                staff5 = new Staff
                {
                    UserName = "tonya.oliverr@LocalTheatre.com",
                    Email = "tonya.oliverr@LocalTheatre.com",
                    FirstName = "Tonya",
                    LastName = "Oliver",
                    Addressline1 = "Address Line 1",
                    Addressline2 = "Address Line 2",
                    PostCode = "G5 7GG",
                    City = "Glasgow",
                    PhoneNumber = "07554395570",
                    EmailConfirmed = true,
                    DateofBirth = new DateTime(1997, 04, 19),
                    RegistereredAt = new DateTime(2020, 03, 15),
                    Role = StaffRole.STAFF
                };

                userManager.Create(staff5, "password123");
                userManager.AddToRole(staff5.Id, "Staff");
            }

            Member member1 = new Member();
            if (userManager.FindByName("Bill@gmail.com") == null)
            {
                member1 = new Member
                {
                    UserName = "Bill@gmail.com",
                    Email = "Bill@gmail.com",
                    FirstName = "Billy",
                    LastName = "Crow",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2020, 07, 02),
                    DateofBirth = new DateTime(1986, 08, 17),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.FREE_MEMBERSHIP
                };

                userManager.Create(member1, "password123");
                userManager.AddToRole(member1.Id, "Member");
            }

            Member member2 = new Member();
            if (userManager.FindByName("Mark@gmail.com") == null)
            {
                member2 = new Member
                {
                    UserName = "Mark@gmail.com",
                    Email = "Mark@gmail.com",
                    FirstName = "Mark",
                    LastName = "Black",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2020, 05, 30),
                    DateofBirth = new DateTime(1979, 10, 05),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.PAID_MEMBERSHIP
                };

                userManager.Create(member2, "password123");
                userManager.AddToRole(member2.Id, "Member");
            }

            Member member3 = new Member();
            if (userManager.FindByName("Dennis@gmail.com") == null)
            {
                member3 = new Member
                {
                    UserName = "Dennis@gmail.com",
                    Email = "Dennis@gmail.com",
                    FirstName = "Dennis",
                    LastName = "Schulist",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2021, 01, 29),
                    DateofBirth = new DateTime(1996, 07, 20),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.FREE_MEMBERSHIP
                };

                userManager.Create(member3, "password123");
                userManager.AddToRole(member3.Id, "Member");
            }

            Member member4 = new Member();
            if (userManager.FindByName("Jevan@gmail.com") == null)
            {
                member4 = new Member
                {
                    UserName = "Jevan@gmail.com",
                    Email = "Jevan@gmail.com",
                    FirstName = "Jevan",
                    LastName = "Rooney",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2021, 01, 29),
                    DateofBirth = new DateTime(1996, 07, 20),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.PAID_MEMBERSHIP
                };

                userManager.Create(member4, "password123");
                userManager.AddToRole(member4.Id, "Member");
            }

            Member member5 = new Member();
            if (userManager.FindByName("Glenna@gmail.com") == null)
            {
                member5 = new Member
                {
                    UserName = "Glenna@gmail.com",
                    Email = "Glenna@gmail.com",
                    FirstName = "Glenna",
                    LastName = "Reichert",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2021, 04, 24),
                    DateofBirth = new DateTime(1999, 12, 01),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.FREE_MEMBERSHIP
                };

                userManager.Create(member5, "password123");
                userManager.AddToRole(member5.Id, "Member");
            }

            Member member6 = new Member();
            if (userManager.FindByName("jimmy.burns@mail.com") == null)
            {
                member6 = new Member
                {
                    UserName = "jimmy.burns@mail.com",
                    Email = "jimmy.burns@mail.com",
                    FirstName = "Jimmy",
                    LastName = "Burns",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2020, 12, 12),
                    DateofBirth = new DateTime(1990, 03, 08),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.PAID_MEMBERSHIP
                };

                userManager.Create(member6, "password123");
                userManager.AddToRole(member6.Id, "Member");
            }

            Member member7 = new Member();
            if (userManager.FindByName("pedro.hale@mail.com") == null)
            {
                member7 = new Member
                {
                    UserName = "pedro.hale@mail.com",
                    Email = "pedro.hale@mail.com",
                    FirstName = "Pedro",
                    LastName = "Hale",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2021, 02, 02),
                    DateofBirth = new DateTime(1980, 07, 12),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.PAID_MEMBERSHIP
                };

                userManager.Create(member7, "password123");
                userManager.AddToRole(member7.Id, "Member");
            }

            Member member8 = new Member();
            if (userManager.FindByName("eric.powell@mail.com") == null)
            {
                member8 = new Member
                {
                    UserName = "eric.powell@mail.com",
                    Email = "eric.powell@mail.com",
                    FirstName = "Eric",
                    LastName = "Powell",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2020, 08, 12),
                    DateofBirth = new DateTime(1961, 01, 05),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.FREE_MEMBERSHIP
                };

                userManager.Create(member8, "password123");
                userManager.AddToRole(member8.Id, "Member");
            }

            Member member9 = new Member();
            if (userManager.FindByName("gloria.ellis@mail.com") == null)
            {
                member9 = new Member
                {
                    UserName = "gloria.ellis@mail.com",
                    Email = "gloria.ellis@mail.com",
                    FirstName = "Gloria",
                    LastName = "Ellis",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2021, 05, 31),
                    DateofBirth = new DateTime(1992, 02, 09),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.FREE_MEMBERSHIP
                };

                userManager.Create(member9, "password123");
                userManager.AddToRole(member9.Id, "Member");
            }

            Member member10 = new Member();
            if (userManager.FindByName("eddie.cole@mail.com") == null)
            {
                member10 = new Member
                {
                    UserName = "eddie.cole@mail.com",
                    Email = "eddie.cole@mail.com",
                    FirstName = "Eddie",
                    LastName = "Cole",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2021, 01, 13),
                    DateofBirth = new DateTime(1992, 04, 20),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.ACTIVE,
                    Membership = Membership.FREE_MEMBERSHIP
                };

                userManager.Create(member10, "password123");
                userManager.AddToRole(member10.Id, "Member");
            }

            Member suspendedMember = new Member();
            if (userManager.FindByName("Bella@gmail.com") == null)
            {
                suspendedMember = new Member
                {
                    UserName = "Bella@gmail.com",
                    Email = "Bella@gmail.com",
                    FirstName = "Bella",
                    LastName = "Mahoney",
                    EmailConfirmed = true,
                    RegistereredAt = new DateTime(2021, 04, 24),
                    DateofBirth = new DateTime(2021, 03, 28),
                    PhoneNumber = "07554395570",
                    MemberStatus = MemberStatus.SUSPENDED,
                    Membership = Membership.FREE_MEMBERSHIP
                };

                userManager.Create(suspendedMember, "password123");
                userManager.AddToRole(suspendedMember.Id, "Suspended");
            }

            context.SaveChanges();

            //-------------------------------------------------------------------------------------------------------------------------------
            var news = new Category { CategoryName = "News" };
            var Announcments = new Category { CategoryName = "Announcments" };
            var MovieReviews = new Category { CategoryName = "Movie Reviews" };
            var TheatreReviews = new Category { CategoryName = "Theatre Reviews"};

            context.Categories.Add(news);
            context.Categories.Add(Announcments);
            context.Categories.Add(MovieReviews);
            context.Categories.Add(TheatreReviews);

            context.SaveChanges();

            //-------------------------------------------------------------------------------------------------------------------------------
            var post1 = new Blog()
            {
                BlogTitle = "The standard Lorem Ipsum passage, used since the 1500s",
                BlogContext = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                BlogPosted = new DateTime(2020, 08, 08),
                BlogApproved = true,
                UserId = staff1.Id,
                Category = news
            };
            context.Blogs.Add(post1);

            var post2 = new Blog()
            {
                BlogTitle = "Section 1.10.32 of 'de Finibus Bonorum et Malorum', written by Cicero in 45 BC",
                BlogContext = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?",
                BlogPosted = new DateTime(2020, 02, 12),
                BlogApproved = true,
                UserId = staff2.Id,
                Category = Announcments
            };
            context.Blogs.Add(post2);

            var post3 = new Blog()
            {
                BlogTitle = "1914 translation by H. Rackham",
                BlogContext = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?",
                BlogPosted = new DateTime(2020, 10, 07),
                BlogApproved = true,
                UserId = staff3.Id,
                Category = MovieReviews
            };
            context.Blogs.Add(post3);

            var post4 = new Blog()
            {
                BlogTitle = "Why do we use it?",
                BlogContext = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                BlogPosted = new DateTime(2021, 02, 10),
                BlogApproved = true,
                UserId = staff4.Id,
                Category = TheatreReviews
            };
            context.Blogs.Add(post4);

            var post5 = new Blog()
            {
                BlogTitle = "Where does it come from?",
                BlogContext = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.",
                BlogPosted = new DateTime(2021, 04, 17),
                BlogApproved = true,
                UserId = staff2.Id,
                Category = TheatreReviews
            };
            context.Blogs.Add(post5);

            var post6 = new Blog()
            {
                BlogTitle = "Where can I get some?",
                BlogContext = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text.",
                BlogPosted = new DateTime(2020, 09, 17),
                BlogApproved = true,
                UserId = staff3.Id,
                Category = news
            };
            context.Blogs.Add(post6);

            var post7 = new Blog()
            {
                BlogTitle = "Privacy Policy",
                BlogContext = "All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                BlogPosted = new DateTime(2021, 05, 01),
                BlogApproved = true,
                UserId = staff1.Id,
                Category = news
            };
            context.Blogs.Add(post7);

            var post8 = new Blog()
            {
                BlogTitle = "In pellentesque accumsan vulputate.",
                BlogContext = "Mauris sapien leo, egestas at pharetra id, dapibus sed nulla. Nam dignissim nec urna sed iaculis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Integer lobortis vulputate felis. Vivamus ut venenatis est. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi vulputate ut est at dignissim.",
                BlogPosted = new DateTime(2020, 12, 18),
                BlogApproved = true,
                UserId = staff4.Id,
                Category = Announcments
            };
            context.Blogs.Add(post8);

            var post9 = new Blog()
            {
                BlogTitle = "It is a paradisematic country, in which roasted parts of sentences fly into your mouth.",
                BlogContext = "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia.",
                BlogPosted = new DateTime(2021, 04, 10),
                BlogApproved = true,
                UserId = staff5.Id,
                Category = TheatreReviews
            };
            context.Blogs.Add(post9);

            var post10 = new Blog()
            {
                BlogTitle = "She packed her seven versalia, put her initial into the belt and made herself on the way.",
                BlogContext = "Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic life One day however a small line of blind text by the name of Lorem Ipsum decided to leave for the far World of Grammar. The Big Oxmox advised her not to do so, because there were thousands of bad Commas, wild Question Marks and devious Semikoli, but the Little Blind Text didn’t listen.",
                BlogPosted = new DateTime(2021, 02, 17),
                BlogApproved = true,
                UserId = staff3.Id,
                Category = Announcments
            };
            context.Blogs.Add(post10);

            var post11 = new Blog()
            {
                BlogTitle = "When she reached the first hills of the Italic Mountains.",
                BlogContext = "The copy warned the Little Blind Text, that where it came from it would have been rewritten a thousand times and everything that was left from its origin would be the word and and the Little Blind Text should turn around and return to its own, safe country. But nothing the copy said could convince her and so it didn’t take long until a few insidious Copy Writers ambushed her, made her drunk with Longe and Parole and dragged her into their agency, where they abused her for their projects again and again.",
                BlogPosted = new DateTime(2021, 04, 01),
                BlogApproved = true,
                UserId = staff5.Id,
                Category = news
            };
            context.Blogs.Add(post11);

            var post12 = new Blog()
            {
                BlogTitle = "Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic life One",
                BlogContext = "And if she hasn’t been rewritten, then they are still using her. Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia. It is a paradisematic country, in which roasted parts of sentences fly into your mouth.",
                BlogPosted = new DateTime(2020, 11, 30),
                BlogApproved = true,
                UserId = staff2.Id,
                Category = MovieReviews
            };
            context.Blogs.Add(post12);

            context.SaveChanges();

            //-------------------------------------------------------------------------------------------------------------------------------


            var comment1 = new Comment()
            {
                CommentContext = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CommentPosted = new DateTime(2020, 12, 19),
                UserId = member1.Id,
                CommentApproved = true,
                Blog = post1
            };
            context.Comments.Add(comment1);

            var comment2 = new Comment()
            {
                CommentContext = "Morbi tempus iaculis urna id volutpat lacus laoreet non.",
                CommentPosted = new DateTime(2020, 05, 05),
                UserId = member2.Id,
                CommentApproved = true,
                Blog = post1
            };
            context.Comments.Add(comment2);

            var comment3 = new Comment()
            {
                CommentContext = "Quis hendrerit dolor magna eget est lorem ipsum dolor sit.",
                CommentPosted = new DateTime(2020, 01, 20),
                UserId = member1.Id,
                CommentApproved = true,
                Blog = post1
            };
            context.Comments.Add(comment3);

            var comment4 = new Comment()
            {
                CommentContext = "Eu feugiat pretium nibh ipsum consequat nisl. Aenean pharetra magna ac placerat vestibulum lectus mauris.",
                CommentPosted = new DateTime(2021, 02, 02),
                UserId = member5.Id,
                CommentApproved = true,
                Blog = post2
            };
            context.Comments.Add(comment4);

            var comment5 = new Comment()
            {
                CommentContext = "Vitae nunc sed velit dignissim sodales. Aliquam ultrices sagittis orci a scelerisque purus semper.",
                CommentPosted = new DateTime(2021, 03, 29),
                UserId = member4.Id,
                CommentApproved = true,
                Blog = post3
            };
            context.Comments.Add(comment5);

            var comment6 = new Comment()
            {
                CommentContext = "Placerat duis ultricies lacus sed turpis tincidunt id aliquet. Molestie ac feugiat sed lectus vestibulum. Mauris sit amet massa vitae tortor condimentum.",
                CommentPosted = new DateTime(2020, 11, 01),
                UserId = member3.Id,
                CommentApproved = true,
                Blog = post3
            };
            context.Comments.Add(comment6);

            var comment7 = new Comment()
            {
                CommentContext = "Malesuada fames ac turpis egestas integer eget aliquet nibh praesent. Sed elementum tempus egestas sed sed risus pretium quam vulputate.",
                CommentPosted = new DateTime(2020, 07, 17),
                UserId = member2.Id,
                CommentApproved = true,
                Blog = post3
            };
            context.Comments.Add(comment7);

            var comment8 = new Comment()
            {
                CommentContext = "Egestas erat imperdiet sed euismod nisi porta lorem mollis aliquam. Vitae ultricies leo integer malesuada nunc vel.",
                CommentPosted = new DateTime(2020, 09, 28),
                UserId = member1.Id,
                CommentApproved = true,
                Blog = post3
            };
            context.Comments.Add(comment8);

            var comment9 = new Comment()
            {
                CommentContext = "Viverra accumsan in nisl nisi scelerisque eu ultrices vitae. Sit amet nisl suscipit adipiscing bibendum est ultricies.",
                CommentPosted = new DateTime(2020, 03, 04),
                UserId = member5.Id,
                CommentApproved = true,
                Blog = post3
            };
            context.Comments.Add(comment9);

            var comment10 = new Comment()
            {
                CommentContext = "Rutrum tellus pellentesque eu tincidunt tortor. Lectus vestibulum mattis ullamcorper velit sed. Sit amet consectetur adipiscing elit ut aliquam.",
                CommentPosted = new DateTime(2021, 05, 01),
                UserId = member4.Id,
                CommentApproved = true,
                Blog = post4
            };
            context.Comments.Add(comment10);

            var comment11 = new Comment()
            {
                CommentContext = "Eu turpis egestas pretium aenean pharetra magna ac placerat vestibulum. Diam ut venenatis tellus in metus.",
                CommentPosted = new DateTime(2021, 01, 25),
                UserId = member2.Id,
                CommentApproved = true,
                Blog = post4
            };
            context.Comments.Add(comment11);

            var comment12 = new Comment()
            {
                CommentContext = "Commodo viverra maecenas accumsan lacus vel facilisis. Vitae sapien pellentesque habitant morbi tristique senectus et. Amet aliquam id diam maecenas ultricies",
                CommentPosted = new DateTime(2021, 05, 30),
                UserId = member3.Id,
                CommentApproved = true,
                Blog = post5
            };
            context.Comments.Add(comment12);

            var comment13 = new Comment()
            {
                CommentContext = "Sagittis purus sit amet volutpat consequat mauris. Nullam non nisi est sit amet facilisis magna. Suspendisse sed nisi lacus sed viverra tellus in hac habitasse.",
                CommentPosted = new DateTime(2020, 07, 27),
                UserId = member2.Id,
                CommentApproved = true,
                Blog = post5
            };
            context.Comments.Add(comment13);

            var comment14 = new Comment()
            {
                CommentContext = "Elementum tempus egestas sed sed.",
                CommentPosted = new DateTime(2020, 11, 30),
                UserId = member1.Id,
                CommentApproved = true,
                Blog = post5
            };
            context.Comments.Add(comment14);

            var comment15 = new Comment()
            {
                CommentContext = "Tincidunt praesent semper feugiat nibh sed pulvinar proin gravida hendrerit. Tincidunt lobortis feugiat vivamus at augue eget arcu. Ut placerat orci nulla pellentesque.",
                CommentPosted = new DateTime(2021, 02, 27),
                UserId = member5.Id,
                CommentApproved = true,
                Blog = post6
            };
            context.Comments.Add(comment15);

            var comment16 = new Comment()
            {
                CommentContext = "Aliquet porttitor lacus luctus accumsan tortor. Sagittis nisl rhoncus mattis rhoncus urna neque. Imperdiet dui accumsan sit amet nulla facilisi morbi tempus iaculis. Amet nisl purus in mollis nunc.",
                CommentPosted = new DateTime(2020, 11, 18),
                UserId = member3.Id,
                CommentApproved = true,
                Blog = post7
            };
            context.Comments.Add(comment16);

            var comment17 = new Comment()
            {
                CommentContext = "Mi sit amet mauris commodo quis imperdiet. Tortor id aliquet lectus proin.",
                CommentPosted = new DateTime(2020, 10, 07),
                UserId = member4.Id,
                CommentApproved = true,
                Blog = post7
            };
            context.Comments.Add(comment17);

            var comment18 = new Comment()
            {
                CommentContext = "Aliquet porttitor lacus luctus accumsan tortor. Sagittis nisl rhoncus mattis rhoncus urna neque. Imperdiet dui accumsan sit amet nulla facilisi morbi tempus iaculis. Amet nisl purus in mollis nunc.",
                CommentPosted = new DateTime(2020, 12, 05),
                UserId = member2.Id,
                CommentApproved = true,
                Blog = post7
            };
            context.Comments.Add(comment18);

            var comment19 = new Comment()
            {
                CommentContext = "Nulla pellentesque dignissim enim sit amet. Morbi tempus iaculis urna id volutpat lacus.",
                CommentPosted = new DateTime(2021, 01, 10),
                UserId = member5.Id,
                CommentApproved = true,
                Blog = post8
            };
            context.Comments.Add(comment19);

            var comment20 = new Comment()
            {
                CommentContext = "Eu scelerisque felis imperdiet proin fermentum leo vel. Nulla facilisi etiam dignissim diam quis enim lobortis scelerisque. In ante metus dictum at tempor commodo.",
                CommentPosted = new DateTime(2021, 02, 04),
                UserId = member1.Id,
                CommentApproved = true,
                Blog = post8
            };
            context.Comments.Add(comment20);

            var comment21 = new Comment()
            {
                CommentContext = "A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine.",
                CommentPosted = new DateTime(2021, 02, 04),
                UserId = member7.Id,
                CommentApproved = true,
                Blog = post9
            };
            context.Comments.Add(comment21);

            var comment22 = new Comment()
            {
                CommentContext = "I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil existence, that I neglect my talents.",
                CommentPosted = new DateTime(2021, 02, 04),
                UserId = member8.Id,
                CommentApproved = true,
                Blog = post9
            };
            context.Comments.Add(comment22);

            var comment23 = new Comment()
            {
                CommentContext = "I should be incapable of drawing a single stroke at the present moment; and yet I feel that I never was a greater artist than now.",
                CommentPosted = new DateTime(2021, 02, 04),
                UserId = member10.Id,
                CommentApproved = true,
                Blog = post10
            };
            context.Comments.Add(comment23);

            var comment24 = new Comment()
            {
                CommentContext = "When, while the lovely valley teems with vapour around me, and the meridian sun strikes the upper surface of the impenetrable foliage of my trees, and but a few stray gleams steal into the inner sanctuary",
                CommentPosted = new DateTime(2021, 02, 04),
                UserId = member10.Id,
                CommentApproved = true,
                Blog = post10
            };
            context.Comments.Add(comment24);

            var comment25 = new Comment()
            {
                CommentContext = "I throw myself down among the tall grass by the trickling stream; and, as I lie close to the earth, a thousand unknown plants are noticed by me, when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects and flies.",
                CommentPosted = new DateTime(2021, 04, 17),
                UserId = mod2.Id,
                CommentApproved = true,
                Blog = post5
            };
            context.Comments.Add(comment25);

            var comment26 = new Comment()
            {
                CommentContext = "It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.",
                CommentPosted = new DateTime(2020, 06, 30),
                UserId = staff1.Id,
                CommentApproved = true,
                Blog = post7
            };
            context.Comments.Add(comment26);

            var comment27 = new Comment()
            {
                CommentContext = "If several languages coalesce, the grammar of the resulting language is more simple and regular than that of the individual languages. The new common language will be more simple and regular than the existing European languages.",
                CommentPosted = new DateTime(2021, 05, 26),
                UserId = staff3.Id,
                CommentApproved = true,
                Blog = post8
            };
            context.Comments.Add(comment27);

            var comment28 = new Comment()
            {
                CommentContext = "Li Europan lingues es membres del sam familie. Lor separat existentie es un myth. Por scientie, musica, sport etc, litot Europa usa li sam vocabular. Li lingues differe solmen in li grammatica, li pronunciation e li plu commun vocabules.",
                CommentPosted = new DateTime(2021, 02, 04),
                UserId = mod2.Id,
                CommentApproved = true,
                Blog = post11
            };
            context.Comments.Add(comment28);

            var comment29 = new Comment()
            {
                CommentContext = "Omnicos directe al desirabilite de un nov lingua franca: On refusa continuar payar custosi traductores. At solmen va esser necessi far uniform grammatica, pronunciation e plu sommun paroles.",
                CommentPosted = new DateTime(2021, 02, 04),
                UserId = staff5.Id,
                CommentApproved = true,
                Blog = post11
            };
            context.Comments.Add(comment29);

            var comment30 = new Comment()
            {
                CommentContext = "Li nov lingua franca va esser plu simplic e regulari quam li existent Europan lingues. It va esser tam simplic quam Occidental in fact, it va esser Occidental.",
                CommentPosted = new DateTime(2021, 04, 17),
                UserId = mod2.Id,
                CommentApproved = true,
                Blog = post12
            };
            context.Comments.Add(comment30);

            var comment31 = new Comment()
            {
                CommentContext = "Omnicos directe al desirabilite de un nov lingua franca: On refusa continuar payar custosi traductores. At solmen va esser necessi far uniform grammatica, pronunciation e plu sommun paroles.",
                CommentPosted = new DateTime(2020, 06, 30),
                UserId = staff5.Id,
                CommentApproved = true,
                Blog = post12
            };
            context.Comments.Add(comment31);

            var comment32 = new Comment()
            {
                CommentContext = "Ma quande lingues coalesce, li grammatica del resultant lingue es plu simplic e regulari quam ti del coalescent lingues. Li nov lingua franca va esser plu simplic e regulari quam li existent Europan lingues.",
                CommentPosted = new DateTime(2021, 05, 26),
                UserId = member7.Id,
                CommentApproved = true,
                Blog = post12
            };
            context.Comments.Add(comment32);

            //--------------------------------------------------UNAPPROVED COMMENTS--------------------------------------------
            var comment93 = new Comment()
            {
                CommentContext = "One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed in his bed into a horrible vermin.",
                CommentPosted = new DateTime(2021, 04, 19),
                UserId = member2.Id,
                CommentApproved = false,
                Blog = post6
            };
            context.Comments.Add(comment93);

            var comment94 = new Comment()
            {
                CommentContext = "He lay on his armour-like back, and if he lifted his head a little he could see his brown belly, slightly domed and divided by arches into stiff sections.",
                CommentPosted = new DateTime(2021, 01, 30),
                UserId = member10.Id,
                CommentApproved = false,
                Blog = post2
            };
            context.Comments.Add(comment94);

            var comment95 = new Comment()
            {
                CommentContext = "The bedding was hardly able to cover it and seemed ready to slide off any moment. His many legs, pitifully thin compared with the size of the rest of him, waved about helplessly as he looked.",
                CommentPosted = new DateTime(2021, 04, 19),
                UserId = member9.Id,
                CommentApproved = false,
                Blog = post6
            };
            context.Comments.Add(comment95);

            var comment96 = new Comment()
            {
                CommentContext = "What's happened to me? he thought. It wasn't a dream. His room, a proper human room although a little too small, lay peacefully between its four familiar walls.",
                CommentPosted = new DateTime(2021, 01, 30),
                UserId = member5.Id,
                CommentApproved = false,
                Blog = post5
            };
            context.Comments.Add(comment96);

            var comment97 = new Comment()
            {
                CommentContext = "A collection of textile samples lay spread out on the table - Samsa was a travelling salesman - and above it there hung a picture that he had recently cut out of an illustrated magazine and housed in a nice, gilded frame.",
                CommentPosted = new DateTime(2021, 01, 30),
                UserId = member3.Id,
                CommentApproved = false,
                Blog = post12
            };
            context.Comments.Add(comment97);

            var comment98 = new Comment()
            {
                CommentContext = "It showed a lady fitted out with a fur hat and fur boa who sat upright, raising a heavy fur muff that covered the whole of her lower arm towards the viewer.",
                CommentPosted = new DateTime(2021, 01, 30),
                UserId = member7.Id,
                CommentApproved = false,
                Blog = post11
            };
            context.Comments.Add(comment98);

            var comment99 = new Comment()
            {
                CommentContext = "Gregor then turned to look out the window at the dull weather.",
                CommentPosted = new DateTime(2021, 01, 30),
                UserId = member10.Id,
                CommentApproved = false,
                Blog = post10
            };
            context.Comments.Add(comment99);

            context.SaveChanges();


            //--------------------------------------------------LIKES--------------------------------------------
            context.Likes.Add(new Like()
            {
                UserId = member1.Id,
                Blog = post1
            });

            context.Likes.Add(new Like()
            {
                UserId = member2.Id,
                Blog = post1
            });

            context.Likes.Add(new Like()
            {
                UserId = staff1.Id,
                Blog = post1
            });

            context.Likes.Add(new Like()
            {
                UserId = member1.Id,
                Blog = post2
            });

            context.Likes.Add(new Like()
            {
                UserId = member2.Id,
                Blog = post2
            });

            context.Likes.Add(new Like()
            {
                UserId = member3.Id,
                Blog = post3
            });

            context.Likes.Add(new Like()
            {
                UserId = member1.Id,
                Blog = post3
            });

            context.Likes.Add(new Like()
            {
                UserId = staff1.Id,
                Blog = post3
            });

            context.Likes.Add(new Like()
            {
                UserId = member4.Id,
                Blog = post4
            });

            context.Likes.Add(new Like()
            {
                UserId = member5.Id,
                Blog = post5
            });

            context.Likes.Add(new Like()
            {
                UserId = member3.Id,
                Blog = post5
            });

            context.Likes.Add(new Like()
            {
                UserId = member2.Id,
                Blog = post6
            });

            context.Likes.Add(new Like()
            {
                UserId = member10.Id,
                Blog = post7
            });

            context.Likes.Add(new Like()
            {
                UserId = member1.Id,
                Blog = post7
            });

            context.Likes.Add(new Like()
            {
                UserId = member5.Id,
                Blog = post7
            });

            context.Likes.Add(new Like()
            {
                UserId = staff1.Id,
                Blog = post8
            });

            context.Likes.Add(new Like()
            {
                UserId = member1.Id,
                Blog = post8
            });

            context.Likes.Add(new Like()
            {
                UserId = mod1.Id,
                Blog = post7
            });

            context.Likes.Add(new Like()
            {
                UserId = mod1.Id,
                Blog = post3
            });

            context.Likes.Add(new Like()
            {
                UserId = staff2.Id,
                Blog = post2
            });

            context.Likes.Add(new Like()
            {
                UserId = staff4.Id,
                Blog = post1
            });

            context.Likes.Add(new Like()
            {
                UserId = mod1.Id,
                Blog = post5
            });

            context.Likes.Add(new Like()
            {
                UserId = sysadmin.Id,
                Blog = post6
            });

            context.Likes.Add(new Like()
            {
                UserId = staff3.Id,
                Blog = post5
            });

            context.Likes.Add(new Like()
            {
                UserId = staff4.Id,
                Blog = post4
            });

            context.Likes.Add(new Like()
            {
                UserId = member10.Id,
                Blog = post9
            });

            context.Likes.Add(new Like()
            {
                UserId = mod2.Id,
                Blog = post10
            });

            context.Likes.Add(new Like()
            {
                UserId = member8.Id,
                Blog = post8
            });

            context.Likes.Add(new Like()
            {
                UserId = member6.Id,
                Blog = post5
            });

            context.Likes.Add(new Like()
            {
                UserId = staff5.Id,
                Blog = post10
            });

            context.Likes.Add(new Like()
            {
                UserId = staff5.Id,
                Blog = post9
            });

            context.Likes.Add(new Like()
            {
                UserId = sysadmin.Id,
                Blog = post10
            });

            context.Likes.Add(new Like()
            {
                UserId = member3.Id,
                Blog = post9
            });

            context.Likes.Add(new Like()
            {
                UserId = member4.Id,
                Blog = post9
            });

            context.Likes.Add(new Like()
            {
                UserId = member7.Id,
                Blog = post1
            });

            context.Likes.Add(new Like()
            {
                UserId = member7.Id,
                Blog = post5
            });

            context.Likes.Add(new Like()
            {
                UserId = member7.Id,
                Blog = post10
            });

            context.Likes.Add(new Like()
            {
                UserId = member8.Id,
                Blog = post9
            });

            context.Likes.Add(new Like()
            {
                UserId = member8.Id,
                Blog = post7
            });

            context.Likes.Add(new Like()
            {
                UserId = member9.Id,
                Blog = post6
            });

            context.Likes.Add(new Like()
            {
                UserId = member9.Id,
                Blog = post2
            });

            context.Likes.Add(new Like()
            {
                UserId = member9.Id,
                Blog = post9
            });

            context.Likes.Add(new Like()
            {
                UserId = member7.Id,
                Blog = post12
            });

            context.Likes.Add(new Like()
            {
                UserId = member10.Id,
                Blog = post12
            });

            context.Likes.Add(new Like()
            {
                UserId = mod2.Id,
                Blog = post11
            });

            context.Likes.Add(new Like()
            {
                UserId = sysadmin.Id,
                Blog = post11
            });

            context.Likes.Add(new Like()
            {
                UserId = member10.Id,
                Blog = post11
            });

            context.SaveChanges();

        }
    }
}