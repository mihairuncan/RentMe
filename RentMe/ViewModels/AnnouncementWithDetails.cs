using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public ICollection<PhotoForDetailed> Photos { get; set; }
    }
}
