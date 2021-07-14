using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bb.Extensions.Secures;

namespace Bb.Extensions.Configuration
{

    /// <summary>
    ///     A SecureStore file based <see cref="FileConfigurationProvider" />.
    /// </summary>
    public class SecureStoreConfigurationProvider : FileConfigurationProvider
    {

        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public SecureStoreConfigurationProvider(SecureStoreConfigurationSource source) : base(source)
        {

        }

        /// <summary>
        /// Loads the SecureStore data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {

            var source = (SecureStoreConfigurationSource)Source;
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var vault = Vault.LoadStore(stream);
            if (!string.IsNullOrEmpty(source.Key))
                vault.InitializeVault(source.Key);

            foreach (var item in vault.Entries)
                dictionary.Add(item.Key, item.Decode(vault));

            Data = dictionary;

        }

    }
}
