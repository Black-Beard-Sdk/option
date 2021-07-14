using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Bb.Extensions.Configuration
{

    /// <summary>
    ///     Extension methods for adding <see cref="SecureStoreConfigurationProvider" />.
    /// </summary>
    public static class SecureStoreConfigurationExtensions
    {

        /// <summary>
        ///     Adds the SecureStore configuration provider at <paramref name="path" /> to <paramref name="builder" />.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder" /> to add to.</param>
        /// <param name="path">
        ///     Path relative to the base path stored in
        ///     <see cref="IConfigurationBuilder.Properties" /> of <paramref name="builder" />.
        /// </param>
        /// <param name="key">The SecureStore key.</param>
        /// <returns>The <see cref="IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder AddSecureStoreFile(this IConfigurationBuilder builder, string path,
            string key)
        {
            return AddSecureStoreFile(builder, null, path, key, false, false);
        }

        /// <summary>
        ///     Adds the SecureStore configuration provider at <paramref name="path" /> to <paramref name="builder" />.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder" /> to add to.</param>
        /// <param name="path">
        ///     Path relative to the base path stored in
        ///     <see cref="IConfigurationBuilder.Properties" /> of <paramref name="builder" />.
        /// </param>
        /// <param name="key">The SecureStore key.</param>
        /// <param name="optional">Whether the file is optional.</param>
        /// <returns>The <see cref="IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder AddSecureStoreFile(this IConfigurationBuilder builder, string path,
            string key, bool optional)
        {
            return AddSecureStoreFile(builder, null, path, key, optional, false);
        }

        /// <summary>
        ///     Adds the SecureStore configuration provider at <paramref name="path" /> to <paramref name="builder" />.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder" /> to add to.</param>
        /// <param name="path">
        ///     Path relative to the base path stored in
        ///     <see cref="IConfigurationBuilder.Properties" /> of <paramref name="builder" />.
        /// </param>
        /// <param name="key">The SecureStore key.</param>
        /// <param name="optional">Whether the file is optional.</param>
        /// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes.</param>
        /// <returns>The <see cref="IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder AddSecureStoreFile(this IConfigurationBuilder builder, string path,
            string key, bool optional,
            bool reloadOnChange)
        {
            return AddSecureStoreFile(builder, null, path, key, optional, reloadOnChange);
        }

        /// <summary>
        ///     Adds a SecureStore configuration source to <paramref name="builder" />.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder" /> to add to.</param>
        /// <param name="provider">The <see cref="IFileProvider" /> to use to access the file.</param>
        /// <param name="path">
        ///     Path relative to the base path stored in
        ///     <see cref="IConfigurationBuilder.Properties" /> of <paramref name="builder" />.
        /// </param>
        /// <param name="key">The SecureStore key.</param>
        /// <param name="optional">Whether the file is optional.</param>
        /// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes.</param>
        /// <returns>The <see cref="IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder AddSecureStoreFile(this IConfigurationBuilder builder,
            IFileProvider provider, string path, string key, bool optional, bool reloadOnChange)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("File path must be a non-empty string.", nameof(path));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("File key/path must be a non-empty string.", nameof(key));

            return builder.AddSecureStoreFile(source =>
            {
                source.FileProvider = provider;
                source.Path = path;
                source.Key = key;
                source.Optional = optional;
                source.ReloadOnChange = reloadOnChange;
                source.ResolveFileProvider();
                source.ResolveKeyFileProvider();
            });

        }

        /// <summary>
        ///     Adds a SecureStore configuration source to <paramref name="builder" />.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder" /> to add to.</param>
        /// <param name="configureSource">Configures the source.</param>
        /// <returns>The <see cref="IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder AddSecureStoreFile(this IConfigurationBuilder builder,
            Action<SecureStoreConfigurationSource> configureSource)
        {
            return builder.Add(configureSource);
        }
    }
}
