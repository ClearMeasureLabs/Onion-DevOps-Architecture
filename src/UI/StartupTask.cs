using System;
using System.Diagnostics;
using ClearMeasure.OnionDevOpsArchitecture.Core;

namespace ClearMeasure.OnionDevOpsArchitecture.UI
{
    public class StartupTask : IStartupTask
    {
        public void Run()
        {
            Trace.WriteLine("Woohoo!!!!!!!!!!!!!!!!!!!!");
        }
    }
}