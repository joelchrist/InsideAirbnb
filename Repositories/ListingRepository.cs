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

        public async Task<List<Listing>> Get(ListingFilter filter)
        {  
            if (filter == null)
            {
                return await this._db.Listings.AsNoTracking().ToListAsync();
            }

            IQueryable<Listing> query = this._db.Listings;
            if (filter.Neighbourhood != null)
            {
                query = query.Where(l => l.Neighbourhood == filter.Neighbourhood);
            }

            return await query.AsNoTracking().ToListAsync();
        }
        
    }
}