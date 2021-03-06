﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.Services;
using RentMe.ViewModels;
using System;
using System.Collections.Generic;
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
            IMapper mapper
            )
        {
            _announcementService = announcementService;
            _subcategoryService = subcategoryService;
            _mapper = mapper;
        }

        [HttpPost]
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

        [HttpPut("{announcementId}")]
        public async Task<IActionResult> UpdateAnnouncement(Guid announcementId, AnnouncementForAdd announcementForAdd)
        {


            var announcement = _mapper.Map<Announcement>(announcementForAdd);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var subcategoy = await _subcategoryService.GetSubcategoryByName(announcementForAdd.SubcategoryName);

            if (subcategoy == null)
            {
                return BadRequest("Invalid subcategory");
            }

            announcement.Subcategory = subcategoy;
            try
            {
                await _announcementService.UpdateAnnouncement(userId, announcementId, announcement);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(new { announcementId });
        }

        [HttpGet("{id}", Name = "GetAnnouncement")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnnouncement(Guid id)
        {
            var announcement = await _announcementService.GetAnnouncementById(id);

            if (announcement == null)
            {
                return BadRequest("Invalid Request");
            }

            var announcementToReturn = _mapper.Map<AnnouncementWithDetails>(announcement);

            return Ok(announcementToReturn);
        }


        [HttpGet("subcategory/{subcategoryName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnnouncementsBySubcategory(string subcategoryName, [FromQuery] AnnouncementParams announcementParams)
        {
            var announcements = await _announcementService.GetAnnouncementsBySubcategory(subcategoryName, announcementParams);

            var announcementsForReturn = _mapper.Map<IEnumerable<AnnouncementForList>>(announcements);

            Response.AddPagination(announcements.CurrentPage, announcements.PageSize,
                announcements.TotalCount, announcements.TotalPages);

            return Ok(announcementsForReturn);
        }

        [HttpGet("myAnnouncements")]
        public async Task<IActionResult> GetAnnouncementsByUserId([FromQuery] AnnouncementParams announcementParams)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var announcements = await _announcementService.GetAnnouncementsByUserId(userId, announcementParams);

            var announcementsForReturn = _mapper.Map<IEnumerable<AnnouncementForList>>(announcements);

            Response.AddPagination(announcements.CurrentPage, announcements.PageSize,
                announcements.TotalCount, announcements.TotalPages);

            return Ok(announcementsForReturn);
        }

        [HttpGet("unapproved")]
        [Authorize(Policy = "ModerateRole")]
        public async Task<IActionResult> GetUnapprovedAnnouncements([FromQuery] AnnouncementParams announcementParams)
        {
            var announcements = await _announcementService.GetUnapprovedAnnouncements(announcementParams);

            var announcementsForReturn = _mapper.Map<IEnumerable<AnnouncementForList>>(announcements);

            Response.AddPagination(announcements.CurrentPage, announcements.PageSize,
                announcements.TotalCount, announcements.TotalPages);

            return Ok(announcementsForReturn);
        }

        [HttpPut("{announcementId}/approve")]
        [Authorize(Policy = "ModerateRole")]
        public async Task<IActionResult> ApproveAnnouncement(Guid announcementId)
        {
            var approved = await _announcementService.ApproveAnnouncement(announcementId);

            if (approved)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{announcementId}/reject")]
        [Authorize(Policy = "ModerateRole")]
        public async Task<IActionResult> RejectAnnouncement(Guid announcementId)
        {
            var rejected = await _announcementService.DeleteAnnouncement(announcementId);

            if (rejected)
            {
                return NoContent();
            }

            return BadRequest();
        }


    }
}
