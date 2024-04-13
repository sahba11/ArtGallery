using Gallery.DTO.BaseInterfaceAndClass;

namespace Gallery.DTO.DataTransferObjectClasses.Role
{
    public class RoleDTO : BaseDTO<Guid>
    {
        public string RoleEnglishName { get; set; }
        public string RolePersianName { get; set; }
    }
}
