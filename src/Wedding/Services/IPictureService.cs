﻿using System.Security.Policy;
using Wedding.Data.Entities;

namespace Wedding.Services
{
    public interface IPictureService
    {
        public Task<Picture> GetPictureAsync(Guid pictureId);
        public Task<Picture> GetPictureAsync(Uri pictureUrl);
        public Task<List<Picture>> GetAllPicturesAsync();
        public Task<List<Uri>> GetAllPictureThumbnailUrls();
        public Task DeletePictureAsync(Guid pictureId);
        public Task<Picture> ReplacePicture(Guid originalPictureId, Guid replacementPictureId);
    }
}