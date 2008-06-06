/*
Autowikibrowser
Copyright (C) 2007 Martin Richards

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
using System.Text.RegularExpressions;
using WikiFunctions;
using WikiFunctions.Plugin;

namespace AutoWikiBrowser
{
    internal sealed partial class SkipOptions : Form, ISkipOptions
    {
        public SkipOptions()
        {
            InitializeComponent();
        }

        #region Properties

        public bool SkipNoUnicode
        {
            get { return chkNoUnicode.Checked; }
        }

        public bool SkipNoTag
        {
            get { return chkNoTag.Checked; }
        }

        public bool SkipNoHeaderError
        {
            get { return chkNoHeaderError.Checked; }
        }

        public bool SkipNoBoldTitle
        {
            get { return chkNoBoldTitle.Checked; }
        }

        public bool SkipNoBulletedLink
        {
            get { return chkNoBulletedLink.Checked; }
        }

        public bool SkipNoBadLink
        {
            get { return chkNoBadLink.Checked; }
        }

        public bool SkipNoDefaultSortAdded
        {
            get { return chkDefaultSortAdded.Checked; }
        }

        #endregion

        #region Methods

        public bool SkipIf(string articleText)
        {//custom code to skip articles can be added here
            return true;
        }

        private void SkipOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public string SelectedItem
        {
            get
            {
                foreach (CheckBox chk in gbOptions.Controls)
                {
                    if (chk.Checked)
                        return chk.Tag.ToString();
                }

                return "0";
            }
            set
            {
                foreach (CheckBox chk in gbOptions.Controls)
                {
                    if (chk.Tag.ToString() == value)
                    {
                        chk.Checked = true;
                        return;
                    }
                }
            }
        }

        public List<string> SelectedItems
        {
            get 
            {
                List<string> ret = new List<string>();
                foreach (CheckBox chk in gbOptions.Controls)
                {
                    if (chk.Checked)
                        ret.Add(chk.Tag.ToString());
                }

                return ret;
            }
            set { }
        }

        #endregion

    }
}