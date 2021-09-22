using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace VersionInformation
{
    public class VersionInfo
    {
        public string PackageId;
        public string Version;
        public string Authors;
        public string Company;
        public string Product; 
        [XmlElement]
        public string Description;
        public string Copyright;
        public string PackageLicenseExpression; 
        public string PackageLicenseFile;
        public string PackageProjectUrl;
        public string RepositoryUrl;
        public string RepositoryType;
        public string PackageTags;
        public string PackageReleaseNotes;
        public string AssemblyVersion;
        public string FileVersion; 

        public static VersionInfo LoadProject(string projectPath)
        {
            XmlDocument doc = new();
            doc.Load(projectPath);

            bool isNewFormat = string.IsNullOrEmpty(doc.DocumentElement.GetAttribute("Sdk"));

            VersionInfo versionInfo = new();
            if (isNewFormat)
            {
                XmlElement elm = doc.SelectSingleNode(@"/Project/PropertyGroup") as XmlElement;
                versionInfo.PackageId = elm.GetElement("PackageId");
                versionInfo.Version = elm.GetElement("Version");
                versionInfo.Authors = elm.GetElement("Authors");
                versionInfo.Company = elm.GetElement("Company");
                versionInfo.Product = elm.GetElement("Product");
                versionInfo.Description = elm.GetElement("Description");
                versionInfo.Copyright = elm.GetElement("Copyright");
                versionInfo.PackageLicenseExpression = elm.GetElement("Copyright");
                versionInfo.PackageLicenseFile = elm.GetElement("Copyright");
                versionInfo.PackageProjectUrl = elm.GetElement("Copyright");
                versionInfo.RepositoryUrl = elm.GetElement("Copyright");
                versionInfo.RepositoryType = elm.GetElement("Copyright");
                versionInfo.PackageTags = elm.GetElement("Copyright");
                versionInfo.PackageReleaseNotes = elm.GetElement("Copyright");
                versionInfo.AssemblyVersion = elm.GetElement("Copyright");
                versionInfo.FileVersion = elm.GetElement("Copyright");
            }
            else
            {
                using var reader = File.OpenText(Path.Combine(Path.GetDirectoryName(projectPath), @"Properties\AssemblyInfo.cs"));
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    line.GetElement("Description", ref versionInfo.Description);
                    line.GetElement("Company", ref versionInfo.Company);
                    line.GetElement("Product", ref versionInfo.Product);
                    line.GetElement("Copyright", ref versionInfo.Copyright);
                    line.GetElement("Version", ref versionInfo.AssemblyVersion);
                    line.GetElement("FileVersion", ref versionInfo.FileVersion);
                }
            }
            return versionInfo;
        }

        public void UpdateProject(string projectPath)
        {
            XmlDocument doc = new();
            doc.Load(projectPath);

            bool isNewFormat = string.IsNullOrEmpty(doc.DocumentElement.GetAttribute("Sdk"));

            if (isNewFormat)
            {
                XmlElement elm = doc.SelectSingleNode(@"/Project/PropertyGroup") as XmlElement;

                elm.SetElement("PackageId", this.PackageId);
                elm.SetElement("Version", this.Version);
                elm.SetElement("Authors", this.Authors);
                elm.SetElement("Company", this.Company);
                elm.SetElement("Product", this.Product);

                elm.SetElement("Description", this.Description);
                elm.SetElement("Copyright", this.Copyright);
                elm.SetElement("PackageLicenseExpression", this.PackageLicenseExpression);
                elm.SetElement("PackageLicenseFile", this.PackageLicenseFile);
                elm.SetElement("PackageProjectUrl", this.PackageProjectUrl);
                elm.SetElement("RepositoryUrl", this.RepositoryUrl);
                elm.SetElement("RepositoryType", this.RepositoryType);
                elm.SetElement("PackageTags", this.PackageTags);
                elm.SetElement("PackageReleaseNotes", this.PackageReleaseNotes);

                elm.SetElement("AssemblyVersion", this.AssemblyVersion);
                elm.SetElement("FileVersion", this.FileVersion);
            }
            else
            { }
        }

        public static VersionInfo LoadFile(string path)
        {
            return new XmlSerializer(typeof(VersionInfo)).Deserialize(File.OpenRead(path)) as VersionInfo;
        }

        
    }

    public static class XmlHelper
    {
        public static string GetElement(this XmlElement elm, string name)
        {
            return elm[name]?.InnerText;
        }

        public static void SetElement(this XmlElement elm, string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            XmlNode item = elm[name] ?? elm.AppendChild(elm.OwnerDocument.CreateElement(name));
            item.InnerText = value;
        }

        public static void GetElement(this string line, string name, ref string value)
        {
            if (line.StartsWith($"[assembly: Assembly{name}"))
            {
                string[] items = line.Split('"');
                if (items.Length >= 3)
                {
                    value = items[1];
                }
            }
        }
    }
}
