using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ComputerAutomationServer
{
    class ApplicationLauncher
    {
        public ApplicationLauncher()
        {

        }

        public void Start(string applicationPath, string arguments = "")
        {
            ProcessStartInfo process = new ProcessStartInfo();
            process.FileName = applicationPath;

            process.Arguments = arguments;

            using (Process proc = Process.Start(process))
            {

            }
        }
    }
}
