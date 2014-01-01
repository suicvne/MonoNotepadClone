using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MonoNotepadClone
{
    public partial class MainForm : Form
    {
        public bool isFirstSave = true;
        public bool textHasChanged = false;
        public string fileToOpen = "";
        public string fileToSave = "";
        public static string userDocumentsDirectory = System.Environment.SpecialFolder.MyDocuments.ToString();

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textHasChanged == true)
            {
                var result = MessageBox.Show("You have unsaved changes, do you wish to save?", "Warning!", MessageBoxButtons.YesNoCancel);

                switch (result)
                { 
                    case DialogResult.Yes:
                        if (isFirstSave == false)
                        {
                            writeFile();

                        }
                        else if (isFirstSave == true)
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.InitialDirectory = fileToOpen;
                            saveFileDialog1.Title = "Select file to save";
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                            fileToOpen = saveFileDialog1.FileName.ToString();
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                writeFile();
                            }
                        }
                        break;
                    case DialogResult.No:
                        Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        //do nothing
                        break;
                    default:

                        break;

                }
            }
            else
            {
                Application.Exit();
            }
            
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = userDocumentsDirectory;
            openFileDialog1.Title = "Select text file";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileToOpen = openFileDialog1.FileName.ToString();
                isFirstSave = false;
                readFile();
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = userDocumentsDirectory;
            saveFileDialog1.Title = "Select file to save";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fileToOpen = saveFileDialog1.FileName.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                writeFile();
            }

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstSave = true;
            textBox1.ResetText();
            textHasChanged = false;
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFirstSave == true)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = fileToOpen;
                saveFileDialog1.Title = "Select file to save";
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileToOpen = saveFileDialog1.FileName.ToString();
                    writeFile();
                }
            }
            else
            {
                writeFile();
            }
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control ctrl = this.ActiveControl;
            if (ctrl != null)
            {
                string copied = "";
                int sPos;
                if (ctrl is TextBox)
                {
                    TextBox tx = (TextBox)ctrl;
                    tx.Cut();
                }
            } 
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control ctrl = this.ActiveControl;
            if (ctrl != null)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = (TextBox)ctrl;
                    tx.Copy();
                }
            }
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control ctrl = this.ActiveControl;
            if (ctrl != null)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = (TextBox)ctrl;
                    tx.Paste();
                }
            }
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textHasChanged = true;

        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                textBox1.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
            }
            else if (wordWrapToolStripMenuItem.Checked == false)
            {
                textBox1.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
            }
        }

        ///<summary>
        ///All the user made methods.
        ///</summary>
        
        public void readFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileToOpen))
                {
                    textBox1.Text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file could not be read:\n" + e.Message);
            }
        }

        public void writeFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileToOpen))
                {
                    sw.WriteLine(textBox1.Text.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file could not be saved:\n" + e.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (textHasChanged == true)
            {
                var result = MessageBox.Show("You have unsaved changes, do you wish to save?", "Warning!", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.Yes:
                        if (isFirstSave == false)
                        {
                            writeFile();

                        }
                        else if (isFirstSave == true)
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.InitialDirectory = fileToOpen;
                            saveFileDialog1.Title = "Select file to save";
                            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                            fileToOpen = saveFileDialog1.FileName.ToString();
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                writeFile();
                            }
                        }
                        break;
                    case DialogResult.No:
                        Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:

                        break;

                }
            }
            else
            {
                Application.Exit();
            }

        }

       

        

        
        

           

        
        

        
        

    }
}
