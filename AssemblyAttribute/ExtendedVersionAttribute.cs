using System;
using System.Collections;
using System.Linq;

namespace AssemblyAttribute
{
    /// <summary><see cref="BaseAssemblyAttribute"/>-derived attribute that can be used for tags that contain semantic versioning.<br/>This doesn't follow attribute naming conventions because it isn't a conventional attribute.</summary>
    /// <remarks>
    /// Possible Implementation:
    /// <code language="xml">
    /// &lt;PropertyGroup&gt;
    ///    &lt;!-- ... --&gt;
    ///    &lt;ExtendedVersionAttribute&gt;0.1.2-rev3.4&lt;/ExtendedVersionAttribute&gt;
    /// &lt;/PropertyGroup&gt;
    /// 
    /// 
    /// &lt;ItemGroup&gt;
    ///     &lt;AssemblyAttribute Include="AssemblyAttribute.ExtendedVersionAttribute"&gt;
    ///         &lt;_Parameter1&gt;$(ExtendedVersionAttribute)&lt;/_Parameter1&gt;
    ///     &lt;/AssemblyAttribute&gt;
    /// &lt;/ItemGroup&gt;
    /// </code>
    /// 
    /// Usage:
    /// <code language="cs">
    /// string version = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttribute&lt;ExtendedVersionAttribute&gt;().Version;
    /// </code>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class ExtendedVersionAttribute : BaseAssemblyAttribute
    {
        #region Constructor
        /// <summary>Constructor.</summary>
        /// <remarks><b>This shouldn't be called directly, see <see cref="BaseAssemblyAttribute"/> for more information.</b></remarks>
        /// <param name="value">The attribute's associated value.</param>
        public ExtendedVersionAttribute(params string[] values) : base(values)
        {
            Version = values.Aggregate(string.Concat);
            VersionSegments = Version.Split('.', '-', '+');
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets the full version as a string.
        /// </summary>
        /// <remarks>This value is cached and is read-only.</remarks>
        public string Version { get; }
        /// <summary>
        /// This is the value resulting from:
        /// <code>Version.Split('.', '-', '+')</code>
        /// </summary>
        /// <remarks>This value is cached and is read-only.</remarks>
        public string[] VersionSegments { get; }
        #endregion Methods
    }
}
