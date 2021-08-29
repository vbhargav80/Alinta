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
    public class GetCustomerQueryHandler : BaseHandler, IRequestHandler<GetCustomerQuery, CustomerResponse>
    {
        public GetCustomerQueryHandler(AlintaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (customer == null)
                return null;

            var customerResponse = Mapper.Map<CustomerResponse>(customer);

            return customerResponse;
        }
    }
}
