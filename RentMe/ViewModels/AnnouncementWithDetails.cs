using System;
using System.Collections.Generic;

namespace RentMe.ViewModels
{
    public class AnnouncementWithDetails
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float RentPrice { get; set; }
        public string RentPeriod { get; set; }
        public DateTime AddedOn { get; set; }
        public string PostedByName { get; set; }
        public string PostedById { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsApproved { get; set; }
        public ICollection<PhotoForDetailed> Photos { get; set; }

    }
}
