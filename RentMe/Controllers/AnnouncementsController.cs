using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentMe.Models;
using RentMe.Services;
using RentMe.ViewModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly IMapper _mapper;

        public AnnouncementsController(
            IAnnouncementService announcementService,
            ISubcategoryService subcategoryService,
            IMapper mapper)
        {
            _announcementService = announcementService;
            _subcategoryService = subcategoryService;
            _mapper = mapper;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddAnnouncement(AnnouncementForAdd announcementForAdd)
        {
            var announcement = _mapper.Map<Announcement>(announcementForAdd);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var subcategoy = await _subcategoryService.GetSubcategoryByName(announcementForAdd.SubcategoryName);

            if (subcategoy == null)
            {
                return BadRequest("Invalid subcategory");
            }

            announcement.PostedById = userId;
            announcement.Subcategory = subcategoy;

            await _announcementService.AddAnnouncement(announcement);

            return Ok(new { announcementId = announcement.Id });
        }

        [HttpGet("{id}", Name = "GetAnnouncement")]
        public async Task<IActionResult> GetAnnouncement(Guid id)
        {
            var announcement = await _announcementService.GetAnnouncementById(id);

            if(announcement == null)
            {
                return BadRequest("Invalid Request");
            }

            var announcementToReturn = _mapper.Map<AnnouncementWithDetails>(announcement);

            return Ok(announcementToReturn);
        }


    }
}
