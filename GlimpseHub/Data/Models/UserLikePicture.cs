namespace GlimpseHub.Data.Models
{
    public class UserLikePicture
    {
        public string AppUserId { get; set; }
        public virtual AppUser User { get; set; }   
        public string PictureId { get; set; }
        public virtual Picture Picture { get; set; }

    }
}
