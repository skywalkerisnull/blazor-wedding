namespace Wedding.Services
{
    public interface IFileStorageService
    {
        public Task UploadFileAsync(Uri filePath, Stream fileContents);

        public Task<Uri> GetFileValet(Uri filePath);

        public Task<Stream> GetFile(Uri filePath);
        public Task DeleteFileAsync(Uri filePath);
        public Task UpdateFileAsync(Uri filePath, Stream fileContents);
    }
}
