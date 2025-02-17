﻿/**
 * SpikeLite C# IRC Bot
 * Copyright (c) 2009-2011 FreeNode ##Csharp Community
 * 
 * This source is licensed under the terms of the MIT license. Please see the 
 * distributed license.txt for details.
 */

using System.ServiceModel;

namespace SpikeLite.IPC.WebHost.Services
{
    /// <summary>
    /// Provides the service contract for our Ping service. We don't really have any namespace stuff setup, so use the gold ol' fashioned "tempuri."
    /// </summary>
    [ServiceContract(Namespace = "http://tempuri.org")]
    public interface IPingService
    {
        /// <summary>
        /// The simplest possible service: says PONG!.
        /// </summary>
        /// 
        /// <returns>A PONG sequence.</returns>
        [OperationContract]
        [SecuredOperation]
        string Ping();    
    }

    /// <summary>
    /// Implements our <see cref="IPingService"/> contract.
    /// </summary>
    public class PingService : IPingService
    {
        public string Ping()
        {
            return "Pong!";
        }
    }
}
