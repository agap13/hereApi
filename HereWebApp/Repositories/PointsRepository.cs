using HereWebApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace HereWebApp.Repositories
{
    public interface IPointsRepository
    {
        IEnumerable<PointDTO> GetPoints(string pathToFile);
    }

    public class PointsRepository : IPointsRepository
    {
        public IEnumerable<PointDTO> GetPoints(string pathToFile)
        {
            using (StreamReader r = new StreamReader(pathToFile))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<PointDTO>>(json);
            }
        }
    }
}
