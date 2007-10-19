/*
Copyright (C) 2007

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
using System.Text;
using WikiFunctions.Plugin;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WikiFunctions;
using WikiFunctions.Logging;
using WikiFunctions.Parse;
using WikiFunctions.Lists;
using System.Xml.Serialization;

namespace AutoWikiBrowser.Plugins.IFD
{
    public sealed class IfdCore : IAWBPlugin
    {
        private ToolStripMenuItem pluginenabledMenuItem = new ToolStripMenuItem("Images For Deletion plugin");
        private ToolStripMenuItem pluginconfigMenuItem = new ToolStripMenuItem("Configuration");
        private ToolStripMenuItem aboutMenuItem = new ToolStripMenuItem("About the IFD plugin");
        internal static IAutoWikiBrowser AWB;
        internal static IfdSettings Settings = new IfdSettings();

        public void Initialise(IAutoWikiBrowser mainForm)
        {
            AWB = mainForm;

            // Menuitem should be checked when IFD plugin is active and unchecked when not, and default to not!
            pluginenabledMenuItem.CheckOnClick = true;
            PluginEnabled = false;

            pluginconfigMenuItem.Click += ShowSettings;
            pluginenabledMenuItem.CheckedChanged += PluginEnabledCheckedChange;
            aboutMenuItem.Click += AboutMenuItemClicked;
            pluginenabledMenuItem.DropDownItems.Add(pluginconfigMenuItem);

            mainForm.PluginsToolStripMenuItem.DropDownItems.Add(pluginenabledMenuItem);
            mainForm.HelpToolStripMenuItem.DropDownItems.Add(aboutMenuItem);
        }

        public string Name
        { get { return "IFD-Plugin"; } }

        public string WikiName
        {
            get
            {
                return "[[WP:IFD|IFD]] Plugin version " +
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        //This is the regex we will use, with the options of
        //ignoring case, and compiled which is more efficient if we are going to re-use it repeatedly.
        Regex imgRegex = new Regex("img", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public string ProcessArticle(IAutoWikiBrowser sender, ProcessArticleEventArgs processArticleEventArgs)
        {
            //If menu item is not checked, then return
            if (!PluginEnabled || Settings.Images.Count == 0)
            {
                processArticleEventArgs.Skip = false;
                return processArticleEventArgs.ArticleText;
            }

            processArticleEventArgs.EditSummary = "";
            string text = processArticleEventArgs.ArticleText;

            Parsers parse = new Parsers();

            foreach (KeyValuePair<string, string> p in Settings.Images)
            {
                bool noChange = true;

                if (p.Value.Length == 0)
                {
                    text = parse.RemoveImage(p.Key, text, Settings.Comment, "", out noChange);
                    if (!noChange) processArticleEventArgs.EditSummary += ", removed " + Variables.Namespaces[6] + p.Key;
                }
                else
                {
                    text = parse.ReplaceImage(p.Key, p.Value, text, out noChange);
                    if (!noChange) processArticleEventArgs.EditSummary += ", replaced: " + Variables.Namespaces[6]
                         + p.Key + " → " + Variables.Namespaces[6] + p.Value;
                }
                if (!noChange) text = Regex.Replace(text, "<includeonly>[\\s\\r\\n]*\\</includeonly>", "");
            }

            processArticleEventArgs.Skip = (text == processArticleEventArgs.ArticleText) && Settings.Skip;

            return text;
        }

        // TODO: Why doesn't plugin load and save settings? Perhaps it couldn't be made to work at the time? (should be possible now, as I fixed the AWB plugin settings code to get kingbotk to work)
        public void LoadSettings(object[] prefs)
        {
            //Settings = (CfdSettings)Prefs[0];
            //PluginEnabled = Settings.Enabled;
        }

        public object[] SaveSettings()
        {
            //Settings.Enabled = PluginEnabled;

            //object[] Prefs = new object[1];
            //Prefs[0] = Settings;

            return null;//Prefs;
        }

        public void Reset()
        {
            //set default settings
            Settings = new IfdSettings();
            PluginEnabled = false;
        }

        public void Nudge(out bool cancel) { cancel = false; }
        public void Nudged(int nudges) { }

        private void ShowSettings(Object sender, EventArgs e)
        {
            IfdOptions o = new IfdOptions();

            o.Show();
        }

        private bool PluginEnabled
        {
            get { return pluginenabledMenuItem.Checked; }
            set { pluginenabledMenuItem.Checked = value; }
        }

        private void PluginEnabledCheckedChange(Object sender, EventArgs e)
        {
            Settings.Enabled = PluginEnabled;
            if (PluginEnabled)
                AWB.NotifyBalloon("IFD plugin enabled", ToolTipIcon.Info);
            else
                AWB.NotifyBalloon("IFD plugin disabled", ToolTipIcon.Info);
        }

        private void AboutMenuItemClicked(Object sender, EventArgs e)
        {
            new AboutBox().Show();
        }
    }

    [Serializable]
    internal sealed class IfdSettings
    { // TODO: This is crap!
        public bool Enabled = false;
        public Dictionary<string, string> Images = new Dictionary<string, string>();
        public bool Comment = true;
        public bool Skip = true;
    }

}