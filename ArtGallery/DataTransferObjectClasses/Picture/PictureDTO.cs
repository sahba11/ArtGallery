using Gallery.DTO.BaseInterfaceAndClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DTO.DataTransferObjectClasses.Picture
{
    public class PictureDTO : BaseDTO<int>
    {
        public string Title { get; set; }
    }
}
