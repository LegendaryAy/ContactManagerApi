using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.DTO
{
    public class PhotoToAddDto
    {
        public IFormFile PhotoFile { get; set; }
        public string Alt { get; set; } 

    }
}