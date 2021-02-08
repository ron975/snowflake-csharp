﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowflake.Filesystem
{
    public interface IDisposableDirectory
        : IDisposable, 
        IMutableDirectoryBase<IProjectingDirectory>
    {
    }
}