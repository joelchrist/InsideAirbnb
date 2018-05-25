using Microsoft.AspNetCore.Mvc;

namespace InsideAirbnb.Filters
{
    public class ListingFilter
    {
        [FromQuery(Name = "neighbourhood")]
        public string Neighbourhood { get; set; }
    }
}