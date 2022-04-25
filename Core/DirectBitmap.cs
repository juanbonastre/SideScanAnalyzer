using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core
{
    // This class is taken from a Stackoverflow thread: 
    // https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
    // is a better performance implementation of the Bitmap class

    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }
        public DirectBitmap(string filePath)
        {
            Bitmap = new Bitmap(filePath);
            Width = Bitmap.Size.Width;
            Height = Bitmap.Size.Height;
            Bits = new Int32[Width * Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }

        public void PaintLine(double x1, double x2, double y1, double y2, double prediction)
        {
            // Trace.WriteLine("x1:"+x1+"-x2:"+x2+" | y1:"+y1+"-y2:"+y2);
            if (x1 > Width || x2 > Width || y1 > Height || y2 > Height)
            {
                return;
            }

            x1 -= 1;
            if (x1 < 0) x1 = 0;
            x2 -= 1;
            if (x2 < 0) x2 = 0;
            y1 -= 1;
            if (y1 < 0) y1 = 0;
            y2 -= 1;
            if (y2 < 0) y2 = 0;

            Color color = Color.FromArgb(255, 255, Convert.ToInt32(255*Math.Abs(1-prediction)), 0);

            // Upper and Bottom line
            for (int i = (int)Math.Truncate(x1); i < x2; i++)
            {
                // Trace.WriteLine("i:"+i+"-y1:"+y1+" | i:"+i+"-y2:"+y2);
                SetPixel(i, (int)y1, color);
                SetPixel(i, (int)y2, color);

                if (y1-1>0) SetPixel(i, (int)y1-1, color);
                if (y2-1>0) SetPixel(i, (int)y2-1, color);
                if (y1+1<Height) SetPixel(i, (int)y1+1, color);
                if (y2+1<Height) SetPixel(i, (int)y2+1, color);
            }
            // Right and Left line
            for (int i = (int)Math.Truncate(y1); i < y2; i++)
            {
                SetPixel((int)x1, i, color);
                SetPixel((int)x2, i, color);

                if (x1-1>0) SetPixel((int)x1-1, i, color);
                if (x2-1>0) SetPixel((int)x2-1, i, color);
                if (x1+1<Width) SetPixel((int)x1+1, i, color);
                if (x2+1<Width) SetPixel((int)x2+1, i, color);
            }
        }
    }
}
