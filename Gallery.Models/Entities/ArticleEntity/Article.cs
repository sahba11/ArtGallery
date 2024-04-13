using Gallery.Models.BaseEntityModel;

namespace Gallery.Models.Entities.ArticleEntity
{
    public class Article : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public bool IsDeleted { get; set; }
    }
}
