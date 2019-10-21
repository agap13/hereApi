using HereWebApp.Mapper;
using HereWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HereWebApp.Services
{
    public class RouteMatchingIntegrator
    {
        private readonly CsvFileReaderWriter _csvFileWriter;
        private readonly IHereApiSettings _apiSettings;

        public RouteMatchingIntegrator( IHereApiSettings apiSettings)
        {
            _csvFileWriter = new CsvFileReaderWriter();
            _apiSettings = apiSettings;
        }

        public async Task<IEnumerable<PointDTO>> GetRouteMatchingPoints(CancellationToken ct, string content, RouteMode mode = RouteMode.Car)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiSettings.BaseUrl);
                    var requestUri = CreateRequestUri(mode);

                    using (var request = new HttpRequestMessage(HttpMethod.Post, requestUri))
                    {
                        request.Content = new StringContent(content, Encoding.UTF8, "text/plain");
                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();
                            return await GetPointsFromResponse(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<IEnumerable<PointDTO>> GetPointsFromResponse(HttpResponseMessage response)
        {
            var stringResult = await response.Content.ReadAsStringAsync();
            var tracePoints = JsonConvert.DeserializeObject<RouteMatchingResponse>(stringResult)?.TracePoints;
            return tracePoints?.Select(x => x.MapToPointDTO());
        }

        private string CreateRequestUri(RouteMode mode = RouteMode.Car)
        {
            var modeStr = (mode.ToString()).ToCamelCase();
            return $"/2/matchroute.json?app_id={_apiSettings.AppId}" +
                                                $"&app_code={_apiSettings.AppCode}" +
                                                $"&routemode={modeStr}" +
                                                $"&Te=gzip,deflate" +
                                                $"&Accept-encoding=gzip,deflate";
        }
    }
}
