﻿using System.Collections.Generic;

namespace Snowflake.Configuration
{
    public interface IConfigurationCollection<out T> : IConfigurationCollection where T : class, IConfigurationCollection<T>
    {
        T Configuration { get; }
    }

    public interface IConfigurationCollection : IEnumerable<IConfigurationSection> 
    {
        IDictionary<string, IConfigurationSection> Sections { get; }
        IDictionary<string, string> Outputs { get; }
        IConfigurationSection this[string sectionName] { get; }

    }
}
