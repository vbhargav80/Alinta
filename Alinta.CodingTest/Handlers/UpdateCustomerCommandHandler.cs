using System;
using System.Threading;
using System.Threading.Tasks;
using Alinta.CodingTest.Commands;
using Alinta.CodingTest.Data;
using Alinta.CodingTest.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alinta.CodingTest.Handlers
{
    public class UpdateCustomerCommandHandler : BaseHandler, IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        public UpdateCustomerCommandHandler(AlintaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CustomerResponse> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (customer == null)
            {
                return null;
            }

            Mapper.Map(command, customer);

            await DbContext.SaveChangesAsync(cancellationToken);
            var customerResponse = Mapper.Map<CustomerResponse>(customer);

            return customerResponse;
        }
    }
}
