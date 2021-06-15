using System;
using System.Collections.Generic;
using System.Text;
using CustomTools.Data.Access.DAL;
using CustomTools.Data.Access.DAL.Interfaces;
using CustomTools.Data.Access.DAL.Interfaces.Customer;
using CustomTools.Data.Access.DAL.Interfaces.Sustainability;
using CustomTools.Data.Access.DAL.Interfaces.User;
using CustomTools.Data.Access.DAL.Repositories;
using CustomTools.Data.Access.DAL.Repositories.Customer;
using CustomTools.Data.Access.DAL.Repositories.Sustainability;
using CustomTools.Data.Access.DAL.Repositories.User;
using Microsoft.Extensions.DependencyInjection;

namespace CustomTools.Data.Access
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ISustainabilityRepository, SustainabilityRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
