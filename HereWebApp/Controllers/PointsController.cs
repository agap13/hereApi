using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HereWebApp.Models;
using HereWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HereWebApp.Controllers
{
    [Route("api/[controller]")]
    public class PointsController : Controller
    {
        private readonly IPointsService _pointsService;
        private readonly CsvFileReaderWriter _csvFileWriter;
        private readonly RouteMatchingIntegrator _routeMatchingIntegrator;
        private readonly IHereApiSettings _apiSettings;

        public PointsController(IPointsService pointsService, IHereApiSettings apiSettings)
        {
            _pointsService = pointsService;
            _csvFileWriter = new CsvFileReaderWriter();
            _apiSettings = apiSettings;
            _routeMatchingIntegrator = new RouteMatchingIntegrator(_apiSettings);
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<PointDTO>> Get(CancellationToken ct, string pathToFile, RouteMode mode = RouteMode.Car, bool useRM = false)
        {
            if (string.IsNullOrEmpty(pathToFile))
            {
                return null;
            }

            var points = await _pointsService.GetPoints(ct, pathToFile);
            if (!useRM)
            {
                return points;
            }
            _csvFileWriter.WriteToFile(points);
            return await _routeMatchingIntegrator.GetRouteMatchingPoints(ct, await _csvFileWriter.ReadAndDeleteFile(ct));
        }       
    }
}
