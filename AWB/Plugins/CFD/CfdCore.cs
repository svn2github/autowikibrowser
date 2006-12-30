using System;
using System.Collections.Generic;
using System.Text;
using WikiFunctions.Plugin;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WikiFunctions;
using WikiFunctions.Parse;
using WikiFunctions.Lists;
using System.Xml.Serialization;

namespace CFD
{
    public class CfdCore : IAWBPlugin
    {
        public event PluginEventHandler Preview;
        public event PluginEventHandler Save;
        public event PluginEventHandler Skip;
        public event PluginEventHandler Start;
        public event PluginEventHandler Stop;
        public event PluginEventHandler Diff;

        ToolStripMenuItem menuitem;

        ListMaker listMaker;

        CfdSettings Settings = new CfdSettings();

        public void Initialise(ListMaker list, WikiFunctions.Browser.WebControl web, ToolStripMenuItem tsmi, ContextMenuStrip cms, TabControl tab, Form frm, TextBox txt)
        {
            listMaker = list;

            //Make our new menu item
            menuitem = new ToolStripMenuItem(Name);

            //Set its check state to true
            menuitem.Checked = true;

            menuitem.Click += MenuItemClicked;

            //Make it change its checked state on click
            //menuitem.CheckOnClick = true;

            //Add it to the menu
            tsmi.DropDownItems.Add(menuitem);
        }

        public string Name
        {
            get { return "Recategorize per CFD"; }
        }

        //This is the regex we will use, with the optional options of
        //ignoring case and compiled which is more efficient if we are going to re-use it repeatedly.
        Regex catRegex = new Regex("cat", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public string ProcessArticle(string ArticleText, string ArticleTitle, int Namespace, out string Summary, out bool Skip)
        {
            //If menu item is not checked, then return
            Summary = "";
            if (!menuitem.Checked || Settings.Categories.Count == 0)
            {
                Skip = false;
                return ArticleText;
            }

            string text = ArticleText;

            Parsers parse = new Parsers();

            
            foreach (KeyValuePair<string, string> p in Settings.Categories)
            {
                bool noChange = true;

                if (p.Value == "")
                {
                    text = parse.RemoveCategory(p.Key, text, out noChange);
                    if(!noChange) Summary += ", removed " + Variables.Namespaces[14] + p.Key;
                }
                else
                {
                    text = parse.ReCategoriser(p.Key, p.Value, text, out noChange);
                    if (!noChange) Summary += ", replaced: " + Variables.Namespaces[14]
                         + p.Key + " → " + Variables.Namespaces[14] + p.Value;
                }
            }
            
            Skip = (text == ArticleText) && Settings.Skip;

            ArticleText = text;

            return ArticleText;
        }

        public void MenuItemClicked(Object sender, EventArgs e)
        {
            CfdOptions o = new CfdOptions();

            o.Show(Settings, listMaker);

            menuitem.Checked = Settings.Enabled;

        }

        public void LoadSettings(object[] Prefs)
        {
            //Settings = (CfdSettings)Prefs[0];
            //menuitem.Checked = Settings.Enabled;
        }

        public object[] SaveSettings()
        {
            //Settings.Enabled = menuitem.Checked;

            //object[] Prefs = new object[1];
            //Prefs[0] = Settings;

            return null;//Prefs;
        }

        public void Reset()
        {
            //set default settings
            menuitem.Checked = true;
            Settings = new CfdSettings();
        }
    }

    [Serializable]
    public class CfdSettings
    {
        public bool Enabled = true;
        public Dictionary<string, string> Categories = new Dictionary<string, string>();
        public bool Skip = true;
    }

}

