using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iSpyApplication
{
    public partial class AlertForm : Form
    {
        private SoundPlayer music = new SoundPlayer();
        public AlertForm(string camName)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            music.SoundLocation = "C:\\Program Files\\iSpy\\Sounds\\emergency-alarm-69780.wav";
            this.Text = "Alert: "+ camName;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AlertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            music.Stop();
            music.Dispose();
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            music.PlayLooping();
        }
    }
}
