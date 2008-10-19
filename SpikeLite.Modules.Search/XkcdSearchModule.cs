﻿/**
 * SpikeLite C# IRC Bot
 * Copyright (c) 2008 FreeNode ##Csharp Community
 * 
 * This source is licensed under the terms of the MIT license. Please see the 
 * distributed license.txt for details.
 */
using SpikeLite.Persistence.Authentication;

namespace SpikeLite.Modules.Search
{
    [Module("xkcd", "Search the XKCD comic", "~xkcd <search terms>", AccessLevel.Public)]
    public class XkcdSearchModule : SiteSearchModuleBase
    {
        public XkcdSearchModule() : base("xkcd", "xkcd", "xkcd.com/") {}
    }
}