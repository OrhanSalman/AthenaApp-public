﻿/*
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaWebApp.Areas.Identity.Data
{

    // ToDo: What To Do with these?


    // Uses all the built-in Identity types
    // Uses `string` as the key type
    public class IdentityRolesDbContext
        : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
    }

    // Uses the built-in Identity types except with a custom User type
    // Uses `string` as the key type
    public class IdentityRolesDbContext<TUser>
        : IdentityDbContext<TUser, IdentityRole, string>
            where TUser : IdentityUser
    {
    }

    // Uses the built-in Identity types except with custom User and Role types
    // The key type is defined by TKey
    public class IdentityRolesDbContext<TUser, TRole, TKey> : IdentityDbContext<
        TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
        IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
    {
    }

    // No built-in Identity types are used; all are specified by generic arguments
    // The key type is defined by TKey
    public abstract class IdentityRolesDbContext<
        TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        : IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken>
             where TUser : IdentityUser<TKey>
             where TRole : IdentityRole<TKey>
             where TKey : IEquatable<TKey>
             where TUserClaim : IdentityUserClaim<TKey>
             where TUserRole : IdentityUserRole<TKey>
             where TUserLogin : IdentityUserLogin<TKey>
             where TRoleClaim : IdentityRoleClaim<TKey>
             where TUserToken : IdentityUserToken<TKey>
    {
    }
        
}
*/
