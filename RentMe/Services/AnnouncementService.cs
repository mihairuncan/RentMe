using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentMe.Data;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Services
{

    public interface IAnnouncementService
    {
        public Task AddAnnouncement(Announcement announcement);
        public Task<Announcement> GetAnnouncementById(Guid id);
        public Task<PhotoForReturn> AddPhotoForAnnouncement(Announcement announcement, PhotoForCreation photoForCreation);
        public Task<Photo> GetPhoto(Guid id);
        public Task<IEnumerable<Photo>> GetAnnouncementPhotos(Guid announcementId);
        public Task<bool> SetMainPhoto(Announcement announcement, Guid photoId);
        public Task<bool> DeletePhoto(Guid photoId);
    }

    public class AnnouncementService : IAnnouncementService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        private Cloudinary _cloudinary;

        public AnnouncementService(DatabaseContext context, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _context = context;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task AddAnnouncement(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task<PhotoForReturn> AddPhotoForAnnouncement(Announcement announcement, PhotoForCreation photoForCreation)
        {
            var file = photoForCreation.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreation.Url = uploadResult.Url.ToString();
            photoForCreation.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreation);
            photo.Id = new Guid();

            if (!announcement.Photos.Any(u => u.IsMain))
            {
                photo.IsMain = true;
            }

            announcement.Photos.Add(photo);
            await _context.SaveChangesAsync();

            var photoToReturn = _mapper.Map<PhotoForReturn>(photo);
            return photoToReturn;
        }

        public async Task<Announcement> GetAnnouncementById(Guid id)
        {
            var announcementToReturn = await _context.Announcements
                                                        .IgnoreQueryFilters()
                                                        .Include(a => a.Photos)
                                                        .FirstOrDefaultAsync(a => a.Id == id);
            return announcementToReturn;
        }

        public async Task<Photo> GetPhoto(Guid id)
        {
            var photo = await _context.Photos
                            .FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<IEnumerable<Photo>> GetAnnouncementPhotos(Guid announcementId)
        {
            var photos = await _context.Photos
                                        .Where(p => p.AnnouncementId == announcementId)
                                        .ToListAsync();

            return photos;
        }

        public async Task<bool> SetMainPhoto(Announcement announcement, Guid photoId)
        {
            var currentMainPhoto = announcement.Photos.FirstOrDefault(p => p.IsMain == true);
            currentMainPhoto.IsMain = false;

            var mainPhoto = announcement.Photos.FirstOrDefault(p => p.Id == photoId);
            mainPhoto.IsMain = true;
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePhoto(Guid photoId)
        {
            var photoFromRepo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == photoId);
            var deletionParams = new DeletionParams(photoFromRepo.PublicId);
            var result = _cloudinary.Destroy(deletionParams);

            if (result.Result == "ok")
            {
                _context.Photos.Remove(photoFromRepo);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
