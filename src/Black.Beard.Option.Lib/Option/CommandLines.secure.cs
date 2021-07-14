using Bb.CommandLines;
using Bb.CommandLines.Validators;
using System;
using System.IO;
using Bb.CommandLines.Ins;
using Bb.CommandLines.Outs;
using Bb.Extensions.Secures;

namespace Bb.Option
{

    public partial class CommandLines : Command<CommandLineOption>
    {

        public CommandLineOption CommandSecure(CommandLineOption app)
        {

            var schema = app.Command("secure", config =>
            {
                config.Description = "manage secure configuration";
                config.HelpOption(HelpFlag);
            });

            schema.Command("set", config =>
            {

                config.Description = "create a new key";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);
                var keyPathOpt = validator.Option("--file", "path where the file key is generated");
                var passwordOpt = validator.Option("--pwd", "specified the password");

                var keyOpt = validator.Option("--k", "key must be encrypted");
                var valueOpt = validator.Option("--v", "value must be encrypted");

                config.OnExecute(() =>
                {

                    string password = string.Empty;

                    if (!validator.Evaluate(out int result))
                        return result;

                    string secretName = keyOpt.Value().TrimPath(); ;
                    string secretValue = valueOpt.Value().TrimPath();

                    Vault vault;
                    FileInfo keyfile = null;
                    if (keyPathOpt.HasValue())
                    {
                        keyfile = new FileInfo(keyPathOpt.Value().TrimPath());

                        vault = Vault.LoadStore(keyfile.FullName);
                        if (passwordOpt.HasValue())
                            vault.InitializeVault(passwordOpt.Value().TrimPath());

                        vault.Add(secretName, secretValue);

                        vault.SaveStore(keyfile.FullName);

                    }

                    return 0;

                });
            });

            schema.Command("get", config =>
            {

                config.Description = "get decrypted value for the specified key";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);
                var keyPathOpt = validator.Option("--file", "path where the file key is generated");
                var passwordOpt = validator.Option("--pwd", "specified the password");

                var keyOpt = validator.Option("--k", "key must be encrypted");

                config.OnExecute(() =>
                {

                    string password = string.Empty;

                    if (!validator.Evaluate(out int result))
                        return result;

                    string secretName = keyOpt.Value().TrimPath(); ;

                    Vault vault;
                    FileInfo keyfile = null;
                    if (keyPathOpt.HasValue())
                    {

                        keyfile = new FileInfo(keyPathOpt.Value().TrimPath());

                        vault = Vault.LoadStore(keyfile.FullName);
                        if (passwordOpt.HasValue())
                            vault.InitializeVault(passwordOpt.Value().TrimPath());

                        var resultValue = vault.Get(secretName);

                    }

                    return 0;

                });
            });

            schema.Command("export", config =>
            {

                config.Description = "export all decrypted values in the output";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);
                var keyPathOpt = validator.Option("--file", "path where the file key is generated");
                var passwordOpt = validator.Option("--pwd", "specified the password");
                var outOpt = validator.Option("--out", "output file path");

                config.OnExecute(() =>
                {

                    string password = string.Empty;

                    if (!validator.Evaluate(out int result))
                        return result;

                    Vault vault;
                    FileInfo keyfile = null;
                    if (keyPathOpt.HasValue())
                    {

                        keyfile = new FileInfo(keyPathOpt.Value().TrimPath());

                        vault = Vault.LoadStore(keyfile.FullName);
                        if (passwordOpt.HasValue())
                            vault.InitializeVault(passwordOpt.Value().TrimPath());

                        var decodedVault = vault.Export();

                        decodedVault.SaveStore(outOpt.Value().TrimPath());

                    }

                    return 0;

                });
            });

            schema.Command("import", config =>
            {

                config.Description = "load all values from decrypted source";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);
                var keyPathOpt = validator.Option("--file", "path where the file key is generated");
                var passwordOpt = validator.Option("--pwd", "specified the password");
                var inputOpt = validator.Option("--in", "input file path");

                config.OnExecute(() =>
                {

                    string password = string.Empty;

                    if (!validator.Evaluate(out int result))
                        return result;

                    Vault vault;
                    FileInfo keyfile = null;
                    if (keyPathOpt.HasValue())
                    {

                        keyfile = new FileInfo(keyPathOpt.Value().TrimPath());

                        vault = Vault.LoadStore(keyfile.FullName);
                        if (passwordOpt.HasValue())
                            vault.InitializeVault(passwordOpt.Value().TrimPath());

                        var vaultSource = Vault.LoadStore(inputOpt.Value().TrimPath());

                        vault.Import(vaultSource);

                        vault.SaveStore(keyfile.FullName);

                    }

                    return 0;

                });
            });

            return app;

        }

    }


}


//using (var sman = SecretsManager.CreateStore())
//{
//    if (!string.IsNullOrEmpty(password))
//    {
//        sman.LoadKeyFromPassword(password);
//    }

//    if (command == "create")
//    {
//        if (File.Exists(path) && new FileInfo(path).Length > 0)
//        {
//            Input.Confirm($"Overwrite existing store at {path}? [yes/no]: ");
//        }
//        if (password == null)
//        {
//            if (File.Exists(keyfile) && new FileInfo(keyfile).Length > 0)
//            {
//                sman.LoadKeyFromFile(keyfile);
//            }
//            else
//            {
//                sman.GenerateKey();
//                sman.ExportKey(keyfile);
//            }
//        }
//        else if (!string.IsNullOrEmpty(keyfile))
//        {
//            sman.ExportKey(keyfile);
//        }
//    }
//    //else if (password == null && keyfile != null)
//    //{
//    //    sman.LoadKeyFromFile(keyfile);
//    //}

//    var client = new SecureClient(sman);

//    switch (command)
//    {
//        case "create":
//            client.Create();
//            break;
//        //case "delete":
//        //    client.Delete(secretName);
//        //    break;
//        //case "set":
//        //    client.Update(secretName, secretValue);
//        //    break;
//        //case "get":
//        //    if (!decryptAll)
//        //    {
//        //        if (format != DecryptFormat.None)
//        //        {
//        //            Help(Console.Error, "--format can only be used in conjunction with --all!",
//        //                command, parseOptions);
//        //        }

//        //        client.Decrypt(secretName);
//        //    }
//        //    else
//        //    {
//        //        client.DecryptAll(format);
//        //    }
//        //    break;
//        default:
//            throw new NotImplementedException($"Case {command} not handled!");
//    }

//    sman.SaveStore(path);

//}
