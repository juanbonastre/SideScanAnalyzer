using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.Core.xtfreader
{
    public class XTFFileHeader
    {
        public List<XTFChanInfo> xtfChanInfoList;

        int FileFormat;
        int SystemType;
        string RecordingProgramName;
        string RecordingProgramVersion;
        string SonarName;
        int SensorsType;

        string NoteString;
        string ThisFileName;
        int NavUnits;
        int NumberOfSonarChannels;
        int NumberOfBathymetryChannels;
        int NumberOfSnippetChannels;
        int NumberOfForwardLookArrays;
        int NumberOfEchoStrengthChannels;
        int NumberOfInterferometryChannels;
        int Reserved1;
        int Reserved2;
        int Reserved3;

        byte[] ProjectionType;
        byte[] SpheriodType;
        long NavigationLatency;
        float ReferencePointHeight;

        float OriginY;
        float OriginX;

        float NavOffsetY;
        float NavOffsetX;
        float NavOffsetZ;

        float NavOffsetYaw;
        float MRUOffsetY;
        float MRUOffsetX;
        float MRUOffsetZ;

        float MRUOffsetYaw;
        float MRUOffsetPitch;
        float MRUOffsetRoll;

        public XTFFileHeader(byte[] byte_array)
        {
            this.FileFormat = BitConverter.ToInt16(new byte[] {byte_array[0], 0});
            this.SystemType = BitConverter.ToInt16(new byte[] {byte_array[1], 0});
            this.RecordingProgramName = Encoding.UTF8.GetString(byte_array[2..10]).Replace("\0","");
            this.RecordingProgramVersion = Encoding.UTF8.GetString(byte_array[10..18]).Replace("\0", "");
            this.SonarName = Encoding.UTF8.GetString(byte_array[18..34]).Replace("\0", "");
            this.SensorsType = BitConverter.ToUInt16(byte_array[34..36]);
            this.NoteString = Encoding.UTF8.GetString(byte_array[36..100]).Replace("\0", "");
            this.ThisFileName = Encoding.UTF8.GetString(byte_array[100..164]).Replace("\0", "");
            this.NavUnits = BitConverter.ToUInt16(byte_array[164..166]);
            this.NumberOfSonarChannels = BitConverter.ToUInt16(byte_array[166..168]);
            this.NumberOfBathymetryChannels = BitConverter.ToUInt16(byte_array[168..170]);
            this.NumberOfSnippetChannels = BitConverter.ToInt16(new byte[] {byte_array[170], 0 });
            this.NumberOfForwardLookArrays = BitConverter.ToInt16(new byte[] {byte_array[171], 0 });
            this.NumberOfEchoStrengthChannels = BitConverter.ToUInt16(byte_array[172..174]);
            this.NumberOfInterferometryChannels = BitConverter.ToInt16(new byte[] {byte_array[174], 0 });
            this.Reserved1 = BitConverter.ToInt16(new byte[] {byte_array[175], 0 });
            this.Reserved2 = BitConverter.ToInt16(new byte[] {byte_array[176], 0 });
            this.Reserved3 = BitConverter.ToInt16(new byte[] {byte_array[177], 0 });
            this.ReferencePointHeight = BitConverter.ToSingle(byte_array[178..182]);
            this.ProjectionType = byte_array[182..194];
            this.SpheriodType = byte_array[194..204];
            this.NavigationLatency = BitConverter.ToInt64(byte_array[204..208].Concat((new byte[] { 0, 0, 0, 0 })).ToArray());
            this.OriginY = BitConverter.ToSingle(byte_array[208..212]);
            this.OriginX = BitConverter.ToSingle(byte_array[212..216]);
            this.NavOffsetY = BitConverter.ToSingle(byte_array[216..220]);
            this.NavOffsetX = BitConverter.ToSingle(byte_array[220..224]);
            this.NavOffsetZ = BitConverter.ToSingle(byte_array[224..228]);
            this.NavOffsetYaw = BitConverter.ToSingle(byte_array[228..232]);
            this.MRUOffsetY = BitConverter.ToSingle(byte_array[232..236]);
            this.MRUOffsetX = BitConverter.ToSingle(byte_array[236..240]);
            this.MRUOffsetZ = BitConverter.ToSingle(byte_array[240..244]);
            this.MRUOffsetYaw = BitConverter.ToSingle(byte_array[244..248]);
            this.MRUOffsetPitch = BitConverter.ToSingle(byte_array[248..252]);
            this.MRUOffsetRoll = BitConverter.ToSingle(byte_array[252..256]);

            // Now obtain the chan info packages
            this.xtfChanInfoList = new List<XTFChanInfo>();
            for (int i=0;i<this.NumberOfSonarChannels; i++)
            {
                int s_pos = (int)Constants.XTF_CHANINFO_START_POS + i * (int)Sizes.XTF_CHANINFO_SIZE;
                int e_pos = s_pos + (int)Sizes.XTF_CHANINFO_SIZE;
                this.xtfChanInfoList.Add(new XTFChanInfo(byte_array[s_pos..e_pos]));
            }
                
        }

        public override string ToString()
        {
            string xtfFileHeaderString = "";
            xtfFileHeaderString += "XTF File Header";
            xtfFileHeaderString += "\n\tFileFormat: " + this.FileFormat;
            xtfFileHeaderString += "\n\tSystemType: " + this.SystemType;
            xtfFileHeaderString += "\n\tRecordingProgramName: " + this.RecordingProgramName;
            xtfFileHeaderString += "\n\tRecordingProgramVersion: " + this.RecordingProgramVersion;
            xtfFileHeaderString += "\n\tSonarName: " + this.SonarName;
            xtfFileHeaderString += "\n\tSensorsType: " + this.SensorsType;
            xtfFileHeaderString += "\n\tNoteString: " + this.NoteString;
            xtfFileHeaderString += "\n\tThisFileName: " + this.ThisFileName;
            xtfFileHeaderString += "\n\tNavUnits: " + this.NavUnits;
            xtfFileHeaderString += "\n\tNumberOfSonarChannels: " + this.NumberOfSonarChannels;
            xtfFileHeaderString += "\n\tNumberOfBathymetryChannels: " + this.NumberOfBathymetryChannels;
            xtfFileHeaderString += "\n\tNumberOfSnippetChannels: " + this.NumberOfSnippetChannels;
            xtfFileHeaderString += "\n\tNumberOfForwardLookArrays: " + this.NumberOfForwardLookArrays;
            xtfFileHeaderString += "\n\tNumberOfEchoStrengthChannels: " + this.NumberOfEchoStrengthChannels;
            xtfFileHeaderString += "\n\tNumberOfInterferometryChannels: " + this.NumberOfInterferometryChannels;
            xtfFileHeaderString += "\n\tReserved1: " + this.Reserved1;
            xtfFileHeaderString += "\n\tReserved2: " + this.Reserved2;
            xtfFileHeaderString += "\n\tReserved3: " + this.Reserved3;
            xtfFileHeaderString += "\n\tReferencePointHeight: " + this.ReferencePointHeight;
            xtfFileHeaderString += "\n\tProjectionType: " + this.ProjectionType;
            xtfFileHeaderString += "\n\tSpheriodType: " + this.SpheriodType;
            xtfFileHeaderString += "\n\tNavigationLatency: " + this.NavigationLatency;
            xtfFileHeaderString += "\n\tOriginY: " + this.OriginY;
            xtfFileHeaderString += "\n\tOriginX: " + this.OriginX;
            xtfFileHeaderString += "\n\tNavOffsetY: " + this.NavOffsetY;
            xtfFileHeaderString += "\n\tNavOffsetX: " + this.NavOffsetX;
            xtfFileHeaderString += "\n\tNavOffsetZ: " + this.NavOffsetZ;
            xtfFileHeaderString += "\n\tNavOffsetYaw: " + this.NavOffsetYaw;
            xtfFileHeaderString += "\n\tMRUOffsetY: " + this.MRUOffsetY;
            xtfFileHeaderString += "\n\tMRUOffsetX: " + this.MRUOffsetX;
            xtfFileHeaderString += "\n\tMRUOffsetZ: " + this.MRUOffsetZ;
            xtfFileHeaderString += "\n\tMRUOffsetYaw: " + this.MRUOffsetYaw;
            xtfFileHeaderString += "\n\tMRUOffsetPitch: " + this.MRUOffsetPitch;
            xtfFileHeaderString += "\n\tMRUOffsetRoll: " + this.MRUOffsetRoll;

            /*foreach (XTFChanInfo xtfChanInfo in this.xtfChanInfoList){
                xtfFileHeaderString += xtfChanInfo.ToString();
            }*/

            return xtfFileHeaderString;
        }
    }
}
