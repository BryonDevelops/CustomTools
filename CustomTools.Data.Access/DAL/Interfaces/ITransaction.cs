using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTools.Data.Access.DAL
{
    public interface ITransaction
    {
        void Commit();
        void Rollback();
    }
}
