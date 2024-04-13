using Gallery.DTO.DataTransferObjectClasses.User;
using Gallery.Models.Entities;

namespace Gallery.Services.ServiceClasses.Users
{
    public interface IUserService
    {
        User TranslateToEntity(UserDTO model);
        UserDTO TranslateToDTO(User entity);
        Task<UserDTO> GeneralValidation(UserDTO model);
        Task<UserDTO> CreateValidation(UserDTO model);
        Task<UserDTO> UpdateValidation(UserDTO model);
        Task<UserDTO> DeleteValidation(int id);
        Task<UserDTO> RegisterUser(UserDTO user);
    }
}
