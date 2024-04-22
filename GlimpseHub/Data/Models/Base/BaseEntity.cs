namespace GlimpseHub.Data.Models.Base
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
