using Gallery.Infrastructure.BaseInterfaceAndClass;
using Gallery.Models.Entities;

namespace Gallery.Infrastructure.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User> { }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GalleryDbContext readContext, GalleryDbContext writeContext) : base(readContext, writeContext) { }
    }
}
