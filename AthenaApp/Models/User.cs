﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AthenaApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int CompanyId { get; set; }
    }
}
