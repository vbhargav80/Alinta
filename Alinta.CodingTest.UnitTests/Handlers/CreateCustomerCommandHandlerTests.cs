using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alinta.CodingTest.Commands;
using Alinta.CodingTest.Handlers;
using Alinta.CodingTest.Responses;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Alinta.CodingTest.UnitTests.Handlers
{
    public class CreateCustomerCommandHandlerTests : TestBase
    {
        private readonly IRequestHandler<CreateCustomerCommand, CustomerResponse> _requestHandler;

        public CreateCustomerCommandHandlerTests()
        {
            _requestHandler = new CreateCustomerCommandHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task When_CreatedCustomerCommandIsReceived_CreatesCustomerInDatabase()
        {
            var createCustomerCommand = new CreateCustomerCommand
            {
                DayOfBirth = MockCustomerDayOfBirth,
                FirstName = MockCustomerFirstName,
                LastName = MockCustomerLastName,
                MonthOfBirth = MockCustomerMonthOfBirth,
                YearOfBirth = MockCustomerYearOfBirth
            };

            var response = await _requestHandler.Handle(createCustomerCommand, CancellationToken.None);

            response.Should().NotBeNull();

            response.Id.Should().BeGreaterThan(0);
            response.FirstName.Should().Be(MockCustomerFirstName);
            response.LastName.Should().Be(MockCustomerLastName);
            response.DayOfBirth.Should().Be(MockCustomerDayOfBirth);
            response.MonthOfBirth.Should().Be(MockCustomerMonthOfBirth);
            response.YearOfBirth.Should().Be(MockCustomerYearOfBirth);
        }
    }
}
