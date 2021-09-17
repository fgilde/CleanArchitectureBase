using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureBase.Application;
using CleanArchitectureBase.Application.Common.Models;
using CleanArchitectureBase.Application.Common.Security;
using CleanArchitectureBase.Application.RequestHandling.System;
using CleanArchitectureBase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Nextended.Core.Extensions;

namespace CleanArchitectureBase.WebAPI.Controllers
{
    [Route(Constants.ApiBasePath + "/v{version:apiVersion}/[controller]/[action]")]
    public class SystemController : BaseController<SystemController>
    {
        [HttpGet]
        public async Task<ActionResult<VersionInfoModel>> Version()
        {
            return Ok(await Mediator.Send(new GetVersion.Request()));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Version>> AvailableApiVersions()
        {
            return Ok(ApiVersions.All.Select(v => v.ToVersion()));
        }

        [HttpPost("{queue}")]
        public async Task<ActionResult> SendOnServiceBus(string queue, [FromBody] MyEntity entity)
        {
            return Ok(await Mediator.Send(new SendToServiceBus.Request
            {
                Queue = queue,
                Content = entity
            }));
        }
    }
}
