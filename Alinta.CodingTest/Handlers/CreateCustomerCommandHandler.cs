using System.Threading;
using System.Threading.Tasks;
using Alinta.CodingTest.Commands;
using Alinta.CodingTest.Data;
using Alinta.CodingTest.Models;
using Alinta.CodingTest.Responses;
using AutoMapper;
using MediatR;

namespace Alinta.CodingTest.Handlers
{
    public class CreateCustomerCommandHandler : BaseHandler, IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        public CreateCustomerCommandHandler(AlintaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CustomerResponse> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = Mapper.Map<Models.Customer>(command);
            DbContext.Customers.Add(customer);

            await DbContext.SaveChangesAsync(cancellationToken);

            var customerResponse = Mapper.Map<CustomerResponse>(customer);
            return customerResponse;
        }
    }
}
