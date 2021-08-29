using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Alinta.CodingTest.Commands
{
    public class DeleteCustomerCommand : IRequest<bool?>
    {
        public long Id { get; set; }
    }
}
