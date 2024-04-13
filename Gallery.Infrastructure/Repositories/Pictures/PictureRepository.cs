using Gallery.Infrastructure.BaseInterfaceAndClass;
using Gallery.Models.Entities;

namespace Gallery.Infrastructure.Repositories.Pictures
{
    public interface IPictureRepository : IBaseRepository<Picture> { }
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        public PictureRepository(GalleryDbContext readContext, GalleryDbContext writeContext) : base(readContext, writeContext) { }
    }
}
