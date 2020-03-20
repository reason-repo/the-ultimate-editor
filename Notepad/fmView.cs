using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class fmView : Form
    {
        private string title = "The Ultimate Editor";
        private string _fileName = null;
        private bool isChanged = false;
        public string FileName { get => _fileName; set {
                this._fileName = value;
                setTitle();
            } }

        public void setTitle()
        {
            var changedSign = this.isChanged ? "*" : "";
            this.Text = String.Format("{0} [{1}]{2}", this.title, this._fileName, changedSign);
        }
        public fmView()
        {
            InitializeComponent();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbPrimary.Cut();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            if(DialogResult.OK == fileDialog.ShowDialog())
            {
                this.FileName = fileDialog.FileName;
                tbPrimary.Text = File.ReadAllText(this.FileName);
                this.isChanged = false;
                setTitle();
            }
        }


        private void saveAsFile()
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            if (DialogResult.OK == saveDialog.ShowDialog())
            {
                this.FileName = saveDialog.FileName;
                saveFile();
            }
        }

        private void saveFile()
        {
            this.isChanged = false;
            setTitle();
            File.WriteAllText(this.FileName, tbPrimary.Text);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.FileName == null)
            {
                saveAsFile();
            } else
            {
                saveFile();
            }
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbPrimary_TextChanged(object sender, EventArgs e)
        {
            this.isChanged = true;
            setTitle();
        }

        private void fmView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isChanged)
            {
                var answer = MessageBox.Show("You are not saved your changes.", "Close application?", MessageBoxButtons.YesNo);
                e.Cancel = (answer == DialogResult.No);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbPrimary.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbPrimary.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbPrimary.Paste();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                String.Format("{0}\nVersion 1.0", this.title), "About");
        }
    }
}
