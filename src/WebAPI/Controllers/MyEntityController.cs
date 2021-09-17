using System.Threading.Tasks;
using CleanArchitectureBase.Application;
using CleanArchitectureBase.Application.RequestHandling.MyEntity;
using CleanArchitectureBase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureBase.WebAPI.Controllers
{
    [Route(Constants.ApiBasePath + "/v{version:apiVersion}/[controller]/[action]")]
    public class MyEntityController : BaseController<MyEntityController>
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await Mediator.Send(new GetMyEntity.Request()));
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<MyEntity>> Create(string name)
        {
            return Ok(await Mediator.Send(new CreateMyEntity.Request { Name = name }));
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult> Delete(int? id = null)
        {
            return Ok(await Mediator.Send(new DeleteMyEntity.Request { Id = id }));
        }
    }
}
