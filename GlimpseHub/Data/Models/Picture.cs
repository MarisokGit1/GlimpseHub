using GlimpseHub.Data.Models.Base;
using System.Security.Policy;

namespace GlimpseHub.Data.Models
{
    public class Picture
    {
      
        public Picture()
        {
            PictureUserLikes = new HashSet<UserLikePicture>();
            Id = Guid.NewGuid().ToString();
        }

        public Picture(string pictureUrl):this()
        {
            PictureUrl = pictureUrl;
        }
        public string Id { get; set; }
        public string PictureUrl { get; set; }      
        public int GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual ICollection<UserLikePicture>PictureUserLikes { get; set; }
    }
}
