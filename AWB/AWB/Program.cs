/*
Autowikibrowser
Copyright (C) 2007 Martin Richards
(C) 2007 Stephen Kennedy (Kingboyk) http://www.sdk-software.com/

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
using System.Windows.Forms;

namespace AutoWikiBrowser
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string fileToLoad = "";
            int profileID = -1;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "/s":
                        try
                        {
                            string tmp = args[i + 1].ToString();
                            if (tmp.Contains(".xml") && System.IO.File.Exists(tmp))
                                fileToLoad = tmp;
                        }
                        catch { }
                        break;
                    case "/u":
                        try { profileID = Convert.ToInt32(args[i + 1]); }
                        catch { }
                        break;
                }
            }

            MainForm awb = new MainForm();

            awb.ProfileToLoad = profileID;
            awb.SettingsFile = fileToLoad;

            Program.AWB = awb;
            Application.Run(awb);
        }

        internal static System.Version Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; } }
        internal static string VersionString { get { return Version.ToString(); } }
        static internal WikiFunctions.Plugin.IAutoWikiBrowser AWB;
        static internal Logging.MyTrace MyTrace = new AutoWikiBrowser.Logging.MyTrace();
    }
}