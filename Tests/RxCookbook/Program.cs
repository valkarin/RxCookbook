﻿using System;
using System.Diagnostics;

/*
    Currently we have a dependency on HdrHistogram.NET. This currently does not have a Nuget 
    package, but I am working on it!

    To run you will need to get a copy of the source from https://github.com/LeeCampbell/HdrHistogram.NET 
    and drop the compiled (RELEASE Build) into the .\packages\ folder (with all the other nuget 
    packages).
*/



namespace RxCookbook
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WarnIfInvalidEnvironment();


            INPC.InpcPerfTests.Run();
            //LoadShedding.ObserveLatestOnPerfTests.Run();
            //DisposableOptimisation.StressTester.Run();
            //DisposableOptimisation.ThroughputTester.Run();
            Console.ReadLine();
        }
        
        public static void Clean()
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }


        private static void WarnIfInvalidEnvironment()
        {
            bool isValidRun = true;
#if DEBUG
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Running in a DEBUG build. This will not provide very useful results. Run in RELEASE mode.");
            Console.WriteLine();
            isValidRun = false;
#endif

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Debugger is attached!");
                Console.WriteLine();
                isValidRun = false;
            }

            if (!isValidRun)
                ShowRecommendedRunSettings();
        }

        private static void ShowRecommendedRunSettings()
        {
            Console.WriteLine("Run a RELEASE build with out the Debugger attached (Ctrl+F5 from Visual Studio).");
            Console.WriteLine("For best results, run on a clean machine with nothing else running.");
        }
    }
}
