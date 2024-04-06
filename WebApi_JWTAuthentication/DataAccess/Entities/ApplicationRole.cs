﻿using Microsoft.AspNetCore.Identity;

namespace WebApiJWTAuthentication.DataAccess.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}

