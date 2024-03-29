﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CustomTools.Data.Access.DAL.DTOs.Customers;
using CustomTools.Data.Access.DAL.Interfaces.Customer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomTools.Api.Queries.Customer.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
        public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ILogger<GetAllCustomersHandler> _logger;
            private readonly IMapper _mapper;

            public GetAllCustomersHandler(ICustomerRepository customerRepository, ILogger<GetAllCustomersHandler> logger, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request,
                CancellationToken cancellationToken)
            {
                var customers = await _customerRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
        }
    }
}
