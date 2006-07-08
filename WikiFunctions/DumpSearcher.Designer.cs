using System.Windows.Forms;

namespace WikiFunctions
{
    partial class DumpSearcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DumpSearcher));
            this.txtList = new System.Windows.Forms.TextBox();
            this.openXMLDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreRedirectsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreDisambigsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ignoreImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreWikipediaNamespaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreCategoryNamespaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreTemplateNamespaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreMainNamespaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ignoreCommentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboveNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.belowNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSingle = new System.Windows.Forms.CheckBox();
            this.chkMulti = new System.Windows.Forms.CheckBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.chkRegex = new System.Windows.Forms.CheckBox();
            this.cmboLinks = new System.Windows.Forms.ComboBox();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.chkDoesNotContain = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkDoesContain = new System.Windows.Forms.CheckBox();
            this.nudLinks = new System.Windows.Forms.NumericUpDown();
            this.cmboLength = new System.Windows.Forms.ComboBox();
            this.txtPatternNot = new System.Windows.Forms.TextBox();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitleNotContains = new System.Windows.Forms.TextBox();
            this.chkCheckNotInTitle = new System.Windows.Forms.CheckBox();
            this.txtTitleContains = new System.Windows.Forms.TextBox();
            this.chkCheckTitle = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTitleCase = new System.Windows.Forms.CheckBox();
            this.chkTitleRegex = new System.Windows.Forms.CheckBox();
            this.chkSimpleLinks = new System.Windows.Forms.CheckBox();
            this.chkNoBold = new System.Windows.Forms.CheckBox();
            this.nudLimitResults = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudHeadingSpace = new System.Windows.Forms.NumericUpDown();
            this.chkHeading = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnTransfer = new System.Windows.Forms.Button();
            this.AlphaList = new System.Windows.Forms.Button();
            this.lbClear = new System.Windows.Forms.Button();
            this.lbArticles = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoBullet = new System.Windows.Forms.RadioButton();
            this.rdoHash = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkHTML = new System.Windows.Forms.CheckBox();
            this.chkBadLinks = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLinks)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimitResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadingSpace)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtList
            // 
            this.txtList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtList.Location = new System.Drawing.Point(10, 70);
            this.txtList.MaxLength = 0;
            this.txtList.Multiline = true;
            this.txtList.Name = "txtList";
            this.txtList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtList.Size = new System.Drawing.Size(266, 232);
            this.txtList.TabIndex = 0;
            this.txtList.WordWrap = false;
            // 
            // openXMLDialog
            // 
            this.openXMLDialog.FileName = "current or articles XML file";
            this.openXMLDialog.Filter = "XML file|*.xml";
            this.openXMLDialog.Title = "Open \"current\" or \"articles\" XML file";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(11, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(62, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "text file|.txt";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem1,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.openToolStripMenuItem.Text = "&Open XML dump";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.saveToolStripMenuItem.Text = "&Save results list";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem1.Text = "Reset settings";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(188, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(112, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem1,
            this.otherToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(613, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ignoreRedirectsToolStripMenuItem1,
            this.ignoreDisambigsToolStripMenuItem,
            this.toolStripSeparator1,
            this.ignoreImagesToolStripMenuItem,
            this.ignoreWikipediaNamespaceToolStripMenuItem,
            this.ignoreCategoryNamespaceToolStripMenuItem,
            this.ignoreTemplateNamespaceToolStripMenuItem,
            this.ignoreMainNamespaceToolStripMenuItem,
            this.toolStripSeparator2,
            this.ignoreCommentsToolStripMenuItem});
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem1.Text = "Options";
            // 
            // ignoreRedirectsToolStripMenuItem1
            // 
            this.ignoreRedirectsToolStripMenuItem1.Checked = true;
            this.ignoreRedirectsToolStripMenuItem1.CheckOnClick = true;
            this.ignoreRedirectsToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreRedirectsToolStripMenuItem1.Name = "ignoreRedirectsToolStripMenuItem1";
            this.ignoreRedirectsToolStripMenuItem1.Size = new System.Drawing.Size(248, 22);
            this.ignoreRedirectsToolStripMenuItem1.Text = "Ignore redirects";
            // 
            // ignoreDisambigsToolStripMenuItem
            // 
            this.ignoreDisambigsToolStripMenuItem.CheckOnClick = true;
            this.ignoreDisambigsToolStripMenuItem.Name = "ignoreDisambigsToolStripMenuItem";
            this.ignoreDisambigsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ignoreDisambigsToolStripMenuItem.Text = "Ignore disambigs";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(245, 6);
            // 
            // ignoreImagesToolStripMenuItem
            // 
            this.ignoreImagesToolStripMenuItem.Checked = true;
            this.ignoreImagesToolStripMenuItem.CheckOnClick = true;
            this.ignoreImagesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreImagesToolStripMenuItem.Name = "ignoreImagesToolStripMenuItem";
            this.ignoreImagesToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ignoreImagesToolStripMenuItem.Text = "Ignore image namespace";
            // 
            // ignoreWikipediaNamespaceToolStripMenuItem
            // 
            this.ignoreWikipediaNamespaceToolStripMenuItem.Checked = true;
            this.ignoreWikipediaNamespaceToolStripMenuItem.CheckOnClick = true;
            this.ignoreWikipediaNamespaceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreWikipediaNamespaceToolStripMenuItem.Name = "ignoreWikipediaNamespaceToolStripMenuItem";
            this.ignoreWikipediaNamespaceToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ignoreWikipediaNamespaceToolStripMenuItem.Text = "Ignore Wikipedia namespace";
            // 
            // ignoreCategoryNamespaceToolStripMenuItem
            // 
            this.ignoreCategoryNamespaceToolStripMenuItem.Checked = true;
            this.ignoreCategoryNamespaceToolStripMenuItem.CheckOnClick = true;
            this.ignoreCategoryNamespaceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreCategoryNamespaceToolStripMenuItem.Name = "ignoreCategoryNamespaceToolStripMenuItem";
            this.ignoreCategoryNamespaceToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ignoreCategoryNamespaceToolStripMenuItem.Text = "Ignore Category namespace";
            // 
            // ignoreTemplateNamespaceToolStripMenuItem
            // 
            this.ignoreTemplateNamespaceToolStripMenuItem.Checked = true;
            this.ignoreTemplateNamespaceToolStripMenuItem.CheckOnClick = true;
            this.ignoreTemplateNamespaceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreTemplateNamespaceToolStripMenuItem.Name = "ignoreTemplateNamespaceToolStripMenuItem";
            this.ignoreTemplateNamespaceToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ignoreTemplateNamespaceToolStripMenuItem.Text = "Ignore Template namespace";
            // 
            // ignoreMainNamespaceToolStripMenuItem
            // 
            this.ignoreMainNamespaceToolStripMenuItem.CheckOnClick = true;
            this.ignoreMainNamespaceToolStripMenuItem.Name = "ignoreMainNamespaceToolStripMenuItem";
            this.ignoreMainNamespaceToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ignoreMainNamespaceToolStripMenuItem.Text = "Ignore Main namespace";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(245, 6);
            // 
            // ignoreCommentsToolStripMenuItem
            // 
            this.ignoreCommentsToolStripMenuItem.CheckOnClick = true;
            this.ignoreCommentsToolStripMenuItem.Name = "ignoreCommentsToolStripMenuItem";
            this.ignoreCommentsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.ignoreCommentsToolStripMenuItem.Text = "Ignore <!-- commented out text -->";
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threadPriorityToolStripMenuItem});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.otherToolStripMenuItem.Text = "Other";
            // 
            // threadPriorityToolStripMenuItem
            // 
            this.threadPriorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highestToolStripMenuItem,
            this.aboveNormalToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.belowNormalToolStripMenuItem,
            this.lowestToolStripMenuItem});
            this.threadPriorityToolStripMenuItem.Name = "threadPriorityToolStripMenuItem";
            this.threadPriorityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.threadPriorityToolStripMenuItem.Text = "Thread priority";
            // 
            // highestToolStripMenuItem
            // 
            this.highestToolStripMenuItem.CheckOnClick = true;
            this.highestToolStripMenuItem.Name = "highestToolStripMenuItem";
            this.highestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.highestToolStripMenuItem.Text = "Highest";
            this.highestToolStripMenuItem.Click += new System.EventHandler(this.highestToolStripMenuItem_Click);
            // 
            // aboveNormalToolStripMenuItem
            // 
            this.aboveNormalToolStripMenuItem.CheckOnClick = true;
            this.aboveNormalToolStripMenuItem.Name = "aboveNormalToolStripMenuItem";
            this.aboveNormalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboveNormalToolStripMenuItem.Text = "Above normal";
            this.aboveNormalToolStripMenuItem.Click += new System.EventHandler(this.aboveNormalToolStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Checked = true;
            this.normalToolStripMenuItem.CheckOnClick = true;
            this.normalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // belowNormalToolStripMenuItem
            // 
            this.belowNormalToolStripMenuItem.CheckOnClick = true;
            this.belowNormalToolStripMenuItem.Name = "belowNormalToolStripMenuItem";
            this.belowNormalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.belowNormalToolStripMenuItem.Text = "Below normal";
            this.belowNormalToolStripMenuItem.Click += new System.EventHandler(this.belowNormalToolStripMenuItem_Click);
            // 
            // lowestToolStripMenuItem
            // 
            this.lowestToolStripMenuItem.CheckOnClick = true;
            this.lowestToolStripMenuItem.Name = "lowestToolStripMenuItem";
            this.lowestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lowestToolStripMenuItem.Text = "Lowest";
            this.lowestToolStripMenuItem.Click += new System.EventHandler(this.lowestToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSingle);
            this.groupBox1.Controls.Add(this.chkMulti);
            this.groupBox1.Controls.Add(this.chkCaseSensitive);
            this.groupBox1.Controls.Add(this.chkRegex);
            this.groupBox1.Controls.Add(this.cmboLinks);
            this.groupBox1.Controls.Add(this.numLength);
            this.groupBox1.Controls.Add(this.chkDoesNotContain);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkDoesContain);
            this.groupBox1.Controls.Add(this.nudLinks);
            this.groupBox1.Controls.Add(this.cmboLength);
            this.groupBox1.Controls.Add(this.txtPatternNot);
            this.groupBox1.Controls.Add(this.txtPattern);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 187);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text matches";
            // 
            // chkSingle
            // 
            this.chkSingle.AutoSize = true;
            this.chkSingle.Enabled = false;
            this.chkSingle.Location = new System.Drawing.Point(209, 17);
            this.chkSingle.Name = "chkSingle";
            this.chkSingle.Size = new System.Drawing.Size(71, 17);
            this.chkSingle.TabIndex = 22;
            this.chkSingle.Text = "Singleline";
            this.toolTip1.SetToolTip(this.chkSingle, "Changes meaing of \".\"  so it matches all characters, as opposed to all apart from" +
                    " newlines");
            this.chkSingle.UseVisualStyleBackColor = true;
            // 
            // chkMulti
            // 
            this.chkMulti.AutoSize = true;
            this.chkMulti.Enabled = false;
            this.chkMulti.Location = new System.Drawing.Point(209, 34);
            this.chkMulti.Name = "chkMulti";
            this.chkMulti.Size = new System.Drawing.Size(64, 17);
            this.chkMulti.TabIndex = 21;
            this.chkMulti.Text = "Multiline";
            this.toolTip1.SetToolTip(this.chkMulti, "Changes meaning of \"^\" and \"$\" so they represent the beginning and end respective" +
                    "ly of every line, rather just of the entire string");
            this.chkMulti.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(99, 17);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(94, 17);
            this.chkCaseSensitive.TabIndex = 20;
            this.chkCaseSensitive.Text = "Case sensitive";
            this.toolTip1.SetToolTip(this.chkCaseSensitive, "Changes case sensitivity");
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // chkRegex
            // 
            this.chkRegex.AutoSize = true;
            this.chkRegex.Location = new System.Drawing.Point(11, 17);
            this.chkRegex.Name = "chkRegex";
            this.chkRegex.Size = new System.Drawing.Size(82, 17);
            this.chkRegex.TabIndex = 19;
            this.chkRegex.Text = "Are regexes";
            this.chkRegex.UseVisualStyleBackColor = true;
            this.chkRegex.CheckedChanged += new System.EventHandler(this.chkRegex_CheckedChanged);
            // 
            // cmboLinks
            // 
            this.cmboLinks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboLinks.FormattingEnabled = true;
            this.cmboLinks.Items.AddRange(new object[] {
            "Don\'t count links",
            "Greater than:",
            "Less than:"});
            this.cmboLinks.Location = new System.Drawing.Point(90, 132);
            this.cmboLinks.Name = "cmboLinks";
            this.cmboLinks.Size = new System.Drawing.Size(113, 21);
            this.cmboLinks.TabIndex = 16;
            this.cmboLinks.SelectedIndexChanged += new System.EventHandler(this.cmboLinks_SelectedIndexChanged);
            // 
            // numLength
            // 
            this.numLength.Enabled = false;
            this.numLength.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLength.Location = new System.Drawing.Point(209, 107);
            this.numLength.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLength.Name = "numLength";
            this.numLength.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numLength.Size = new System.Drawing.Size(73, 20);
            this.numLength.TabIndex = 10;
            this.numLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // chkDoesNotContain
            // 
            this.chkDoesNotContain.AutoSize = true;
            this.chkDoesNotContain.Location = new System.Drawing.Point(12, 81);
            this.chkDoesNotContain.Name = "chkDoesNotContain";
            this.chkDoesNotContain.Size = new System.Drawing.Size(110, 17);
            this.chkDoesNotContain.TabIndex = 13;
            this.chkDoesNotContain.Text = "Does not contain:";
            this.chkDoesNotContain.UseVisualStyleBackColor = true;
            this.chkDoesNotContain.CheckedChanged += new System.EventHandler(this.chkDoesNotContain_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "No. of links";
            // 
            // chkDoesContain
            // 
            this.chkDoesContain.AutoSize = true;
            this.chkDoesContain.Location = new System.Drawing.Point(12, 55);
            this.chkDoesContain.Name = "chkDoesContain";
            this.chkDoesContain.Size = new System.Drawing.Size(122, 17);
            this.chkDoesContain.TabIndex = 12;
            this.chkDoesContain.Text = "Article does contain:";
            this.chkDoesContain.UseVisualStyleBackColor = true;
            this.chkDoesContain.CheckedChanged += new System.EventHandler(this.chkDoesContain_CheckedChanged);
            // 
            // nudLinks
            // 
            this.nudLinks.Enabled = false;
            this.nudLinks.Location = new System.Drawing.Point(210, 133);
            this.nudLinks.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLinks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLinks.Name = "nudLinks";
            this.nudLinks.Size = new System.Drawing.Size(72, 20);
            this.nudLinks.TabIndex = 17;
            this.nudLinks.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // cmboLength
            // 
            this.cmboLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboLength.FormattingEnabled = true;
            this.cmboLength.Items.AddRange(new object[] {
            "Don\'t check length",
            "Greater than:",
            "Less than:"});
            this.cmboLength.Location = new System.Drawing.Point(90, 106);
            this.cmboLength.Name = "cmboLength";
            this.cmboLength.Size = new System.Drawing.Size(113, 21);
            this.cmboLength.TabIndex = 11;
            this.cmboLength.SelectedIndexChanged += new System.EventHandler(this.cmboLength_SelectedIndexChanged);
            // 
            // txtPatternNot
            // 
            this.txtPatternNot.Enabled = false;
            this.txtPatternNot.Location = new System.Drawing.Point(141, 81);
            this.txtPatternNot.Name = "txtPatternNot";
            this.txtPatternNot.Size = new System.Drawing.Size(142, 20);
            this.txtPatternNot.TabIndex = 14;
            // 
            // txtPattern
            // 
            this.txtPattern.Enabled = false;
            this.txtPattern.Location = new System.Drawing.Point(141, 55);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(142, 20);
            this.txtPattern.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Characters";
            // 
            // txtTitleNotContains
            // 
            this.txtTitleNotContains.Enabled = false;
            this.txtTitleNotContains.Location = new System.Drawing.Point(139, 66);
            this.txtTitleNotContains.Name = "txtTitleNotContains";
            this.txtTitleNotContains.Size = new System.Drawing.Size(137, 20);
            this.txtTitleNotContains.TabIndex = 27;
            this.txtTitleNotContains.Leave += new System.EventHandler(this.txtTitleNotContains_Leave);
            // 
            // chkCheckNotInTitle
            // 
            this.chkCheckNotInTitle.AutoSize = true;
            this.chkCheckNotInTitle.Location = new System.Drawing.Point(10, 66);
            this.chkCheckNotInTitle.Name = "chkCheckNotInTitle";
            this.chkCheckNotInTitle.Size = new System.Drawing.Size(128, 17);
            this.chkCheckNotInTitle.TabIndex = 26;
            this.chkCheckNotInTitle.Text = "Title does not contain";
            this.chkCheckNotInTitle.UseVisualStyleBackColor = true;
            this.chkCheckNotInTitle.CheckedChanged += new System.EventHandler(this.chkCheckNotInTitle_CheckedChanged);
            // 
            // txtTitleContains
            // 
            this.txtTitleContains.Enabled = false;
            this.txtTitleContains.Location = new System.Drawing.Point(139, 40);
            this.txtTitleContains.Name = "txtTitleContains";
            this.txtTitleContains.Size = new System.Drawing.Size(137, 20);
            this.txtTitleContains.TabIndex = 22;
            this.txtTitleContains.Leave += new System.EventHandler(this.txtTitleContains_Leave);
            // 
            // chkCheckTitle
            // 
            this.chkCheckTitle.AutoSize = true;
            this.chkCheckTitle.Location = new System.Drawing.Point(10, 40);
            this.chkCheckTitle.Name = "chkCheckTitle";
            this.chkCheckTitle.Size = new System.Drawing.Size(113, 17);
            this.chkCheckTitle.TabIndex = 23;
            this.chkCheckTitle.Text = "Title does contain:";
            this.chkCheckTitle.UseVisualStyleBackColor = true;
            this.chkCheckTitle.CheckedChanged += new System.EventHandler(this.chkCheckTitle_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTitleCase);
            this.groupBox2.Controls.Add(this.chkTitleRegex);
            this.groupBox2.Controls.Add(this.txtTitleNotContains);
            this.groupBox2.Controls.Add(this.chkCheckNotInTitle);
            this.groupBox2.Controls.Add(this.txtTitleContains);
            this.groupBox2.Controls.Add(this.chkCheckTitle);
            this.groupBox2.Location = new System.Drawing.Point(322, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 98);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Title";
            // 
            // chkTitleCase
            // 
            this.chkTitleCase.AutoSize = true;
            this.chkTitleCase.Checked = true;
            this.chkTitleCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTitleCase.Enabled = false;
            this.chkTitleCase.Location = new System.Drawing.Point(182, 17);
            this.chkTitleCase.Name = "chkTitleCase";
            this.chkTitleCase.Size = new System.Drawing.Size(94, 17);
            this.chkTitleCase.TabIndex = 29;
            this.chkTitleCase.Text = "Case sensitive";
            this.chkTitleCase.UseVisualStyleBackColor = true;
            // 
            // chkTitleRegex
            // 
            this.chkTitleRegex.AutoSize = true;
            this.chkTitleRegex.Location = new System.Drawing.Point(94, 17);
            this.chkTitleRegex.Name = "chkTitleRegex";
            this.chkTitleRegex.Size = new System.Drawing.Size(82, 17);
            this.chkTitleRegex.TabIndex = 28;
            this.chkTitleRegex.Text = "Are regexes";
            this.chkTitleRegex.UseVisualStyleBackColor = true;
            // 
            // chkSimpleLinks
            // 
            this.chkSimpleLinks.AutoSize = true;
            this.chkSimpleLinks.Location = new System.Drawing.Point(6, 41);
            this.chkSimpleLinks.Name = "chkSimpleLinks";
            this.chkSimpleLinks.Size = new System.Drawing.Size(150, 17);
            this.chkSimpleLinks.TabIndex = 31;
            this.chkSimpleLinks.Text = "Has links AWB will simplify";
            this.chkSimpleLinks.UseVisualStyleBackColor = true;
            // 
            // chkNoBold
            // 
            this.chkNoBold.AutoSize = true;
            this.chkNoBold.Location = new System.Drawing.Point(6, 21);
            this.chkNoBold.Name = "chkNoBold";
            this.chkNoBold.Size = new System.Drawing.Size(158, 17);
            this.chkNoBold.TabIndex = 30;
            this.chkNoBold.Text = "Has title AWB will embolden";
            this.chkNoBold.UseVisualStyleBackColor = true;
            // 
            // nudLimitResults
            // 
            this.nudLimitResults.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLimitResults.Location = new System.Drawing.Point(213, 22);
            this.nudLimitResults.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudLimitResults.Name = "nudLimitResults";
            this.nudLimitResults.Size = new System.Drawing.Size(84, 20);
            this.nudLimitResults.TabIndex = 13;
            this.nudLimitResults.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Limit no. of results";
            // 
            // nudHeadingSpace
            // 
            this.nudHeadingSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHeadingSpace.Enabled = false;
            this.nudHeadingSpace.Location = new System.Drawing.Point(133, 18);
            this.nudHeadingSpace.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudHeadingSpace.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHeadingSpace.Name = "nudHeadingSpace";
            this.nudHeadingSpace.Size = new System.Drawing.Size(43, 20);
            this.nudHeadingSpace.TabIndex = 20;
            this.nudHeadingSpace.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // chkHeading
            // 
            this.chkHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHeading.AutoSize = true;
            this.chkHeading.Location = new System.Drawing.Point(16, 18);
            this.chkHeading.Name = "chkHeading";
            this.chkHeading.Size = new System.Drawing.Size(115, 17);
            this.chkHeading.TabIndex = 19;
            this.chkHeading.Text = "Add heading every";
            this.chkHeading.UseVisualStyleBackColor = true;
            this.chkHeading.CheckedChanged += new System.EventHandler(this.chkHeading_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(168, 308);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(45, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.btnClear, "Clear");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(10, 308);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 23);
            this.btnCopy.TabIndex = 19;
            this.btnCopy.Text = "Copy to clipboard";
            this.toolTip1.SetToolTip(this.btnCopy, "Copy to clipboard");
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(10, 42);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(55, 22);
            this.btnTransfer.TabIndex = 23;
            this.btnTransfer.Text = "Make";
            this.toolTip1.SetToolTip(this.btnTransfer, "Turn list into wiki formatted text, which can be saved or copied.");
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.button1_Click);
            // 
            // AlphaList
            // 
            this.AlphaList.Location = new System.Drawing.Point(11, 308);
            this.AlphaList.Name = "AlphaList";
            this.AlphaList.Size = new System.Drawing.Size(47, 23);
            this.AlphaList.TabIndex = 24;
            this.AlphaList.Text = "abc...";
            this.toolTip1.SetToolTip(this.AlphaList, "Alphabetize the list");
            this.AlphaList.UseVisualStyleBackColor = true;
            this.AlphaList.Click += new System.EventHandler(this.AlphaList_Click);
            // 
            // lbClear
            // 
            this.lbClear.Location = new System.Drawing.Point(64, 308);
            this.lbClear.Name = "lbClear";
            this.lbClear.Size = new System.Drawing.Size(39, 23);
            this.lbClear.TabIndex = 25;
            this.lbClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.lbClear, "Clear the list");
            this.lbClear.UseVisualStyleBackColor = true;
            this.lbClear.Click += new System.EventHandler(this.lbClear_Click);
            // 
            // lbArticles
            // 
            this.lbArticles.ContextMenuStrip = this.contextMenuStrip1;
            this.lbArticles.FormattingEnabled = true;
            this.lbArticles.Location = new System.Drawing.Point(11, 51);
            this.lbArticles.Name = "lbArticles";
            this.lbArticles.Size = new System.Drawing.Size(286, 251);
            this.lbArticles.TabIndex = 22;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.lbClear);
            this.groupBox3.Controls.Add(this.AlphaList);
            this.groupBox3.Controls.Add(this.lbArticles);
            this.groupBox3.Controls.Add(this.nudLimitResults);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(13, 226);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 337);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Get results";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(79, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(38, 23);
            this.btnStop.TabIndex = 32;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoBullet);
            this.groupBox4.Controls.Add(this.rdoHash);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Controls.Add(this.txtList);
            this.groupBox4.Controls.Add(this.chkHeading);
            this.groupBox4.Controls.Add(this.nudHeadingSpace);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnTransfer);
            this.groupBox4.Controls.Add(this.btnCopy);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(322, 226);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(286, 337);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Make wikified list from results";
            // 
            // rdoBullet
            // 
            this.rdoBullet.AutoSize = true;
            this.rdoBullet.Location = new System.Drawing.Point(247, 18);
            this.rdoBullet.Name = "rdoBullet";
            this.rdoBullet.Size = new System.Drawing.Size(29, 17);
            this.rdoBullet.TabIndex = 26;
            this.rdoBullet.Text = "*";
            this.rdoBullet.UseVisualStyleBackColor = true;
            // 
            // rdoHash
            // 
            this.rdoHash.AutoSize = true;
            this.rdoHash.Checked = true;
            this.rdoHash.Location = new System.Drawing.Point(212, 18);
            this.rdoHash.Name = "rdoHash";
            this.rdoHash.Size = new System.Drawing.Size(32, 17);
            this.rdoHash.TabIndex = 25;
            this.rdoHash.TabStop = true;
            this.rdoHash.Text = "#";
            this.rdoHash.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(116, 308);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(47, 23);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1,
            this.lblCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 568);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(613, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // lblCount
            // 
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkHTML);
            this.groupBox5.Controls.Add(this.chkBadLinks);
            this.groupBox5.Controls.Add(this.chkNoBold);
            this.groupBox5.Controls.Add(this.chkSimpleLinks);
            this.groupBox5.Location = new System.Drawing.Point(322, 136);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(286, 84);
            this.groupBox5.TabIndex = 32;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Other";
            // 
            // chkHTML
            // 
            this.chkHTML.AutoSize = true;
            this.chkHTML.Location = new System.Drawing.Point(168, 21);
            this.chkHTML.Name = "chkHTML";
            this.chkHTML.Size = new System.Drawing.Size(114, 17);
            this.chkHTML.TabIndex = 33;
            this.chkHTML.Text = "Has HTML entities";
            this.chkHTML.UseVisualStyleBackColor = true;
            // 
            // chkBadLinks
            // 
            this.chkBadLinks.AutoSize = true;
            this.chkBadLinks.Location = new System.Drawing.Point(6, 61);
            this.chkBadLinks.Name = "chkBadLinks";
            this.chkBadLinks.Size = new System.Drawing.Size(148, 17);
            this.chkBadLinks.TabIndex = 32;
            this.chkBadLinks.Text = "Has bad links AWB will fix";
            this.chkBadLinks.UseVisualStyleBackColor = true;
            // 
            // DumpSearcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 590);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DumpSearcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wiki Data Dump Searcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLinks)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimitResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadingSpace)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtList;
        private System.Windows.Forms.OpenFileDialog openXMLDialog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ignoreRedirectsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ignoreDisambigsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudLimitResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem ignoreImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ignoreWikipediaNamespaceToolStripMenuItem;
        private Button btnClear;
        private Button btnCopy;
        private ToolTip toolTip1;
        private CheckBox chkHeading;
        private ToolStripMenuItem ignoreCategoryNamespaceToolStripMenuItem;
        private ToolStripMenuItem ignoreTemplateNamespaceToolStripMenuItem;
        private ToolStripMenuItem ignoreMainNamespaceToolStripMenuItem;
        private NumericUpDown nudHeadingSpace;
        private TextBox txtTitleContains;
        private CheckBox chkCheckTitle;
        private ListBox lbArticles;
        private Button btnTransfer;
        private Button AlphaList;
        private Button lbClear;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Button btnSave;
        private RadioButton rdoBullet;
        private RadioButton rdoHash;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar progressBar1;
        private Timer timer1;
        private ToolStripStatusLabel lblCount;
        private ToolStripMenuItem removeToolStripMenuItem;
        private TextBox txtTitleNotContains;
        private CheckBox chkCheckNotInTitle;
        private CheckBox chkTitleRegex;
        private CheckBox chkTitleCase;
        private CheckBox chkNoBold;
        private CheckBox chkSimpleLinks;
        private Button btnStop;
        private ToolStripMenuItem otherToolStripMenuItem;
        private ToolStripMenuItem threadPriorityToolStripMenuItem;
        private ToolStripMenuItem highestToolStripMenuItem;
        private ToolStripMenuItem aboveNormalToolStripMenuItem;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripMenuItem belowNormalToolStripMenuItem;
        private ToolStripMenuItem lowestToolStripMenuItem;
        private GroupBox groupBox5;
        private CheckBox chkCaseSensitive;
        private CheckBox chkRegex;
        private NumericUpDown numLength;
        private Label label4;
        private ComboBox cmboLength;
        private TextBox txtPattern;
        private Label label2;
        private TextBox txtPatternNot;
        private NumericUpDown nudLinks;
        private CheckBox chkDoesContain;
        private CheckBox chkDoesNotContain;
        private ComboBox cmboLinks;
        private CheckBox chkBadLinks;
        private CheckBox chkHTML;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem ignoreCommentsToolStripMenuItem;
        private CheckBox chkSingle;
        private CheckBox chkMulti;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator4;
    }
}

