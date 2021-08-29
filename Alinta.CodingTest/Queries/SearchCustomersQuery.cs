using System.Collections.Generic;
using Alinta.CodingTest.Responses;
using MediatR;

namespace Alinta.CodingTest.Queries
{
    public class SearchCustomersQuery : IRequest<IList<CustomerResponse>>
    {
        public string Keyword { get; set; }
    }
}
