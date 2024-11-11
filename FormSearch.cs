using iSpyApplication.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace iSpyApplication
{
    public partial class FormSearch : Form
    {
        private List<string> _videos;
        string videoDirectory;
        private Color defaultRowColor;
        public FormSearch(List<string> videos)
        {
            InitializeComponent();

            dgv_Videos.Dock = DockStyle.Fill;
            defaultRowColor = dgv_Videos.DefaultCellStyle.BackColor;


            videoDirectory = Program.AppDataPath + @"WebServerRoot\Media\";
            this.Text = "Search Video";
            _videos = videos;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Cập nhật kích thước của DataGridView
            dgv_Videos.Width = this.ClientSize.Width;
            dgv_Videos.Height = this.ClientSize.Height;
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            LoadMarkedVideosFromFile(); // Nạp danh sách video đã đánh dấu từ file JSON
            LoadVideosToDataGridView(); // Hiển thị danh sách video trên DataGridView
                                        // Kiểm tra và xóa các video không được đánh dấu nếu quá 30 ngày
            DeleteOldUnmarkedVideos();
            if (_videos.Count == 0)
            {
                MessageBox.Show("Không có video nào phù hợp với kết quả tìm kiếm.");
            }
        }

        private void LoadVideosToDataGridView()
        {
            dgv_Videos.Columns.Clear();

            dgv_Videos.Columns.Add("STT", "STT");
            dgv_Videos.Columns.Add("FileName", "Tên Video");
            dgv_Videos.Columns.Add("FilePath", "Đường Dẫn Video");
            dgv_Videos.Columns.Add("Date", "Ngày Tháng Năm");
            dgv_Videos.Columns.Add("CameraName", "Tên Camera");

            dgv_Videos.Columns["STT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_Videos.Columns["FileName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_Videos.Columns["FilePath"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_Videos.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_Videos.Columns["CameraName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv_Videos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Videos.Rows.Clear();

            for (int i = 0; i < _videos.Count; i++)
            {
                string video = _videos[i];
                string fileName = Path.GetFileNameWithoutExtension(video);
                DateTime creationDate = File.GetCreationTime(video);
                string date = creationDate.ToString("dd/MM/yyyy");
                string cameraName = GetCameraNameFromVideo(video);

                int rowIndex = dgv_Videos.Rows.Add(i + 1, fileName, video, date, cameraName);

               
                if (GlobalMarkedVideos.MarkedVideos.Contains(video))
                {
                    dgv_Videos.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Yellow; // Màu đánh dấu
                }
            }
        }



        private string GetCameraNameFromVideo(string videoPath)
        {
            string cameraDirectory = Path.GetDirectoryName(videoPath);
            string cameraName = Path.GetFileName(cameraDirectory);
            return cameraName;
        }


        private void btnPlayVideo_Click_1(object sender, EventArgs e)
        {
            if (dgv_Videos.SelectedRows.Count > 0)
            {
                string videoPath = dgv_Videos.SelectedRows[0].Cells["FilePath"].Value.ToString();
                PreviewBox playvid = new PreviewBox();
                playvid.FileName = videoPath;
                playvid.PlayMedia(Enums.PlaybackMode.Default);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một video để phát.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgv_Videos_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        { 
            if (e.RowIndex >= 0)
            {
                string videoPath = dgv_Videos.Rows[e.RowIndex].Cells["FilePath"].Value.ToString();
                PreviewBox playvid = new PreviewBox();
                playvid.FileName = videoPath;
                playvid.PlayMedia(Enums.PlaybackMode.Default);
            }

        }

        private void btnDeleteVideo_Click(object sender, EventArgs e)
        {
            if (dgv_Videos.SelectedRows.Count > 0)
            {
               
                string videoPath = dgv_Videos.SelectedRows[0].Cells["FilePath"].Value.ToString();

                // Kiểm tra nếu video đã được đánh dấu
                bool isMarked = GlobalMarkedVideos.MarkedVideos.Contains(videoPath);

                string message = isMarked
                    ? "Video này đã được đánh dấu không xóa. Bạn có chắc chắn muốn xóa video này không?"
                    : "Bạn có chắc chắn muốn xóa video này không?";

              
                var confirmResult = MessageBox.Show(message,
                                                    "Xác Nhận Xóa",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                       
                        File.Delete(videoPath);

                     
                        dgv_Videos.Rows.RemoveAt(dgv_Videos.SelectedRows[0].Index);

                       
                        if (isMarked)
                        {
                            GlobalMarkedVideos.MarkedVideos.Remove(videoPath);
                        }

                        MessageBox.Show("Video đã được xóa thành công.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa video: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một video để xóa.");
            }
        }

        private Dictionary<string, bool> markedVideos = new Dictionary<string, bool>();
        public static class GlobalMarkedVideos
        {
            public static HashSet<string> MarkedVideos = new HashSet<string>();
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            if (dgv_Videos.SelectedRows.Count > 0)
            {
                string videoPath = dgv_Videos.SelectedRows[0].Cells["FilePath"].Value.ToString();
                if (!GlobalMarkedVideos.MarkedVideos.Contains(videoPath))
                {
                    GlobalMarkedVideos.MarkedVideos.Add(videoPath);
                    dgv_Videos.SelectedRows[0].DefaultCellStyle.BackColor = Color.LightCoral;
                    MessageBox.Show("Video đã được đánh dấu không xóa.");
                    SaveMarkedVideosToFile(); // Lưu trạng thái vào file
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một video để đánh dấu.");
            }
        }

        private void btnUnmark_Click(object sender, EventArgs e)
        {
            if (dgv_Videos.SelectedRows.Count > 0)
            {
                string videoPath = dgv_Videos.SelectedRows[0].Cells["FilePath"].Value.ToString();
                if (GlobalMarkedVideos.MarkedVideos.Contains(videoPath))
                {
                    GlobalMarkedVideos.MarkedVideos.Remove(videoPath);
                    dgv_Videos.SelectedRows[0].DefaultCellStyle.BackColor = Color.White;
                    MessageBox.Show("Video đã được bỏ đánh dấu.");
                    SaveMarkedVideosToFile(); // Lưu trạng thái vào file
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một video để bỏ đánh dấu.");
            }
        }
        private void SaveMarkedVideosToFile()
        {
            string filePath = Path.Combine(Program.AppDataPath, "marked_videos.json");
            string json = JsonConvert.SerializeObject(GlobalMarkedVideos.MarkedVideos, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        private void LoadMarkedVideosFromFile()
        {
            string filePath = Path.Combine(Program.AppDataPath, "marked_videos.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var markedVideos = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                GlobalMarkedVideos.MarkedVideos = new HashSet<string>(markedVideos);

            }
        }
        private void DeleteOldUnmarkedVideos()
        {
          
            foreach (var video in _videos)
            {
          
                if (!markedVideos.ContainsKey(video))
                {
                   
                    DateTime creationDate = File.GetCreationTime(video);
                   
                    if ((DateTime.Now - creationDate).TotalDays > 30)
                    {
                        try
                        {
                            // Xóa video
                            File.Delete(video);
                            _videos.Remove(video);
                            Console.WriteLine($"Video {video} đã bị xóa do quá 30 ngày.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi xóa video {video}: {ex.Message}");
                        }
                    }
                }
            }

           
            LoadVideosToDataGridView();
        }

        private void dgv_Videos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Kiểm tra xem cột được nhấn có phải là cột chứa đường dẫn video không
                if (dgv_Videos.Columns[e.ColumnIndex].Name == "FilePath")
                {
                    // Lấy đường dẫn video từ ô được nhấn
                    string videoPath = dgv_Videos.Rows[e.RowIndex].Cells["FilePath"].Value.ToString();

                    try
                    {
                        // Lấy thư mục chứa video
                        string folderPath = Path.GetDirectoryName(videoPath);

                        // Mở thư mục chứa video và chọn file video
                        Process.Start("explorer.exe", $"/select,\"{videoPath}\"");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi mở thư mục chứa video: {ex.Message}");
                    }
                }
            }
        }
  






    }
}
