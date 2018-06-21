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

        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task OnGetAsync(string neighbourhood)
        {
            var neighbourhoods = await _db.Neighbourhoods.AsNoTracking().ToListAsync();
            Neighbourhoods = neighbourhoods.Select(p => new SelectListItem() {Text = p.Name, Value = p.Name});
        }

        [BindProperty] public Neighbourhood Neighbourhood { get; set; }

        public async Task OnPostAsync()
        {
            var neighbourhoods = await _db.Neighbourhoods.AsNoTracking().ToListAsync();
            Neighbourhoods = neighbourhoods.Select(p => new SelectListItem() {Text = p.Name, Value = p.Name});
        }
    }
}