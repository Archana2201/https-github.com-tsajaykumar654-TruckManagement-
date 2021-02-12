using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Truck.Repository
{
    public interface IStorage
    {
        Task<string> Save(string file, string path, string extention = ".png");
        Task<string> Save(IFormFile file, string path, string extention = ".png");
        Task DeleteIfExists(string path);
        Task MoveFolders(string dir);
    }
}
