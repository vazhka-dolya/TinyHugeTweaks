using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinyHugeTweaks
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            this.KeyPreview = true;
            this.KeyDown += mainForm_KeyDown;
            this.KeyUp += mainForm_KeyUp;

            InitializeComponent();
            lbVer.Text = $"{ProductVersion}";
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                pictureIcon.BackgroundImage = Properties.Resources.liquoricepie_icon;
            }
        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                pictureIcon.BackgroundImage = Properties.Resources.tbk_icon_m;
            }

        }
    }
}
