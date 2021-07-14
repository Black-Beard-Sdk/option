using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Bb.Extensions.Configuration
{

    /// <summary>
    ///     Represents a SecureStore file as an <see cref="IConfigurationSource" />.
    /// </summary>
    public class SecureStoreConfigurationSource : FileConfigurationSource
    {

        /// <summary>
        /// Used to access the contents of the key file.
        /// </summary>
        public IFileProvider KeyFileProvider { get; set; }

        /// <summary>
        /// The key to decrypt the SecureStore file.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Builds the <see cref="SecureStoreConfigurationProvider" /> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder" />.</param>
        /// <returns>A <see cref="SecureStoreConfigurationProvider" /></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            // ref: https://github.com/aspnet/Configuration/blob/master/src/Config.FileExtensions/FileConfigurationSource.cs#L59
            KeyFileProvider ??= builder.GetFileProvider();
            return new SecureStoreConfigurationProvider(this);
        }

        /// <summary>
        /// If no file provider has been set, for absolute Key path, this will creates a physical file provider
        /// for the nearest existing directory.
        /// </summary>
        public void ResolveKeyFileProvider()
        {
            // ref: https://github.com/aspnet/Configuration/blob/master/src/Config.FileExtensions/FileConfigurationSource.cs#L67
            if (KeyFileProvider == null &&
                !string.IsNullOrEmpty(Key) //&&
                //System.IO.Path.IsPathRooted(Key)
                )
            {
                var directory = System.IO.Path.GetDirectoryName(Path);
                var pathToFile = System.IO.Path.GetFileName(Path);
                while (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    pathToFile = System.IO.Path.Combine(System.IO.Path.GetFileName(directory), pathToFile);
                    directory = System.IO.Path.GetDirectoryName(directory);
                }

                if (Directory.Exists(directory))
                {
                    KeyFileProvider = new PhysicalFileProvider(directory);
                    Key = pathToFile;
                }
            }
        }


    }
}
