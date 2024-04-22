using GlimpseHub.Data.Models.Base;
using GlimpseHub.Data.Models.Enum;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace GlimpseHub.Data.Models
{
    public class Rating: BaseEntity<int>
    {
        public Grade Ratings { get; set; }    
      
        public string AppUserId {  get; set; }
        public virtual AppUser User { get; set; }

        public int GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; } 

    }
}
