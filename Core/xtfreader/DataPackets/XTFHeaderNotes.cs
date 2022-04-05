using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core.xtfreader.DataPackets
{
    internal class XTFHeaderNotes : XTFDataPacket
    {
        public int SubChannelNumber;
        public int NumChansToFollow;
        // Specification says it should be a dword list but, because it is reserved, it doesn't matter
        public byte[] Reserved;
        //uint NumBytesThisRecord;
        public uint Year;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;
        public byte[] ReservedBytes;
        public string NotesText;
        public XTFHeaderNotes(byte[] byte_array, List<XTFChanInfo> _) : this(byte_array) { }
        public XTFHeaderNotes(byte[] byte_array) : base(byte_array)
        {
            this.SubChannelNumber = BitConverter.ToInt16(new byte[] {byte_array[3], 0});
            this.NumChansToFollow = BitConverter.ToUInt16(new byte[] {byte_array[4], 0 });
            this.Reserved = byte_array[6..10];
            //this.NumBytesThisRecord = BitConverter.ToUInt32(byte_array[10..14]);
            this.Year = BitConverter.ToUInt16(new byte[] {byte_array[14], 0 });
            this.Month = BitConverter.ToInt16(new byte[] {byte_array[16], 0 });
            this.Day = BitConverter.ToInt16(new byte[] {byte_array[17], 0 });
            this.Hour = BitConverter.ToInt16(new byte[] {byte_array[18], 0 });
            this.Minute = BitConverter.ToInt16(new byte[] {byte_array[19], 0 });
            this.Second = BitConverter.ToInt16(new byte[] {byte_array[20], 0 });
            this.ReservedBytes = byte_array[21..56];
            this.NotesText = Encoding.UTF8.GetString(byte_array[56..256]).Replace("\0", "");
        }

        public override string ToString()
        {
            string xtfHeaderNotesString = base.ToString();
            xtfHeaderNotesString += "\nXTF HEADER NOTES";
            xtfHeaderNotesString += "\n\tMagicNumber: " + this.MagicNumber;
            xtfHeaderNotesString += "\n\tHeaderType: " + this.HeaderType;
            xtfHeaderNotesString += "\n\tSubChannelNumber: " + this.SubChannelNumber;
            xtfHeaderNotesString += "\n\tNumChansToFollow: " + this.NumChansToFollow;
            xtfHeaderNotesString += "\n\tReserved: " + this.Reserved;
            //xtfHeaderNotesString += "\n\tNumBytesThisRecord: " + this.NumBytesThisRecord;
            xtfHeaderNotesString += "\n\tYear: " + this.Year;
            xtfHeaderNotesString += "\n\tMonth: " + this.Month;
            xtfHeaderNotesString += "\n\tDay: " + this.Day;
            xtfHeaderNotesString += "\n\tHour: " + this.Hour;
            xtfHeaderNotesString += "\n\tMinute: " + this.Minute;
            xtfHeaderNotesString += "\n\tSecond: " + this.Second;
            xtfHeaderNotesString += "\n\tReservedBytes: " + this.ReservedBytes;
            xtfHeaderNotesString += "\n\tNotesText: " + this.NotesText;
            return xtfHeaderNotesString;
        }
    }
}
