using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowflake.Romfile.Naming
{
    /// <summary>
    /// Represents a version in a filename
    /// </summary>
    public sealed record Version
    {
        /// <summary>
        /// The tag used to specify this version. 
        /// For example, 'Version' or 'Rev'
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// The major version or revision number.
        /// </summary>
        public string Major { get; }

        /// <summary>
        /// The minor version or revision number
        /// </summary>
        public string Minor { get; }

        /// <summary>
        /// Intantiates a Version instance.
        /// </summary>
        /// <param name="tag">The tag used to specify this version. </param>
        /// <param name="major">The major version.</param>
        /// <param name="minor">The minor version</param>
        public Version(string tag, string major, string minor) => (Tag, Major, Minor) = (tag, major, minor);
    }
}
