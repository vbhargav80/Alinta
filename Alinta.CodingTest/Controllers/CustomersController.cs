using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Alinta.CodingTest.Commands;
using Alinta.CodingTest.Queries;
using Alinta.CodingTest.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Alinta.CodingTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a customer with the given id
        /// </summary>
        /// <param name="id">id of the customer</param>
        /// <returns>CustomerResponse</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="400">Customer could not be found</response>
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomer(long id)
        {
            var customerResponse = await _mediator.Send(new GetCustomerQuery {Id = id});
            if (customerResponse == null)
            {
                return NotFound();
            }

            return Ok(customerResponse);
        }

        /// <summary>
        /// Searches for customers where the keyword matches the first name or the last name
        /// </summary>
        /// <param name="keyword">keyword to search in the first or last name</param>
        /// <returns>CustomerResponse</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="204">No customers matched the keyword</response>
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IList<CustomerResponse>>> SearchCustomers([FromQuery]string keyword)
        {
            var customerResponse = await _mediator.Send(new SearchCustomersQuery { Keyword = keyword});
            if (customerResponse == null || !customerResponse.Any())
            {
                return NoContent();
            }

            return Ok(customerResponse);
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <response code="201">Customer created successfully</response>
        /// <response code="400">Customer data is invalid</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer(CreateCustomerCommand createCustomerCommand)
        {
            var customerResponse = await _mediator.Send(createCustomerCommand);

            return CreatedAtAction(nameof(GetCustomer), new GetCustomerQuery { Id = customerResponse.Id}, customerResponse);
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <response code="204">Customer updated successfully</response>
        /// <response code="404">Customer could not be found</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResponse>> Put(long id, [FromBody] UpdateCustomerCommand updateCustomerCommand)
        {
            var customerResponse = await _mediator.Send(updateCustomerCommand);
            if (customerResponse == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <response code="204">Customer deleted successfully</response>
        /// <response code="404">Customer could not be found</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool?>> Delete(int id)
        {
            var deletionResponse = await _mediator.Send(new DeleteCustomerCommand { Id = id});
            if (deletionResponse == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
