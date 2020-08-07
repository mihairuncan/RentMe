using Microsoft.AspNetCore.Http;
using System;

namespace RentMe.ViewModels
{
    public class PhotoForCreation
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        public PhotoForCreation()
        {
            DateAdded = DateTime.Now;
        }
    }
}
