using System.Threading.Tasks;
using InsideAirbnb.Filters;
using InsideAirbnb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InsideAirbnb.Controllers
{
    public class StatsController : Controller
    {
        private readonly StatsRepository _statsRepository;

        public StatsController(ListingRepository listingRepository, StatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }
        
        [Route("api/stats")]
        public async Task<JsonResult> GetStats(Filter filter)
        {
            var roomTypeStat = await _statsRepository.GetRoomTypeStats(filter);
            var availabilityStat = await _statsRepository.GetAvailabilityStats(filter);
            var priceStat = await _statsRepository.getPriceStats(filter);

            return Json(new
            {
                Availability = availabilityStat,
                RoomType = roomTypeStat,
                Price = priceStat
            });
        }
    }
}