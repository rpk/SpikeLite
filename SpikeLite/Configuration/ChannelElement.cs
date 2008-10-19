﻿/**
 * SpikeLite C# IRC Bot
 * Copyright (c) 2008 FreeNode ##Csharp Community
 * 
 * This source is licensed under the terms of the MIT license. Please see the 
 * distributed license.txt for details.
 */
using System.Configuration;

namespace SpikeLite.Configuration
{
    public class ChannelElement : ConfigurationElement
    {
        #region Static Fields

        private static readonly ConfigurationProperty _name;
        private static readonly ConfigurationPropertyCollection _properties;

        #endregion

        #region Properties

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base[_name]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Constructor

        static ChannelElement()
        {
            _name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

            _properties = new ConfigurationPropertyCollection {_name};
        }

        #endregion
    }
}