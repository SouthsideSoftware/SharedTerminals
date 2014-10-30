using System;

namespace Terminals.Data
{
    internal interface IBeforeConnectExecuteOptions
    {
        Boolean Execute { get; set; }
        String Command { get; set; }
        String CommandArguments { get; set; }
        String InitialDirectory { get; set; }
        Boolean WaitForExit { get; set; }
    }
}