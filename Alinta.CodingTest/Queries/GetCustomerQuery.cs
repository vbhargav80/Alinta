using Alinta.CodingTest.Responses;
using MediatR;

namespace Alinta.CodingTest.Queries
{
    public class GetCustomerQuery : IRequest<CustomerResponse>
    {
        public long Id { get; set; }
    }
}
