using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alinta.CodingTest.Commands;
using Alinta.CodingTest.Handlers;
using AutoMapper.Configuration.Annotations;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Alinta.CodingTest.UnitTests.Handlers
{
    public class DeleteCustomerCommandHandlerTests : TestBase
    {
        private readonly IRequestHandler<DeleteCustomerCommand, bool?> _requestHandler;

        public DeleteCustomerCommandHandlerTests()
        {
            _requestHandler = new DeleteCustomerCommandHandler(DbContext, Mapper);
        }

        [Fact]
        public async Task WhenCommandIsReceived_DeletesCustomerFromDatabase()
        {
            var customer = InsertMockCustomerInDb();

            var deleteCommand = new DeleteCustomerCommand { Id = customer.Id };

            var response = await _requestHandler.Handle(deleteCommand, CancellationToken.None);

            response.Should().BeTrue();

            var existingCustomer = DbContext.Customers.SingleOrDefault(x => x.Id == customer.Id);
            existingCustomer.Should().BeNull();
        }

        [Fact]
        public async Task When_CustomerDoesNotExistInDatabase_ReturnsNull()
        {
            ClearDatabase();

            var deleteCommand = new DeleteCustomerCommand { Id = 3};
            var response = await _requestHandler.Handle(deleteCommand, CancellationToken.None);

            response.Should().BeNull();
        }
    }
}
