using Gallery.Infrastructure;
using Gallery.Infrastructure.BaseInterfaceAndClass;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Gallery.Services.BaseInterfaceAndClass
{
    public class RegisterServices
    {
        private readonly IServiceCollection _services;
        public RegisterServices(IServiceCollection services)
        {
            _services = services;
            var allRepositories = typeof(BaseRepository<>).Assembly.ExportedTypes.Where(x => x.IsClass && !x.IsAbstract && x != typeof(BaseRepository) && x.BaseType.Name != nameof(Object) && x.Name != nameof(MappingProfile) && x.Name != nameof(GalleryDbContext) && x.BaseType.Name != nameof(Migration)).ToList();
            foreach (var register in allRepositories)
            {
                _services.AddScoped(register.GetInterface($"I{register.Name}"), register);
            }

            var allServiceBaseClasses = typeof(ServiceBase<,,>).Assembly.ExportedTypes.Where(x => x.IsClass && !x.IsAbstract && x != typeof(ServiceBase) && x.BaseType.Name != nameof(Object) && x.Name != nameof(MappingProfile)).ToList();
            foreach (var register in allServiceBaseClasses)
            {
                _services.AddScoped(register.GetInterface($"I{register.Name}"), register);
            }
        }

        public void AddCustomRegister(Type serviceType, Type implementedType) => _services.AddScoped(serviceType, implementedType);
    }
}
