using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core.xtfreader.DataPackets
{
    internal class XTFPingChanHeader : XTFDataPacket
    {
        public int ChannelNumber;
        public int DownsampleMethod;
        public float SlantRange;
        public float GroundRange;
        public float TimeDelay;
        public float TimeDuration;
        public float SecondsPerPing;
        public int ProcessingFlags;
        public int Frequency;
        public int InitialGainCode;
        public int GainCode;
        public int BandWidth;
        public uint ContactNumber;
        public int ContactClassification;
        public int ContactSubNumber;
        public int ContactType;
        public uint NumSamples;
        public int MillivoltScale;
        public float ContactTimeOffTrack;
        public int ContactCloseNumber;
        public int Reserved2;
        public float FixedVSOP;
        public int Weight;
        public byte[] ReservedSpace;
        public byte[] data_bytes;
        public XTFPingChanHeader(byte[] byte_array, List<XTFChanInfo> xtfChanInfoList) : base(byte_array)
        {
            this.ChannelNumber = BitConverter.ToUInt16(byte_array[0..2]);
            this.DownsampleMethod = BitConverter.ToUInt16(byte_array[2..4]);
            this.SlantRange = BitConverter.ToSingle(byte_array[4..8]);
            this.GroundRange = BitConverter.ToSingle(byte_array[8..12]);
            this.TimeDelay = BitConverter.ToSingle(byte_array[12..16]);
            this.TimeDuration = BitConverter.ToSingle(byte_array[16..20]);
            this.SecondsPerPing = BitConverter.ToSingle(byte_array[20..24]);
            this.ProcessingFlags = BitConverter.ToUInt16(byte_array[24..26]);
            this.Frequency = BitConverter.ToUInt16(byte_array[26..28]);
            this.InitialGainCode = BitConverter.ToUInt16(byte_array[28..30]);
            this.GainCode = BitConverter.ToUInt16(byte_array[30..32]);
            this.BandWidth = BitConverter.ToUInt16(byte_array[32..34]);
            this.ContactNumber = BitConverter.ToUInt32(byte_array[34..38]);
            this.ContactClassification = BitConverter.ToUInt16(byte_array[38..40]);
            this.ContactSubNumber = BitConverter.ToInt16(new byte[] { byte_array[40], 0 });
            this.ContactType = BitConverter.ToInt16(new byte[] { byte_array[41], 0 });
            this.NumSamples = BitConverter.ToUInt32(byte_array[42..46]);
            this.MillivoltScale = BitConverter.ToUInt16(byte_array[46..48]);
            this.ContactTimeOffTrack = BitConverter.ToSingle(byte_array[48..52]);
            this.ContactCloseNumber = BitConverter.ToInt16(new byte[] { byte_array[52], 0 });
            this.Reserved2 = BitConverter.ToInt16(new byte[] { byte_array[53], 0 });
            this.FixedVSOP = BitConverter.ToSingle(byte_array[54..58]);
            this.Weight = BitConverter.ToInt16(new byte[] { byte_array[58], 0 });
            this.ReservedSpace = byte_array[60..64];

            XTFChanInfo xtfChanInfo = xtfChanInfoList.Find(ch => ch.SubChannelNumber == this.ChannelNumber);

            int e_pos = (int)Sizes.XTF_PING_CHAN_HEADER_SIZE + (int)this.NumSamples * xtfChanInfo.BytesPerSample;
            this.data_bytes = byte_array[(int)Sizes.XTF_PING_CHAN_HEADER_SIZE.. e_pos];
        }

        public override string ToString()
        {
            string xtfPingChanHeaderString = base.ToString();
            xtfPingChanHeaderString += "\n\tXTF PING CHAN HEADER";
            xtfPingChanHeaderString += "\n\t\tChannelNumber: {}" + this.ChannelNumber;
            xtfPingChanHeaderString += "\n\t\tDownsampleMethod: {}" + this.DownsampleMethod;
            xtfPingChanHeaderString += "\n\t\tSlantRange: {}" + this.SlantRange;
            xtfPingChanHeaderString += "\n\t\tGroundRange: {}" + this.GroundRange;
            xtfPingChanHeaderString += "\n\t\tTimeDelay: {}" + this.TimeDelay;
            xtfPingChanHeaderString += "\n\t\tTimeDuration: {}" + this.TimeDuration;
            xtfPingChanHeaderString += "\n\t\tSecondsPerPing: {}" + this.SecondsPerPing;
            xtfPingChanHeaderString += "\n\t\tProcessingFlags: {}" + this.ProcessingFlags;
            xtfPingChanHeaderString += "\n\t\tFrequency: {}" + this.Frequency;
            xtfPingChanHeaderString += "\n\t\tInitialGainCode: {}" + this.InitialGainCode;
            xtfPingChanHeaderString += "\n\t\tGainCode: {}" + this.GainCode;
            xtfPingChanHeaderString += "\n\t\tBandWidth: {}" + this.BandWidth;
            xtfPingChanHeaderString += "\n\t\tContactNumber: {}" + this.ContactNumber;
            xtfPingChanHeaderString += "\n\t\tContactClassification: {}" + this.ContactClassification;
            xtfPingChanHeaderString += "\n\t\tContactSubNumber: {}" + this.ContactSubNumber;
            xtfPingChanHeaderString += "\n\t\tContactType: {}" + this.ContactType;
            xtfPingChanHeaderString += "\n\t\tNumSamples: {}" + this.NumSamples;
            xtfPingChanHeaderString += "\n\t\tMillivoltScale: {}" + this.MillivoltScale;
            xtfPingChanHeaderString += "\n\t\tContactTimeOffTrack: {}" + this.ContactTimeOffTrack;
            xtfPingChanHeaderString += "\n\t\tContactCloseNumber: {}" + this.ContactCloseNumber;
            xtfPingChanHeaderString += "\n\t\tReserved2: {}" + this.Reserved2;
            xtfPingChanHeaderString += "\n\t\tFixedVSOP: {}" + this.FixedVSOP;
            xtfPingChanHeaderString += "\n\t\tWeight: {}" + this.Weight;
            xtfPingChanHeaderString += "\n\t\tReservedSpace: " + this.ReservedSpace;
            xtfPingChanHeaderString += "\n\t\tLength of data: "+ this.data_bytes.Count() + "\t10 item sample: " + this.data_bytes[0..10];
            return xtfPingChanHeaderString;
        }
    }
}
