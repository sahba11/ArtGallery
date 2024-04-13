using Gallery.Models.BaseEntityModel;

namespace Gallery.Models.Entities
{
    public class Picture : BaseEntity<int>
    {
        public string Title { get; set; }
    }
}
