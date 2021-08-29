using System;

namespace Alinta.CodingTest.Models
{
    public abstract class Entity
    {
        public long Id { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset DateModified { get; set; }
    }
}
