using Bb.Option;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace OptionTestProject
{
    [TestClass]
    public class UnitTest1
    {

        public UnitTest1()
        {
            this._directoryRoot = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, @"..\..\..\.."));
            this._directoryDocTests = new DirectoryInfo(Path.Combine(_directoryRoot.FullName, @"DocTests"));
            this._directoryout = new DirectoryInfo(Path.Combine(_directoryRoot.FullName, @"outTests"));

            if (!this._directoryout.Exists)
                this._directoryout.Create();
        }


        [TestMethod]
        public void TestListType()
        {

            string[] args = new string[]
            {
                "schema", "list", "--assembly", typeof(UnitTest1).Assembly.Location, "--assembly", typeof(UnitTest1).Assembly.Location,
            };

            var result = Bb.CommandLine.Run<CommandLines, CommandLineOption>(args) as Bb.Option.CommandLineOption;
            var types = result.Result as List<Type>;

            Assert.AreEqual(types.Count, 1);
            Assert.AreEqual(types[0], typeof(Config));

        }

        [TestMethod]
        public void TestGenerateSchemaType()
        {

            var outPath = Path.Combine(this._directoryout.FullName, "schemas");

            string[] args = new string[]
            {
                "schema", "generate", outPath, "--assembly", typeof(UnitTest1).Assembly.Location
            };

            var result = Bb.CommandLine.Run<CommandLines, CommandLineOption>(args) as Bb.Option.CommandLineOption;
            var types = result.Result as List<Type>;

            //Assert.AreEqual(types.Count, 1);
            //Assert.AreEqual(types[0], typeof(Config));

        }

        [TestMethod]
        public void TestSecure()
        {

            var path = Path.Combine(this._directoryout.FullName, "securevault.json");

            if (File.Exists(path))
                File.Delete(path);


            //string[] args = new string[]
            //{
            //    "secure", "create", path, "--pwd",  "toto"
            //};

            //var result = Bb.CommandLine.Run<CommandLines, CommandLineOption>(args) as Bb.Option.CommandLineOption;


            var args = new string[]
            {
                "secure", "set", "--file", path, "--k", "mykey1", "--v", "tata", "--pwd", "mypwd"
            };

            var result = Bb.CommandLine.Run<CommandLines, CommandLineOption>(args) as Bb.Option.CommandLineOption;

            args = new string[]
           {
                "secure", "get", "--file", path, "--k", "mykey1", "--pwd", "mypwd"
           };

            result = Bb.CommandLine.Run<CommandLines, CommandLineOption>(args) as Bb.Option.CommandLineOption;


            //Assert.AreEqual(types.Count, 1);
            //Assert.AreEqual(types[0], typeof(Config));

        }

        private readonly DirectoryInfo _directoryRoot;
        private readonly DirectoryInfo _directoryout;
        private readonly DirectoryInfo _directoryDocTests;

    }

    [System.ComponentModel.Category("Configuration")]
    public class Config
    {


        public string Test { get; }

    }

}
