using System;
using System.Collections.Generic;
using System.Text;
using CustomTools.Data.Access.DAL.Interfaces;
using CustomTools.Data.Access.DAL.Interfaces.Customer;
using CustomTools.Data.Access.DAL.Interfaces.Sustainability;
using CustomTools.Data.Access.DAL.Interfaces.User;

namespace CustomTools.Data.Access.DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(ICustomerRepository customerRepository, ISustainabilityRepository sustainabilityRepository, IUserRepository loggedInUserInfoRepository)
        {
            Customers = customerRepository;
            Sustainabilities = sustainabilityRepository;
            LoggedInUserInfo = loggedInUserInfoRepository;
        }

        public ICustomerRepository Customers { get; }
        public ISustainabilityRepository Sustainabilities { get; set; }
        public IUserRepository LoggedInUserInfo { get; }
    }
}
