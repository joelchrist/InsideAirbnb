﻿using System.Linq;
using InsideAirbnb.Data;
using InsideAirbnb.Filters;

namespace InsideAirbnb.Repositories
{
    public class CalendarRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ListingRepository _listingRepository;

        public CalendarRepository(ApplicationDbContext db, ListingRepository listingRepository)
        {
            this._db = db;
            this._listingRepository = listingRepository;
        }

        public IQueryable<CalendarItem> GetPriceHistory(Filter filter)
        {
            var query = _db.CalendarItems;
            var listings = _listingRepository.Get(filter);
            var result = listings.Join(_db.CalendarItems,
                listing => listing.Id,
                item => item.ListingId,
                (listing, item) => item);

            return result;
        }
    }
}