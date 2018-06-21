using System.Threading.Tasks;
using InsideAirbnb.Filters;
using InsideAirbnb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnb.Controllers
{    
    public class ListingsController : Controller
    {
        private readonly ListingRepository _listingRepository;
        
        public ListingsController(ListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }
        
        [Route("api/listings/filter")]
        public async Task<JsonResult> Filter([FromQuery] Filter filter )
        {
            var listings = await _listingRepository.Get(filter).AsNoTracking().ToListAsync();
            return Json(listings);
        }

        [Route("api/listings/{id}")]
        public async Task<JsonResult> GetById(int id)
        {
            var listing = await _listingRepository.GetByid(id).AsNoTracking().FirstOrDefaultAsync();
            return Json(listing);
        }
    }
}