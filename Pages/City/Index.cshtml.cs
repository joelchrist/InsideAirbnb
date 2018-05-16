using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnb.Pages.City
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IEnumerable<SelectListItem> Neighbourhoods;
        public int ListingAmount;
        public IList<Listing> Listings;

        public IndexModel(ApplicationDbContext dbContext)
        {    
            _db = dbContext;    
        }

        public async Task OnGetAsync(string neighbourhood)
        {
            var neighbourhoods = await _db.Neighbourhoods.AsNoTracking().ToListAsync();
            Neighbourhoods = neighbourhoods.Select(p => new SelectListItem() { Text = p.Name, Value = p.Name});
            ListingAmount = await GetListingAmount(neighbourhood);
        }
        
        [BindProperty]
        public Neighbourhood Neighbourhood { get; set; }
        
        public async Task OnPostAsync()
        {
            var neighbourhoods = await _db.Neighbourhoods.AsNoTracking().ToListAsync();
            Neighbourhoods = neighbourhoods.Select(p => new SelectListItem() { Text = p.Name, Value = p.Name});
            ListingAmount = await GetListingAmount(Neighbourhood.Name);
            if (Neighbourhood.Name != null)
            {
                Listings = await _db.Listings
                    .Where(l => l.Neighbourhood.Equals(Neighbourhood.Name))
                    .AsNoTracking()
                    .ToListAsync();
            }
            Console.WriteLine(Neighbourhood.Name);
        }


        private async Task<int> GetListingAmount(string neighbourhood)
        {
            if (neighbourhood == null)
            {
                return await _db.Listings.AsNoTracking().CountAsync();
            }

            return await _db.Listings.Where(l => l.Neighbourhood.Equals(neighbourhood)).AsNoTracking().CountAsync();
        }
    }
}