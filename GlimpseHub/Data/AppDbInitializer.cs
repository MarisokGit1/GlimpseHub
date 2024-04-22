using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GlimpseHub.Models;
using GlimpseHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using GlimpseHub.Contracts;
using GlimpseHub.Data.Models.Enum;

namespace GlimpseHub.Data
{
    public class AppDbInitializer : IDbSeeder
    {
        private List<Category> categories;
        private List<Gallery> galleries;
        private List<AppUser> users;

        private readonly ApplicationDbContext db;
        private readonly UserManager<AppUser> uManager;
        private readonly RoleManager<IdentityRole> rManager;

        public AppDbInitializer(ApplicationDbContext db, UserManager<AppUser> uManager, RoleManager<IdentityRole> rManager)
        {
            this.db = db;
            this.uManager = uManager;
            this.rManager = rManager;

            users = new List<AppUser>()
            {
                new AppUser("marin@admin.com","https://i.ytimg.com/vi/u6ak1wB5N8c/maxresdefault.jpg"),
                new AppUser("marin@member.com","https://c8.alamy.com/comp/DXE4CP/despicable-me-2-DXE4CP.jpg")
            };


            categories = new List<Category>()
                    {
                        new Category()
                        {
                         Name = "Animals",
                         CreatedOn = DateTime.Now.AddMonths(-2),
                          Description = "You can post all your pet pictures under on this category!",
                          IsDeleted = false,
                          PictureUrl="https://www.gbzoo.com/resources/media/user/1689619253-Featured-Orangutan-01_1920x1050_desktop.jpg"
                        },
                        new Category()
                        {
                         Name = "Fashion",
                         CreatedOn = DateTime.Now.AddMonths(-2).Date,
                         Description = "If you feel like your outfit today looks fashionable you can post it under this category!",
                         IsDeleted = false,
                         PictureUrl="https://media.voguebusiness.com/photos/642c3460706ee157689b66bd/master/pass/ai-fashion-week-voguebus-story.jpg"
                        },
                        new Category()
                        {
                         Name =  "Art",
                         CreatedOn = DateTime.Now.AddMonths(-2).Date,
                         Description = "Post something that would put a WOW expression on their face!",
                         IsDeleted = false,
                         PictureUrl="https://images.rawpixel.com/image_800/czNmcy1wcml2YXRlL3Jhd3BpeGVsX2ltYWdlcy93ZWJzaXRlX2NvbnRlbnQvcGR2YW5nb2doLXNlbGYtcG9ydHJhaXQtbTAxLWpvYjY2MV8yLWwxMDBvNmVmLmpwZw.jpg"
                        },
                        new Category()
                        {
                         Name = "Food",
                         CreatedOn = DateTime.Now.AddMonths(-2).Date,
                         Description = "Post something tasty under this category!",
                         IsDeleted = false,
                         PictureUrl="https://media.istockphoto.com/id/1457979959/photo/snack-junk-fast-food-on-table-in-restaurant-soup-sauce-ornament-grill-hamburger-french-fries.webp?b=1&s=170667a&w=0&k=20&c=A_MdmsSdkTspk9Mum_bDVB2ko0RKoyjB7ZXQUnSOHl0="
                        },
                        new Category()
                        {
                         Name = "Family",
                         CreatedOn = DateTime.Now.AddMonths(-2).Date,
                         Description = "Post something you can only experience with your own family!",
                         IsDeleted = false,
                         PictureUrl="https://bambiniphoto.sg/wp-content/uploads/family-photography-bambini-025.jpg"
                        },
                           new Category()
                        {
                         Name = "Games",
                         CreatedOn = DateTime.Now.AddMonths(-2).Date,
                         Description = "Post your pictures related to games",
                         IsDeleted = false,
                         PictureUrl="https://media.gq-magazine.co.uk/photos/645b5c3c8223a5c3801b8b26/16:9/w_1280,c_limit/100-best-games-hp-b.jpg"
                        },
                                 new Category()
                        {
                         Name = "Cars",
                         CreatedOn = DateTime.Now.AddMonths(-2).Date,
                         Description = "You can post cool car photos here",
                         IsDeleted = false,
                         PictureUrl="https://www.mercedes-benz.bg/content/bulgaria/bg/passengercars/models/coupe/amg-gt-2-door/overview/_jcr_content/root/responsivegrid/tabs_1864030011_copy/tabitem/simple_teaser/simple_teaser_item_1148938149.component.damq2.3393625603645.jpg/mercedes-amg-gt-c192-equipment-exterior-chromepackage-exterior-764x573-07-2023.jpg" },
            };

            galleries = new List<Gallery>()
                        {
                            new Gallery()
                            {
                             Name = "Pet Pictures",
                             CreatedOn = DateTime.Now.AddDays(-20).Date,
                             Description = "These are some photos of my pets!",
                             IsDeleted = false,
                             IsPrivate = false,
                             Category=categories[0],
                             Author=users[0],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                             , Pictures = "https://www.google.com/imgres?q=pet%20pictures&imgurl=https%3A%2F%2Fblogs.cdc.gov%2Fwp-content%2Fuploads%2Fsites%2F6%2F2020%2F05%2Fgolden_retiver_cat_cropped.jpg&imgrefurl=https%3A%2F%2Fblogs.cdc.gov%2Fpublichealthmatters%2F2020%2F05%2Fpet-week%2F&docid=-lw1uO_w9W3UeM&tbnid=O_6ARpNTLjkH0M&vet=12ahUKEwiJ4oasmsKFAxVLVfEDHTM8CwgQM3oECCEQAA..i&w=1238&h=554&hcb=2&ved=2ahUKEwiJ4oasmsKFAxVLVfEDHTM8CwgQM3oECCEQAA|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR6jsN-2AKB5AOLIuR8X1myKe5pdN0XPA715g&s|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRJ4BFuC61n9JpNTdHtccSX4NqGoRJFEKp3eQ&s|"
                             .Split("|").Select(url => new Picture(url)).ToArray(),
                             Status= Status.Open
                            },
                            new Gallery()
                            {
                              Name = "Family Pictures",
                             CreatedOn = DateTime.Now.AddDays(-10).Date,
                             Description = "My family pictures are stored in this gallery!",
                             IsDeleted = false,
                             IsPrivate = true,
                             Category=categories[4],
                             Author=users[0],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                             ,Pictures = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMIMelPEK5LyascnlMfSRYQd7_mfgPzzWYFA&s|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRPfpU_yVAIQBfBYNIZ5GtTAVZ9shKU38xVHw&s|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSPgOT7L0N-_EkXNcCSd4JEuC6NCNe7CW9RQ&s"
                             .Split("|").Select(url=>new Picture(url)).ToArray(),
                             Status=Status.Pending
                            },
                            new Gallery()
                            {
                              Name = "Food Pictures",
                             CreatedOn = DateTime.Now.AddDays(+5).Date,
                             Description = "This gallery is where I post my yummy stuff",
                             IsDeleted = false,
                             IsPrivate = false,
                             Category=categories[3],
                             Author=users[0],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                            ,Pictures = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSzYWcUVg_I6A6RSYQ-HKY4Szdq7tBFTc65Eg&s|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTgkT1Tkc95r-J9hCjCMf25dk_uhfgP7QX1Vw&s|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRpa_nbHvdVxELcL5wgzCMFNfXakQ_hrAGi0Q&s"
                            .Split("|").Select(url=>new Picture(url)).ToArray(),
                            Status= Status.Open
                            },
                            new Gallery()
                            {
                             Name = "My Art",
                             CreatedOn = DateTime.Now.AddDays(-5).Date,
                             Description = "This is my art!",
                             IsDeleted = false,
                             IsPrivate = false,
                             Category=categories[2],
                             Author=users[0],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                             ,Pictures = "  https://img.freepik.com/free-photo/multi-colored-creativity-close-up-human-eye-generated-by-ai_188544-15574.jpg|https://images.rawpixel.com/image_800/czNmcy1wcml2YXRlL3Jhd3BpeGVsX2ltYWdlcy93ZWJzaXRlX2NvbnRlbnQvcGR2YW5nb2doLXNlbGYtcG9ydHJhaXQtbTAxLWpvYjY2MV8yLWwxMDBvNmVmLmpwZw.jpg|https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_1280.jpg".Split("|").Select(url=>new Picture(url)).ToArray()
                            ,Status= Status.Pending
                            },
                             new Gallery()
                            {
                             Name = "Games",
                             CreatedOn = DateTime.Now.AddDays(-20).Date,
                             Description = "This is where I post my achievments about games",
                             IsDeleted = false,
                             IsPrivate = false,
                             Category=categories[5],
                             Author=users[1],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                             ,Pictures = "https://cdna.artstation.com/p/top_row_items/images/000/002/656/20231128111535/original/hcotm-keyart-890x500.jpg?1701191735|https://static1.cbrimages.com/wordpress/wp-content/uploads/2022/04/10-Indie-Games-With-The-Best-Looking-Pixel-Art.jpg|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiT5VnHZeywu4exqIk2YCAoJ69PodQ_7TdYwTszjNcIg&s".Split("|").Select(url=>new Picture(url)).ToArray()
                            , Status = Status.Closed
                             },
                            new Gallery()
                            {
                             Name = "Cars",
                             CreatedOn = DateTime.Now.AddDays(-10).Date,
                             Description = "I love cars and here is a collection of my favourite cars",
                             IsDeleted = false,
                             IsPrivate = false,
                             Category=categories[6],
                             Author=users[1],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                             ,Pictures = "https://imageio.forbes.com/specials-images/imageserve/5d35eacaf1176b0008974b54/2020-Chevrolet-Corvette-Stingray/0x0.jpg?format=jpg&crop=4560,2565,x790,y784,safe&width=960|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHn8NmUKx3Ca2nCVQBf5KPFeLG4kRyA6hCh9lcvq6o7A&s|https://autobild.bg/wp-content/uploads/2023/07/mercedes-amg-g63-grand-edition-3.jpg".Split("|").Select(url=>new Picture(url)).ToArray()
                            , Status= Status.Pending
                            },
                            new Gallery()
                            {
                             Name = "Motorcycle Pictures",
                             CreatedOn = DateTime.Now.AddDays(+5).Date,
                             Description = "Cool motorcycle pictures",
                             IsDeleted = false,
                             IsPrivate = false,
                             Category=categories[3],
                             Author=users[1],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                             ,Pictures = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRSJUHnO4IOtZuY_fMdVYVOXMHI2ePusPW6CQsUZKR9iA&s|https://andromedamoto.com/cdn/shop/articles/hypersport.jpg?v=1675194839|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTKIZ83xU7lFhBRpPq8oMZFKT0EUZ2n5bgtynRPeCaYfA&s"
                             .Split("|").Select(url => new Picture(url)).ToArray(),
                             Status= Status.Closed
                            },
                            new Gallery()
                            {
                             Name = "Sports",
                             CreatedOn = DateTime.Now.AddDays(-5).Date,
                             Description = "Some of the finest sports moments/pictures.",
                             IsDeleted = false,
                             IsPrivate = false,
                             Category=categories[2],
                             Author=users[1],
                             MainPicture = "https://d28jbe41jq1wak.cloudfront.net/BlogsImages/exploring-pop-art-defini-638237807747168508.jpg"
                            ,Pictures = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQh_o2CzLIBX-KKWqK9idgCo2VUmzF1OsM42A7UP5vidA&s|https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR2rLamnNwmLzxTOVCwYxcgJchuguLKa4XYuhENrNnZWQ&s|https://assets.bwbx.io/images/users/iqjWHBFdfxIU/iji_Mh_Y9JBY/v2/-1x-1.jpg".Split("|").Select(url => new Picture(url)).ToArray(),
                            Status= Status.Pending
                            }
                };
        }

        public async Task<bool> DBHasDataAsync()
        {
            return await rManager.Roles.AnyAsync();
        }

        public async Task SeedAllDataAsync()
        {
            await CreateRolesAndUsersAsync();
            await SeedCategoriesAsync();
            await SeedGalleriesAsync();
        }

        private async Task CreateRolesAndUsersAsync()
        {
            var roles = (new[] { "Admin", "Plebei" }).Select(x => new IdentityRole(x));
            foreach (var role in roles)
            {
                await rManager.CreateAsync(role);
            }

            await uManager.CreateAsync(users[0], "Maringotiniqt123,");
            await uManager.CreateAsync(users[1], "Maringotiniqt123,");
            await uManager.AddToRoleAsync(users[0], "Admin");
            await uManager.AddToRoleAsync(users[1], "Plebei");
        }
        //Categories
        private async Task SeedCategoriesAsync()
        {
            await db.Categories.AddRangeAsync(categories);
            await db.SaveChangesAsync();
        }
        //Galleries
        private async Task SeedGalleriesAsync()
        {
            await db.Galleries.AddRangeAsync(galleries);
            await db.SaveChangesAsync();
        }
    }
}
