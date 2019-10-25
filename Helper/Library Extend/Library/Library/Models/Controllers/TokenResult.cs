﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Controllers
{
    public class TokenResult
    {
        private string accessToken;
        public string AccessToken
        {
            get => $"Bearer {accessToken}";
            set => accessToken = value;
        }
        private string refreshToken;
        public string RefreshToken
        {
            get => $"Bearer {refreshToken}";
            set => refreshToken = value;
        }
    }
}
