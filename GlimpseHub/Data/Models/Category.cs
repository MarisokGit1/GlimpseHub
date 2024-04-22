using GlimpseHub.Data.Models.Base;

namespace GlimpseHub.Data.Models
{
    public class Category : BaseEntity<int>
    {
        public Category()
        {
            Galleries = new HashSet<Gallery>();
        }
        public string PictureUrl {  get; set; }
        public string Description { get; set; }   
        public virtual ICollection<Gallery> Galleries { get; set;}
    }
}
