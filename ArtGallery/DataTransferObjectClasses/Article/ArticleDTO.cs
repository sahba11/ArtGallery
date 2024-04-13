using Gallery.DTO.BaseInterfaceAndClass;

namespace Gallery.DTO.DataTransferObjectClasses.Article
{
    public class ArticleDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
    }
}
