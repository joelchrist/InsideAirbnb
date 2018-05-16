using System;

namespace InsideAirbnb.Data
{
    public partial class CalendarItem
    {
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public string Available { get; set; }
        public string Price { get; set; }

        public Listing Listing { get; set; }
    }
}
