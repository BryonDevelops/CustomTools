using System;
using System.Collections.Generic;
using System.Text;
using CustomTools.Data.Access.DAL.Interfaces;

namespace CustomTools.Data.Access.DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(ICustomerRepository customerRepository, ILoggedInUserInfoRepository loggedInUserInfoRepository)
        {
            Customers = customerRepository;
            LoggedInUserInfo = loggedInUserInfoRepository;
        }

        public ICustomerRepository Customers { get; }
        public ILoggedInUserInfoRepository LoggedInUserInfo { get; }
    }
}
