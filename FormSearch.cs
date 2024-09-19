using iSpyApplication.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace iSpyApplication
{
    public partial class FormSearch : Form
    {
        string videoDirectory;
        public FormSearch()
        {
            InitializeComponent();
            videoDirectory = Program.AppDataPath + @"WebServerRoot\Media\Video\CMEHX";
            this.Text = "Search Video";
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            LoadAllVideos();
        }

      

        private void LoadAllVideos()
        {
            // Đường dẫn tới thư mục lưu video

            // Lấy tất cả các tệp video trong thư mục (có thể lọc theo định dạng tệp, ví dụ: .mp4, .avi, ...)
            string[] videoFiles = Directory.GetFiles(videoDirectory, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".mp4") || file.ToLower().EndsWith(".avi")).ToArray();

            // Làm trống ListBox trước khi hiển thị danh sách video
            lstVideos.Items.Clear();

            // Thêm tất cả các tệp video vào ListBox
            foreach (string file in videoFiles)
            {
                lstVideos.Items.Add(file);
            }
        }


        private void lstVideos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Đường dẫn tới video
            string videoPath = lstVideos.SelectedItem.ToString();
            // Đường dẫn tới trình phát video (ví dụ VLC hoặc Windows Media Player)
            PreviewBox playvid = new PreviewBox();
            playvid.FileName = videoPath;
            playvid.PlayMedia(Enums.PlaybackMode.Default);

      
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Đường dẫn tới thư mục lưu video
            // Từ khóa tìm kiếm từ TextBox
            string searchKeyword = txtSearch.Text.ToLower();

            // Ngày tìm kiếm từ DateTimePicker
            DateTime searchDate = dateTimePicker1.Value.Date;

            // Lấy tất cả các tệp video trong thư mục (có thể lọc theo định dạng tệp, ví dụ: .mp4, .avi, ...)
            string[] videoFiles = Directory.GetFiles(videoDirectory, "*.*", SearchOption.AllDirectories)
                .Where(file => file.ToLower().EndsWith(".mp4") || file.ToLower().EndsWith(".avi")).ToArray();

            // Làm trống ListBox trước khi hiển thị kết quả tìm kiếm mới
            lstVideos.Items.Clear();

            // Tìm kiếm các tệp có tên chứa từ khóa và ngày tạo/tệp
            foreach (string file in videoFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file).ToLower();
                DateTime fileDate = File.GetCreationTime(file).Date;

                // Kiểm tra nếu tên tệp chứa từ khóa và ngày tạo/tệp khớp với ngày tìm kiếm
                if (fileName.Contains(searchKeyword) && fileDate == searchDate)
                {
                    lstVideos.Items.Add(file);
                }
            }

            // Kiểm tra nếu không có kết quả tìm kiếm nào
            if (lstVideos.Items.Count == 0)
            {
                MessageBox.Show("Không có video nào phù hợp với kết quả tìm kiếm.");
            }
        }

    }
}
