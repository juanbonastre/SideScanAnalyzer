using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core.xtfreader
{
    internal static class BitmapConverter
    {
        public static byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];
            for (int i = 0; i < rows; i++)
                Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);
            return newBytes;
        }
        public static Bitmap ConvertToBitmap2(List<int[]> intArraysList, int maxValue)
        {
            int rows = intArraysList.Count;
            int columns = intArraysList[0].Length;
            int stride = columns;

            byte[] imageData = new byte[rows * columns];

            for (int i = 0; i<rows; i++)
            {
                for (int j = 0; j<columns; j++)
                {
                    int pos = i*columns+j;
                    // int value = (int)(255 - (double)intArraysList[i][j]*255/maxValue);
                    int value = (int)(255 - Math.Log10(1 + (double)intArraysList[i][j]*255/maxValue)*100);
                    imageData[pos] = (byte)value;
                }
            }

            // the pixel values are correct

            for (int i = 0; i<10; i++)
            {
                Trace.WriteLine("value: "+ imageData[i]);
            }

            byte[] newbytes = PadLines(imageData, rows, columns);

            Bitmap im = new Bitmap(columns, rows, stride,
                     PixelFormat.Format8bppIndexed,
                     Marshal.UnsafeAddrOfPinnedArrayElement(newbytes, 0));

            return im;
        }

        public static Bitmap ConvertToBitmap(List<int[]> pingsList, int maxValue)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var columns = pingsList.Count;
            var rows = pingsList[0].Length;

            // I used DirectBitmap for better performance, this, class redefines the SetPixel and other methods of Bitmap
            DirectBitmap bitmap = new DirectBitmap(rows, columns);

            stopwatch.Stop();
            TimeSpan stopwatchElapsed = stopwatch.Elapsed;
            float time = ((float)Convert.ToInt32(stopwatchElapsed.TotalMilliseconds)/1000);
            Trace.WriteLine("BITMAPCONVERTER 1: " + time.ToString());
            stopwatch.Start();

            int value;
            for (int i = 0; i < columns; i++)
            {
                for(int j = 0; j < rows; j++)
                {
                    value = (int)(255 - Math.Log10(1 + (double)pingsList[i][j]*255/maxValue)*100);
                    bitmap.SetPixel(j,i, Color.FromArgb(value, value, value));
                }
            }

            stopwatch.Stop();
            stopwatchElapsed = stopwatch.Elapsed;
            time = ((float)Convert.ToInt32(stopwatchElapsed.TotalMilliseconds)/1000);
            Trace.WriteLine("BITMAPCONVERTER 2: " + time.ToString());

            return bitmap.Bitmap;
        }
    }
}
