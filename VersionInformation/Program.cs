using System;
using System.IO;

namespace VersionInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args[0])
            {
            case "show": 
                ShowVersionInformation(args[1]); 
                break;
            case "set": 
                SetVersionInformation(args[1], args[2]); 
                break;
            default:
                Console.WriteLine("VersionInformation");
                Console.WriteLine("Copyright 2021 Ralf Beckers");
                Console.WriteLine();
                Console.WriteLine("VersionInformation show <Solution.sln>");
                Console.WriteLine("VersionInformation set <Solution.sln> <version.xml>");
                break;
            }
        }

        private static void ShowVersionInformation(string solutionFile)
        {
            string basePath = Path.GetDirectoryName(solutionFile);
            foreach (var projectPath in SolutionParser.Parse(solutionFile))
            {
                VersionInfo info = VersionInfo.LoadProject(Path.Combine(basePath, projectPath));
                Console.WriteLine($"{info.Version} {info.Authors} {info.Copyright}");
            }
        }

        private static void SetVersionInformation(string solutionFile, string versionFile)
        {
            foreach (var project in SolutionParser.Parse(solutionFile))
            {

            }
        }
    }
}
