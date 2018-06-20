using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnb.Data;
using InsideAirbnb.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public async Task<List<SummaryListing>> Get(ListingFilter filter)
        {
            var query = this._db.SummaryListings.AsNoTracking().AsQueryable();
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

            return await query.AsNoTracking().ToListAsync();
        }
    }
}