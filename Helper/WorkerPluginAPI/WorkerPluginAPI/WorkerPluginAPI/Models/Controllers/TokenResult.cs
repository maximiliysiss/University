﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Models.Controllers
{
    public class TokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
