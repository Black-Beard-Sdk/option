using Bb.CommandLines;
using Bb.CommandLines.Outs;
using Microsoft.Extensions.CommandLineUtils;
using System;


namespace Bb.Json
{


    public partial class CommandLineProgram<TCommandLine, T> 
        where TCommandLine : Command<T>, new()
         where T : CommandLineApplication, new()
    {


        public static int ExitCode { get; private set; }

        public static void Main(params string[] args)
        {

            T app = null;
            var cmd = new TCommandLine();
            try
            {

                app = cmd.Initialize(new T());
                int result = app.Execute(args);

                Output.Flush();

                Environment.ExitCode = CommandLineProgram<TCommandLine, T>.ExitCode = result;

            }
            catch (System.FormatException e2)
            {
                FormatException(app, e2);
            }
            catch (CommandParsingException e)
            {

                Output.WriteLineError(e.Message);
                Output.WriteLineError(e.StackTrace);
                Output.Flush();

                if (e.HResult > 0)
                    Environment.ExitCode = CommandLineProgram<TCommandLine, T>.ExitCode = e.HResult;

                app.ShowHelp();

                Environment.ExitCode = CommandLineProgram<TCommandLine, T>.ExitCode = 1;

            }
            catch (Exception e)
            {

                Output.WriteLineError(e.Message);
                Output.WriteLineError(e.StackTrace);
                Output.Flush();

                if (e.HResult > 0)
                    Environment.ExitCode = CommandLineProgram<TCommandLine, T>.ExitCode = e.HResult;

                Environment.ExitCode = CommandLineProgram<TCommandLine, T>.ExitCode = 1;

            }

            

        }

        private static void FormatException(CommandLineApplication app, FormatException e2)
        {
            Output.WriteLineError(e2.Message);
            Output.Flush();
            app.ShowHelp();
            Environment.ExitCode = CommandLineProgram<TCommandLine, T>.ExitCode = 2;
        }

    }

}
