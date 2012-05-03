using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NConsoler;

namespace Restuta.AttachVsToProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Consolery.Run(typeof(Program), args);
        }

        [Action]
        public static void DoWork([Optional(-1)] int processId)
        {
            if (processId == -1)
            {
                Console.WriteLine("Please use /processId:<PID> parameter to specify process id to attach to.");
                Environment.Exit(1);
            }

            Console.WriteLine("Attaching to the process with Id=" + processId);

            int procId = processId; // valid process-id
            EnvDTE80.DTE2 dte2 = (EnvDTE80.DTE2)System.Runtime.InteropServices.
              Marshal.GetActiveObject("VisualStudio.DTE.10.0");
            foreach (EnvDTE80.Process2 proc in dte2.Debugger.LocalProcesses)
            {
                if (proc.ProcessID == procId)
                {
                    proc.Attach2();
                    break;
                }
            }
        }
    }
}
