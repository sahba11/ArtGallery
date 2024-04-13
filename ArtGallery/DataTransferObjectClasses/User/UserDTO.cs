using Gallery.DTO.BaseInterfaceAndClass;

namespace Gallery.DTO.DataTransferObjectClasses.User
{
    public class UserDTO : BaseDTO<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string? NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? UserConfirmed { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public Guid? CreateUserId { get; set; }
        public Guid? LastUpdateUserId { get; set; }
    }
}
