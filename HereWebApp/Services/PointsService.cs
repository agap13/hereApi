using HereWebApp.Models;
using HereWebApp.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HereWebApp.Services
{
    public interface IPointsService
    {
        Task<IEnumerable<PointDTO>> GetPoints(CancellationToken ct, string pathToFile);
    }

    public class PointsService : IPointsService
    {
        private readonly IPointsRepository _pointsRepo;

        public PointsService(IPointsRepository pointsRepo)
        {
            _pointsRepo = pointsRepo;
        }

        public Task<IEnumerable<PointDTO>> GetPoints(CancellationToken ct, string pathToFile)
        {
            return Task.FromResult(_pointsRepo.GetPoints(pathToFile));
        }
    }
}
