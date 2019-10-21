using HereWebApp.Models;

namespace HereWebApp.Mapper
{
    public static class TracePointMapper
    {
        public static PointDTO MapToPointDTO(this TracePointDTO tp)
        {
            return new PointDTO()
            {
                Latitude = tp.LatMatched,
                Longitude = tp.LonMatched
            };
        }
    }
}
