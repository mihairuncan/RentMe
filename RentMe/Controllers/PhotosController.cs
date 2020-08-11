using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.Services;
using RentMe.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentMe.Controllers
{

    [Produces("application/json")]
    [Route("api/announcements/{announcementId}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IMapper _mapper;


        public PhotosController(IAnnouncementService announcementService,
            IMapper mapper)
        {
            _announcementService = announcementService;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(Guid id)
        {
            var photoFromRepo = await _announcementService.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturn>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForAnnouncement(Guid announcementId,
          [FromForm] PhotoForCreation photoForCreation)
        {
            var announcementFromRepo = await _announcementService.GetAnnouncementById(announcementId);

            if (announcementFromRepo == null)
            {
                return BadRequest("Invalid Request");
            }

            if (announcementFromRepo.PostedById != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            var photoToReturn = await _announcementService.AddPhotoForAnnouncement(announcementFromRepo, photoForCreation);
            if (photoToReturn != null)
            {

                return CreatedAtRoute(
                        routeName: "GetPhoto",
                        routeValues: new { announcementId = announcementFromRepo.Id, id = photoToReturn.Id },
                        value: photoToReturn);

            }
            return BadRequest("Could not add the photo");
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(Guid announcementId)
        {
            var announcementFromRepo = await _announcementService.GetAnnouncementById(announcementId);

            if (announcementFromRepo == null)
            {
                return BadRequest("Invalid Request");
            }

            //if (announcementFromRepo.PostedById != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            //{
            //    return Unauthorized();
            //}

            var photos = await _announcementService.GetAnnouncementPhotos(announcementId);
            var photosToReturn = _mapper.Map<IEnumerable<PhotoForReturn>>(photos);

            return Ok(photosToReturn);
        }


        [HttpPost("{photoId}/setMain")]
        public async Task<IActionResult> SetMainPhoto(Guid announcementId, Guid photoId)
        {
            var announcementFromRepo = await _announcementService.GetAnnouncementById(announcementId);

            if (announcementFromRepo == null)
            {
                return BadRequest("Invalid Request");
            }

            if (announcementFromRepo.PostedById != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            if (!announcementFromRepo.Photos.Any(p => p.Id == photoId))
            {
                return Unauthorized();
            }

            if (announcementFromRepo.Photos.FirstOrDefault(p => p.IsMain).Id == photoId)
            {
                return BadRequest("This is already the main photo");
            }

            var photoChanged = await _announcementService.SetMainPhoto(announcementFromRepo, photoId);

            if (photoChanged)
            {
                return NoContent();
            }
            return BadRequest("Could not set photo to main");
        }


        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(Guid announcementId, Guid photoId)
        {
            var announcementFromRepo = await _announcementService.GetAnnouncementById(announcementId);
            if (announcementFromRepo == null)
            {
                return BadRequest("Invalid Request");
            }

            if (announcementFromRepo.PostedById != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            if (!announcementFromRepo.Photos.Any(p => p.Id == photoId))
            {
                return Unauthorized();
            }

            var photoFromRepo = await _announcementService.GetPhoto(photoId);
            if (photoFromRepo.IsMain)
            {
                return BadRequest("You cannot delete your main photo");

            }

            var photoDeleted = await _announcementService.DeletePhoto(photoId);
            if (photoDeleted)
            {
                return Ok();
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}
