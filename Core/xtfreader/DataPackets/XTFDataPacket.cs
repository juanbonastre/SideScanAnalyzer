using System;
using System.Collections.Generic;

// THIS IS THE CODE TO IMPLEMENT IN XTFile.read() but because of performance when accesing the byte array i used other way

/*int headerType = BitConverter.ToInt16(new byte[] { byte_array[i+2], 0 });
Type type = DataPacketDictionary.getDataPacketType(headerType);
if (type!=null)
{
    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    instance = (XTFDataPacket)Activator.CreateInstance(type, byte_array[i..], this.xtfFileHeader.xtfChanInfoList);
    //MessageBox.Show(instance.ToString());
    if (instance.GetType()==typeof(XTFPingHeader))
    {
        //MessageBox.Show(((XTFPingHeader)instance).xtfPingChanHeaderList[0].ToString());
    }
    jump = (int)instance.NumBytesThisRecord;
}*/

namespace SideScanAnalyzer.Core.xtfreader.DataPackets
{
    internal abstract class XTFDataPacket
    {
        public int MagicNumber;
        public int HeaderType;
        public uint NumBytesThisRecord;
        public XTFDataPacket() { }
        public XTFDataPacket(byte[] byte_array, List<XTFChanInfo> _) : this(byte_array) { }
        public XTFDataPacket(byte[] byte_array)
        {
            this.MagicNumber = BitConverter.ToUInt16(byte_array[0..2]);
            this.HeaderType = BitConverter.ToInt16(new byte[] {byte_array[2], 0});
            this.NumBytesThisRecord = BitConverter.ToUInt16(byte_array[10..14]);
        }
        public override string ToString()
        {
            string packetString = "DataPacket";
            packetString += "\n\tMagicNumber: " + MagicNumber;
            packetString += "\n\tHeaderType: " + HeaderType;
            packetString += "\n\tNumBytesThisRecord: " + NumBytesThisRecord;
            return packetString;
        }
    }
}
