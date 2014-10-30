﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Terminals
{
    /// <summary>
    /// Setup upgrade routine of Terminals.Log4net.config and Terminals.Exe.config depending on install type.
    /// Upgrades the files only in case of install to user profile, because deployed files are setup for portable version.
    /// </summary>
    internal static class UpgradeConfigFiles
    {
        internal static void CheckPortableInstallType(string targetDir, bool installToUserProfile)
        {
            if (!installToUserProfile)
                return;

            UpdateConfigFile(targetDir);
            UpdateLog4NetLogDirectory(targetDir);
        }

        private static void UpdateConfigFile(string targetDir)
        {
            string configFilePath = Path.Combine(targetDir, "Terminals.exe.config");
            XDocument configFile = XDocument.Load(configFilePath);
            XElement portalbeElement = SelectPortableElement(configFile);
            if (portalbeElement != null)
                portalbeElement.Value = false.ToString();

            configFile.Save(configFilePath);
        }

        private static XElement SelectPortableElement(XDocument config)
        {
            XElement portableElement = config.Descendants("Terminals.Properties.Settings")
                .Elements("setting")
                .Where(IsPortableSetting)
                .FirstOrDefault();

            if (portableElement != null)
                return portableElement.Element("value");

            return null;
        }

        private static bool IsPortableSetting(XElement setting)
        {
            XAttribute name = setting.Attributes("name").FirstOrDefault();
            if (name != null)
                return name.Value == "Portable";
            return false;
        }

        private static void UpdateLog4NetLogDirectory(string targetDir)
        {
            string log4NetFilePath = Path.Combine(targetDir, "Terminals.log4net.config");
            XDocument configFile = XDocument.Load(log4NetFilePath);
            XAttribute fileAttribute = SelectFileElement(configFile);

            string logDirectoryPath = GetLogDirectoryPath();
            if (fileAttribute != null)
                fileAttribute.Value = logDirectoryPath;

            configFile.Save(log4NetFilePath);
        }

        private static string GetLogDirectoryPath()
        {
            const string RELATIVE_PATH = @"\Robert_Chartier\Terminals\Data\logs\CurrentLog.txt";

            if (IsNewerThanXp())
                return @"${LOCALAPPDATA}" + RELATIVE_PATH;

            return @"${USERPROFILE}\Local Settings\Application Data" + RELATIVE_PATH;
        }

        private static bool IsNewerThanXp()
        {
            // http://stackoverflow.com/questions/2819934/detect-windows-7-in-net
            var osVersion = Environment.OSVersion.Version;
            bool isServer2003 = osVersion.Major == 5 && osVersion.Minor == 2;
            bool isVistaOrNewer = osVersion.Major >= 6;
            return isServer2003 || isVistaOrNewer;
        }

        private static XAttribute SelectFileElement(XDocument configFile)
        {
            return configFile.Descendants("file")
                .Attributes("value")
                .FirstOrDefault();
        }
    }
}
