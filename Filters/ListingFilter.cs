using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace InsideAirbnb.Filters
{
    public class ListingFilter
    {
        [FromQuery(Name = "neighbourhood")]
        public string Neighbourhood { get; set; }
        
        [FromQuery(Name = "hasReview")]
        public bool? HasReview { get; set; }

        [FromQuery(Name = "minPrice")]
        public int? MinPrice { get; set; }

        [FromQuery(Name = "minPrice")]
        public int? MaxPrice { get; set; }
    }
}