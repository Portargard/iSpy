using iSpyApplication.OnvifServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public ObservableCollection<AlertInfor> _lstCamAlert = new ObservableCollection<AlertInfor>();
        public AlertForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            music.SoundLocation = "C:\\Program Files\\iSpy\\Sounds\\emergency-alarm-69780.wav";
            _lstCamAlert.CollectionChanged += ListChange;
            this.Text = "Alert";
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AlertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            music.Stop();
            music.Dispose();
            this.Dispose();
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            music.PlayLooping();
        }
        private void ListChange(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Camera Alert", typeof(string));
                dt.Columns.Add("Date Time Alert", typeof(System.DateTime));
                _lstCamAlert.OrderByDescending(c => c.DateAlert);
                foreach (var item in _lstCamAlert)
                {
                    dt.Rows.Add(item.CameraAlert, item.DateAlert);
                }
                dgv_CamAlertInfor.DataSource = dt;
            }
        }

        private void dgv_CamAlertInfor_DoubleClick(object sender, EventArgs e)
        {
            var selectedIndex = dgv_CamAlertInfor.SelectedRows[0];
            _lstCamAlert.RemoveAt(selectedIndex.Index);
            dgv_CamAlertInfor.Rows.Remove(selectedIndex);           
        }
    }
    public class AlertInfor
    {
        public string CameraAlert {  get; set; }
        public System.DateTime DateAlert {  get; set; }
    }
}
