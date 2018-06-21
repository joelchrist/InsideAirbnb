using System.Linq;
using InsideAirbnb.Data;
using InsideAirbnb.Filters;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnb.Repositories
{
    public class ListingRepository
    {
        private ApplicationDbContext _db;

        public ListingRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IQueryable<SummaryListing> Get(Filter filter)
        {
            var query = _db.SummaryListings.AsNoTracking().AsQueryable();
            if (filter?.Neighbourhood != null)
            {
                query = query.Where(l => l.Neighbourhood == filter.Neighbourhood);
            }

            if (filter?.HasReview != null && filter.HasReview == true)
            {
                query = query.Where(l => l.NumberOfReviews > 0);
            }

            if (filter?.MinPrice != null)
            {
                query = query.Where(l => l.Price >= filter.MinPrice);
            }

            if (filter?.MaxPrice != null)
            {
                query = query.Where(l => l.Price <= filter.MaxPrice);
            }

            return query;
        }

        public IQueryable<Listing> GetByid(int id)
        {
            return _db.Listings.Where(l => l.Id == id).Include(l => l.Calendar);
        }
    }
}