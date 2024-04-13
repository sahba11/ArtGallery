using Gallery.DTO.BaseInterfaceAndClass;
using Gallery.Models.BaseEntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DTO.DataTransferObjectClasses.Article
{
    public class DeleteArticleDTO : BaseDTO<int>
    {
        public bool IsDeleted { get; set; }
    }
}
