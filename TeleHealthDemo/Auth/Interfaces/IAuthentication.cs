using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TeleHealthDemo.Auth.Interfaces
{
    public interface IAuthentication
    {
        bool IsAuthenticated(HttpRequest request);
    }
}
