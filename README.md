ASP.Net Core 5.0 Project Setup Instructions w/ Web API

For best viewing use the link below which will take you to the Notion Project.

Link to Notion: https://www.notion.so/ASP-Net-Core-5-0-Project-Setup-Instructions-w-Web-API-67320e489f374ca28e81f1374b912bff

[Color Key](https://www.notion.so/bbc11130f5684d91b0cbd6eea1d42755)

Notes:

*In this specific project the Name of the project is CustomTools. Replace that with whatever your project is named. Furthermore while following this guide you will see Brackets([]). In these brackets will be something along the lines of [Your Project Name]. This is just a placeholder and you need to put your Project Name there in this case CustomTools.*

**Project Creation:**

I like to add a src folder just to look better. Also you could add another folder for testing if you wanted.

Project References:

- AutoMapper
- AutoMapper.Extensions.Microsoft.DependencyInjection
- AutoQueryable.AspNetCore.Filter
- AutoWrapper.Core
- AutoWrapper.Server
- Dapper
- MediatR
- MediatR.Extensions.Microsoft.DependencyInjection
- Microsoft.AspNetCore.Authentication.JwtBearer
- Newtonsoft.Json
- Swashbuckle.AspNetCore
- System.Data.SqlClient
- Telerik.UI.for.AspNet.Core

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
        private readonly IUserRepository _userRepository;
        public [Your Model]Controller(IMediator mediator, IUserRepository userRepository)
        {
            this._mediator = mediator;
            _userRepository = userRepository;
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

---

[Your Project Name].Api.Common Structure:

- [ ]  Add Folder called Exceptions(Place whatever exceptions you want in here)

---

[Your Project Name].Data.Access Structure:

**Project Packages:**

- AutoMapper.Extensions.Microsoft.DependencyInjection
- Dapper
- FluentValidation.AspNetCore
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Logging
- System.Data.SqlClient

- [ ]  Create DependencyInjection.cs:

```csharp
public static IServiceCollection AddInfrastructure(this IServiceCollection services)
{
    services.AddTransient<I[Your Model]Repository, [Your Model]Repository>();
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    return services;
}
```

- [ ]  Create Constants and, DAL Folders
    - [ ]  Inside of DAL Create DTOs, Interfaces, and Repositories Folders
        - [ ]  Inside of DTOs Create folders for your Model and Users
            - [ ]  Inside of your Model folder add a DTO for your Model called [Your Model]Dto

                A DTO(Data Transfer Object) is what will be shown on the Frontend of the application and lets you only show the properties that you want. 

            - [ ]  Add a Folder called Users for your UserDto
        - [ ]  Inside the Interfaces Folder:
            - [ ]  Add I[Your Model]Repository.cs:

            ```csharp
            public interface I[Your Model]Repository: IRepository<[Your Model]Dto>
            {
                Task<IEnumerable<[Your Model]Dto>> GetAllAsync();
            }
            ```

            - [ ]  Add IGenericRepository.cs:

            ```csharp
            public interface IGenericRepository<T> where T : class
            {
                Task<List<T>> Get(string searchText);
                Task<List<T>> GetAll();

                Task<int> Create(T entity);
                Task<int> Delete(int id);
                Task<int> Update(int entity);
            }
            ```

            - [ ]  Add IUserRepository.cs:

            ```csharp
            public interface IUserRepository
            {
                Task<List<User>> Get(string impersonateUser, string loggedInUser);
            }
            ```

            - [ ]  Add IRepository.cs:

            ```csharp
            public interface IRepository<T>
            {
                Task<IEnumerable<T>> GetAllAsync();
            }
            ```

            - [ ]  Add IUnitOfWork.cs:

            ```csharp
            public interface IUnitOfWork
            {
                I[Your Model]Repository [Your Model] { get; }
            }
            ```

        - [ ]  Inside the Repositories Folder:
            - [ ]  Add [Your Model]Repository.cs:

            ```csharp
            public class [Your Model]Repository : I[Your Model]Repository
            {
                private readonly ILogger<[Your Model]Repository> _logger;
                private readonly IDbConnection _dbConnection;

                public [Your Model]Repository(ILogger<[Your Model]Repository> logger, IDbConnection connection)
                {
                    _logger = logger;
                    _dbConnection = connection;
                }

                public async Task<IEnumerable<[Your Model]Dto>> GetAllAsync()
                {
                    return await _dbConnection.QueryAsync<[Your Model]Dto>("[Your SP Here]", commandType: CommandType.StoredProcedure);
                }

            }
            ```

---

[Your Project Name].Data.Models Structure:

- [ ]  Create Models for [Your Model], Role, User, and UserRole
    - [ ]  Role:

    ```csharp
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    ```

    - [ ]  User:

    ```csharp
    public class User
    {
      public string NtId { get; set; }
      public string LastName { get; set; }
      public string FirstName { get; set; }
      public string Email { get; set; }
      public string Phone { get; set; }
      public string Ext { get; set; }
      public bool Active { get; set; }
      public string CreatedBy { get; set; }
      public DateTime CreatedDate { get; set; } = DateTime.Now;
      public bool IsDeleted { get; set; }

      public virtual IList<UserRole> Roles { get; set; }
    }
    ```

    - [ ]  UserRole:

    ```csharp
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
    ```
