using System;
using System.Collections;
using System.Reflection;

namespace AssemblyAttribute
{
    /// <summary><see cref="BaseAssemblyAttribute"/>-derived attribute that can be used for tags that contain semantic versioning.<br/>This doesn't follow attribute naming conventions because it isn't a conventional attribute.</summary>
    /// <remarks>
    /// Possible Implementation:
    /// <code language="xml">
    /// &lt;PropertyGroup&gt;
    ///    &lt;!-- ... --&gt;
    ///    &lt;ExtendedVersion&gt;0.1.2-rev3.4&lt;/ExtendedVersion&gt;
    /// &lt;/PropertyGroup&gt;
    /// 
    /// 
    /// &lt;ItemGroup&gt;
    ///     &lt;AssemblyAttribute Include="AssemblyAttribute.ExtendedVersion"&gt;
    ///         &lt;_Parameter1&gt;$(ExtendedVersion)&lt;/_Parameter1&gt;
    ///     &lt;/AssemblyAttribute&gt;
    /// &lt;/ItemGroup&gt;
    /// </code>
    /// 
    /// Usage:
    /// <code language="cs">
    /// string version = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttribute&lt;ExtendedVersion&gt;().Version;
    /// </code>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class ExtendedVersion : BaseAssemblyAttribute, ICollection, IEnumerable, IList, IStructuralComparable, IStructuralEquatable, ICloneable
    {
        #region Constructor
        /// <summary>Constructor.</summary>
        /// <remarks><b>This shouldn't be called directly, see <see cref="BaseAssemblyAttribute"/> for more information.</b></remarks>
        /// <param name="value">The attribute's associated value.</param>
        public ExtendedVersion(string value) : base(value)
        {
            _segments = Version.Split('.', '-', '+');
        }
        #endregion Constructor

        #region Fields
        private readonly string[] _segments;
        #endregion Fields

        #region Properties
        /// <summary>The full version as a string.</summary>
        public string Version => Value;

        /// <inheritdoc/>
        public int Count => ((ICollection)_segments).Count;

        /// <inheritdoc/>
        public bool IsSynchronized => _segments.IsSynchronized;

        /// <inheritdoc/>
        public object SyncRoot => _segments.SyncRoot;

        /// <inheritdoc/>
        public bool IsFixedSize => _segments.IsFixedSize;

        /// <inheritdoc/>
        public bool IsReadOnly => _segments.IsReadOnly;
        #endregion Properties

        #region Indexers
        /// <inheritdoc/>
        public object this[int index] { get => ((IList)_segments)[index]; set => ((IList)_segments)[index] = value; }
        #endregion Indexers

        #region Methods
        /// <inheritdoc/>
        public void CopyTo(Array array, int index) => _segments.CopyTo(array, index);
        /// <inheritdoc/>
        public IEnumerator GetEnumerator() => _segments.GetEnumerator();
        /// <inheritdoc/>
        public int Add(object value) => ((IList)_segments).Add(value);
        /// <inheritdoc/>
        public void Clear() => ((IList)_segments).Clear();
        /// <inheritdoc/>
        public bool Contains(object value) => ((IList)_segments).Contains(value);
        /// <inheritdoc/>
        public int IndexOf(object value) => ((IList)_segments).IndexOf(value);
        /// <inheritdoc/>
        public void Insert(int index, object value) => ((IList)_segments).Insert(index, value);
        /// <inheritdoc/>
        public void Remove(object value) => ((IList)_segments).Remove(value);
        /// <inheritdoc/>
        public void RemoveAt(int index) => ((IList)_segments).RemoveAt(index);
        /// <inheritdoc/>
        public int CompareTo(object other, IComparer comparer) => ((IStructuralComparable)_segments).CompareTo(other, comparer);
        /// <inheritdoc/>
        public bool Equals(object other, IEqualityComparer comparer) => ((IStructuralEquatable)_segments).Equals(other, comparer);
        /// <inheritdoc/>
        public int GetHashCode(IEqualityComparer comparer) => ((IStructuralEquatable)_segments).GetHashCode(comparer);
        /// <inheritdoc/>
        public object Clone() => _segments.Clone();
        #endregion Methods
    }
}
