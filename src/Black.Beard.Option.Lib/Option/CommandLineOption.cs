using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Option
{

    public class CommandLineOption : CommandLineApplication
    {

        public override string GetHelpText(string commandName = null)
        {
            return base.GetHelpText(commandName);
        }

        public object Result { get; set; }
        
    }


}
