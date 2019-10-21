using System.Collections.Generic;

namespace HereWebApp.Models
{
    public class RouteMatchingResponse
    {
        public IEnumerable<TracePointDTO> TracePoints { get; set; }
    }

    public class TracePointDTO
    {
        public double LatMatched { get; set; }

        public double LonMatched { get; set; }
    }
}
