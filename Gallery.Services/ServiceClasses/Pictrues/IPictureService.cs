using Gallery.DTO.DataTransferObjectClasses.Picture;
using Gallery.Models.Entities;
using Gallery.Services.BaseInterfaceAndClass;

namespace Gallery.Services.ServiceClasses.Pictrues
{
    public interface IPictureService : IServiceBase<Picture, PictureDTO, int>
    { }
}
