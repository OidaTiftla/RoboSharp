﻿using System;
using System.Globalization;
using System.Linq;
using System.Management;

namespace RoboSharp {

    internal static class VersionManager {
        private static double? version;

        internal static double Version {
            get {
                if (version == null) {
                    var v = GetOsVersion();
                    version = GetOsVersionNumber(v);
                    return version.Value;
                } else {
                    return version.Value;
                }
            }
        }

        private static string GetOsVersion() {
            using (ManagementObjectSearcher objMOS = new ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem")) {
                foreach (ManagementObject objManagement in objMOS.Get()) {
                    var version = objManagement.GetPropertyValue("Version");

                    if (version != null) {
                        return version.ToString();
                    }
                }
            }

            return Environment.OSVersion.Version.ToString();
        }

        private static double GetOsVersionNumber(string version) {
            if (version.IsNullOrWhiteSpace())
                return 0;

            var segments = version.Split(new char[] { '.' });
            var major = Convert.ToDouble(segments[0]);
            var otherSegments = segments.Skip(1).ToArray();
            var dec = Convert.ToDouble("." + string.Join("", otherSegments), CultureInfo.InvariantCulture);
            return major + dec;
        }
    }
}