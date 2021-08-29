using System.Linq;
using System.Threading.Tasks;
using Alinta.CodingTest.Data;
using Alinta.CodingTest.Mapping;
using Alinta.CodingTest.Models;
using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alinta.CodingTest.UnitTests
{
    public class TestBase
    {
        public const string MockCustomerFirstName = "Varun";
        public const string MockCustomerLastName = "Bhargava";
        public const int MockCustomerDayOfBirth = 1;
        public const int MockCustomerMonthOfBirth = 2;
        public const int MockCustomerYearOfBirth = 1981;

        protected AlintaDbContext DbContext;
        protected IMapper Mapper;
        protected Fixture Fixture;

        public TestBase()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AlintaDbContext>()
                .UseInMemoryDatabase(databaseName: "AlintaTests")
                .Options;

            DbContext = new AlintaDbContext(dbContextOptions);

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new CustomerProfile());
            });
            Mapper = config.CreateMapper();
            Fixture = new Fixture();
        }

        public void ClearDatabase()
        {
            foreach (var customer in DbContext.Customers)
            {
                DbContext.Customers.Remove(customer);
            }

            DbContext.SaveChanges();
        }

        public Customer InsertMockCustomerInDb(string firstName = MockCustomerFirstName, string lastName = MockCustomerLastName)
        {
            var mockCustomer = Fixture.Build<Customer>()
                .With(x => x.FirstName, firstName)
                .With(x => x.LastName, lastName)
                .With(x => x.DateOfBirth, new DateOfBirth
                {
                    Day = MockCustomerDayOfBirth, 
                    Month = MockCustomerMonthOfBirth, 
                    Year = MockCustomerYearOfBirth
                })
                .Create();

            DbContext.Customers.Add(mockCustomer);
            DbContext.SaveChanges();

            return mockCustomer;
        }

        public async Task DeleteCustomerByIdIfExists(long customerId)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId);
            if (customer != null)
            {
                DbContext.Customers.Remove(customer);
            }

            await DbContext.SaveChangesAsync();
        }
    }
}
