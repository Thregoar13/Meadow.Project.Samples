using Meadow;
using MeadowBallAnimation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MeadowAnimation
{
    class Program
    {
        static IApp app;
        public static void Main(string[] args)
        {
            if(args.Length > 0 && args[0] ==  "--exitOnDebug") return;

            app = new MeadowApp();
            Thread.Sleep(Timeout.Infinite);

        }
    }
}
