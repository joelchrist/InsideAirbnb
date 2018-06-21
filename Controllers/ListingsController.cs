using System.Collections.Generic;
using System.Threading.Tasks;
using InsideAirbnb.Data;
using InsideAirbnb.Filters;
using InsideAirbnb.Helpers.Cache;
using InsideAirbnb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnb.Controllers
{    
    public class ListingsController : Controller
    {
        private readonly ListingRepository _listingRepository;
        private readonly ICache _cache; 
        
        public ListingsController(ListingRepository listingRepository, ICache cache)
        {
            _listingRepository = listingRepository;
            _cache = cache;
        }
        
        [Route("api/listings/filter")]
        public async Task<JsonResult> Filter([FromQuery] Filter filter )
        {
            var listings = await _cache.Get<List<SummaryListing>, Filter>(filter, Prefix.Listings);
            if (listings != null) return Json(listings);
            listings = await _listingRepository.Get(filter).AsNoTracking().ToListAsync();
            _cache.Set(filter, listings, Prefix.Listings);
            return Json(listings);
        }

        [Route("api/listings/{id}")]
        public async Task<JsonResult> GetById(int id)
        {
            var listing = await _cache.Get<Listing, int>(id, Prefix.Listing);
            if (listing != null) return Json(listing);
            listing = await _listingRepository.GetByid(id).AsNoTracking().FirstOrDefaultAsync();
            _cache.Set(id, listing, Prefix.Listing);
            return Json(listing);
        }
    }
}