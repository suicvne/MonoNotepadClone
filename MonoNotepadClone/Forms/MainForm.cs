using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ini;
using MonoNotepadClone.Forms;

namespace MonoNotepadClone
{
    public partial class MainForm : Form
    {
        IniFile ini = new IniFile(Application.StartupPath + "\\settings.ini");
        
        public bool isFirstSave = true;
        public bool textHasChanged = false;
        public bool wordWrapIsChecked = true;
        public string fileToOpen = "";
        public string fileToSave = "";

        static Disarm disarm = new Disarm();
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
            MainForm main = new MainForm();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = userDocumentsDirectory;
            openFileDialog1.Title = "Select text file";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = openFileDialog1.FileName.ToString();
                fileToOpen = openFileDialog1.FileName.ToString();
                readFile();
                isFirstSave = false;
                
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = userDocumentsDirectory;
            saveFileDialog1.Title = "Select file to save";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName.ToString()))
                    {
                        sw.WriteLine(textBox1.Text.ToString());
                    }
                }
                catch (Exception p)
                {
                    MessageBox.Show("The file could not be saved:\n" + p.Message);
                }
            }
            fileToOpen = saveFileDialog1.FileName.ToString();

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFirstSave = true;
            textBox1.ResetText();
            textHasChanged = false;
            toolStripStatusLabel1.Text = "New File";
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
            if (wordWrapIsChecked == true)
            {
                textBox1.WordWrap = true;
                //wordWrapToolStripMenuItem.Checked = false;
            }
            else if (wordWrapIsChecked == false)
            {
                textBox1.WordWrap = false;
                //wordWrapToolStripMenuItem.Checked = true;
            }
        }

        private void wordWrapToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            var isChecked = wordWrapToolStripMenuItem.CheckState;

            switch (isChecked)
            {
                case(CheckState.Checked):
                    wordWrapIsChecked = true;
                    break;
                case(CheckState.Unchecked):
                    wordWrapIsChecked = false;
                    break;

            }
                        
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control ctrl = this.ActiveControl;
            if (ctrl != null)
            {
                if (ctrl is TextBox)
                {
                    TextBox tx = (TextBox)ctrl;
                    tx.SelectAll();
                    
                }
            }
        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text.Contains("Killswitch Engage"))
            {
                disarm.ShowDialog();
            }
            else if (textBox1.Text.Contains("kse"))
            {
                disarm.ShowDialog();
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
            //if (e.CloseReason == CloseReason.WindowsShutDown) return;

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
                        //Application will exit
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
