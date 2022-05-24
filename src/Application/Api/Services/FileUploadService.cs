using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace CDACommercial.PoC.Application.Api.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration _configuration;
        public FileUploadService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<string>> ProcessAsync(List<IFormFile> files)
        {
            string storage = _configuration["StoragePath"];
            long size = files.Sum(f => f.Length);
            List<string> filenames = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    Guid g = Guid.NewGuid();
                    string guidString = g.ToString("N");
                    string originalFilename = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                    string[] parts = originalFilename.Split(".");
                    string filename = $"{guidString}.{parts[parts.Length - 1]}";
                    using (var stream = System.IO.File.Create(Path.Combine(storage, filename)))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    filenames.Add(filename);
                }
            }
            return filenames;
        }
    }
}