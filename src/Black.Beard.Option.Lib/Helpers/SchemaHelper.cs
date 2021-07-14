﻿using System;
using NJsonSchema;
using NJsonSchema.Generation;
using System.Text;
using System.Collections.Generic;

namespace Bb.Helpers
{

    public static class SchemaHelper
    {

        static SchemaHelper()
        {

            _settings = new NJsonSchema.Generation.JsonSchemaGeneratorSettings()
            {
                FlattenInheritanceHierarchy = true,
                GenerateEnumMappingDescription = true,
            };

            _settings.ActualSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

        }

        /// <summary>
        /// Generates JSON schema for a given C# class using Newtonsoft.Json.Schema library.
        /// </summary>
        /// <param name="myType">class type</param>
        /// <returns>a string containing JSON schema for a given class type</returns>
        public static JsonSchema GenerateSchemaForClass(this Type myType, string name)
        {

            //var b = myType.IsGenericType && myType.GetGenericTypeDefinition() == typeof(OptionConfigurationList<>);

            string id = myType.Name;
            //if (b)
            //    id = GetUri(id.Substring(0, id.IndexOf('`')), myType.GetGenericArguments()[0].Name);
            //else
            id = GetUri(id);

            var _schema = JsonSchema.FromType(myType, _settings);
            _schema.AllowAdditionalProperties = true;
            _schema.Id = id;
            
            //if (b)
            //{
            //    var o = _schema.Properties[nameof(OptionConfigurationList<object>.Kind)];
            //    o.Enumeration.Add(myType.AssemblyQualifiedName);
            //}

            return _schema;

        }

        public static string GetUri(params string[] args)
        {
            var result = System.IO.Path.Combine(args);
            result = System.IO.Path.Combine(result, "schema").Replace("\\", "/");
            return "http://" + result;
        }

        public static Type GetSerializerType(this Type type)
        {

            return typeof(OptionConfigurationList<>).MakeGenericType(type);

        }

        private static readonly JsonSchemaGeneratorSettings _settings;

    }

    public class OptionConfigurationList<T>
    {

        public string Kind { get; set; }

        public List<OptionConfiguration<T>> Items { get; set; }

    }

    public class OptionConfiguration<T>
    {

        public string Name { get; set; }

        public T Datas { get; set; }

    }

}
