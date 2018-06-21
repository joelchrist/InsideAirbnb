using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnb.Data;
using InsideAirbnb.Filters;
using InsideAirbnb.Helpers.Cache;

namespace InsideAirbnb.Repositories
{
    public class StatsRepository
    {
        private ApplicationDbContext _db;
        private ListingRepository _listingRepository;
        private ICache _cache;

        public StatsRepository(ApplicationDbContext db, ListingRepository listingRepository, ICache cache)
        {
            _db = db;
            _listingRepository = listingRepository;
            _cache = cache;
        }

        public async Task<RoomTypeStat> GetRoomTypeStats(Filter filter)
        {

            var result = await _cache.Get<RoomTypeStat, Filter>(filter, Prefix.RoomTypeStat);
            if (result != null) return result;
            
            var listings = _listingRepository.Get(filter);
            
            var privateRooms = listings.Count(listing => listing.RoomType == "Private room");
            var entireHomes = listings.Count(listing => listing.RoomType == "Entire home/apt");
            var sharedRooms = listings.Count(listing => listing.RoomType == "Shared room");
            
            result = new RoomTypeStat
            {
                PrivateRooms = privateRooms,
                EntireHomes = entireHomes,
                SharedRooms = sharedRooms
            };

            _cache.Set(filter, result, Prefix.RoomTypeStat);
            return result;
        }

        public async Task<AvailabiltyStat> GetAvailabilityStats(Filter filter)
        {
            var result = await _cache.Get<AvailabiltyStat, Filter>(filter, Prefix.AvailabilityStat);
            if (result != null) return result;
            
            var listings = _listingRepository.Get(filter);
            
            var high = listings.Count(listing => listing.Availability365 > 200);
            var low = listings.Count(listing => listing.Availability365 <= 200);

            result = new AvailabiltyStat
            {
                High = high,
                Low = low
            };
            
            _cache.Set(filter, result, Prefix.AvailabilityStat);
            return result;
        }

        public async Task<List<double>> getPriceStats(Filter filter)
        {
            
            var result = await _cache.Get<List<double>, Filter>(filter, Prefix.PriceStat);
            if (result != null) return result;
            
            
            var listings = _listingRepository.Get(filter);

            List<double> averagePrices = new List<double>();

            for (int i = 1; i < 13; i++)
            {
                var average = listings.Join(
                        _db.CalendarItems,
                        sl => sl.Id,
                        ci => ci.ListingId,
                        (sl, ci) => ci)
                    .Where(ci => ci.Date.Month == i)
                    .Where(ci => ci.Available == "t")
                    .Select(ci => ci.Price)
                    .Select(price => double.Parse(price.Substring(1)))
                    .ToList()
                    .DefaultIfEmpty()
                    .Average();

                averagePrices.Add(average);
            }

            result = averagePrices;
            
            _cache.Set(filter, result, Prefix.PriceStat);
            return result;
        }
    }

    public class RoomTypeStat
    {
        public int PrivateRooms { get; set; }
        public int EntireHomes { get; set; }
        public int SharedRooms { get; set; }
    }

    public class AvailabiltyStat
    {
        public int High { get; set; }
        public int Low { get; set; }
    }
}