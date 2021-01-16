using System;
using System.Collections.Generic;
using System.Text;
using CustomTools.Data.Access.DAL.Interfaces;

namespace CustomTools.Data.Access.DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(ICustomerRepository customerRepository, IUserRepository loggedInUserInfoRepository)
        {
            Customers = customerRepository;
            LoggedInUserInfo = loggedInUserInfoRepository;
        }

        public ICustomerRepository Customers { get; }
        public IUserRepository LoggedInUserInfo { get; }
    }
}
