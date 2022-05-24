using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using CDACommercial.PoC.Application.Api.DTO;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.Zillow.DTO;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Application.Api.Services
{
    public class ListingService : IListingService
    {
        private readonly int PageSize = 50;
        protected readonly IListingRepository _listingRepository;
        protected readonly ILikeRepository _likeRepository;
        public ListingService(
            IListingRepository listingRepository,
            ILikeRepository likeRepository)
        {
            _listingRepository = listingRepository;
            _likeRepository = likeRepository;
        }

        public async Task<List<ListingInfo>> GetAllListingsByCityIdAsync(long cityId, int pageIndex = 0)
        {
            var listings = new List<ListingInfo>();
            var entities = await _listingRepository.ListAllByCityIdAsync(cityId, pageIndex, PageSize);
            foreach (var entity in entities)
            {
                var info = MapToListingInfo(entity);
                if (info != null) listings.Add(info);
            }
            return listings;
        }

        public async Task<ListingInfo> GetByIdAsync(long id)
        {
            var entity = await _listingRepository.FindByIdAsync(id);
            var info = MapToListingInfo(entity, true);
            var like = await _likeRepository.FindLikedListingByIdAsync(1, id);
            if (like != null) info.Liked = true;
            return info;
        }


        public async Task<List<ListingInfo>> GetLikedListingsAsync()
        {
            var listings = new List<ListingInfo>();
            var likes = await _likeRepository.FindLikedListingsByUserIdAsync(1);
            var entities = await _listingRepository.FindByIdsAsync(likes.Select(l => l.EntityId).ToList());
            foreach (var entity in entities)
            {
                var info = MapToListingInfo(entity);
                if (info != null) listings.Add(info);
            }
            return listings;
        }
        public async Task LikeAsync(LikeRequest dto)
        {
            var like = new Like(dto.UserId, dto.EntityId);
            _likeRepository.Add(like);
            await _likeRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task DislikeAsync(LikeRequest dto)
        {
            var like = await _likeRepository.FindLikedListingByIdAsync(dto.UserId, dto.EntityId);
            if (like != null)
            {
                _likeRepository.Delete(like);
                await _likeRepository.UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteByIdAsync(long id)
        {
            var entity = await _listingRepository.FindByIdAsync(id);
            if (entity != null)
            {
                _listingRepository.SoftDelete(entity);
                await _listingRepository.UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<List<ListingInfo>> SearchAsync(string query)
        {
            var listings = new List<ListingInfo>();
            var entities = await _listingRepository.SearchAsync(query);
            foreach (var entity in entities)
            {
                var info = MapToListingInfo(entity);
                if (info != null) listings.Add(info);
            }
            return listings;
        }

        private ListingInfo MapToListingInfo(Listing entity, bool includeDetails = false)
        {
            var info = new ListingInfo
            {
                Id = entity.Id,
                ZId = entity.ZillowId,
                DaysOnMarket = entity.DaysOnZillow,
                Status = entity.Status,
                Price = entity.Price,
                Tax = entity.LastTaxPaid,
                EstimatedTax = entity.EstimatedTax,
                CreatedAt = entity.CreatedAt,
                Prediction = new ListingPrediction
                {
                    Revenue = entity.PredictedRevenueOptimized,
                    PredVsPrice = entity.PredVsPrice
                },
                CapRate = new ListingCapRate
                {
                    Low = 0, //stats.LowCapRate,
                    Middle = 0, //stats.MiddleCapRate,
                    High = 0, //stats.HighCapRate,
                    LongTerm = 0, //stats.LongTermRental
                },
                Address = new ListingAddress
                {
                    StreetAddress = entity.Address,
                    City = entity.City.Name,
                    State = entity.State,
                    Zipcode = entity.Zipcode
                },
                Description = new ListingDescription
                {
                    SquareFootage = entity.SquareFootage,
                    Bedrooms = entity.Bedrooms,
                    Bathrooms = entity.Bathrooms
                }
            };

            if (includeDetails)
            {
                info.Details = JsonConvert.DeserializeObject<Property>(entity.ZillowData);
            }
            return info;
        }
    }
}