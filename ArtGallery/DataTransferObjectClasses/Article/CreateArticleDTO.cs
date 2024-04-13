using Gallery.DTO.BaseInterfaceAndClass;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Gallery.DTO.DataTransferObjectClasses.Article
{
    public class CreateArticleDTO : BaseDTO<int>
    {
        [DisplayName("عنوان ")]
        [Required(ErrorMessage = "عنون مقاله اجباری است")]
        public string Title { get; set; }
        [DisplayName("مسیر عکس ")]
        [Required(ErrorMessage = "عکس مقاله اجباری است")]
        public string Picture { get; set; }

        [DisplayName("Alt عکس ")]
        public string PictureAlt { get; set; }
        [DisplayName(" عنوان عکس ")]
        public string PictureTitle { get; set; }
        [DisplayName("توضیحات کوتاه ")]
        [Required(ErrorMessage = "توضیحات مقاله اجباری است")]

        public string ShortDescription { get; set; }
        [DisplayName("متن مقاله ")]
        public string Body { get; set; }

        [Display(Name = "متن فایل")]
        [Required(ErrorMessage = "لطفا یک عکس انتخاب نمایید")]
        public List<IFormFile> FormFiles { get; set; }
    }
}
