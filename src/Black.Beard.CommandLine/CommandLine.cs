using Bb.CommandLines;
using Microsoft.Extensions.CommandLineUtils;


namespace Bb
{
    public partial class CommandLine
    {

        public static T Run<TCommandLine, T>(params string[] args)
            where TCommandLine : Command<T>, new()
            where T : CommandLineApplication, new()
        {
            CommandLineProgram<TCommandLine, T>.Main(args);
            return CommandLineProgram<TCommandLine, T>.Result;
        }

    }

}
