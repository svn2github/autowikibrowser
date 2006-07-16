using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using WikiFunctions;

namespace AutoWikiBrowser
{
    public partial class MainForm
    {
        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSettings();
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadSettingsDialog();
        }

        private void loadDefaultSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetSettings();
        }

        private void ResetSettings()
        {
            findAndReplace.Clear();
            replaceSpecial.Clear();
            cmboSourceSelect.SelectedIndex = 0;
            txtSelectSource.Text = "";

            chkGeneralParse.Checked = true;
            chkAutoTagger.Checked = true;
            chkUnicodifyWhole.Checked = true;

            chkFindandReplace.Checked = false;
            chkIgnoreWhenNoFAR.Checked = true;
            findAndReplace.isRegex = false;
            findAndReplace.caseSensitive = false;
            findAndReplace.isMulti = false;
            findAndReplace.isSingle = false;
            findAndReplace.ignoreLinks = false;

            cmboCategorise.SelectedIndex = 0;
            txtNewCategory.Text = "";

            chkIgnoreIfContains.Checked = false;
            chkOnlyIfContains.Checked = false;
            chkIgnoreIsRegex.Checked = false;
            chkIgnoreCaseSensitive.Checked = false;
            txtIgnoreIfContains.Text = "";
            txtOnlyIfContains.Text = "";

            chkAppend.Checked = false;
            rdoAppend.Checked = true;
            txtAppendMessage.Text = "";

            cmboImages.SelectedIndex = 0;
            txtImageReplace.Text = "";
            txtImageWith.Text = "";

            txtFind.Text = "";
            chkFindRegex.Checked = false;
            chkFindCaseSensitive.Checked = false;

            cmboEditSummary.SelectedIndex = 0;

            wordWrapToolStripMenuItem1.Checked = true;
            panel2.Show();
            enableToolBar = false;
            bypassRedirectsToolStripMenuItem.Checked = true;
            ignoreNonexistentPagesToolStripMenuItem.Checked = true;
            doNotAutomaticallyDoAnythingToolStripMenuItem.Checked = false;
            chkSkipNoChanges.Checked = false;
            previewInsteadOfDiffToolStripMenuItem.Checked = false;
            markAllAsMinorToolStripMenuItem.Checked = false;
            addAllToWatchlistToolStripMenuItem.Checked = false;
            showTimerToolStripMenuItem.Checked = false;
            alphaSortInterwikiLinksToolStripMenuItem.Checked = true;
            addIgnoredToLogFileToolStripMenuItem.Checked = false;

            PasteMore1.Text = "";
            PasteMore2.Text = "";
            PasteMore3.Text = "";
            PasteMore4.Text = "";
            PasteMore5.Text = "";
            PasteMore6.Text = "";
            PasteMore7.Text = "";
            PasteMore8.Text = "";
            PasteMore9.Text = "";
            PasteMore10.Text = "";

            chkAutoMode.Checked = false;
            chkQuickSave.Checked = false;
            nudBotSpeed.Value = 15;

            lblStatusText.Text = "Default settings loaded.";
        }

        private void loadSettingsDialog()
        {
            if (openXML.ShowDialog() != DialogResult.OK)
                return;
            loadSettings(openXML.FileName);
        }

        private void loadDefaultSettings()
        {//load Default.xml file if it exists
            try
            {
                string filename = Environment.CurrentDirectory + "\\Default.xml";

                if (File.Exists(filename))
                    loadSettings(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadSettings(string filename)
        {
            try
            {
                strSettingsFile = " - " + filename.Remove(0, filename.LastIndexOf("\\") + 1);
                this.Text = "AutoWikiBrowser" + strSettingsFile;

                Stream stream = new FileStream(filename, FileMode.Open);
                findAndReplace.Clear();
                cmboEditSummary.Items.Clear();

                using (XmlTextReader reader = new XmlTextReader(stream))
                {
                    reader.WhitespaceHandling = WhitespaceHandling.None;
                    while (reader.Read())
                    {
                        if (reader.Name == "datagridFAR" && reader.HasAttributes)
                        {
                            string find = "";
                            string replace = "";

                            reader.MoveToAttribute("find");
                            find = reader.Value;
                            reader.MoveToAttribute("replacewith");
                            replace = reader.Value;

                            if (find.Length > 0)
                                findAndReplace.AddNew(find, replace);

                            continue;
                        }

                        if (reader.Name == WikiFunctions.MWB.ReplaceSpecial.XmlName)
                        {
                            bool enabled = false;
                            replaceSpecial.ReadFromXml(reader, ref enabled);
                            continue;
                        }

                        if (reader.Name == "projectlang" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("proj");
                            string project = reader.Value;
                            reader.MoveToAttribute("lang");
                            string language = reader.Value;
                            SetProject(language, project);

                            continue;
                        }
                        if (reader.Name == "selectsource" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("index");
                            cmboSourceSelect.SelectedIndex = int.Parse(reader.Value);
                            reader.MoveToAttribute("text");
                            txtSelectSource.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "general" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("general");
                            chkGeneralParse.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("tagger");
                            chkAutoTagger.Checked = bool.Parse(reader.Value);
                            //reader.MoveToAttribute("whitespace");
                            //chkRemoveWhiteSpace.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("unicodifyer");
                            chkUnicodifyWhole.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "findandreplace" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            chkFindandReplace.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("regex");
                            findAndReplace.isRegex = bool.Parse(reader.Value);
                            reader.MoveToAttribute("casesensitive");
                            findAndReplace.caseSensitive = bool.Parse(reader.Value);
                            reader.MoveToAttribute("multiline");
                            findAndReplace.isMulti = bool.Parse(reader.Value);
                            reader.MoveToAttribute("singleline");
                            findAndReplace.isSingle = bool.Parse(reader.Value);
                            if (reader.AttributeCount > 5)
                            {
                                reader.MoveToAttribute("ignorenofar");
                                chkIgnoreWhenNoFAR.Checked = bool.Parse(reader.Value);
                            }
                            if (reader.AttributeCount > 6)
                            {
                                reader.MoveToAttribute("ignoretext");
                                findAndReplace.ignoreLinks = bool.Parse(reader.Value);
                            }

                            continue;
                        }
                        if (reader.Name == "categorisation" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("index");
                            cmboCategorise.SelectedIndex = int.Parse(reader.Value);
                            reader.MoveToAttribute("text");
                            txtNewCategory.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "skip" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("does");
                            chkIgnoreIfContains.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("doesnot");
                            chkOnlyIfContains.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("regex");
                            chkIgnoreIsRegex.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("casesensitive");
                            chkIgnoreCaseSensitive.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("doestext");
                            txtIgnoreIfContains.Text = reader.Value;
                            reader.MoveToAttribute("doesnottext");
                            txtOnlyIfContains.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "message" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            chkAppend.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("text");
                            txtAppendMessage.Text = reader.Value;
                            if (reader.AttributeCount > 2)
                            {
                                reader.MoveToAttribute("append");
                                rdoAppend.Checked = bool.Parse(reader.Value);
                            }

                            continue;
                        }
                        if (reader.Name == "imager" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("index");
                            cmboImages.SelectedIndex = int.Parse(reader.Value);
                            reader.MoveToAttribute("replace");
                            txtImageReplace.Text = reader.Value;
                            reader.MoveToAttribute("with");
                            txtImageWith.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "find" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            txtFind.Text = reader.Value;
                            reader.MoveToAttribute("regex");
                            chkFindRegex.Checked = bool.Parse(reader.Value);
                            reader.MoveToAttribute("casesensitive");
                            chkFindCaseSensitive.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "summary" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            if (!cmboEditSummary.Items.Contains(reader.Value) && reader.Value.Length > 0)
                                cmboEditSummary.Items.Add(reader.Value);

                            continue;
                        }
                        if (reader.Name == "summaryindex" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("index");
                            cmboEditSummary.Text = reader.Value;

                            continue;
                        }

                        //menu
                        if (reader.Name == "wordwrap" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            wordWrapToolStripMenuItem1.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "toolbar" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            enableToolBar = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "bypass" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            bypassRedirectsToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "ingnorenonexistent" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            ignoreNonexistentPagesToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "noautochanges" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            doNotAutomaticallyDoAnythingToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "skipnochanges" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            chkSkipNoChanges.Checked = bool.Parse(reader.Value);
                        }
                        if (reader.Name == "preview" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            previewInsteadOfDiffToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "minor" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            markAllAsMinorToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "watch" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            addAllToWatchlistToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "timer" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            showTimerToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "sortinterwiki" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            alphaSortInterwikiLinksToolStripMenuItem.Checked = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "addignoredtolog" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("enabled");
                            addIgnoredToLogFileToolStripMenuItem.Checked = bool.Parse(reader.Value);
                            btnFalsePositive.Visible = bool.Parse(reader.Value);

                            continue;
                        }
                        if (reader.Name == "pastemore1" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore1.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore2" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore2.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore3" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore3.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore4" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore4.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore5" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore5.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore6" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore6.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore7" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore7.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore8" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore8.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore9" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore9.Text = reader.Value;

                            continue;
                        }
                        if (reader.Name == "pastemore10" && reader.HasAttributes)
                        {
                            reader.MoveToAttribute("text");
                            PasteMore10.Text = reader.Value;

                            continue;
                        }
                    }
                    stream.Close();
                    lblStatusText.Text = "Settings successfully loaded";
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveSettings()
        {
            try
            {
                if (saveXML.ShowDialog() != DialogResult.OK)
                    return;

                strSettingsFile = " - " + saveXML.FileName.Remove(0, saveXML.FileName.LastIndexOf("\\") + 1);
                this.Text = "AutoWikiBrowser" + strSettingsFile;

                XmlTextWriter textWriter = new XmlTextWriter(saveXML.FileName, UTF8Encoding.UTF8);
                // Opens the document
                textWriter.Formatting = Formatting.Indented;
                textWriter.WriteStartDocument();

                // Write first element
                textWriter.WriteStartElement("Settings");
                textWriter.WriteAttributeString("program", "AWB");
                textWriter.WriteAttributeString("schema", "1");

                textWriter.WriteStartElement("Project");
                textWriter.WriteStartElement("projectlang");
                textWriter.WriteAttributeString("proj", Variables.Project);
                textWriter.WriteAttributeString("lang", Variables.LangCode);
                textWriter.WriteEndElement();
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Options");

                textWriter.WriteStartElement("selectsource");
                textWriter.WriteAttributeString("index", cmboSourceSelect.SelectedIndex.ToString());
                textWriter.WriteAttributeString("text", txtSelectSource.Text);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("general");
                textWriter.WriteAttributeString("general", chkGeneralParse.Checked.ToString());
                textWriter.WriteAttributeString("tagger", chkAutoTagger.Checked.ToString());
                //textWriter.WriteAttributeString("whitespace", chkRemoveWhiteSpace.Checked.ToString());
                textWriter.WriteAttributeString("unicodifyer", chkUnicodifyWhole.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("categorisation");
                textWriter.WriteAttributeString("index", cmboCategorise.SelectedIndex.ToString());
                textWriter.WriteAttributeString("text", txtNewCategory.Text);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("skip");
                textWriter.WriteAttributeString("does", chkIgnoreIfContains.Checked.ToString());
                textWriter.WriteAttributeString("doesnot", chkOnlyIfContains.Checked.ToString());
                textWriter.WriteAttributeString("regex", chkIgnoreIsRegex.Checked.ToString());
                textWriter.WriteAttributeString("casesensitive", chkIgnoreCaseSensitive.Checked.ToString());
                textWriter.WriteAttributeString("doestext", txtIgnoreIfContains.Text);
                textWriter.WriteAttributeString("doesnottext", txtOnlyIfContains.Text);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("message");
                textWriter.WriteAttributeString("enabled", chkAppend.Checked.ToString());
                textWriter.WriteAttributeString("text", txtAppendMessage.Text);
                textWriter.WriteAttributeString("append", rdoAppend.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("imager");
                textWriter.WriteAttributeString("index", cmboImages.SelectedIndex.ToString());
                textWriter.WriteAttributeString("replace", txtImageReplace.Text);
                textWriter.WriteAttributeString("with", txtImageWith.Text);
                textWriter.WriteEndElement();
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("FindAndReplaceSettings");
                findAndReplace.WriteToXml(textWriter, chkFindandReplace.Checked, chkIgnoreWhenNoFAR.Checked);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("FindAndReplace");
                replaceSpecial.WriteToXml(textWriter, chkFindandReplace.Checked);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("startoptions");
                int j = 0;
                while (j < cmboEditSummary.Items.Count)
                {
                    textWriter.WriteStartElement("summary");
                    textWriter.WriteAttributeString("text", cmboEditSummary.Items[j].ToString());
                    textWriter.WriteEndElement();
                    j++;
                }

                if (!cmboEditSummary.Items.Contains(cmboEditSummary.Text))
                {
                    textWriter.WriteStartElement("summary");
                    textWriter.WriteAttributeString("text", cmboEditSummary.Text);
                    textWriter.WriteEndElement();
                }

                textWriter.WriteStartElement("summaryindex");
                textWriter.WriteAttributeString("index", cmboEditSummary.Text);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("find");
                textWriter.WriteAttributeString("text", txtFind.Text);
                textWriter.WriteAttributeString("regex", chkFindRegex.Checked.ToString());
                textWriter.WriteAttributeString("casesensitive", chkFindCaseSensitive.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("menu");

                textWriter.WriteStartElement("wordwrap");
                textWriter.WriteAttributeString("enabled", wordWrapToolStripMenuItem1.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("toolbar");
                textWriter.WriteAttributeString("enabled", enableToolBar.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("bypass");
                textWriter.WriteAttributeString("enabled", bypassRedirectsToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("ingnorenonexistent");
                textWriter.WriteAttributeString("enabled", ignoreNonexistentPagesToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("noautochanges");
                textWriter.WriteAttributeString("enabled", doNotAutomaticallyDoAnythingToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("skipnochanges");
                textWriter.WriteAttributeString("enabled", chkSkipNoChanges.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("preview");
                textWriter.WriteAttributeString("enabled", previewInsteadOfDiffToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("minor");
                textWriter.WriteAttributeString("enabled", markAllAsMinorToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("watch");
                textWriter.WriteAttributeString("enabled", addAllToWatchlistToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("timer");
                textWriter.WriteAttributeString("enabled", showTimerToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("sortinterwiki");
                textWriter.WriteAttributeString("enabled", alphaSortInterwikiLinksToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("addignoredtolog");
                textWriter.WriteAttributeString("enabled", addIgnoredToLogFileToolStripMenuItem.Checked.ToString());
                textWriter.WriteEndElement();
                textWriter.WriteEndElement();
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("pastemore");

                textWriter.WriteStartElement("pastemore1");
                textWriter.WriteAttributeString("text", PasteMore1.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore2");
                textWriter.WriteAttributeString("text", PasteMore2.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore3");
                textWriter.WriteAttributeString("text", PasteMore3.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore4");
                textWriter.WriteAttributeString("text", PasteMore4.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore5");
                textWriter.WriteAttributeString("text", PasteMore5.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore6");
                textWriter.WriteAttributeString("text", PasteMore6.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore7");
                textWriter.WriteAttributeString("text", PasteMore7.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore8");
                textWriter.WriteAttributeString("text", PasteMore8.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore9");
                textWriter.WriteAttributeString("text", PasteMore9.Text);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("pastemore10");
                textWriter.WriteAttributeString("text", PasteMore10.Text);
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
                textWriter.WriteEndElement();

                // Ends the document.
                textWriter.WriteEndDocument();
                // close writer
                textWriter.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lblStatusText.Text = "Settings successfully saved";
        }
    }
}
