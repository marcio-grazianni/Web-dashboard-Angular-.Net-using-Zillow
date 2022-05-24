using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CDACommercial.PoC.Application.Api.Contracts
{
    public interface IFileUploadService
    {
        Task<List<string>> ProcessAsync(List<IFormFile> files);
    }
}