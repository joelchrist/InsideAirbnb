using System.Linq;
using System.Threading.Tasks;
using InsideAirbnb.Data;
using InsideAirbnb.Filters;
using InsideAirbnb.Repositories;
using Microsoft.AspNetCore.Cors;
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
        
        
//        [Route("api/listings/{skip?}")]
//        public async Task<JsonResult> Index(int skip = 0)
//        {
//            var listings = await _listingRepository.Get(null);
//            return Json(listings);
//        }

        //https://stackoverflow.com/questions/11862069/optional-parameters-in-asp-net-web-api

        [Route("api/listings/filter")]
        public async Task<JsonResult> Filter([FromQuery] ListingFilter filter )
        {
            var listings = await _listingRepository.Get(filter);
            return Json(listings);
        }
    }
}