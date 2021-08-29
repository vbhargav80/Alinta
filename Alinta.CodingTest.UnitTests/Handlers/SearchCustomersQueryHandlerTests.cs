using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alinta.CodingTest.Handlers;
using Alinta.CodingTest.Queries;
using Alinta.CodingTest.Responses;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Alinta.CodingTest.UnitTests.Handlers
{
    public class SearchCustomersQueryHandlerTests : TestBase
    {
        private readonly IRequestHandler<SearchCustomersQuery, IList<CustomerResponse>> _requestHandler;

        public SearchCustomersQueryHandlerTests() : base()
        {
            _requestHandler = new SearchCustomersQueryHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task WhenNoMatchingRecordsInDb_ReturnsNull()
        {
            var query = new SearchCustomersQuery { Keyword = "findMe" };

            var response = await _requestHandler.Handle(query, CancellationToken.None);

            response.Should().BeNull();
        }

        [Fact]
        public async Task WhenMatchingRecordsInDb_ReturnsMatchesOnFirstNameOrLastName()
        {
            var mockCustomer1 = InsertMockCustomerInDb("Martin", "Jones");
            var mockCustomer2 = InsertMockCustomerInDb("Simon", "Bart");
            var mockCustomer3 = InsertMockCustomerInDb();

            var query = new SearchCustomersQuery { Keyword = "art" };

            var response = await _requestHandler.Handle(query, CancellationToken.None);

            response.Count.Should().Be(2);

            response[0].FirstName.Should().Be(mockCustomer1.FirstName);
            response[1].LastName.Should().Be(mockCustomer2.LastName);
        }
    }
}
