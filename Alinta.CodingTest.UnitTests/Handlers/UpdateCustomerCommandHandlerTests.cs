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
    public class UpdateCustomerCommandHandlerTests : TestBase
    {
        private readonly IRequestHandler<UpdateCustomerCommand, CustomerResponse> _updateCustomerCommandHandler;

        public UpdateCustomerCommandHandlerTests()
        {
            _updateCustomerCommandHandler = new UpdateCustomerCommandHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task WhenCommandIsReceived_UpdatesCustomerInDatabase()
        {
            var customer = InsertMockCustomerInDb();

            var updateCustomerCommand = new UpdateCustomerCommand
            {
                Id = customer.Id,
                FirstName = "Updated Firstname",
                LastName = "Updated lastname",
                DayOfBirth = 12,
                MonthOfBirth = 11,
                YearOfBirth = 2000
            };

            var response = await _updateCustomerCommandHandler.Handle(updateCustomerCommand, CancellationToken.None);

            response.FirstName.Should().Be("Updated Firstname");
            response.LastName.Should().Be("Updated lastname");
            response.DayOfBirth.Should().Be(12);
            response.MonthOfBirth.Should().Be(11);
            response.YearOfBirth.Should().Be(2000);
        }

        [Fact]
        public async Task When_CustomerDoesNotExistInDatabase_ReturnsNull()
        {
            await DeleteCustomerByIdIfExists(1034);

            var updateCustomerCommand = new UpdateCustomerCommand
            {
                Id = 1034,
                FirstName = "Updated Firstname",
                LastName = "Updated lastname",
                DayOfBirth = 12,
                MonthOfBirth = 11,
                YearOfBirth = 2000
            };
            var response = await _updateCustomerCommandHandler.Handle(updateCustomerCommand, CancellationToken.None);

            response.Should().BeNull();
        }
    }
}
