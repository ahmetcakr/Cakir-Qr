using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.Application.Features.ItemImages.Requests
{
    public class CreateItemImageRequest
    {
        public int ItemId { get; set; }
        public IFormFile Image { get; set; }
        public string? Description { get; set; }
    }
}
