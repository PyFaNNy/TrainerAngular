using Microsoft.AspNetCore.Http;
using Trainer.Application.Models.Blob;

namespace Trainer.Application.Interfaces
{
    public interface IBlobStorageService
    {
        Task<byte[]> GetFile(string containerName, string url);

        Task<Dictionary<string, byte[]>> GetFiles(string containerName, IEnumerable<string> urls);

        Task<string> UploadFile(string containerName, IFormFile file, bool isUnique);

        Task<IEnumerable<UploadedFileInfo>> UploadFiles(string containerName, IEnumerable<IFormFile> files, bool isUnique);

    }
}
