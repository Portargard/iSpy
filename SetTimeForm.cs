using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iSpyApplication
{
    public partial class SetTimeForm : Form
    {
        public double DelayTime;
        public SetTimeForm()
        {
            InitializeComponent();
            nud_TimeDelay.Maximum = decimal.MaxValue;
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            DelayTime = (double)nud_TimeDelay.Value;
            this.Close();
        }
    }
}
