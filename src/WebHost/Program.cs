﻿using NetModular.Lib.Host.Web;

namespace NetModular.Module.Common.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new HostBuilder().RunAsync(args);
        }
    }
}
