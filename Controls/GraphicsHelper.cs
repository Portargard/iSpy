using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using AForge.Imaging;
using OpenCL.Net;
using OpenClProgram = OpenCL.Net.Program;

namespace iSpyApplication.Controls
{
    public static class GraphicsHelper
    {
        private static Context _context;
        private static Device _device;
        private static CommandQueue _commandQueue;

        static GraphicsHelper()
        {
            // Khởi tạo OpenCL
            ErrorCode error;
            Platform platform = Cl.GetPlatformIDs(out error)[0];
            _device = Cl.GetDeviceIDs(platform, DeviceType.Gpu, out error)[0];
            _context = Cl.CreateContext(null, 1, new[] { _device }, null, IntPtr.Zero, out error);
            _commandQueue = Cl.CreateCommandQueue(_context, _device, CommandQueueProperties.None, out error);
        }

        public static bool UseManaged = true;

        public static void GdiDrawImage(this Graphics graphics, UnmanagedImage image, int x, int y, int w, int h)
        {
            // Thực hiện chuyển đổi từ GDI sang OpenCL
            ExecuteOpenCLDraw(graphics, image, new Rectangle(x, y, w, h));
        }

        public static void GdiDrawImage(this Graphics graphics, Bitmap image, Rectangle r)
        {
            if (UseManaged)
            {
                graphics.DrawImage(image, r);
                return;
            }

            // Chuyển đổi từ GDI sang OpenCL để vẽ ảnh
            ExecuteOpenCLDraw(graphics, image, r);
        }

        public static void GdiDrawImage(this Graphics graphics, UnmanagedImage image, Rectangle r)
        {
            ExecuteOpenCLDraw(graphics, image, r);
        }

        private static void ExecuteOpenCLDraw(Graphics graphics, UnmanagedImage image, Rectangle r)
        {
            IntPtr imgPtr = image.ImageData;
            int width = image.Width;
            int height = image.Height;

            ErrorCode error;
            IMem imgBuffer = Cl.CreateBuffer(_context, MemFlags.ReadOnly | MemFlags.CopyHostPtr, (IntPtr)(width * height * 4), imgPtr, out error);
            IMem outputBuffer = Cl.CreateBuffer(_context, MemFlags.WriteOnly, (IntPtr)(r.Width * r.Height * 4), IntPtr.Zero, out error);

            string kernelSource = @"
            __kernel void StretchBltKernel(__global uchar* src, __global uchar* dst, int srcWidth, int srcHeight, int dstWidth, int dstHeight) {
                int x = get_global_id(0);
                int y = get_global_id(1);

                int srcX = (x * srcWidth) / dstWidth;
                int srcY = (y * srcHeight) / dstHeight;

                int srcIndex = (srcY * srcWidth + srcX) * 4;
                int dstIndex = (y * dstWidth + x) * 4;

                dst[dstIndex] = src[srcIndex];
                dst[dstIndex + 1] = src[srcIndex + 1];
                dst[dstIndex + 2] = src[srcIndex + 2];
                dst[dstIndex + 3] = src[srcIndex + 3];
            }";

            OpenClProgram program = Cl.CreateProgramWithSource(_context, 1, new[] { kernelSource }, null, out error);
            Cl.BuildProgram(program, 1, new[] { _device }, string.Empty, null, IntPtr.Zero);

            Kernel kernel = Cl.CreateKernel(program, "StretchBltKernel", out error);

            Cl.SetKernelArg(kernel, 0, imgBuffer);
            Cl.SetKernelArg(kernel, 1, outputBuffer);
            Cl.SetKernelArg(kernel, 2, width);
            Cl.SetKernelArg(kernel, 3, height);
            Cl.SetKernelArg(kernel, 4, r.Width);
            Cl.SetKernelArg(kernel, 5, r.Height);

            IntPtr[] globalWorkSize = new IntPtr[] { (IntPtr)r.Width, (IntPtr)r.Height };
            Cl.EnqueueNDRangeKernel(_commandQueue, kernel, 2, null, globalWorkSize, null, 0, null, out _);

            Bitmap bitmap = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, r.Width, r.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            Cl.EnqueueReadBuffer(_commandQueue, outputBuffer, Bool.True, IntPtr.Zero, new IntPtr(r.Width * r.Height * 4), bitmapData.Scan0, 0, null, out _);
            bitmap.UnlockBits(bitmapData);

            graphics.DrawImage(bitmap, r);

            Cl.ReleaseKernel(kernel);
            Cl.ReleaseProgram(program);
            Cl.ReleaseMemObject(imgBuffer);
            Cl.ReleaseMemObject(outputBuffer);
        }

        private static void ExecuteOpenCLDraw(Graphics graphics, Bitmap image, Rectangle r)
        {
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr imgPtr = imageData.Scan0;
            int width = image.Width;
            int height = image.Height;

            ErrorCode error;
            IMem imgBuffer = Cl.CreateBuffer(_context, MemFlags.ReadOnly | MemFlags.CopyHostPtr, (IntPtr)(width * height * 4), imgPtr, out error);
            IMem outputBuffer = Cl.CreateBuffer(_context, MemFlags.WriteOnly, (IntPtr)(r.Width * r.Height * 4), IntPtr.Zero, out error);

            string kernelSource = @"
            __kernel void StretchBltKernel(__global uchar* src, __global uchar* dst, int srcWidth, int srcHeight, int dstWidth, int dstHeight) {
                int x = get_global_id(0);
                int y = get_global_id(1);

                int srcX = (x * srcWidth) / dstWidth;
                int srcY = (y * srcHeight) / dstHeight;

                int srcIndex = (srcY * srcWidth + srcX) * 4;
                int dstIndex = (y * dstWidth + x) * 4;

                dst[dstIndex] = src[srcIndex];
                dst[dstIndex + 1] = src[srcIndex + 1];
                dst[dstIndex + 2] = src[srcIndex + 2];
                dst[dstIndex + 3] = src[srcIndex + 3];
            }";

            OpenClProgram program = Cl.CreateProgramWithSource(_context, 1, new[] { kernelSource }, null, out error);
            Cl.BuildProgram(program, 1, new[] { _device }, string.Empty, null, IntPtr.Zero);

            Kernel kernel = Cl.CreateKernel(program, "StretchBltKernel", out error);

            Cl.SetKernelArg(kernel, 0, imgBuffer);
            Cl.SetKernelArg(kernel, 1, outputBuffer);
            Cl.SetKernelArg(kernel, 2, width);
            Cl.SetKernelArg(kernel, 3, height);
            Cl.SetKernelArg(kernel, 4, r.Width);
            Cl.SetKernelArg(kernel, 5, r.Height);

            IntPtr[] globalWorkSize = new IntPtr[] { (IntPtr)r.Width, (IntPtr)r.Height };
            Cl.EnqueueNDRangeKernel(_commandQueue, kernel, 2, null, globalWorkSize, null, 0, null, out _);

            Bitmap outputBitmap = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb);
            BitmapData outputData = outputBitmap.LockBits(new Rectangle(0, 0, r.Width, r.Height), ImageLockMode.WriteOnly, outputBitmap.PixelFormat);
            Cl.EnqueueReadBuffer(_commandQueue, outputBuffer, Bool.True, IntPtr.Zero, new IntPtr(r.Width * r.Height * 4), outputData.Scan0, 0, null, out _);
            outputBitmap.UnlockBits(outputData);

            image.UnlockBits(imageData);

            graphics.DrawImage(outputBitmap, r);

            Cl.ReleaseKernel(kernel);
            Cl.ReleaseProgram(program);
            Cl.ReleaseMemObject(imgBuffer);
            Cl.ReleaseMemObject(outputBuffer);
        }
    }
}
