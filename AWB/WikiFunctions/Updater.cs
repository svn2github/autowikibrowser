/*
Copyright (C) 2007 Sam Reed
This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

using System.Windows.Forms;
using WikiFunctions.Background;

namespace WikiFunctions
{
    public static class Updater
    {
        private static readonly string AWBDirectory;

        /// <summary>
        /// Runs Update() at creation time
        /// </summary>
        static Updater()
        {
            AWBDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            Result = AWBEnabledStatus.None;
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum AWBEnabledStatus
        {
            None = 0,
            Error = 1,
            Disabled = 2,
            Enabled = 4,
            UpdaterUpdate = 8,
            OptionalUpdate = 12
        }

        /// <summary>
        /// 
        /// </summary>
        public static AWBEnabledStatus Result { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static string GlobalVersionPage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private static void UpdateFunc()
        {
            try
            {
                string text =
                    Tools.GetHTML(
                        "http://en.wikipedia.org/w/index.php?title=Wikipedia:AutoWikiBrowser/CheckPage/Version&action=raw");
                GlobalVersionPage = text;

                int awbCurrentVersion =
                    StringToVersion(Regex.Match(text, @"<!-- Current version: (.*?) -->").Groups[1].Value);

                int awbNewestVersion =
                    StringToVersion(Regex.Match(text, @"<!-- Newest version: (.*?) -->").Groups[1].Value);

                int updaterVersion =
                    StringToVersion(Regex.Match(text, @"<!-- Updater version: (.*?) -->").Groups[1].Value);

                if ((awbCurrentVersion > 4000) || (awbNewestVersion > 4000))
                {
                    FileVersionInfo awbVersionInfo =
                        FileVersionInfo.GetVersionInfo(AWBDirectory + "AutoWikiBrowser.exe");
                    int awbFileVersion = StringToVersion(awbVersionInfo.FileVersion);

                    if (awbFileVersion < awbCurrentVersion)
                    {
                        Result = AWBEnabledStatus.Disabled;
                        return;
                    }

                    Result = AWBEnabledStatus.Enabled;

                    if ((updaterVersion > 1400) &&
                        (updaterVersion >
                         StringToVersion(FileVersionInfo.GetVersionInfo(AWBDirectory + "AWBUpdater.exe").FileVersion)))
                    {
                        Result |= AWBEnabledStatus.UpdaterUpdate;
                    }

                    if ((awbFileVersion >= awbCurrentVersion) && (awbFileVersion < awbNewestVersion))
                    {
                        Result |= AWBEnabledStatus.OptionalUpdate;
                    }
                }
            }
            catch
            {
                Result = AWBEnabledStatus.Error;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private static int StringToVersion(string version)
        {
            int res;
            if (!int.TryParse(version.Replace(".", ""), out res))
                res = 0;

            return res;
        }

        private static BackgroundRequest Request;

        /// <summary>
        /// Checks to see if AWBUpdater.exe.new exists, if it does, replace it.
        /// </summary>
        public static void UpdateUpdaterFile(Tools.SetProgress setProgress)
        {
            setProgress(22);
            string awbDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            if (File.Exists(awbDirectory + "AWBUpdater.exe.new"))
            {
                File.Copy(awbDirectory + "AWBUpdater.exe.new", awbDirectory + "AWBUpdater.exe", true);
                File.Delete(awbDirectory + "AWBUpdater.exe.new");
            }
            setProgress(29);
        }

        /// <summary>
        /// Background request to check enabled state of AWB
        /// </summary>
        public static void Update()
        {
            Request = new BackgroundRequest();
            Request.Execute(UpdateFunc);
        }

        /// <summary>
        /// Waits for background enabled check to complete
        /// </summary>
        public static void WaitForCompletion()
        {
            if (Request == null) return;
            Request.Wait();
            Request = null;
        }

        /// <summary>
        /// Runs the Updater program
        /// </summary>
        public static void RunUpdater()
        {
            Process.Start(AWBDirectory + "AWBUpdater.exe");
        }
    }
}
