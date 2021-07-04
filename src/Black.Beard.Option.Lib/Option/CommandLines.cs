using Bb.CommandLines;
using Microsoft.Extensions.CommandLineUtils;

namespace Bb.Option.Option
{
    
    public partial class CommandLines : Command<CommandLineOption>
    {

        public CommandLineOption CommandExport(CommandLineOption app)
        {

            var cmd = app.Command("export", config =>
            {

                config.Description = "export process";
                config.HelpOption(HelpFlag);

            });
           
            return app;

        }
   
  
    }


}
