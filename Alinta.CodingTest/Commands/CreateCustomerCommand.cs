using System.ComponentModel.DataAnnotations;
using Alinta.CodingTest.Responses;
using MediatR;

namespace Alinta.CodingTest.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerResponse>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int DayOfBirth { get; set; }
        [Required]
        public int MonthOfBirth { get; set; }
        [Required]
        public int YearOfBirth { get; set; }
    }
}
