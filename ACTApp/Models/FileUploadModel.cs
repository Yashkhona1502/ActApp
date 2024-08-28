using System;
using System.ComponentModel.DataAnnotations;

namespace ACTApp.Models
{
    public class FileUploadModel
    {
        [Required(ErrorMessage = "Please Select File")]
        public IFormFile File
        {
            get;
            set;
        }
    }
}

