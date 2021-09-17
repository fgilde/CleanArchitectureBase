using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitectureBase.Application.Common.Models;
using CleanArchitectureBase.Application.RequestHandling.WeatherForecasts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureBase.WebAPI.Controllers
{
    [ApiVersion(ApiVersions.V1)]
    [ApiVersion(ApiVersions.V2)]
    public class WeatherForecastController : BaseController<WeatherForecastController>
    {
        /// <summary>
        /// Returns the weather forecast
        /// </summary>
        /// <returns></returns>
        [HttpGet("Weather")]
        [MapToApiVersion(ApiVersions.V1)]
        [Authorize]
        public async Task<IEnumerable<WeatherForecast>> GetV1()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }

        /// <summary>
        /// Returns the weather forecast
        /// </summary>
        [HttpGet("Weather")]
        [MapToApiVersion(ApiVersions.V2)]
        [Authorize]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}
