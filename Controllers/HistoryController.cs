using System.Threading.Tasks;
using InsideAirbnb.Filters;
using InsideAirbnb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnb.Controllers
{
    public class HistoryController : Controller
    {
        private readonly CalendarRepository _calendarRepository;

        public HistoryController(CalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        [Route("api/history/price")]
        public async Task<JsonResult> GetPriceHistory([FromQuery] Filter filter)
        {
            var result = await _calendarRepository.GetPriceHistory(filter).AsNoTracking().ToListAsync();
            return Json(result);
        }
        
    }
}