using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VersionInformation
{
    public static class SolutionParser
    {
        //Project("{D954291E-2A0B-460D-934E-DC6B0785DB48}") = "MediaDevicesShare", "Src\MediaDevicesShare\MediaDevicesShare.shproj", "{8C6734B5-5BBB-413E-A5F2-FC7E6AAFFCE7}"

        public static List<string> Parse(string solutionPath)
        {
            List<string> projects = new();
            using var reader = File.OpenText(solutionPath);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.StartsWith("Project"))
                {
                    string[] items = line.Split(',');
                    if (items.Length >= 3)
                    {
                        string project = items[1].Trim(' ', '"');

                        if (project.EndsWith(".csproj"))

                        projects.Add(project);
                    }
                }
            }
            return projects;
        }
    }
}
