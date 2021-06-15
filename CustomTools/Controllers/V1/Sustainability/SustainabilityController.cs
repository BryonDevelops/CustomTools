using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomTools.Api.Queries.Sustainability.GetAllSustainabilities;
using CustomTools.Data.Access.DAL.DTOs.Sustainability;
using CustomTools.Data.Access.DAL.Interfaces;
using CustomTools.Data.Access.DAL.Interfaces.User;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MediatR;

namespace CustomTools.Api.Controllers.V1.Sustainability
{
    public class SustainabilityController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _loggedInUserInfoRepository;
        public SustainabilityController(IMediator mediator, IUserRepository loggedInUserInfoRepository)
        {
            this._mediator = mediator;
            _loggedInUserInfoRepository = loggedInUserInfoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("/Pages/Sustainability/SustainabilitySearch.cshtml");
            }

            return RedirectToAction("NotAuthorized");
        }

        public IActionResult NotAuthorized()
        {
            return View("/Pages/Shared/NotAuthorized.cshtml");
        }

        public async Task<JsonResult> GetAllSustainabilities([DataSourceRequest] DataSourceRequest request)
        {
            var sustainabilities = (List<SustainabilityDto>)await _mediator.Send(new GetAllSustainabilitiesQuery());

            var result = sustainabilities.ToDataSourceResult(request, c =>
                new
                {
                    c.SustainabilityID,
                    c.ManufacturingLocationID,
                    c.Utility,
                    c.AccountNumber,
                    c.StartDate,
                    c.EndDate,
                    c.Usage,
                    c.UOM,
                    c.Cost
                });

            return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
