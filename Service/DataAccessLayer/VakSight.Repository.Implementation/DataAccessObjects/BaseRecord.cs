using System;

namespace VakSight.Repository.Implementation.DataAccessObjects
{
    public abstract class BaseRecord
    {
        public long Key { get; set; }
        public Guid Id { get; set; }
    }
}
