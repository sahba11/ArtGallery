using Gallery.DTO.BaseInterfaceAndClass;

namespace Gallery.DTO.DataTransferObjectClasses.Article
{
    public class UpdateArticleDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string AltPicture { get; set; }
    }
}
