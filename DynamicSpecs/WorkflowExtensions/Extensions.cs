namespace DynamicSpecs.Core.WorkflowExtensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class which is used to register extensions.
    /// </summary>
    public class Extensions
    {
        /// <summary>
        /// All registered extensions.
        /// </summary>
        private static readonly Dictionary<Type, List<IExtend>> RegisteredExtensions = new Dictionary<Type, List<IExtend>>();

        /// <summary>
        /// Tries the get a list of extensions for the given type.
        /// </summary>
        /// <param name="key">
        /// The type for which to request the extensions.
        /// </param>
        /// <param name="value">
        /// The list of extensions registered for the type.
        /// </param>
        /// <returns>
        /// True if extensions were registered, false if not.
        /// </returns>
        internal static bool TryGetValue(Type key, out List<IExtend> value)
        {
            lock (RegisteredExtensions)
            {
                return RegisteredExtensions.TryGetValue(key, out value);
            }
        }

        /// <summary>
        /// Defines that an extension for the target type needs to be registered.
        /// </summary>
        /// <typeparam name="TTarget">
        /// Type which will be extended.
        /// </typeparam>
        /// <returns>
        /// Instance with which will be used for additional configuration.
        /// </returns>
        protected static ExtensionPoint<TTarget> Extend<TTarget>() where TTarget : class
        {
            var extensionPoint = new ExtensionPoint<TTarget>();
            Add(typeof(TTarget), extensionPoint);
            return extensionPoint;
        }

        private static void Add(Type type, IExtend extension)
        {
            lock (RegisteredExtensions)
            {
                if (!RegisteredExtensions.ContainsKey(type))
                {
                    RegisteredExtensions.Add(type, new List<IExtend>());
                }

                RegisteredExtensions[type].Add(extension);
            }
        }
    }
}