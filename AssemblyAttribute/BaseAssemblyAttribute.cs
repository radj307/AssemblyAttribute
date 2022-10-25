using System;
using System.Reflection;

namespace AssemblyAttribute
{
    /// <summary>
    /// This read-only attribute is intended to bind to an attribute defined in a .csproj file.
    /// This is intended to be used with PowerShell / continuous integration (CI) scripts that modify .csproj files in order to retrieve custom data.
    /// </summary>
    /// <remarks>
    /// In order to implement this, you need to create an object that derives from this abstract class, and add the following to your .csproj file:
    /// <code>
    /// &lt;PropertyGroup&gt;
    /// ...
    /// &lt;[DERIVED]&gt;My custom attribute value&lt;/[DERIVED]&gt;
    /// &lt;/PropertyGroup&gt;
    /// &lt;ItemGroup&gt;
    ///     &lt;AssemblyAttribute Include="[NAMESPACE.DERIVED]"&gt;
    ///         &lt;_Parameter1&gt;$([DERIVED])&lt;/_Parameter1&gt;
    ///     &lt;/AssemblyAttribute&gt;
    /// &lt;/ItemGroup&gt;
    /// </code>
    /// where '[NAMESPACE]' is the namespace where your derived object is located, and '[DERIVED]' is the name of the derived object, exactly as it appears in the code.
    /// <br /><br />
    /// Note that '&lt;PropertyGroup&gt;...&lt;/PropertyGroup&gt;' shown above is the same property group that contains the settings visible through the project's Visual Studio property pages, and this is where the value string may be located.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public abstract class BaseAssemblyAttribute : Attribute
    {
        #region Constructor
        /// <summary>
        /// Creates a new <see cref="BaseAssemblyAttribute"/> instance with the given <paramref name="values"/>.
        /// </summary>
        /// <remarks>
        /// <b>If you're reading this, this likely isn't what you're looking for!</b><br/>
        /// The <see cref="BaseAssemblyAttribute"/> type is automatically constructed when you add it to your <i>.csproj</i> project file and you don't have to call this constructor yourself.<br/>
        /// See the <see href="https://www.nuget.org/packages/AssemblyAttribute#readme-body-tab">documentation</see> for more information.
        /// </remarks>
        /// <param name="values">Any number of string arguments.<br/>The <see cref="Values"/> property is assigned directly to the received array.</param>
        protected BaseAssemblyAttribute(params string[] values) => Values = values;
        #endregion Constructor

        #region Properties
        /// <summary>
        /// These are string(s) passed from the constructor.<br/>
        /// Each one corresponds to 
        /// </summary>
        protected string[] Values { get; }
        #endregion Properties
    }
}
