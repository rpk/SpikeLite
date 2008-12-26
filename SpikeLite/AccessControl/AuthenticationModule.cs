﻿/**
 * SpikeLite C# IRC Bot
 * Copyright (c) 2008 FreeNode ##Csharp Community
 * 
 * This source is licensed under the terms of the MIT license. Please see the 
 * distributed license.txt for details.
 */
namespace SpikeLite.AccessControl
{
    /// <summary>
    /// Defines the public contract for a given authentication provider.
    /// </summary>
    public interface AuthenticationModule
    {
        AuthToken Authenticate( UserToken user );
    }
}
