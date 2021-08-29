using System.Threading;
using System.Threading.Tasks;
using Alinta.CodingTest.Commands;
using Alinta.CodingTest.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alinta.CodingTest.Handlers
{
    public class DeleteCustomerCommandHandler : BaseHandler, IRequestHandler<DeleteCustomerCommand, bool?>
    {
        public DeleteCustomerCommandHandler(AlintaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<bool?> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (customer == null)
            {
                return null;
            }

            DbContext.Customers.Remove(customer);
            await DbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
