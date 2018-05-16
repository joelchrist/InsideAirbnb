using System.Linq;
using System.Threading.Tasks;
using InsideAirbnb.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnb.Controllers
{    
    [Route("api/[controller]/{skip?}")]
    public class ListingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public ListingsController(ApplicationDbContext context)
        {
            _db = context;
        }
        
        public async Task<JsonResult> Index(int skip = 0)
        {
            var listings = await _db.Listings.Skip(skip).Take(10).ToListAsync();
            return Json(listings);
        }
    }
}