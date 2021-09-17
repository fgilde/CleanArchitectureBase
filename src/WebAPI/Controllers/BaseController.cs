using CleanArchitectureBase.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureBase.WebAPI.Controllers
{
    [ApiController]
    [Route(Constants.ApiBasePath + "/v{version:apiVersion}/[controller]")]
    public abstract class BaseController<TController> : ControllerBase
        where TController : BaseController<TController>
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator ??= Get<IMediator>();
        protected IStringLocalizer<TController> Localizer => Get<IStringLocalizer<TController>>();
        protected IHtmlLocalizer<TController> HtmlLocalizer => Get<IHtmlLocalizer<TController>>();
        protected ILogger<TController> logger => Get<ILogger<TController>>();
        protected T Get<T>() => HttpContext.RequestServices.GetService<T>();
    }
}
