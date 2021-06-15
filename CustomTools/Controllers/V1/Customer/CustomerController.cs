using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using CustomTools.Api.Queries.Customer.GetAllCustomers;
using CustomTools.Data.Access.DAL.DTOs.Customers;
using CustomTools.Data.Access.DAL.Interfaces;
using CustomTools.Data.Access.DAL.Interfaces.User;
using CustomTools.Data.Models;
using CustomTools.Data.Models.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MediatR;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace CustomTools.Api.Controllers.V1.Customer
{
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _loggedInUserInfoRepository;
        public CustomerController(IMediator mediator, IUserRepository loggedInUserInfoRepository)
        {
            this._mediator = mediator;
            _loggedInUserInfoRepository = loggedInUserInfoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("HERE");
                return View("/Pages/Index.cshtml");
            }

            return RedirectToAction("NotAuthorized");
        }

        public IActionResult NotAuthorized()
        {
            return View("/Pages/Shared/NotAuthorized.cshtml");
        }

        public async Task<JsonResult> GetAllCustomers([DataSourceRequest] DataSourceRequest request)
        {
            var customers = (List<CustomerDto>)await _mediator.Send(new GetAllCustomersQuery());

            var result = customers.ToDataSourceResult(request, c =>
                new
                {
                    c.CustomerId,
                    c.CustomerName,
                    c.MarkedForDeletion,
                    c.SalesOffice,
                    c.SalesVPName,
                    c.ParentName
                });

            return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
