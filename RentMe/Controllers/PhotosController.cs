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
        public async Task<IActionResult> AddPhotos(Guid announcementId)
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

            var photos = await _announcementService.GetAnnouncementPhotos(announcementId);
            var photosToReturn = _mapper.Map<IEnumerable<PhotoForReturn>>(photos);

            return Ok(photosToReturn);
        }


        [HttpPost("{id}/setMain")]
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

            await _announcementService.SetMainPhoto(announcementId, photoId);

            var user = await _announcementService.GetUser(userId, true);

            if (!user.Photos.Any(p => p.Id == id))
            {
                return Unauthorized();
            }

            var photoFromRepo = await _announcementService.GetPhoto(id);

            if (photoFromRepo.IsMain)
            {
                return BadRequest("This is already the main photo");
            }

            var currentMainPhoto = await _announcementService.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;

            if (await _announcementService.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not set photo to main");
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePhoto(int userId, int id)
        //{
        //    if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //    {
        //        return Unauthorized();
        //    }

        //    var user = await _announcementService.GetUser(userId, true);

        //    if (!user.Photos.Any(p => p.Id == id))
        //    {
        //        return Unauthorized();
        //    }

        //    var photoFromRepo = await _announcementService.GetPhoto(id);

        //    if (photoFromRepo.IsMain)
        //    {
        //        return BadRequest("You cannot delete your main photo");

        //    }

        //    if (photoFromRepo.PublicId != null)
        //    {
        //        var deletionParams = new DeletionParams(photoFromRepo.PublicId);
        //        var result = _cloudinary.Destroy(deletionParams);

        //        if (result.Result == "ok")
        //        {
        //            _announcementService.Delete(photoFromRepo);
        //        }
        //    }
        //    if (photoFromRepo.PublicId == null)
        //    {
        //        _announcementService.Delete(photoFromRepo);
        //    }

        //    if (await _announcementService.SaveAll())
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Failed to delete the photo");
        //}
    }
}
