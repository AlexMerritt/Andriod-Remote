using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidRemote
{
    public static class Debug
    {
        public static void Log(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }
    }
}