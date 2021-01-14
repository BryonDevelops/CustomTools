# CustomTools

[Color Key](https://www.notion.so/bbc11130f5684d91b0cbd6eea1d42755)

Notes:

*In this specific project the Name of the project is CustomTools. Replace that with whatever your project is named. Furthermore while following this guide you will see Brackets([]). In these brackets will be something along the lines of [Your Project Name]. This is just a placeholder and you need to put your Project Name there in this case CustomTools.*

**Project Creation:**

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/bb3218c2-373e-45c0-824d-8c91dcf07b1c/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/bb3218c2-373e-45c0-824d-8c91dcf07b1c/Capture.png)

I like to add a src folder just to look better. Also you could add another folder for testing if you wanted.

Project References:

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/88fa0c56-4664-4419-a404-5b82a4149c5f/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/88fa0c56-4664-4419-a404-5b82a4149c5f/Capture.png)

- [ ]  Create new Folder called [Your Project Name]Project
- [ ]  Create new [ASP.Net](http://asp.Net) Core Web App Project
- [ ]  Enter name for Project
- [ ]  Select [ASP.Net](http://asp.Net) Core 5.0
- [ ]  Select Win Auth under Auth
- [ ]  Select [ASP.N](http://asp.NEt)et Core MVC Project
- [ ]  Create .NET Core Class Library
    - [ ]  Name it [Your Project Name].Api.Common
- [ ]  Create .NET Core Class Library
    - [ ]  Name it [Your Project Name].Data.Access
- [ ]  Create .NET Core Class Library
    - [ ]  Name it [Your Project Name].Data.Models

---

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/77ce7701-9da9-42c0-9457-8f10467227ee/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/77ce7701-9da9-42c0-9457-8f10467227ee/Capture.png)

Main Project Folder Structure

**Main Project Structure([Your Project Name]):**

- [ ]  Delete Models Folder
- [ ]  Rename Views folder to Pages
- [ ]  Delete Home Controller
- [ ]  Add V1 Folder inside of Controllers Folder
- [ ]  Add Contracts Folder
- [ ]  Add V1 Folder inside of Contracts Folder
- [ ]  Create new Folder called [Your Model]
    - [ ]  Create subfolders for Commands and Queries
        - [ ]  Commands: Where your POST requests will go(Create, Delete, Update)
        - [ ]  Queries: Where your GET requests will go(GetAll, GetById...)

Startup.cs ConfigureServices Method:

- [ ]  Change to:

```csharp
public void ConfigureServices(IServiceCollection services)
{
		// Access to DB Connection String
    string dbConnectionString = this.Configuration.GetConnectionString("SQLDBConnectionString");
    services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

    // Register your repositories
    services.AddScoped<[IRepository], [Your Repository]>();
		
    // For Windows Auth
    services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);

    services.AddRazorPages();
    services.AddInfrastructure();
    services.AddControllers();

		// For Telerik
    services.AddKendo();

    services.AddAutoMapper(typeof(Mapper));
    services.AddMediatR(typeof(Startup));

    services.AddHttpContextAccessor();

    services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

    // Register the Swagger generator, defining one or more Swagger documents
    services.AddSwaggerGen();

    services.AddMvcCore().AddApiExplorer();
}
```

**Startup.cs** Configure Method:

- [ ]  Change to:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    { 
        app.UseHsts();
    }

    var swaggerConfig = new SwaggerConfig();
    Configuration.GetSection(nameof(SwaggerConfig)).Bind(swaggerConfig);

    app.UseSwagger(option => { option.RouteTemplate = swaggerConfig.JsonRoute; });

    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint(swaggerConfig.UiEndpoint, swaggerConfig.Description);
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

- [ ]  Create Configuration Folder for Middleware
- [ ]  Create SwaggerConfig.cs class in Configuration Folder:

```csharp
public class SwaggerConfig
{
    public string JsonRoute { get; set; }

    public string Description { get; set; }

    public string UiEndpoint { get; set; }
}
```

appsettings.json:

- [ ]  Add:

```csharp
"ConnectionStrings": {
    "SQLDBConnectionString": "Server=[Your Server];Database=[Your DB];Trusted_Connection=True;MultipleActiveResultSets=true;"
  },
  "SwaggerOptions": {
    "JsonRoute": "swagger/{documentName}/swagger.json",
    "Description": "Our API",
    "UIEndpoint": "v1/swagger.json"
  },
```

- [ ]  Add ApiRoutes.cs to Contract/V1:

```csharp
public static class ApiRoutes
{
    public const string Root = "api";

    public const string Version = "v1";

    public const string Base = Root + "/" + Version;

    public static class [Your Entity]
    {
        public const string GetAll = Base + "/[Your Entity]s";

        public const string Update = Base + "/[Your Entity]s/{[Your Entity]Id}";

        public const string Delete = Base + "/[Your Entity]s/{[Your Entity]Id}";

        public const string Get = Base + "/[Your Entity]s/{[Your Entity]Id}";

        public const string Create = Base + "/[Your Entity]s";
    }
}
```

- [ ]  Add Requests and Responses Folders to Contracts Folder
    - [ ]  In Requests Folder:
        - [ ]  Add Sub-Folder for each of the models you are using
            - [ ]  In [Your Model] Folder:
                - [ ]  Create Create[Your Model]Request.cs with your models properties
    - [ ]  In Responses Folder:
        - [ ]  Add Sub-Folder for each of the models you are using
            - [ ]  In [Your Model] Folder:
                - [ ]  Create [Your Model]Response.cs with your models properties

[Your Model]Controller.cs:

```csharp
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using [Your Project].[Your Model].Queries.GetAll[Your Model];
using [Your Project].Data.Access.DAL.DTOs.[Your Model];
using [Your Project].Data.Access.DAL.Interfaces;
using [Your Project].Data.Models;
using [Your Project].Data.Models.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MediatR;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace [Your Project].Controllers.V1
{
    public class [Your Model]Controller : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILoggedInUserInfoRepository _loggedInUserInfoRepository;
        public [Your Model]Controller(IMediator mediator, ILoggedInUserInfoRepository loggedInUserInfoRepository)
        {
            this._mediator = mediator;
            _loggedInUserInfoRepository = loggedInUserInfoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("~/Pages/Index.cshtml");
            }

            return RedirectToAction("NotAuthorized");
        }

        public IActionResult NotAuthorized()
        {
            return View("~/Pages/[Your Model]/NotAuthorized.cshtml");
        }

        public async Task<JsonResult> GetAll[Your Model]([DataSourceRequest] DataSourceRequest request)
        {
            var [Your Model] = (List<[Your Model]Dto>)await _mediator.Send(new GetAll[Your Model]Query());

            var result = [Your Model].ToDataSourceResult(request, c =>
                new
                {
                    c.[Your Model]Id,
                    // c.Other properties
                });

            return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
```

```csharp

```

---

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/3e76d912-93dc-4f5f-803d-cfeba623a86b/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/3e76d912-93dc-4f5f-803d-cfeba623a86b/Capture.png)

- [ ]  In [Your Model]/Queries Add Folder called GetAll[Your Model]:
    - [ ]  Add GetAll[Your Model]Query.cs:

    ```csharp
    public class GetAll[Your Model]Query : IRequest<IEnumerable<[Your Model]Dto>>
    {
        public class GetAll[Your Model]Handler : IRequestHandler<GetAll[Your Model]Query, IEnumerable<[Your Model]Dto>>
        {
            private readonly [IRepository] _[Your Model]Repository;
            private readonly ILogger<GetAll[Your Model]Handler> _logger;
            private readonly IMapper _mapper;

            public GetAll[Your Model]Handler(ICustomerRepository [Your Model]Repository, ILogger<GetAll[Your Model]Handler> logger, IMapper mapper)
            {
                _[Your Model]Repository = [Your Model]Repository;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<[Your Model]Dto>> Handle(GetAll[Your Model]Query request,
                CancellationToken cancellationToken)
            {
                var [Your Model] = await _[Your Model]Repository.GetAllAsync();
                return _mapper.Map<IEnumerable<[Your Model]Dto>>([Your Model]);
            }
        }
    }
    ```

- [ ]  In Pages open _ViewImports.cshtml and change to:

```csharp
@using Kendo.Mvc.UI
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

- [ ]  In Pages open the Index.cshtml View and change to:

```csharp
@using CustomTools.Data.Access.DAL.DTOs.[Your Model]
@using Kendo.Mvc.UI
@addTagHelper *, Kendo.Mvc

@{
    Layout = "_Layout";
}

<h2>Title</h2>

<br />

@(Html.Kendo().Grid<[Your Model]Dto>()
    .Name("Grid")
    .Columns(columns =>
    {
        columns.Bound(c => c.CustomerId).Title("Customer ID").Width(25);
        ...
    })

    .ToolBar(toolbar =>
    {
        toolbar.Custom()
            .HtmlAttributes ( new { onclick="clearFilters(); " } )
            .Name("clearFilters")/* creates a button with class k-grid-duplicate */
            .Text("Clear Filters")
            .IconClass("k-icon k-i-delete");
        toolbar.Excel();
    })
    //.Excel(excel => excel
    //    .FileName("Kendo UI Grid Export.xlsx")
    //    .Filterable(true)
    //    .ProxyURL(Url.Action("Excel_Export_Save", "CustomerSearch"))
    //)
    .Pageable(pager => pager.AlwaysVisible(true).PageSizes(new int[] { 5, 10, 20, 100 }))
    .Navigatable()
    .Sortable()
    .Filterable()
    .DataSource(dataSource => dataSource
        .Ajax()
        .Batch(true)
        .PageSize(10)
        .ServerOperation(false)
        .Model(model =>
        {
            model.Id(c => c.CustomerId);

        })
        .Read("GetAll[Your Model]", "[Your Model]")
    ))

<script>
    function clearFilters() {
        console.log('Clicked!');
        var grid = document.getElementsByName('Grid');
        grid.filter({});
    }
</script>
```

- [ ]  In Pages Create Sub-Folder for [Your Model]
    - [ ]  Add View called NotAuthorized.cshtml
    - [ ]  Under Shared Folder open _Layout.cshtml and change to:

    ```csharp
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>@ViewData["Title"] - [Your Title]</title>
        <script src="~/lib/jquery/dist/jquery.js"></script>
        <script src="https://kendo.cdn.telerik.com/2017.3.1026/js/kendo.all.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2017.3.1026/js/kendo.aspnetmvc.min.js"></script>
        <script src="~/lib/bootstrap/dist/js/bootstrap.js"></script>
        
        <link rel="stylesheet" src="~/css/site.css" >

        @RenderSection("Scripts", required: false)
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">

        <link href="https://kendo.cdn.telerik.com/2020.3.915/styles/kendo.bootstrap-v4.min.css" rel="stylesheet" type="text/css" />
        <script src="https://kendo.cdn.telerik.com/2020.3.915/js/jquery.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2020.3.915/js/kendo.all.min.js"></script>
        <script src="https://kendo.cdn.telerik.com/2020.3.915/js/kendo.aspnetmvc.min.js"></script>
    </head>
    <body>
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div class="px-5">
                    <a class="navbar-brand" asp-area="" asp-page="/Index">Custom Tools</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <p class="nav navbar-text">Hello, @User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\") + 1,
                            (User.Identity.Name.Length - (User.Identity.Name.IndexOf("\\") + 1)))!</p>
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <a class="nav-link text-dark" asp-area="" asp-page="/Index">Home</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        <div class="px-5">
            <main role="main">
                @RenderBody()
            </main>
        </div>

        <footer class="border-top footer text-muted">
            <div class="container">
                &copy; 2021 - [Your Project] - <a asp-area="" asp-page="/Privacy">Privacy</a>
            </div>
        </footer>

    </body>
    </html>
    ```

[Your Project Name].Api.Common Structure:

- [ ]  

---

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/f8f42c5a-d030-42a7-9a1d-1714343baae4/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/f8f42c5a-d030-42a7-9a1d-1714343baae4/Capture.png)

[Your Project Name].Data.Access Structure:

- [ ]  

---

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/d9d436a4-1263-4d1e-89c8-c5df4116a7f2/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/d9d436a4-1263-4d1e-89c8-c5df4116a7f2/Capture.png)

Project Packages:

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/466ca750-f6b5-4dee-8685-061b70e87f3d/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/466ca750-f6b5-4dee-8685-061b70e87f3d/Capture.png)

---

[Your Project Name].Data.Models Structure:

- [ ]  

---

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/dd7d6f32-8806-425e-85a7-9bad20a27c2d/Capture.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/dd7d6f32-8806-425e-85a7-9bad20a27c2d/Capture.png)

[Your Project Name].Queries Structure:

- [ ]
