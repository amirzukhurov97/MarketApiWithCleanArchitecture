﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.DTOs.Auth
{
    public record AuthSessionToken
    {
        public string AccessToken { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}
