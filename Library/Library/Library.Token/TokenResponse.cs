﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Token
{
    public class TokenResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public Token Token { get; set; }
    }
}
