using AutoMapper;
using Gallery.DTO.BaseInterfaceAndClass;
using Gallery.Models.BaseEntityModel;

namespace Gallery.Services.BaseInterfaceAndClass
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            List<Type> allEntities = (typeof(BaseEntity<>)).Assembly.ExportedTypes.Where(x => x.IsClass && !x.IsAbstract && x != typeof(BaseEntity) && x.BaseType.Name != nameof(Object)).ToList();
            List<Type> allDtos = (typeof(BaseDTO<>)).Assembly.ExportedTypes.Where(x => x.IsClass && !x.IsAbstract && x != typeof(BaseDTO) && x.BaseType.Name != nameof(BaseDTO) && x.BaseType.Name != nameof(Object)).ToList();

            foreach (Type? entity in allEntities)
            {
                Type? dtoMapToEntity = allDtos.FirstOrDefault(x => x.Name.Contains(entity.Name));
                CreateMap(entity, dtoMapToEntity).ReverseMap();
            }
        }
    }
}
