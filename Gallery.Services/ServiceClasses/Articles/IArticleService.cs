using Gallery.DTO.DataTransferObjectClasses.Article;
using Gallery.Models.Entities.ArticleEntity;
using Gallery.Services.BaseInterfaceAndClass;

namespace Gallery.Services.ServiceClasses.Articles
{
    public interface IArticleService : IServiceBase<Article, ArticleDTO, int>
    {
        Task<ArticleDTO> CreateArticle(CreateArticleDTO articleDto);
        Task<List<ArticleDTO>> GetArticlesAsList();
        Task<ArticleDTO> UpdateArticle(UpdateArticleDTO articleDto);
        Task<ArticleDTO> DeleteArticle(DeleteArticleDTO articleDto);

    }
}
