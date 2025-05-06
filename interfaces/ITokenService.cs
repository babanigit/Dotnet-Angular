using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Angular_Project.Models;

namespace Dotnet_Angular_Project.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}