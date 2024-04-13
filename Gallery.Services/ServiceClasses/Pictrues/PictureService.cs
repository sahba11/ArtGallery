using AutoMapper;
using Gallery.DTO.DataTransferObjectClasses.Picture;
using Gallery.Infrastructure.Repositories.Pictures;
using Gallery.Models.Entities;
using Gallery.Services.BaseInterfaceAndClass;

namespace Gallery.Services.ServiceClasses.Pictrues
{
    public class PictureService : ServiceBase<Picture, PictureDTO, int>, IPictureService
    {
        private readonly IMapper _mapper;

        public PictureService(IMapper mapper, IPictureRepository repository) : base(repository)
        {
            _mapper = mapper;
        }

        public override Picture TranslateToEntity(PictureDTO model)
        {
            return _mapper.Map<Picture>(model);
        }

        public override PictureDTO TranslateToDTO(Picture entity)
        {
            return _mapper.Map<PictureDTO>(entity);
        }

        public override async Task<PictureDTO> GeneralValidation(PictureDTO model)
        {
            return await Task.Run(() => new PictureDTO());
        }

        public override async Task<PictureDTO> CreateValidation(PictureDTO model)
        {
            return await Task.Run(() => new PictureDTO());
        }

        public override async Task<PictureDTO> UpdateValidation(PictureDTO model)
        {
            return await Task.Run(() => new PictureDTO());
        }

        public override async Task<PictureDTO> DeleteValidation(int id)
        {
            return await Task.Run(() => new PictureDTO());
        }
    }
}
