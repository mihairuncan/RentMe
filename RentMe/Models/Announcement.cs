using System;
using System.Collections.Generic;

namespace RentMe.Models
{
    public class Announcement
    {
        public Announcement()
        {
            AddedOn = DateTime.Now;
            IsApproved = false;
            IsActive = true;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float RentPrice { get; set; }
        public string RentPeriod { get; set; }
        public DateTime AddedOn { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public User PostedBy { get; set; }
        public string PostedById { get; set; }
        public Subcategory Subcategory { get; set; }
        public Guid SubcategoryId { get; set; }
        public ICollection<Photo> Photos { get; set; }

    }
}
