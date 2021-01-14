using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTools.Data.Access.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        ILoggedInUserInfoRepository LoggedInUserInfo { get; }
    }
}
