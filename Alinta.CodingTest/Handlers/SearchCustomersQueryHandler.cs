using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alinta.CodingTest.Data;
using Alinta.CodingTest.Queries;
using Alinta.CodingTest.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alinta.CodingTest.Handlers
{
    public class SearchCustomersQueryHandler : BaseHandler, IRequestHandler<SearchCustomersQuery, IList<CustomerResponse>>
    {
        public SearchCustomersQueryHandler(AlintaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IList<CustomerResponse>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await DbContext
                .Customers
                .Where(x => x.FirstName.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase) ||
                            x.LastName.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase))
                .ToListAsync(cancellationToken);

            if (customers == null || !customers.Any())
                return null;

            var customersResponse = customers.Select(x => Mapper.Map<CustomerResponse>(x));
            return customersResponse.ToList();
        }
    }
}
