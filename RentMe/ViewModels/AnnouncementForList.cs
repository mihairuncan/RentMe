using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.ViewModels
{
    public class AnnouncementForList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float RentPrice { get; set; }
        public string RentPeriod { get; set; }
        public DateTime AddedOn { get; set; }
        public bool IsApproved { get; set; }
        public string SubcategoryName { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}
