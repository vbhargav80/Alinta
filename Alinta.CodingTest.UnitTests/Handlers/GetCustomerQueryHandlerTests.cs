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
    public class GetCustomerQueryHandlerTests : TestBase
    {
        private readonly IRequestHandler<GetCustomerQuery, CustomerResponse> _getCustomerQueryHandler;

        public GetCustomerQueryHandlerTests() : base()
        {
            _getCustomerQueryHandler = new GetCustomerQueryHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task When_CustomerExistsInDatabase_ReturnsCustomer()
        {
            var customer = InsertMockCustomerInDb();

            var getCustomerQuery = new GetCustomerQuery { Id = customer.Id };
            var response = await _getCustomerQueryHandler.Handle(getCustomerQuery, CancellationToken.None);

            response.Should().NotBeNull();
            response.Id.Should().Be(customer.Id);
            response.FirstName.Should().Be(MockCustomerFirstName);
            response.LastName.Should().Be(MockCustomerLastName);
        }

        [Fact]
        public async Task When_CustomerDoesNotExistInDatabase_ReturnsNull()
        {
            var getCustomerQuery = new GetCustomerQuery { Id = 3 };
            var response = await _getCustomerQueryHandler.Handle(getCustomerQuery, CancellationToken.None);

            response.Should().BeNull();
        }
    }
}
