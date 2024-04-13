using AutoMapper;
using Gallery.DTO.DataTransferObjectClasses.User;
using Gallery.Infrastructure.Repositories.Users;
using Gallery.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gallery.Services.ServiceClasses.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(SignInManager<User> signInManager, IMapper mapper, UserManager<User> userManager, IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public User TranslateToEntity(UserDTO model)
        {
            return _mapper.Map<User>(model);
        }

        public UserDTO TranslateToDTO(User entity)
        {
            return _mapper.Map<UserDTO>(entity);
        }

        public async Task<UserDTO> GeneralValidation(UserDTO model)
        {
            return await Task.Run(() => new UserDTO());
        }

        public async Task<UserDTO> CreateValidation(UserDTO model)
        {
            return await Task.Run(() => new UserDTO());
        }

        public async Task<UserDTO> UpdateValidation(UserDTO model)
        {
            return await Task.Run(() => new UserDTO());
        }

        public async Task<UserDTO> DeleteValidation(int id)
        {
            return await Task.Run(() => new UserDTO());
        }

        public async Task<UserDTO> RegisterUser(UserDTO user)
        {
            UserDTO result = await GeneralValidation(user);

            if (await result.HasError())
                return result;

            else
            {
                result = await CreateValidation(user);

                if (await result.HasError())
                    return result;

                User userEntity = TranslateToEntity(user);
                userEntity.UserName = user.MobileNumber;
                userEntity.CreateDateTime = DateTime.Now;
                userEntity.UpdateDateTime = DateTime.Now;

                IdentityResult finalResult = await _userManager.CreateAsync(userEntity, user.Password);
                if (finalResult.Errors.Any())
                {
                    List<string> errors = new List<string>();
                    finalResult.Errors.ToList().ForEach(error => errors.Add(error.Description));
                    await user.SetError(errors);
                    return user;
                }
                return user;
            }
        }
    }
}
