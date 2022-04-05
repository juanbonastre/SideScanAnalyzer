using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core.xtfreader
{
    public class XTFChanInfo
    {
        public int TypeOfChannel;

        public int SubChannelNumber;
        public int CorrectionFlags;

        public int UniPolar;
        public int BytesPerSample;
        public uint Reserved;

        public string ChannelName;
        public float VoltScale;

        public float Frequency;
        public float HorizBeamAngle;
        public float TiltAngle;
        public float BeamWidth;


        public float OffsetX;
        public float OffsetY;
        public float OffsetZ;

        public float OffsetYaw;

        public float OffsetPitch;
        public float OffsetRoll;

        public int BeamsPerArray;
        public int SampleFormat;
        public byte[] ReservedArea2;

        public XTFChanInfo(byte[] byte_array)
        {
            this.TypeOfChannel = BitConverter.ToInt16(new byte[] { byte_array[0], 0 });
            this.SubChannelNumber = BitConverter.ToInt16(new byte[] { byte_array[1], 0 });
            this.CorrectionFlags = BitConverter.ToUInt16(byte_array[2..4]);
            this.UniPolar = BitConverter.ToUInt16(byte_array[4..6]);
            this.BytesPerSample = BitConverter.ToUInt16(byte_array[6..8]);
            this.Reserved = BitConverter.ToUInt32(byte_array[8..12]);
            this.ChannelName = Encoding.UTF8.GetString(byte_array[12..28]).Replace("\0", "");
            this.VoltScale = BitConverter.ToSingle(byte_array[28..32]);
            this.Frequency = BitConverter.ToSingle(byte_array[32..36]);
            this.HorizBeamAngle = BitConverter.ToSingle(byte_array[36..40]);
            this.TiltAngle = BitConverter.ToSingle(byte_array[40..44]);
            this.BeamWidth = BitConverter.ToSingle(byte_array[44..48]);
            this.OffsetX = BitConverter.ToSingle(byte_array[48..52]);
            this.OffsetY = BitConverter.ToSingle(byte_array[52..56]);
            this.OffsetZ = BitConverter.ToSingle(byte_array[56..60]);
            this.OffsetYaw = BitConverter.ToSingle(byte_array[60..64]);
            this.OffsetPitch = BitConverter.ToSingle(byte_array[64..68]);
            this.OffsetRoll = BitConverter.ToSingle(byte_array[68..72]);
            this.BeamsPerArray = BitConverter.ToUInt16(byte_array[72..74]);
            this.SampleFormat = BitConverter.ToInt16(new byte[] { byte_array[74], 0 });
            this.ReservedArea2 = byte_array[75..128];
        }

        public override string ToString()
        {
            string xtfChanInfoString = "ChanInfo";
            xtfChanInfoString += "\n\tTypeOfChannel: " + this.TypeOfChannel;
            xtfChanInfoString += "\n\tSubChannelNumber: " + this.SubChannelNumber;
            xtfChanInfoString += "\n\tCorrectionFlags: " + this.CorrectionFlags;
            xtfChanInfoString += "\n\tUniPolar: " + this.UniPolar;
            xtfChanInfoString += "\n\tBytesPerSample: " + this.BytesPerSample;
            xtfChanInfoString += "\n\tReserved: " + this.Reserved;
            xtfChanInfoString += "\n\tChannelName: " + this.ChannelName;
            xtfChanInfoString += "\n\tVoltScale: " + this.VoltScale;
            xtfChanInfoString += "\n\tFrequency: " + this.Frequency;
            xtfChanInfoString += "\n\tHorizBeamAngle: " + this.HorizBeamAngle;
            xtfChanInfoString += "\n\tTiltAngle: " + this.TiltAngle;
            xtfChanInfoString += "\n\tBeamWidth: " + this.BeamWidth;
            xtfChanInfoString += "\n\tOffsetX: " + this.OffsetX;
            xtfChanInfoString += "\n\tOffsetY: " + this.OffsetY;
            xtfChanInfoString += "\n\tOffsetZ: " + this.OffsetZ;
            xtfChanInfoString += "\n\tOffsetYaw: " + this.OffsetYaw;
            xtfChanInfoString += "\n\tOffsetPitch: " + this.OffsetPitch;
            xtfChanInfoString += "\n\tOffsetRoll: " + this.OffsetRoll;
            xtfChanInfoString += "\n\tBeamsPerArray: " + this.BeamsPerArray;
            xtfChanInfoString += "\n\tSampleFormat: " + this.SampleFormat;
            xtfChanInfoString += "\n\tReservedArea2: " + this.ReservedArea2;
            return xtfChanInfoString;
        }
    }
}
