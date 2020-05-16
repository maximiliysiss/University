using AuthAPI.Models.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommonCoreLibrary.APIClient
{
    public interface IAuthBaseAPIClient
    {
        Task AuthLoginGetAsync();
        Task<LoginResult> AuthRefreshtokenAsync(string v1, string v2);
    }
}
