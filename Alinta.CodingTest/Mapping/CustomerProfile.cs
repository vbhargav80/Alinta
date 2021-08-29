using Alinta.CodingTest.Commands;
using Alinta.CodingTest.Models;
using Alinta.CodingTest.Responses;
using AutoMapper;

namespace Alinta.CodingTest.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerCommand, Models.Customer>()
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => new DateOfBirth
                {
                    Day = s.DayOfBirth,
                    Month = s.MonthOfBirth,
                    Year = s.YearOfBirth
                }));

            CreateMap<UpdateCustomerCommand, Models.Customer>()
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => new DateOfBirth
                {
                    Day = s.DayOfBirth,
                    Month = s.MonthOfBirth,
                    Year = s.YearOfBirth
                }));

            CreateMap<Models.Customer, CustomerResponse>()
                .ForMember(d => d.DayOfBirth, opt => opt.MapFrom(s => s.DateOfBirth.Day))
                .ForMember(d => d.MonthOfBirth, opt => opt.MapFrom(s => s.DateOfBirth.Month))
                .ForMember(d => d.YearOfBirth, opt => opt.MapFrom(s => s.DateOfBirth.Year))
                ;
        }
    }
}
