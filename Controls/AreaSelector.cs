using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace iSpyApplication.Controls
{
    public sealed partial class AreaSelector : Panel
    {
        private Point _rectStart = Point.Empty;
        private Point _rectStop = Point.Empty;
        private Point _hoverPoint = Point.Empty;

        private bool _bMouseDown;
        private List<Rectangle> _motionZonesRectangles = new List<Rectangle>();

        private List<Point> _polygonPoints = new List<Point>();
        private Point _draggedPoint = Point.Empty;
        private bool _isMouseDown;
        private int _draggedPointIndex = -1;

        private Bitmap _lastFrame;
        private readonly object _lockobject = new object();
        public Bitmap LastFrame
        {
            get
            {
                lock (_lockobject)
                {
                    if (_lastFrame == null)
                    {
                        return null;
                    }
                    return (Bitmap)_lastFrame.Clone();
                }
            }
            set
            {
                lock (_lockobject)
                {
                    if (_lastFrame != null)
                        _lastFrame.Dispose();
                    if (value != null)
                        _lastFrame = (Bitmap)value.Clone();
                    else
                    {
                        _lastFrame = null;
                    }
                }
                Invalidate();
            }
        }

        public void ClearRectangles()
        {
            _motionZonesRectangles = new List<Rectangle>();
            _polygonPoints.Clear();
            Invalidate();
        }

        private int _rectIndex = -1;
        private Rectangle _rectOrig;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isMouseDown = true;

            // Kiểm tra xem người dùng có nhấp vào một đỉnh của đa giác hay không
            for (int i = 0; i < _polygonPoints.Count; i++)
            {
                if (CalcDist(_polygonPoints[i], e.Location) < 10)
                {
                    _draggedPoint = _polygonPoints[i];
                    _draggedPointIndex = i;
                    return;
                }
            }

            // Nếu không nhấp vào đỉnh nào, thêm điểm mới
            _polygonPoints.Add(e.Location);
            Invalidate();

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isMouseDown = false;
            _draggedPoint = Point.Empty;
            _draggedPointIndex = -1;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Nếu đang kéo thả một đỉnh, cập nhật vị trí của nó
            if (_isMouseDown && _draggedPointIndex >= 0)
            {
                _polygonPoints[_draggedPointIndex] = e.Location;
                Invalidate();
            }
        }

        internal Rectangle NormRect(Point p1, Point p2)
        {
            int x = p1.X, y = p1.Y, w, h;
            w = Math.Abs(p1.X - p2.X);
            h = Math.Abs(p1.Y - p2.Y);

            if (p2.X < p1.X)
                x = p2.X;
            if (p2.Y < p1.Y)
                y = p2.Y;
            return new Rectangle(x, y, w, h);
        }

        private double CalcDist(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _bMouseDown = false;
        }

        public AreaSelector()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
            Margin = new Padding(0, 0, 0, 0);
            Padding = new Padding(0, 0, 3, 3);
            _motionZonesRectangles = new List<Rectangle>();
            BackgroundImageLayout = ImageLayout.Stretch;
        }
        public objectsCameraDetectorZone[] MotionZones
        {
            get
            {
                var ocdzs = new List<objectsCameraDetectorZone>();
                for (int index = 0; index < _motionZonesRectangles.Count; index++)
                {
                    Rectangle r = _motionZonesRectangles[index];
                    var ocdz = new objectsCameraDetectorZone
                                   {
                                       left = r.Left,
                                       top = r.Top,
                                       width = r.Width,
                                       height = r.Height
                                   };
                    ocdzs.Add(ocdz);
                }
                return ocdzs.ToArray();
            }
            set
            {
                _motionZonesRectangles = new List<Rectangle>();
                if (value == null) return;
                foreach (var r in value)
                    _motionZonesRectangles.Add(new Rectangle(r.left, r.top, r.width, r.height));
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            var g = pe.Graphics;
            var brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            var pen = new Pen(Color.DarkGray);

            var bmp = LastFrame;
            if (bmp != null)
                g.DrawImage(_lastFrame, 0, 0, Width, Height);

            // Vẽ các đỉnh và đường của đa giác
            if (_polygonPoints.Count > 1)
            {
                g.FillPolygon(brush, _polygonPoints.ToArray());
                g.DrawPolygon(pen, _polygonPoints.ToArray());
            }

            // Vẽ các đỉnh
            foreach (var point in _polygonPoints)
            {
                g.FillEllipse(Brushes.DeepSkyBlue, point.X - 5, point.Y - 5, 10, 10);
            }

            pen.Dispose();
            brush.Dispose();
        }
        public event EventHandler BoundsChanged;
        public event EventHandler PolygonChanged;
        private void OnBoundsChanged()
        {
            if (BoundsChanged!=null)
                BoundsChanged(this, EventArgs.Empty);
            PolygonChanged?.Invoke(this, EventArgs.Empty);
        }
        // Thuộc tính để lấy đa giác dưới dạng danh sách các điểm
        public List<Point> PolygonPoints
        {
            get => _polygonPoints;
            set
            {
                _polygonPoints = value ?? new List<Point>();
                Invalidate();
            }
        }
        public string LstToString()
        {
            List<string> pointStrings = new List<string>();
            foreach (var point in _polygonPoints)
            {
                pointStrings.Add($"{{X={point.X},Y={point.Y}}}");
            }
            return string.Join(", ", pointStrings);
        }
        public void ConvertToPoint(string data)
        {
            List<Point> points = new List<Point>();

            string[] pointStrings = data.Split(new[] { "}, {" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var pointString in pointStrings)
            {
                // Loại bỏ các ký tự không cần thiết
                string cleanString = pointString.Trim('{', '}', ' ');

                // Tách các phần X và Y
                string[] coordinates = cleanString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int x = int.Parse(coordinates[0].Split('=')[1]);
                int y = int.Parse(coordinates[1].Split('=')[1]);

                // Thêm vào danh sách
                points.Add(new Point(x, y));
            }
            _polygonPoints = points;
        }
    }
}