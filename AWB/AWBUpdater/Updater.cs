/*
AWBUpdater
Copyright (C) 2008 Sam Reed, Max Semenik

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;

using System.Text.RegularExpressions;
using System.Net;

namespace AwbUpdater
{
    internal sealed partial class Updater : Form
    {
        string AWBdirectory = "", tempDirectory = "";
        string AWBZipName = "", AWBWebAddress = "";
        string UpdaterZipName = "", UpdaterWebAddress = "";

        IWebProxy proxy;

        bool updaterUpdate, awbUpdate;

        public Updater()
        {
            InitializeComponent();

            AWBdirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            if (!tempDirectory.EndsWith("\\")) tempDirectory += "\\";
            tempDirectory += "$AWB$Updater$Temp$\\";
        }

        /// <summary>
        /// Version of the Updater
        /// </summary>
        public static int AssemblyVersion
        {
            get { return VersionToString(Assembly.GetExecutingAssembly().GetName().Version.ToString()); }
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            tmrTimer.Enabled = true;
        }

        /// <summary>
        /// Main program function
        /// </summary>
        private void UpdateAwb()
        {
            try
            {
                proxy = WebRequest.GetSystemWebProxy();

                if (proxy.IsBypassed(new Uri("http://en.wikipedia.org")))
                    proxy = null;

                UpdateUI("Getting current AWB and Updater versions", true);
                AWBversion();

                if ((!updaterUpdate && !awbUpdate) && string.IsNullOrEmpty(AWBWebAddress))
                    ExitEarly();
                else
                {
                    UpdateUI("Creating a temporary directory", true);
                    CreateTempDir();

                    UpdateUI("Downloading AWB", true);
                    GetAwbFromInternet();

                    UpdateUI("Unzipping AWB to the temporary directory", true);
                    UnzipAwb();

                    UpdateUI("Making sure AWB is closed", true);
                    CloseAwb();

                    UpdateUI("Copying AWB files from temp to AWB directory", true);
                    CopyFiles();
                    MessageBox.Show("AWB Update Successful", "Update Successful");

                    UpdateUI("Starting AWB", true);
                    StartAwb();

                    UpdateUI("Cleaning up from Update", true);
                    KillTempDir();

                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
            }
        }

        #region UI functions
        /// <summary>
        /// Multiple use function to update the GUI items
        /// </summary>
        /// <param name="currentStatus">What the updater is currently doing</param>
        /// <param name="newLine">If true, adds new line to log instead of reusing last existing one</param>
        private void UpdateUI(string currentStatus, bool newLine)
        {
            if (newLine)
            {
                lstLog.Items.Add(currentStatus);
            }
            else
            {
                lstLog.Items[lstLog.Items.Count - 1] = currentStatus;
            }
            Application.DoEvents();
        }

        private void AppendLine(string line)
        {
            lstLog.Items[lstLog.Items.Count] += line;
        }

        /// <summary>
        /// Close the updater early due to lack of updates
        /// </summary>
        private void ExitEarly()
        {
            MessageBox.Show("Nothing to update. The Updater will now close");
            StartAwb();
            Application.Exit();
        }

        /// <summary>
        /// Sets UI to "ready to exit" state
        /// </summary>
        private void ReadyToExit()
        {
            btnCancel.Text = "Close";
            lblStatus.Text = "Press close to exit";
            progressUpdate.Visible = false;
            btnCancel.Enabled = true;
        }
        #endregion

        /// <summary>
        /// Checks and compares the current AWB version with the version listed on the checkpage
        /// </summary>
        private void AWBversion()
        {
            string text = "";

            try
            {
                HttpWebRequest rq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=Wikipedia:AutoWikiBrowser/CheckPage/Version&action=raw");

                rq.Proxy = proxy;

                rq.UserAgent = "AWBUpdater " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                HttpWebResponse response = (HttpWebResponse)rq.GetResponse();

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);

                text = sr.ReadToEnd();

                sr.Close();
                stream.Close();
                response.Close();
            }
            catch
            {
                MessageBox.Show("Error fetching current version number.");
                Application.Exit();
            }

            int awbCurrentVersion = 
                VersionToString(Regex.Match(text, @"<!-- Current version: (.*?) -->").Groups[1].Value);
            int awbNewestVersion = 
                VersionToString(Regex.Match(text, @"<!-- Newest version: (.*?) -->").Groups[1].Value);
            int updaterVersion = VersionToString(Regex.Match(text, @"<!-- Updater version: (.*?) -->").Groups[1].Value);

            if ((awbCurrentVersion > 4000) || (awbNewestVersion > 4000))
            {
                try
                {
                    awbUpdate = updaterUpdate = false;
                    FileVersionInfo awbVersionInfo = FileVersionInfo.GetVersionInfo(AWBdirectory + "AutoWikiBrowser.exe");
                    int awbFileVersion = VersionToString(awbVersionInfo.FileVersion);

                    if (awbFileVersion < awbCurrentVersion)
                        awbUpdate = true;
                    else if ((awbFileVersion >= awbCurrentVersion) &&
                        (awbFileVersion < awbNewestVersion) &&
                        MessageBox.Show("There is an optional update to AutoWikiBrowser. Would you like to upgrade?", "Optional Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        awbUpdate = true;

                    if (awbUpdate)
                    {
                        AWBZipName = "AutoWikiBrowser" + awbNewestVersion.ToString() + ".zip";
                        AWBWebAddress = "http://downloads.sourceforge.net/autowikibrowser/" + AWBZipName;
                    }
                    else if ((updaterVersion > 1400) &&
                        (updaterVersion > AssemblyVersion))
                    {
                        UpdaterZipName = "AWBUpdater" + updaterVersion.ToString() + ".zip";
                        UpdaterWebAddress = "http://downloads.sourceforge.net/autowikibrowser/" + UpdaterZipName;
                        updaterUpdate = true;
                    }
                }
                catch
                { MessageBox.Show("Unable to find AutoWikiBrowser.exe to query its version."); }

                progressUpdate.Value = 30;
            }
        }

        /// <summary>
        /// Creates the temporary folder if it doesnt already exist
        /// </summary>
        private void CreateTempDir()
        {
            if (Directory.Exists(tempDirectory))
            {
                // clear its content just to be sure that no parasitic files are left
                Directory.Delete(tempDirectory, true); 
            }
            
            Directory.CreateDirectory(tempDirectory);

            progressUpdate.Value = 35;
        }

        /// <summary>
        /// Check the addresses for the files are valid (not null or empty), and downloads the files from the internet
        /// </summary>
        private void GetAwbFromInternet()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Proxy = proxy;

            if (!string.IsNullOrEmpty(AWBWebAddress))
                client.DownloadFile(AWBWebAddress, tempDirectory + AWBZipName);
            else if (!string.IsNullOrEmpty(UpdaterWebAddress))
                client.DownloadFile(UpdaterWebAddress, tempDirectory + UpdaterZipName);

            client.Dispose();

            progressUpdate.Value = 50;
        }

        /// <summary>
        /// Checks the zip files exist and calls the functions to unzip them
        /// </summary>
        private void UnzipAwb()
        {
            if (!string.IsNullOrEmpty(AWBZipName) && File.Exists(tempDirectory + AWBZipName))
                Extract(tempDirectory + AWBZipName);

            if (!string.IsNullOrEmpty(UpdaterZipName) && File.Exists(tempDirectory + UpdaterZipName))
                Extract(tempDirectory + UpdaterZipName);              

            progressUpdate.Value = 70;
        }

        /// <summary>
        /// Code used to unzip the zip files to the temporary directory
        /// </summary>
        /// <param name="File"></param>
        private void Extract(string File)
        {
            try
            {
                FastZip zip = new FastZip();

                zip.ExtractZip(File, tempDirectory, null);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
                Application.Exit();
            }
        }

        /// <summary>
        /// Looks if AWB or IRCM are open. If they are, tell the user
        /// </summary>
        private void CloseAwb()
        {
            bool awbOpen = false;

            do
            {
                foreach (Process p in Process.GetProcesses())
                {
                    awbOpen = (p.ProcessName == "AutoWikiBrowser" || p.ProcessName == "IRCMonitor");
                    if (awbOpen)
                    {
                        MessageBox.Show("Please save your settings (if you wish) and close " + p.ProcessName + " completely before pressing OK.");
                        break;
                    }
                }
            }
            while (awbOpen);

            progressUpdate.Value = 75;
        }

        void CopyFile(string source, string destination)
        {
            do
            {
                try
                {
                    File.Copy(source, destination, true);
                    break;
                }
                catch (IOException ex)
                {
                    if (MessageBox.Show(
                        this,
                        "Problem replacing file:\r\n   " + ex.Message + "\r\n\r\n" +
                            "Please close all applications that may use it and press 'Retry' to try again " +
                            "or 'Cancel' to cancel the upgrade.",
                        "Error",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error) != DialogResult.Retry)
                    {
                        MessageBox.Show(this, "Update aborted. AutoWikiBrowser may be unfunctional.", "AWB Updater");
                        KillTempDir();
                        Close();
                    }
                }
            }
            while (true);
        }

        /// <summary>
        /// Copies files from the temporary to the working directory
        /// </summary>
        private void CopyFiles()
        {
            if (File.Exists(tempDirectory + "AWBUpdater.exe"))
                CopyFile(tempDirectory + "AWBUpdater.exe", AWBdirectory + "AWBUpdater.exe.new");

            if (awbUpdate)
            {
                CopyFile(tempDirectory + "AutoWikiBrowser.exe", AWBdirectory + "AutoWikiBrowser.exe");
                CopyFile(tempDirectory + "AutoWikiBrowser.exe.config", AWBdirectory + "AutoWikiBrowser.exe.config");
                CopyFile(tempDirectory + "WikiFunctions.dll", AWBdirectory + "WikiFunctions.dll");
                CopyFile(tempDirectory + "IRCMonitor.exe", AWBdirectory + "IRCMonitor.exe");
                CopyFile(tempDirectory + "Diff.dll", AWBdirectory + "Diff.dll");

                if (File.Exists(AWBdirectory + "CFD.dll"))
                    CopyFile(tempDirectory + "Plugins\\CFD\\CFD.dll", AWBdirectory + "CFD.dll");

                if (!Directory.Exists(AWBdirectory + "\\Plugins\\CFD"))
                    Directory.CreateDirectory(AWBdirectory + "\\Plugins\\CFD");

                CopyFile(tempDirectory + "Plugins\\CFD\\CFD.dll", AWBdirectory + "Plugins\\CFD\\CFD.dll");

                if (File.Exists(AWBdirectory + "IFD.dll"))
                    CopyFile(tempDirectory + "Plugins\\IFD\\IFD.dll", AWBdirectory + "IFD.dll");

                if (!Directory.Exists(AWBdirectory + "\\Plugins\\IFD"))
                    Directory.CreateDirectory(AWBdirectory + "\\Plugins\\IFD");

                CopyFile(tempDirectory + "Plugins\\IFD\\IFD.dll", AWBdirectory + "Plugins\\IFD\\IFD.dll");

                if (File.Exists(AWBdirectory + "Kingbotk AWB Plugin.dll"))
                    CopyFile(tempDirectory + "Plugins\\Kingbotk\\Kingbotk AWB Plugin.dll", AWBdirectory + "Kingbotk AWB Plugin.dll");

                if (!Directory.Exists(AWBdirectory + "Plugins\\Kingbotk"))
                    Directory.CreateDirectory(AWBdirectory + "Plugins\\Kingbotk");

                CopyFile(tempDirectory + "Plugins\\Kingbotk\\Kingbotk AWB Plugin.dll", AWBdirectory + "Plugins\\Kingbotk\\Kingbotk AWB Plugin.dll");

                if (File.Exists(AWBdirectory + "WikiFunctions2.dll"))
                    File.Delete(AWBdirectory + "WikiFunctions2.dll");

                if (File.Exists(AWBdirectory + "WPAssessmentsCatCreator.dll"))
                    File.Delete(AWBdirectory + "WPAssessmentsCatCreator.dll");

                if (Directory.Exists(AWBdirectory + "\\Plugins\\WPAssessmentsCatCreator"))
                    Directory.Delete(AWBdirectory + "\\Plugins\\WPAssessmentsCatCreator", true);
            }
            progressUpdate.Value = 90;
        }

        /// <summary>
        /// Starts AWB if exists and is not already running
        /// </summary>
        private void StartAwb()
        {
            bool awbOpen = false;
            foreach (Process p in Process.GetProcesses())
            {
                awbOpen = (p.ProcessName == "AutoWikiBrowser");

                if (awbOpen)
                    break;
            }

            if (!awbOpen && System.IO.File.Exists(AWBdirectory + "AutoWikiBrowser.exe") && MessageBox.Show("Would you like to Start AWB?", "Start AWB?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                System.Diagnostics.Process.Start(AWBdirectory + "AutoWikiBrowser.exe");

            progressUpdate.Value = 95;
        }

        /// <summary>
        /// Deletes the temporary directory
        /// </summary>
        private void KillTempDir()
        {
            Directory.Delete(tempDirectory, true);
            progressUpdate.Value = 100;
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            tmrTimer.Enabled = false;
            UpdateAwb();
        }

        static int VersionToString(string version)
        {
            int res;
            if (!int.TryParse(version.Replace(".", ""), out res))
                res = 0;

            return res;
        }
    }
}
