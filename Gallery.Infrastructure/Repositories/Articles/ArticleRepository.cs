using Gallery.Infrastructure;
using Gallery.Infrastructure.BaseInterfaceAndClass;
using Gallery.Models.Entities.ArticleEntity;

namespace Gallery.Infrastructrue.Repositories.Articles
{
    public interface IArticleRepository : IBaseRepository<Article> { }

    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(GalleryDbContext readContext, GalleryDbContext writeContext) : base(readContext, writeContext) { }
    }
}
