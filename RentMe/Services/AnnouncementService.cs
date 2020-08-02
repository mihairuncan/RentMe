using RentMe.Data;
using RentMe.Models;
using System.Threading.Tasks;

namespace RentMe.Services
{

    public interface IAnnouncementService
    {
        public Task AddAnnouncement(Announcement announcement);
    }

    public class AnnouncementService : IAnnouncementService
    {
        private readonly DatabaseContext _context;
        public AnnouncementService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAnnouncement(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();
        }
    }
}
