using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using iSpyApplication.Controls;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace iSpyApplication
{
    public partial class HumanDetect : Form
    {
        CameraWindow CameraWindow;
        private Mat _frame;
        private Net _net;
        private string[] _classLabels;
        private Timer _timer;

        public HumanDetect(CameraWindow cameraWindow)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            CameraWindow = cameraWindow;
            _frame = new Mat();
            // Tải mô hình YOLO
            _net = DnnInvoke.ReadNetFromDarknet(Program.AppDataPath+@"HumanDetect\yolov3.cfg", Program.AppDataPath + @"HumanDetect\yolov3.weights");
            _classLabels = System.IO.File.ReadAllLines(Program.AppDataPath + @"HumanDetect\coco.names"); // File chứa tên các lớp (person, car, dog,...)
            if (CudaInvoke.HasCuda)
            {
                MessageBox.Show("CUDA khả dụng.");
                // Tải mô hình YOLO sử dụng backend CUDA
                _net.SetPreferableBackend(Emgu.CV.Dnn.Backend.Cuda);
                _net.SetPreferableTarget(Emgu.CV.Dnn.Target.Cuda);
            }
            else
            {
                MessageBox.Show("CUDA không khả dụng. Chương trình sẽ chạy trên CPU.");
            }
            _timer = new Timer();
            _timer.Tick += ProcessFrame;
            _timer.Start();
        }
        private void ProcessFrame(object sender, EventArgs e)
        {
            Image<Bgr, byte> img = CameraWindow.LastFrame.ToImage<Bgr, byte>();
            _frame = img.Mat;
            if (!_frame.IsEmpty)
            {
                var blob = DnnInvoke.BlobFromImage(_frame, 0.00392, new Size(320, 320), new MCvScalar(0, 0, 0), true, false);
                _net.SetInput(blob);

                var outputLayerNames = _net.UnconnectedOutLayersNames;
                using (VectorOfMat outputLayers = new VectorOfMat())
                {
                    _net.Forward(outputLayers, outputLayerNames);

                    List<Rectangle> boxes = new List<Rectangle>();
                    List<float> confidences = new List<float>();
                    List<string> classLabelsDetected = new List<string>();
                    for (int i = 0; i < outputLayers.Size; i++)
                    {
                        Mat layer = outputLayers[i];
                        float[] layerData = new float[layer.Total.ToInt32()];
                        layer.CopyTo(layerData);

                        for (int j = 0; j < layerData.Length; j += 85)
                        {
                            float confidence = layerData[j + 4];
                            int classId = GetClassId(layerData, j);
                            if (classId > _classLabels.Length || classId == -1)
                            {
                                continue;
                            }
                            if (confidence > 0.5 && _classLabels[GetClassId(layerData, j)] == "person")
                            {
                                string label = _classLabels[classId];

                                float centerX = layerData[j] * _frame.Width;
                                float centerY = layerData[j + 1] * _frame.Height;
                                float boxWidth = layerData[j + 2] * _frame.Width;
                                float boxHeight = layerData[j + 3] * _frame.Height;

                                int x = (int)(centerX - boxWidth / 2);
                                int y = (int)(centerY - boxHeight / 2);
                                int width = (int)boxWidth;
                                int height = (int)boxHeight;

                                // Lưu thông tin bounding box và độ tin cậy
                                boxes.Add(new Rectangle(x, y, width, height));
                                confidences.Add(confidence);
                                classLabelsDetected.Add(label);
                            }
                        }
                    }

                    // Thực hiện Non-Maximum Suppression
                    int[] indices = DnnInvoke.NMSBoxes(boxes.ToArray(), confidences.ToArray(), 0.5f, 0.4f); // Ngưỡng cho độ tin cậy và NMS
                    foreach (var index in indices)
                    {
                        int i = index;
                        DrawLabel(_frame, classLabelsDetected[i], boxes[i].X, boxes[i].Y, boxes[i].Width, boxes[i].Height);
                    }

                }

                pictureBox1.Image = _frame.ToBitmap();
            }
        }
        private int GetClassId(float[] layerData, int offset)
        {
            // Tính toán class id từ 80 class scores
            int classId = -1;
            float maxScore = 0.0f;

            for (int i = 5; i < 85; i++)
            {
                if (layerData[offset + i] > maxScore)
                {
                    maxScore = layerData[offset + i];
                    classId = i - 5; // Lớp bắt đầu từ vị trí 5
                }
            }

            return classId;
        }

        private void DrawLabel(Mat image, string label, int x, int y, int width, int height)
        {
            //// Vẽ bounding box
            //CvInvoke.Rectangle(image, new Rectangle(x, y, width, height), new MCvScalar(0, 255, 0), 2);

            //// Vẽ nhãn
            //CvInvoke.PutText(image, label, new Point(x, y - 5), FontFace.HersheySimplex, 0.5, new MCvScalar(0, 0, 255), 2);



            int rectangleThickness;
            float fontSize;
            // Tính toán độ dày cho hình chữ nhật (3% chiều cao hình ảnh)
            if (width > height)
            {
                rectangleThickness = (int)(image.Height * 0.01);
                // Tính toán kích thước chữ là 10% chiều rộng hình ảnh
                fontSize = image.Height * 0.001f; // Kích thước chữ là 10% chiều rộng hình ảnh
            }
            else
            {
                rectangleThickness = (int)(image.Width * 0.01);
                // Tính toán kích thước chữ là 10% chiều rộng hình ảnh
                fontSize = image.Width * 0.001f; // Kích thước chữ là 10% chiều rộng hình ảnh
            }

            int textHeight = 0; // Khởi tạo biến chiều cao
            Size textSize = CvInvoke.GetTextSize(label, FontFace.HersheySimplex, fontSize, 2, ref textHeight); // Tính toán kích thước chữ
            // Vẽ bounding box với độ dày đã tính
            CvInvoke.Rectangle(image, new Rectangle(x, y, width, height), new MCvScalar(0, 255, 0), rectangleThickness);
            // Vẽ nhãn lên hình ảnh bằng màu đỏ
            CvInvoke.PutText(image, label, new Point(x, y - 5), FontFace.HersheySimplex, fontSize, new MCvScalar(255, 0, 0), 2); // Màu đỏ, độ dày 2

        }

        private void HumanDetect_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frame.Dispose();
            _timer.Dispose();
        }
    }
}
