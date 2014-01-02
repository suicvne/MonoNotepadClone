using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonoNotepadClone.Forms
{
    public partial class Disarm : Form
    {
        public Disarm()
        {
            InitializeComponent();
        }

        private void Disarm_Load(object sender, EventArgs e)
        {
            
        }

        private void Disarm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DONT FORGET KSE RULEZ D00D");
            this.Close();
        }
    }
}
