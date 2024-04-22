using GlimpseHub.Data.Models.Base;
using GlimpseHub.Data.Models.Enum;

namespace GlimpseHub.Data.Models
{
    public class Gallery : BaseEntity<int>
    {
        public Gallery()
        {
            Pictures = new HashSet<Picture>();
            Ratings = new HashSet<Rating>();
            Status = Status.Pending;
        }
        public string MainPicture { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public string AppUserId { get; set; }
        public int CategoryId { get; set; } 
        public virtual AppUser Author { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public virtual Category Category { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }


    }
}
