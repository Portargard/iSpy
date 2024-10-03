using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
namespace iSpyApplication.Controls
{
    public partial class Filter : Form
    {
        public static List<int> CheckedCameraIDs = new List<int>();
        public static List<int> CheckedMicIDs = new List<int>();
        public static DateTime StartDate = Helper.Now.AddDays(-7);
        public static DateTime EndDate = Helper.Now;
        public static bool Filtered = false;
        public Filter()
        {
            InitializeComponent();
            RenderResources();
            dateTimePicker1.CustomFormat = "dd MMM yyyy";
            dateTimePicker2.CustomFormat = "dd MMM yyyy";
        }
        private void RenderResources()
        {
            Text = LocRm.GetString("Filter");
            button1.Text = LocRm.GetString("OK");
            label1.Text = LocRm.GetString("Objects");
            label2.Text = LocRm.GetString("From");
            label3.Text = LocRm.GetString("To");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckedCameraIDs = new List<int>();
            CheckedMicIDs = new List<int>();
            for (int i = 0; i < clbObjects.Items.Count; i++)
            {
                var o = (Li)clbObjects.Items[i];
                if (clbObjects.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (o.Ot == 1)
                    {
                        CheckedMicIDs.Add(o.ID);
                    }
                    if (o.Ot == 2)
                    {
                        CheckedCameraIDs.Add(o.ID);
                    }
                }
            }
            StartDate = dateTimePicker1.Value.Date;
            EndDate = dateTimePicker2.Value.Date;
            if (StartDate > EndDate)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Filtered = chkFilter.Checked;
            SearchVideos(StartDate,EndDate);
            DialogResult = DialogResult.OK;
        }
        private void SearchVideos(DateTime startDate,DateTime endDate)
        {
            DateTime x = DateTime.Now;
            var y = x.Year;
            string rootDirectory = Path.Combine(Program.AppDataPath + @"WebServerRoot\Media\", "video");
            List<string> foundVideos = new List<string>();
            string[] years = Directory.GetDirectories(rootDirectory);
            // Duyệt qua thư mục theo Year/Month/Day/CameraName
            foreach (var yearDir in Directory.GetDirectories(rootDirectory))
            {
                var yearFolder = Path.GetFileName(yearDir);
                if (int.TryParse(yearFolder, out int year) && year >= startDate.Year && year <= endDate.Year)
                {
                    foreach (var monthDir in Directory.GetDirectories(yearDir))
                    {
                        var monthFolder = Path.GetFileName(monthDir);
                        if (int.TryParse(monthFolder, out int month) && month >= 1 && month <= 12)
                        {
                            foreach (var dayDir in Directory.GetDirectories(monthDir))
                            {
                                var dayFolder = Path.GetFileName(dayDir);
                                if (int.TryParse(dayFolder, out int day) && day >= 1 && day <= 31)
                                {
                                    // Tạo đối tượng DateTime từ thư mục Year/Month/Day
                                    DateTime currentDate = new DateTime(year, month, day);

                                    // Kiểm tra ngày có nằm trong khoảng thời gian không
                                    if (currentDate >= startDate && currentDate <= endDate)
                                    {
                                       
                                        foreach (var cameraDir in Directory.GetDirectories(dayDir))
                                        {
                                            var z = Directory.GetFiles(cameraDir);
                                            foreach (var videoFile in Directory.GetFiles(cameraDir))
                                            {
                                                foundVideos.Add(videoFile);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Hiển thị kết quả tìm kiếm
            if (foundVideos.Count > 0)
            {
                MessageBox.Show($"Tìm thấy {foundVideos.Count} video:\n{string.Join("\n", foundVideos)}", "Kết quả tìm kiếm");
            }
            else
            {
                MessageBox.Show("Không tìm thấy video nào phù hợp.", "Kết quả tìm kiếm");
            }
        }
        private void Filter_Load(object sender, EventArgs e)
        {
            foreach (var c in MainForm.Cameras)
            {
                var l = new Li { Name = c.name, Ot = 2, Selected = false, ID = c.id };
                if (CheckedCameraIDs.Contains(c.id))
                    l.Selected = true;
                clbObjects.Items.Add(l);
                clbObjects.SetItemCheckState(clbObjects.Items.Count - 1, l.Selected ? CheckState.Checked : CheckState.Unchecked);
            }
            foreach (var c in MainForm.Microphones)
            {
                var l = new Li { Name = c.name, Ot = 1, Selected = false, ID = c.id };
                if (CheckedMicIDs.Contains(c.id))
                    l.Selected = true;
                clbObjects.Items.Add(l);
                clbObjects.SetItemCheckState(clbObjects.Items.Count - 1, l.Selected ? CheckState.Checked : CheckState.Unchecked);
            }
            dateTimePicker1.Value = StartDate;
            dateTimePicker2.Value = EndDate;
            chkFilter.Checked = Filtered;
            tlpFilter.Enabled = chkFilter.Checked;
        }
        private struct Li
        {
            public string Name;
            public int Ot;
            public int ID;
            public bool Selected;
            public override string ToString()
            {
                return Name;
            }
        }
        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            tlpFilter.Enabled = chkFilter.Checked;
        }
    }
}