﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoWikiBrowser.Plugins.TheTemplator
{
	public partial class DebugOutput : Form
	{
		public DebugOutput()
		{
			InitializeComponent();

			HeaderFonts = new [] { 
				new Font("Arial", 14, FontStyle.Bold | FontStyle.Underline),
				new Font("Arial", 13, FontStyle.Bold),
				new Font("Arial", 12, FontStyle.Bold | FontStyle.Underline),
				new Font("Arial", 11, FontStyle.Bold),
				new Font("Arial", 10, FontStyle.Bold | FontStyle.Underline),
			};
		}

		public void AddSection(string heading, string text)
		{
			textBox.SelectionFont = HeaderFonts[Math.Max(0, Math.Min(HeaderFont, HeaderFonts.GetUpperBound(0)))];
			textBox.SelectedText = heading + "\n";
			textBox.SelectionFont = body;
			textBox.SelectionIndent += indent;
			textBox.SelectedText = text + "\n\n";
			textBox.SelectionIndent -= indent;
		}

		public void StartSection(string heading)
		{
			textBox.SelectionFont = HeaderFonts[Math.Max(0, Math.Min(HeaderFont, HeaderFonts.GetUpperBound(0)))];
			textBox.SelectedText = heading + "\n";
			++HeaderFont;
			textBox.SelectionIndent += indent;
		}

		public void EndSection()
		{
			System.Diagnostics.Debug.Assert(HeaderFont > 0);
			if (HeaderFont > 0)
				--HeaderFont;
			textBox.SelectionIndent -= indent;
		}

        public void Clear()
        {
            HeaderFont = 0;
            textBox.Text = "";
            textBox.SelectionIndent = 0;
        }

        readonly Font body = new Font("Lucida Console", 10);
        int HeaderFont = 0;
        Font[] HeaderFonts;
        const int indent = 20;
    }
}
