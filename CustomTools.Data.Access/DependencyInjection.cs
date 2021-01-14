using System;
using System.Collections.Generic;
using System.Text;
using CustomTools.Data.Access.DAL;
using CustomTools.Data.Access.DAL.Interfaces;
using CustomTools.Data.Access.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomTools.Data.Access
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ILoggedInUserInfoRepository, LoggedInUserInfoRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
