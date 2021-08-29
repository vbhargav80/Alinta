using Alinta.CodingTest.Data;
using AutoMapper;

namespace Alinta.CodingTest.Handlers
{
    public abstract class BaseHandler
    {
        protected readonly AlintaDbContext DbContext;
        protected readonly IMapper Mapper;

        protected BaseHandler(AlintaDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }
    }
}
