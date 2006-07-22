/*
DumpSearcher
Copyright (C) 2006 Martin Richards

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
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading;

namespace WikiFunctions.DumpSearcher
{
    /// <summary>
    /// Provides a form and functions for searching XML data dumps
    /// </summary>
    public partial class DumpSearcher : Form
    {
        MainProcess Main;

        public DumpSearcher()
        {
            InitializeComponent();
            Thread.CurrentThread.Name = "Main";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmboLength.SelectedIndex = 0;
            cmboLinks.SelectedIndex = 0;
            loadSettings();
        }

        #region main process

        int intMatches = 0;
        int intTimer = 0;

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Main != null)
                    return;

                if (fileName.Length == 0)
                {
                    MessageBox.Show("Please open an \"Articles\" XML data-dump file from the file menu\r\n\r\nSee the About menu for where to download this file.", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                     

                groupBox4.Enabled = false;
                btnStart.Text = "Working";
                btnStart.Enabled = false;
                lbArticles.Items.Clear();
                lblCount.Text = "";

                intMatches = 0;
                intTimer = 0;

                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 100;

                timer1.Enabled = true;

                Start();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Regex TitleDoesRegex;
        Regex TitleDoesNotRegex;
        Regex ArticleDoesRegex;
        Regex ArticleDoesNotRegex;
        private void makePatterns()
        {
            string strTitleNot = "";
            string strTitle = "";
            RegexOptions TitleRegOptions;            

            string strArticleDoes = convert(txtPattern.Text);
            string strArticleDoesNot = convert(txtPatternNot.Text);
            RegexOptions ArticleRegOptions;            

            strTitle = convert(txtTitleContains.Text);
            strTitleNot = convert(txtTitleNotContains.Text);
            strArticleDoes = convert(txtPattern.Text);
            strArticleDoesNot = convert(txtPatternNot.Text);

            ArticleRegOptions = RegexOptions.ExplicitCapture;
            TitleRegOptions = RegexOptions.ExplicitCapture;
                        
            if (!chkRegex.Checked)
            {
                strArticleDoes = Regex.Escape(strArticleDoes);
                strArticleDoesNot = Regex.Escape(strArticleDoesNot);
            }

            if (!chkTitleRegex.Checked)
            {
                strTitle = Regex.Escape(strTitle);
                strTitleNot = Regex.Escape(strTitleNot);
            }

            if (!chkCaseSensitive.Checked)
                ArticleRegOptions = ArticleRegOptions | RegexOptions.IgnoreCase;
            if (chkMulti.Checked)
                ArticleRegOptions = ArticleRegOptions | RegexOptions.Multiline;
            if (chkSingle.Checked)
                ArticleRegOptions = ArticleRegOptions | RegexOptions.Singleline;

            if (!chkTitleCase.Checked)
                TitleRegOptions = TitleRegOptions | RegexOptions.IgnoreCase;

            ArticleDoesRegex = new Regex(strArticleDoes, ArticleRegOptions);
            ArticleDoesNotRegex = new Regex(strArticleDoesNot, ArticleRegOptions);

            TitleDoesRegex = new Regex(strTitle, TitleRegOptions);
            TitleDoesNotRegex = new Regex(strTitleNot, TitleRegOptions);
        }

        
        private void Start()
        {
            Scanners scn = new Scanners();

            makePatterns();

            scn.ArticleDoesContain = chkArticleDoesContain.Checked;
            scn.ArticleDoesNotContain = chkArticleDoesNotContain.Checked;
            scn.ArticleDoesContainRegex = ArticleDoesRegex;
            scn.ArticleDoesNotContainRegex = ArticleDoesNotRegex;

            scn.TitleContainsEnabled = chkTitleContains.Checked;
            scn.TitleNotContainsEnabled = chkTitleDoesNotContain.Checked;
            scn.TitleContainsRegex = TitleDoesRegex;
            scn.TitleNotContainsRegex = TitleDoesNotRegex;

            scn.CheckLength = cmboLength.SelectedIndex;
            scn.Length = (int)nudLength.Value;
            scn.CountLinks = cmboLinks.SelectedIndex;
            scn.Links = (int)nudLinks.Value;

            scn.BadLinks = chkBadLinks.Checked;
            scn.NoBold = chkNoBold.Checked;
            scn.SimpleLinks = chkSimpleLinks.Checked;
            scn.HasHTML = chkHasHTML.Checked;
            scn.HeaderError = chkHeaderError.Checked;
            scn.UnbulletedLinks = chkUnbulletedLinks.Checked;

            Main = new MainProcess(scn, fileName, (int)nudLimitResults.Value);
            Main.FoundArticle += MessageReceived;
            Main.Stopped += Stopped;
            Main.Start();
        }

        private void MessageReceived(object msg)
        {
            lbArticles.Items.Add(msg);
            intMatches++;
            lblCount.Text = intMatches.ToString();
        }

        private void Stopped()
        {
            Main.FoundArticle -= MessageReceived;
            Main.Stopped -= Stopped;
            StopProgressBar();
            Main = null;
        }

        private delegate void SetProgBarDelegate();
        private void StopProgressBar()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new SetProgBarDelegate(StopProgressBar));
                    return;
                }

                progressBar1.MarqueeAnimationSpeed = 0;
                progressBar1.Style = ProgressBarStyle.Continuous;

                timer1.Enabled = false;
                groupBox4.Enabled = true;
                btnStart.Enabled = true;
                btnStart.Text = "Start";
                Tools.FlashWindow(this);
                lblCount.Text = lbArticles.Items.Count.ToString() + " results";
                if (Main != null && Main.Message)
                    MessageBox.Show(lbArticles.Items.Count.ToString() + " matches in " + intTimer.ToString() + " seconds");
            }
            catch (Exception ex)
            {
                if (Main != null && Main.Message)
                    MessageBox.Show(ex.Message);
            }
        }

        #endregion

        # region other

        private void timer1_Tick(object sender, EventArgs e)
        {
            intTimer++;
        }

        private void wikifyToList()
        {
            bool b = true;

            StringBuilder strbList = new StringBuilder("");
            int i = 0;
            string s = "";
            string l = "";
            string sr = "";
            string strBullet = "#";
            int intSection = 0;
            int intSectionNumber = 0;
            int intHeadingSpace = Convert.ToInt32(nudHeadingSpace.Value);

            if (rdoHash.Checked)
                strBullet = "#";
            else
                strBullet = "*";

            if (chkHeading.Checked)
            {
                strbList.Append("==0==\r\n");
                intSectionNumber++;
            }

            if (b)
            {
                while (i < lbArticles.Items.Count)
                {
                    s = lbArticles.Items[i].ToString();

                    s = s.Replace("&amp;", "&");

                    strbList.Append(strBullet + " [[" + s + "]]\r\n");

                    intSection++;
                    if (chkHeading.Checked && intSection == intHeadingSpace)
                    {
                        strbList.Append("\r\n==" + intSectionNumber + "==\r\n");
                        intSectionNumber++;
                        intSection = 0;
                    }

                    i++;
                }
            }
            else
            {
                while (i < lbArticles.Items.Count)
                {
                    s = lbArticles.Items[i].ToString();

                    s = s.Replace("&amp;", "&");

                    if (s.Length > 1)
                    {
                        sr = s.Remove(1);
                    }
                    else
                        sr = s;

                    if (sr != l)
                        strbList.Append("\r\n== " + sr + " ==\r\n");

                    strbList.Append(strBullet + " [[" + s + "]]\r\n");

                    l = sr;
                    i++;
                }
            }


            txtList.Text = strbList.ToString().Trim();

        }

        string strfileName = "";
        private string fileName
        {
            get { return strfileName; }
            set
            {
                strfileName = value;

                if (value.Length > 0)
                {
                    string shortened = value.Substring(fileName.LastIndexOf("\\") + 1);
                    this.Text = "Wiki Data Dump Searcher - " + shortened;
                }
                else
                    this.Text = "Wiki Data Dump Searcher";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openXMLDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openXMLDialog.FileName;
                }
            }
            catch { }
        }

        private void Save()
        {
            try
            {
                string strList = txtList.Text;

                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(saveFileDialog2.FileName);
                    sw.Write(strList);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AboutBox About = new AboutBox();
                About.Show();
            }
            catch { }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //try
            //{
            //    if (PThread != null && PThread.IsAlive)
            //        PThread.Abort();
            //}
            //catch { }
        }

        private void chkRegex_CheckedChanged(object sender, EventArgs e)
        {
            bool regex = chkRegex.Checked;
            chkMulti.Enabled = regex;
            chkSingle.Enabled = regex;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblCount.Text = "";
            txtList.Clear();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            txtList.SelectAll();
            txtList.Copy();
            txtList.Select(0, 0);
        }

        private void chkDoesContain_CheckedChanged(object sender, EventArgs e)
        {
            txtPattern.Enabled = chkArticleDoesContain.Checked;
        }

        private void chkDoesNotContain_CheckedChanged(object sender, EventArgs e)
        {
            txtPatternNot.Enabled = chkArticleDoesNotContain.Checked;
        }

        private void chkCheckTitle_CheckedChanged(object sender, EventArgs e)
        {
            txtTitleContains.Enabled = chkTitleContains.Checked;
        }

        private void chkCheckNotInTitle_CheckedChanged(object sender, EventArgs e)
        {
            txtTitleNotContains.Enabled = chkTitleDoesNotContain.Checked;
        }

        private void cmboLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmboLength.SelectedIndex == 0)
                nudLength.Enabled = false;
            else
                nudLength.Enabled = true;

        }

        private void cmboLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmboLinks.SelectedIndex == 0)
                nudLinks.Enabled = false;
            else
                nudLinks.Enabled = true;
        }

        private void chkHeading_CheckedChanged(object sender, EventArgs e)
        {
            nudHeadingSpace.Enabled = chkHeading.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wikifyToList();
        }

        private void AlphaList_Click(object sender, EventArgs e)
        {
            lbArticles.Sorted = true;
            lbArticles.Sorted = false;
        }

        private void lbClear_Click(object sender, EventArgs e)
        {
            lbArticles.Items.Clear();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lbArticles.SelectedItem.ToString(), true);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbArticles.Items.RemoveAt(lbArticles.SelectedIndex);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (lbArticles.SelectedIndex >= 0)
            {
                copyToolStripMenuItem.Enabled = true;
                removeToolStripMenuItem.Enabled = true;
            }
            else
            {
                copyToolStripMenuItem.Enabled = false;
                removeToolStripMenuItem.Enabled = false;
            }
        }

        private string convert(string text)
        {
            text = text.Replace("&", "&amp;");
            text = text.Replace("<", "&lt;");
            text = text.Replace(">", "&gt;");
            text = text.Replace(@"\r\n", @"
");
            return text;
        }

        private void txtTitleContains_Leave(object sender, EventArgs e)
        {
            txtTitleContains.Text = convert(txtTitleContains.Text);
        }

        private void txtTitleNotContains_Leave(object sender, EventArgs e)
        {
            txtTitleNotContains.Text = convert(txtTitleNotContains.Text);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Main != null)
                Main.Run = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Main != null)
            {
                Main.Message = false;
                Main.Run = false;
            }

            saveSettings();
        }

        private void listComparerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WikiFunctions.ListComparer LC = new WikiFunctions.ListComparer();
            LC.Show();
        }

        private void highestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboveNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;
            lowestToolStripMenuItem.Checked = false;

            Main.Priority = ThreadPriority.Highest;
        }

        private void aboveNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            highestToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;
            lowestToolStripMenuItem.Checked = false;

            Main.Priority = ThreadPriority.AboveNormal;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            highestToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;
            lowestToolStripMenuItem.Checked = false;

            Main.Priority = ThreadPriority.Normal;
        }

        private void belowNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            highestToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            lowestToolStripMenuItem.Checked = false;

            Main.Priority = ThreadPriority.BelowNormal;
        }

        private void lowestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            highestToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;

            Main.Priority = ThreadPriority.Lowest;
        }
        #endregion

        #region properties

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            resetSettings();
        }

        private void saveSettings()
        {
            //menu
            //WikiDumpSearcher.Properties.Settings.Default.IgnoreRedirects = ignoreRedirectsToolStripMenuItem1.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IgnoreComments = ignoreCommentsToolStripMenuItem.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IgnoreDisambigs = ignoreDisambigsToolStripMenuItem.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IgnoreImage = ignoreImagesToolStripMenuItem.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IgnoreTemplate = ignoreTemplateNamespaceToolStripMenuItem.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IgnoreWikipedia = ignoreWikipediaNamespaceToolStripMenuItem.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IngoreCategory = ignoreCategoryNamespaceToolStripMenuItem.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IgnoreMain = ignoreMainNamespaceToolStripMenuItem.Checked;

            //WikiDumpSearcher.Properties.Settings.Default.ThreadPriority = Threadpriority;

            ////contains
            //WikiDumpSearcher.Properties.Settings.Default.DoesContain = txtPattern.Text;
            //WikiDumpSearcher.Properties.Settings.Default.DoesNotContain = txtPatternNot.Text;
            //WikiDumpSearcher.Properties.Settings.Default.DoesContainEnabled = chkDoesContain.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.DoesNotContainEnabled = chkDoesNotContain.Checked;

            //WikiDumpSearcher.Properties.Settings.Default.IsRegex = chkRegex.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IsCaseSens = chkCaseSensitive.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IsMulti = chkMulti.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.IsSingle = chkSingle.Checked;

            ////title
            //WikiDumpSearcher.Properties.Settings.Default.TitleContains = txtTitleContains.Text;
            //WikiDumpSearcher.Properties.Settings.Default.TitleDoesNotContain = txtTitleNotContains.Text;
            //WikiDumpSearcher.Properties.Settings.Default.TitleContainsEnabled = chkCheckTitle.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.TitleNotContainsEnabled = chkCheckNotInTitle.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.TitleIsRegex = chkTitleRegex.Checked;

            ////characters and links
            //WikiDumpSearcher.Properties.Settings.Default.CountCharatersIndex = cmboLength.SelectedIndex;
            //WikiDumpSearcher.Properties.Settings.Default.CountLinksIndex = cmboLinks.SelectedIndex;
            //WikiDumpSearcher.Properties.Settings.Default.Characters = numLength.Value;
            //WikiDumpSearcher.Properties.Settings.Default.Links = nudLinks.Value;

            ////extra
            //WikiDumpSearcher.Properties.Settings.Default.BoldTitle = chkNoBold.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.BadLinks = chkBadLinks.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.HTMLEntities = chkHTML.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.SimpleLinks = chkSimpleLinks.Checked;

            ////results
            //WikiDumpSearcher.Properties.Settings.Default.AddHeading = chkHeading.Checked;
            //WikiDumpSearcher.Properties.Settings.Default.HeadingGap = nudHeadingSpace.Value;
            //WikiDumpSearcher.Properties.Settings.Default.ResultsLimit = nudLimitResults.Value;
            //WikiDumpSearcher.Properties.Settings.Default.BulletHash = rdoBullet.Checked;

            //WikiDumpSearcher.Properties.Settings.Default.FileName = fileName;

            //WikiDumpSearcher.Properties.Settings.Default.Save();
        }

        private void loadSettings()
        {
            //menu
            //ignoreRedirectsToolStripMenuItem1.Checked = WikiDumpSearcher.Properties.Settings.Default.IgnoreRedirects;
            //ignoreCommentsToolStripMenuItem.Checked = WikiDumpSearcher.Properties.Settings.Default.IgnoreComments;
            //ignoreDisambigsToolStripMenuItem.Checked = WikiDumpSearcher.Properties.Settings.Default.IgnoreDisambigs;
            //ignoreImagesToolStripMenuItem.Checked = WikiDumpSearcher.Properties.Settings.Default.IgnoreImage;
            //ignoreTemplateNamespaceToolStripMenuItem.Checked = WikiDumpSearcher.Properties.Settings.Default.IgnoreTemplate;
            //ignoreWikipediaNamespaceToolStripMenuItem.Checked = WikiDumpSearcher.Properties.Settings.Default.IgnoreWikipedia;
            //ignoreCategoryNamespaceToolStripMenuItem.Checked = WikiDumpSearcher.Properties.Settings.Default.IngoreCategory;
            //ignoreMainNamespaceToolStripMenuItem.Checked = WikiDumpSearcher.Properties.Settings.Default.IgnoreMain;

            //Threadpriority = WikiDumpSearcher.Properties.Settings.Default.ThreadPriority;

            ////contains
            //txtPattern.Text = WikiDumpSearcher.Properties.Settings.Default.DoesContain;
            //txtPatternNot.Text = WikiDumpSearcher.Properties.Settings.Default.DoesNotContain;
            //chkDoesContain.Checked = WikiDumpSearcher.Properties.Settings.Default.DoesContainEnabled;
            //chkDoesNotContain.Checked = WikiDumpSearcher.Properties.Settings.Default.DoesNotContainEnabled;

            //chkRegex.Checked = WikiDumpSearcher.Properties.Settings.Default.IsRegex;
            //chkCaseSensitive.Checked = WikiDumpSearcher.Properties.Settings.Default.IsCaseSens;
            //chkMulti.Checked = WikiDumpSearcher.Properties.Settings.Default.IsMulti;
            //chkSingle.Checked = WikiDumpSearcher.Properties.Settings.Default.IsSingle;

            ////title
            //txtTitleContains.Text = WikiDumpSearcher.Properties.Settings.Default.TitleContains;
            //txtTitleNotContains.Text = WikiDumpSearcher.Properties.Settings.Default.TitleDoesNotContain;
            //chkCheckTitle.Checked = WikiDumpSearcher.Properties.Settings.Default.TitleContainsEnabled;
            //chkCheckNotInTitle.Checked = WikiDumpSearcher.Properties.Settings.Default.TitleNotContainsEnabled;
            //chkTitleRegex.Checked = WikiDumpSearcher.Properties.Settings.Default.TitleIsRegex;

            ////characters and links
            //cmboLength.SelectedIndex = WikiDumpSearcher.Properties.Settings.Default.CountCharatersIndex;
            //cmboLinks.SelectedIndex = WikiDumpSearcher.Properties.Settings.Default.CountLinksIndex;
            //numLength.Value = WikiDumpSearcher.Properties.Settings.Default.Characters;
            //nudLinks.Value = WikiDumpSearcher.Properties.Settings.Default.Links;

            ////extra
            //chkNoBold.Checked = WikiDumpSearcher.Properties.Settings.Default.BoldTitle;
            //chkBadLinks.Checked = WikiDumpSearcher.Properties.Settings.Default.BadLinks;
            //chkHTML.Checked = WikiDumpSearcher.Properties.Settings.Default.HTMLEntities;
            //chkSimpleLinks.Checked = WikiDumpSearcher.Properties.Settings.Default.SimpleLinks;

            ////results
            //chkHeading.Checked = WikiDumpSearcher.Properties.Settings.Default.AddHeading;
            //nudHeadingSpace.Value = WikiDumpSearcher.Properties.Settings.Default.HeadingGap;
            //nudLimitResults.Value = WikiDumpSearcher.Properties.Settings.Default.ResultsLimit;
            //rdoBullet.Checked = WikiDumpSearcher.Properties.Settings.Default.BulletHash;

            //fileName = WikiDumpSearcher.Properties.Settings.Default.FileName;
        }

        private void resetSettings()
        {
            //menu
            ignoreRedirectsToolStripMenuItem1.Checked = true;
            ignoreCommentsToolStripMenuItem.Checked = false;
            ignoreImagesToolStripMenuItem.Checked = true;
            ignoreTemplateNamespaceToolStripMenuItem.Checked = true;
            ignoreWikipediaNamespaceToolStripMenuItem.Checked = true;
            ignoreCategoryNamespaceToolStripMenuItem.Checked = true;
            ignoreMainNamespaceToolStripMenuItem.Checked = false;

            //contains
            txtPattern.Text = "";
            txtPatternNot.Text = "";
            chkArticleDoesContain.Checked = false; ;
            chkArticleDoesNotContain.Checked = false;

            chkRegex.Checked = false;
            chkCaseSensitive.Checked = false;
            chkMulti.Checked = false;
            chkSingle.Checked = false;

            //title
            txtTitleContains.Text = "";
            txtTitleNotContains.Text = "";
            chkTitleContains.Checked = false;
            chkTitleDoesNotContain.Checked = false;
            chkTitleRegex.Checked = false;

            //characters and links
            cmboLength.SelectedIndex = 0;
            cmboLinks.SelectedIndex = 0;
            nudLength.Value = 1000;
            nudLinks.Value = 5;

            //extra
            chkNoBold.Checked = false;
            chkBadLinks.Checked = false;
            chkHasHTML.Checked = false;
            chkSimpleLinks.Checked = false;

            //results
            chkHeading.Checked = false;
            nudHeadingSpace.Value = 25;
            nudLimitResults.Value = 30000;
            rdoBullet.Checked = false;
            rdoHash.Checked = true;

            fileName = ""; ;
        }

        #endregion
    }
}