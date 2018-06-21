using Microsoft.AspNetCore.Mvc;

namespace InsideAirbnb.Filters
{
    public class Filter
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