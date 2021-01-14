using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomTools.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Customers
        {
            public const string GetAll = Base + "/customers";

            public const string Update = Base + "/customers/{customerId}";

            public const string Delete = Base + "/customers/{customerId}";

            public const string Get = Base + "/customers/{customerId}";

            public const string Create = Base + "/customers";
        }
    }
}
