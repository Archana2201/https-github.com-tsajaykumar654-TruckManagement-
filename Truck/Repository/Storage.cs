using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace Truck.Repository
{
    public class Storage : IStorage
    {
        IConfiguration _configuration;

        private string connectionString => _configuration.GetValue<string>("Azure:BlobConnectionString");

        private string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
        public Storage(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task DeleteIfExists(string path)
        {
            try
            {
                path = path.Substring(8, path.Length - 8);
                var container = await GetContainer("images");
                await container.DeleteBlobIfExistsAsync(path);
            }
            catch (Exception e)
            {

            }

        }

        public async Task<string> Save(string file, string path, string extention = ".png")
        {
            try
            {
                path = path.Substring(1, path.Length - 1);
                var container = await GetContainer("images");
                var fileName = Guid.NewGuid().ToString() + extention;
                byte[] bytes = Convert.FromBase64String(file);
                using (Stream stream = new MemoryStream(bytes))
                {
                    await container.UploadBlobAsync($"{path}/{fileName}", stream);
                }
                return $"/images/{path}/{fileName}";
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<BlobContainerClient> GetContainer(string ContainerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            await containerClient.CreateIfNotExistsAsync();
            await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
            return containerClient;
        }

        public async Task<string> Save(IFormFile file, string path, string extention = ".png")
        {
            try
            {
                path = path.Substring(1, path.Length - 1);
                var container = await GetContainer("images");
                var fileName = Guid.NewGuid().ToString() + extention;
                using (Stream stream = file.OpenReadStream())
                {
                    await container.UploadBlobAsync($"{path}/{fileName}", stream);
                }
                return $"/images/{path}/{fileName}";
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task MoveFolders(string dir)
        {
            var container = await GetContainer("images");
            var path = Path.Combine(rootPath, dir);
            var files = Directory.GetFiles(path).ToList();
            foreach (var p in files)
            {
                var filePath = Path.Combine(path, p);
                var fileInfo = new FileInfo(filePath);
                using (var file = fileInfo.OpenRead())
                {
                    await container.UploadBlobAsync($"{dir}/{fileInfo.Name}", file);
                }
            }
        }
    }
}
