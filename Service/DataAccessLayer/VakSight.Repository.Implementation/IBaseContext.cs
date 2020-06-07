using System;
using System.Collections.Generic;
using System.Text;
using VakSight.Shared.DataAccess;

namespace VakSight.Repository.Implementation
{
    public interface IBaseContext<TUserRecord> where TUserRecord : IBaseUserRecord
    {
    }
}
