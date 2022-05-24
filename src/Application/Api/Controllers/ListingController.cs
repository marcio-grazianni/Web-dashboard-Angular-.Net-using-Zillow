using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using CDACommercial.PoC.Application.Api.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDACommercial.PoC.Application.Api.Controllers
{
    [ApiController]
    [Route("api/listings")]
    public class ListingController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        protected readonly IListingService _listingService;
        protected readonly IJobService _jobService;

        public ListingController(IFileUploadService fileUploadService, IListingService listingService, IJobService jobService)
        {
            _listingService = listingService;
            _jobService = jobService;
            _fileUploadService = fileUploadService;
        }

        [HttpGet("{id}")]
        public async Task<ListingInfo> GetById(long id)
        {
            return await _listingService.GetByIdAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteById(long id)
        {
            await _listingService.SoftDeleteByIdAsync(id);
        }

        [HttpGet("city/{id}")]
        public async Task<List<ListingInfo>> GetAllByCityId(long id, [FromQuery] int page = 0)
        {
            return await _listingService.GetAllListingsByCityIdAsync(id, page);
        }

        [HttpPost("like")] 
        public async Task Like(LikeRequest dto)
        {
            await _listingService.LikeAsync(dto);
        }

        [HttpPost("dislike")] 
        public async Task Dislike(LikeRequest dto)
        {
            await _listingService.DislikeAsync(dto);
        }

        [HttpGet("liked")]
        public async Task<List<ListingInfo>> GetLiked()
        {
            return await _listingService.GetLikedListingsAsync();
        }

        [HttpGet("search/{query?}")]
        public async Task<List<ListingInfo>> Search(string query = "")
        {
            return await _listingService.SearchAsync(query);
        }

        [HttpPost]
        [Route("predictions")]
        public async Task<Response<string>> UploadPredictionsAsync(IFormFile file)
        {
            Response<string> response = new Response<string>();
            try
            {
                List<string> filenames = await _fileUploadService.ProcessAsync(new List<IFormFile> { file });
                await _jobService.ScheduleListingPredictionAsync(filenames.FirstOrDefault());
                response.Data = "Successfully uploaded file and scheduled job.";
            }
            catch (Exception e)
            {
                response.Error = e.Message;
            }
            return response;
        }
    }
}
