using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;

namespace Bleaky.Infrastructure.Authentication
{
    public interface IUserMapper
    {
        IUserIdentity GetUserIdentityFromIdentifier(Guid userId);
    }
}
