using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCoreLibrary.Auth.Interfaces
{
    public interface IAuthResult
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }
}
