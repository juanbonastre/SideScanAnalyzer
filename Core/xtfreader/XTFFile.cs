using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Core.xtfreader
{
    public class XTFFile
    {
        public string filePath;
        public long fileSize;
        public XTFFileHeader xtfFileHeader;
        //data_packets:
        public PingPacketsManager pingPacketsManager;
        //pixel_arrays;
        public int nPings;
        public int max_n_samples;


        public XTFFile(string filePath)
        {

            this.filePath = filePath;
            this.fileSize = 0;
            this.nPings = 0;
            //this.data_packets = [];
            this.pingPacketsManager = new PingPacketsManager();
            //this.pixel_arrays = [];
            this.max_n_samples = 0;
        }

        public bool ReadSideScanData()
        {
            // Read the hole 
            byte[] byteArray = ReadBytes();

            // Get the file header
            this.xtfFileHeader = new XTFFileHeader(byteArray[0..(int)Sizes.XTF_FILE_HEADER_SIZE]);
            //MessageBox.Show(this.xtfFileHeader.ToString());
            //MessageBox.Show(this.xtfFileHeader.xtfChanInfoList[0].ToString());

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int jump;
            for (int i=(int)Sizes.XTF_FILE_HEADER_SIZE; i<this.fileSize; i+=jump)
            {
                jump=1;
                int magicNumber = BitConverter.ToUInt16(byteArray[i..(i+2)]);
                if (magicNumber == (int)Constants.MAGIC_NUMBER)
                {
                    int headerType = BitConverter.ToInt16(new byte[] { byteArray[i+2], 0 });
                    switch (headerType)
                    {
                        case (int)PacketTypes.XTF_HEADER_NOTES:
                            break;
                        case (int)PacketTypes.XTF_HEADER_SONAR:
                            uint pingHeaderNumChansToFollow = BitConverter.ToUInt16(byteArray[(i+4)..(i+6)]);
                            uint pingHeaderNumBytesThisRecord = BitConverter.ToUInt32(byteArray[(i+10)..(i+14)]);

                            int lastPingChanSize = 0;
                            for(int j=0; j<pingHeaderNumChansToFollow; j++)
                            {
                                int sPos = i+(int)Sizes.XTF_PING_HEADER_SIZE + lastPingChanSize*j;
                                uint pingChanHeaderChannelNumber = BitConverter.ToUInt16(byteArray[(sPos)..(sPos+2)]);
                                uint pingChanHeaderNumSamples = BitConverter.ToUInt32(byteArray[(sPos+42)..(sPos+46)]);

                                XTFChanInfo xtfChanInfo = this.xtfFileHeader.xtfChanInfoList.Find(ch => ch.SubChannelNumber == pingChanHeaderChannelNumber);

                                sPos = sPos+(int)Sizes.XTF_PING_CHAN_HEADER_SIZE;
                                int dataBytesSize = (int) pingChanHeaderNumSamples * xtfChanInfo.BytesPerSample;
                                int ePos = sPos + dataBytesSize;

                                this.pingPacketsManager.AddPing((int)pingChanHeaderChannelNumber, byteArray[sPos..ePos], xtfChanInfo.BytesPerSample);
                                lastPingChanSize = (int)Sizes.XTF_PING_CHAN_HEADER_SIZE + dataBytesSize; 
                                /*if ping_chan_header_NumSamples>self.max_n_samples:
                                    self.max_n_samples = ping_chan_header_NumSamples*/
                            }
                            this.nPings++;
                            break;
                        case (int)PacketTypes.XTF_HEADER_KLEINV4_DATA_PAGE:
                            break;
                        default:
                            MessageBox.Show("Header not found");
                            break;
                    }
                    jump = BitConverter.ToUInt16(byteArray[(i+10)..(i+14)]);
                }
            }
            stopwatch.Stop();

            TimeSpan stopwatchElapsed = stopwatch.Elapsed;
            float time = ((float)Convert.ToInt32(stopwatchElapsed.TotalMilliseconds)/1000);
            Trace.WriteLine(time.ToString());

            Trace.WriteLine("Time reading file passed: " + time.ToString());
            //Trace.WriteLine("Data Packets: " + this.pingPacketsManager.processedData[0].Count);

            return true;
        }
        


        /*def get_rastered_images(self) -> list:
        data_images = []
        for dt in self.pixel_arrays:
            data_images.append(self.get_rastered_image(dt))
        return data_images

        def get_rastered_image(self, data_array) -> im.Image:
            data_image = im.fromarray(data_array)
            return data_image

        def merge_arrays(self, np_array1, np_array2) -> np.array:
            return np.concatenate((np_array1, np_array2), axis=1)

        def _to_pixel_values(self, voltage_arrays) -> np.array:
            s_time = time.time()
            m = np.max(voltage_arrays)
            voltage_arrays = 255-(voltage_arrays*255)/m
            voltage_arrays[voltage_arrays<0] = 0
            voltage_arrays = voltage_arrays.astype(np.uint8)
            e_time = time.time()
            print("Time converting to pixel values", e_time-s_time)
            print("SHAPE ", np.shape(voltage_arrays))
            return voltage_arrays
        */

        private byte[] ReadBytes()
        {
            byte[] byteArray;
            if (File.Exists(this.filePath))
            {
                this.fileSize = new System.IO.FileInfo(this.filePath).Length;
                byteArray = File.ReadAllBytes(this.filePath);
            }
            else
            {
                byteArray = new byte[0];
                MessageBox.Show("File not found!");
            }

            return byteArray;
        }

    }
}
