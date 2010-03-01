﻿/**
 * SpikeLite C# IRC Bot
 * Copyright (c) 2008 FreeNode ##Csharp Community
 * 
 * This source is licensed under the terms of the MIT license. Please see the 
 * distributed license.txt for details.
 */
using SpikeLite.Domain.Model.Authentication;

namespace SpikeLite.AccessControl
{
    /// <summary>
    /// Defines the public contract for a given authentication provider.
    /// </summary>
    public interface IAuthenticationModule
    {
        /// <summary>
        /// Attempts to resolve an anonymous set of user information into a user within the system.
        /// </summary>
        /// 
        /// <param name="user">A <see cref="UserToken"/> representing a message source.</param>
        /// 
        /// <returns>An <see cref="AuthToken"/> for the user information.</returns>
        AuthToken Authenticate(UserToken user);

        /// <summary>
        /// Returns a <see cref="KnownHost"/> for the given cloak given. Is guaranteed to return a single item, or null if none is found. Multiple
        /// items will yield an exception.
        /// </summary>
        /// 
        /// <param name="cloak">The cloak literal string to search for.</param>
        /// 
        /// <returns>The <see cref="KnownHost"/> for the cloak in question, or null if no such host exists.</returns>
        KnownHost FindHostByCloak(string cloak);
    }
}