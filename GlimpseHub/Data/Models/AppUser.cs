using Microsoft.AspNetCore.Identity;

namespace GlimpseHub.Data.Models
{
    public class AppUser : IdentityUser<string>
    {
        public AppUser(string email,string userPicUrl):this()
        {
            Email = email;
            UserName = email;
            ProfilePicture = userPicUrl;
        }

        public AppUser()
        {
            Id= Guid.NewGuid().ToString();
            Galleries = new HashSet<Gallery>();
            Ratings = new HashSet<Rating>();
            UserPictureLikes = new HashSet<UserLikePicture>();
        }
        public string ProfilePicture { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<UserLikePicture> UserPictureLikes { get; set; }
    }
}
