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
    internal static class GlobalObjects
    {
        static internal WikiFunctions.Plugin.IAutoWikiBrowser AWB;
        static internal Logging.MyTrace MyTrace = new AutoWikiBrowser.Logging.MyTrace();
    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm awb = new MainForm();
            GlobalObjects.AWB = awb;
            Application.Run(awb);
        }
        
        internal static System.Version Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; } }
        internal static string VersionString { get { return Version.ToString(); } }
    }
}