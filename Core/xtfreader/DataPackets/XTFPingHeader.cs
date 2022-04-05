﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core.xtfreader.DataPackets
{
    internal class XTFPingHeader : XTFDataPacket
    {
        public List<XTFPingChanHeader> xtfPingChanHeaderList;

        public int SubChannelNumber;
        public int NumChansToFollow;
        public byte[] Reserved1;
        //uint NumBytesThisRecord;
        public int Year;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;
        public int HSeconds;
        public int JulianDay;
        public uint EventNumber;
        public uint PingNumber;
        public float SoundVelocity;
        public float OceanTide;
        public uint Reserved2;
        public float ConductivityFreq;
        public float TemperatureFreq;
        public float PressureFreq;
        public float PressureTemp;
        public float Conductivity;
        public float WaterTemperature;
        public float Pressure;
        public float ComputedSoundVelocity;
        public float MagX;
        public float MagY;
        public float MagZ;
        public float AuxVal1;
        public float AuxVal2;
        public float AuxVal3;
        public float Reserved3;
        public float Reserved4;
        public float Reserved5;
        public float SpeedLog;
        public float Turbidity;
        public float ShipSpeed;
        public float ShipGyro;
        public double ShipYcoordinate;
        public double ShipXcoordinate;
        public int ShipAltitude;
        public int ShipDepth;
        public int FixTimeHour;
        public int FixTimeMinute;
        public int FixTimeSecond;
        public int FixTimeHsecond;
        public float SensorSpeed;
        public float KP;
        public double SensorYcoordinate;
        public double SensorXcoordinate;
        public int SonarStatus;
        public int RangeToFish;
        public int BearingToFish;
        public int CableOut;
        public float Layback;
        public float CableTension;
        public float SensorDepth;
        public float SensorPrimaryAltitude;
        public float SensorAuxAltitude;
        public float SensorPitch;
        public float SensorRoll;
        public float SensorHeading;
        public float Heave;
        public float Yaw;
        public uint AttitudeTimeTag;
        public float DOT;
        public uint NavFixMilliseconds;
        public int ComputerClockHour;
        public int ComputerClockMinute;
        public int ComputerClockSecond;
        public int ComputerClockHsec;
        public int FishPositionDeltaX;
        public int FishPositionDeltaY;
        public int FishPositionErrorCode;
        public uint OptionalOffset;
        public int CableOutHundredths;
        public byte[] ReservedSpace2;
        public XTFPingHeader(byte[] byte_array)
        {

        }
        public XTFPingHeader()
        {

        }

        public XTFPingHeader(byte[] byte_array, List<XTFChanInfo> xtfChanInfoList) : base(byte_array)
        {
            this.SubChannelNumber = BitConverter.ToInt16(new byte[] { byte_array[3], 0});
            this.NumChansToFollow = BitConverter.ToUInt16(byte_array[4..6]);
            this.Reserved1 = byte_array[6..10];
            //this.NumBytesThisRecord = BitConverter.ToUInt32(byte_array[10..14]);
            this.Year = BitConverter.ToUInt16(byte_array[14..16]);
            this.Month = BitConverter.ToInt16(new byte[] { byte_array[16], 0 });
            this.Day = BitConverter.ToInt16(new byte[] { byte_array[17], 0 });
            this.Hour = BitConverter.ToInt16(new byte[] { byte_array[18], 0 });
            this.Minute = BitConverter.ToInt16(new byte[] { byte_array[19], 0 });
            this.Second = BitConverter.ToInt16(new byte[] { byte_array[20], 0 });
            this.HSeconds = BitConverter.ToInt16(new byte[] { byte_array[21], 0 });
            this.JulianDay = BitConverter.ToUInt16(byte_array[22..24]);
            this.EventNumber = BitConverter.ToUInt32(byte_array[24..28]);
            this.PingNumber = BitConverter.ToUInt32(byte_array[28..32]);
            this.SoundVelocity = BitConverter.ToSingle(byte_array[32..36]);
            this.OceanTide = BitConverter.ToSingle(byte_array[36..40]);
            this.Reserved2 = BitConverter.ToUInt32(byte_array[40..44]);
            this.ConductivityFreq = BitConverter.ToSingle(byte_array[44..48]);
            this.TemperatureFreq = BitConverter.ToSingle(byte_array[48..52]);
            this.PressureFreq = BitConverter.ToSingle(byte_array[52..56]);
            this.PressureTemp = BitConverter.ToSingle(byte_array[56..60]);
            this.Conductivity = BitConverter.ToSingle(byte_array[60..64]);
            this.WaterTemperature = BitConverter.ToSingle(byte_array[64..68]);
            this.Pressure = BitConverter.ToSingle(byte_array[68..72]);
            this.ComputedSoundVelocity = BitConverter.ToSingle(byte_array[72..76]);
            this.MagX = BitConverter.ToSingle(byte_array[76..80]);
            this.MagY = BitConverter.ToSingle(byte_array[80..84]);
            this.MagZ = BitConverter.ToSingle(byte_array[84..88]);
            this.AuxVal1 = BitConverter.ToSingle(byte_array[88..92]);
            this.AuxVal2 = BitConverter.ToSingle(byte_array[92..96]);
            this.AuxVal3 = BitConverter.ToSingle(byte_array[96..100]);
            this.Reserved3 = BitConverter.ToSingle(byte_array[100..104]);
            this.Reserved4 = BitConverter.ToSingle(byte_array[104..108]);
            this.Reserved5 = BitConverter.ToSingle(byte_array[108..112]);
            this.SpeedLog = BitConverter.ToSingle(byte_array[112..116]);
            this.Turbidity = BitConverter.ToSingle(byte_array[116..120]);
            this.ShipSpeed = BitConverter.ToSingle(byte_array[120..124]);
            this.ShipGyro = BitConverter.ToSingle(byte_array[124..128]);
            this.ShipYcoordinate = BitConverter.ToDouble(byte_array[128..136]);
            this.ShipXcoordinate = BitConverter.ToDouble(byte_array[136..144]);
            this.ShipAltitude = BitConverter.ToUInt16(byte_array[144..146]);
            this.ShipDepth = BitConverter.ToUInt16(byte_array[146..148]);
            this.FixTimeHour = BitConverter.ToInt16(new byte[] { byte_array[148], 0 });
            this.FixTimeMinute = BitConverter.ToInt16(new byte[] { byte_array[149], 0 });
            this.FixTimeSecond = BitConverter.ToInt16(new byte[] { byte_array[150], 0 });
            this.FixTimeHsecond = BitConverter.ToInt16(new byte[] { byte_array[151], 0 });
            this.SensorSpeed = BitConverter.ToSingle(byte_array[152..156]);
            this.KP = BitConverter.ToSingle(byte_array[156..160]);
            this.SensorYcoordinate = BitConverter.ToDouble(byte_array[160..168]);
            this.SensorXcoordinate = BitConverter.ToDouble(byte_array[168..176]);
            this.SonarStatus = BitConverter.ToUInt16(byte_array[176..178]);
            this.RangeToFish = BitConverter.ToUInt16(byte_array[178..180]);
            this.BearingToFish = BitConverter.ToUInt16(byte_array[180..182]);
            this.CableOut = BitConverter.ToUInt16(byte_array[182..184]);
            this.Layback = BitConverter.ToSingle(byte_array[184..188]);
            this.CableTension = BitConverter.ToSingle(byte_array[188..192]);
            this.SensorDepth = BitConverter.ToSingle(byte_array[192..196]);
            this.SensorPrimaryAltitude = BitConverter.ToSingle(byte_array[196..200]);
            this.SensorAuxAltitude = BitConverter.ToSingle(byte_array[200..204]);
            this.SensorPitch = BitConverter.ToSingle(byte_array[204..208]);
            this.SensorRoll = BitConverter.ToSingle(byte_array[208..212]);
            this.SensorHeading = BitConverter.ToSingle(byte_array[212..216]);
            this.Heave = BitConverter.ToSingle(byte_array[216..220]);
            this.Yaw = BitConverter.ToSingle(byte_array[220..224]);
            this.AttitudeTimeTag = BitConverter.ToUInt32(byte_array[224..228]);
            this.DOT = BitConverter.ToSingle(byte_array[228..232]);
            this.NavFixMilliseconds = BitConverter.ToUInt32(byte_array[232..236]);
            this.ComputerClockHour = BitConverter.ToInt16(new byte[] { byte_array[236], 0 });
            this.ComputerClockMinute = BitConverter.ToInt16(new byte[] { byte_array[237], 0 });
            this.ComputerClockSecond = BitConverter.ToInt16(new byte[] { byte_array[238], 0 });
            this.ComputerClockHsec = BitConverter.ToInt16(new byte[] { byte_array[239], 0 });
            this.FishPositionDeltaX = BitConverter.ToInt16(new byte[] { byte_array[240], 0 });
            this.FishPositionDeltaY = BitConverter.ToInt16(new byte[] { byte_array[242], 0 });
            this.FishPositionErrorCode = BitConverter.ToInt16(new byte[] { byte_array[244], 0 });
            this.OptionalOffset = BitConverter.ToUInt32(byte_array[245..249]);
            this.CableOutHundredths = BitConverter.ToInt16(new byte[] { byte_array[249], 0 });
            this.ReservedSpace2 = byte_array[250..256];

            this.xtfPingChanHeaderList = new List<XTFPingChanHeader>();
            int lastDataSize = 0;
            for (int i=0; i<this.NumChansToFollow; i++)
            {
                int pos = (int)Sizes.XTF_PING_HEADER_SIZE + ((int)Sizes.XTF_PING_CHAN_HEADER_SIZE + lastDataSize) * i;

                XTFPingChanHeader xtfPingChanHeader = new XTFPingChanHeader(byte_array[pos..], xtfChanInfoList);
                this.xtfPingChanHeaderList.Add(xtfPingChanHeader);

                XTFChanInfo xtfChanInfo = xtfChanInfoList.Find(ch => ch.SubChannelNumber == xtfPingChanHeader.ChannelNumber);
                lastDataSize = (int)xtfPingChanHeader.NumSamples * xtfChanInfo.BytesPerSample;
            }
        }

        public override string ToString()
        {
            string xtfPingHeaderString = base.ToString();
            xtfPingHeaderString += "\nXTF PING HEADER";
            xtfPingHeaderString += "\n\tMagicNumber: " + this.MagicNumber;
            xtfPingHeaderString += "\n\tHeaderType: " + this.HeaderType;
            xtfPingHeaderString += "\n\tSubChannelNumber: " + this.SubChannelNumber;
            xtfPingHeaderString += "\n\tNumChansToFollow: " + this.NumChansToFollow;
            xtfPingHeaderString += "\n\tReserved1: " + this.Reserved1;
            xtfPingHeaderString += "\n\tNumBytesThisRecord: " + this.NumBytesThisRecord;
            xtfPingHeaderString += "\n\tYear: " + this.Year;
            xtfPingHeaderString += "\n\tMonth: " + this.Month;
            xtfPingHeaderString += "\n\tDay: " + this.Day;
            xtfPingHeaderString += "\n\tHour: " + this.Hour;
            xtfPingHeaderString += "\n\tMinute: " + this.Minute;
            xtfPingHeaderString += "\n\tSecond: " + this.Second;
            xtfPingHeaderString += "\n\tHSeconds: " + this.HSeconds;
            xtfPingHeaderString += "\n\tJulianDay: " + this.JulianDay;
            xtfPingHeaderString += "\n\tEventNumber: " + this.EventNumber;
            xtfPingHeaderString += "\n\tPingNumber: " + this.PingNumber;
            xtfPingHeaderString += "\n\tSoundVelocity: " + this.SoundVelocity;
            xtfPingHeaderString += "\n\tOceanTide: " + this.OceanTide;
            xtfPingHeaderString += "\n\tReserved2: " + this.Reserved2;
            xtfPingHeaderString += "\n\tConductivityFreq: " + this.ConductivityFreq;
            xtfPingHeaderString += "\n\tTemperatureFreq: " + this.TemperatureFreq;
            xtfPingHeaderString += "\n\tPressureFreq: " + this.PressureFreq;
            xtfPingHeaderString += "\n\tPressureTemp: " + this.PressureTemp;
            xtfPingHeaderString += "\n\tConductivity: " + this.Conductivity;
            xtfPingHeaderString += "\n\tWaterTemperature: " + this.WaterTemperature;
            xtfPingHeaderString += "\n\tPressure: " + this.Pressure;
            xtfPingHeaderString += "\n\tComputedSoundVelocity: " + this.ComputedSoundVelocity;
            xtfPingHeaderString += "\n\tMagX: " + this.MagX;
            xtfPingHeaderString += "\n\tMagY: " + this.MagY;
            xtfPingHeaderString += "\n\tMagZ: " + this.MagZ;
            xtfPingHeaderString += "\n\tAuxVal1: " + this.AuxVal1;
            xtfPingHeaderString += "\n\tAuxVal2: " + this.AuxVal2;
            xtfPingHeaderString += "\n\tAuxVal3: " + this.AuxVal3;
            xtfPingHeaderString += "\n\tReserved3: " + this.Reserved3;
            xtfPingHeaderString += "\n\tReserved4: " + this.Reserved4;
            xtfPingHeaderString += "\n\tReserved5: " + this.Reserved5;
            xtfPingHeaderString += "\n\tSpeedLog: " + this.SpeedLog;
            xtfPingHeaderString += "\n\tTurbidity: " + this.Turbidity;
            xtfPingHeaderString += "\n\tShipSpeed: " + this.ShipSpeed;
            xtfPingHeaderString += "\n\tShipGyro: " + this.ShipGyro;
            xtfPingHeaderString += "\n\tShipYcoordinate: " + this.ShipYcoordinate;
            xtfPingHeaderString += "\n\tShipXcoordinate: " + this.ShipXcoordinate;
            xtfPingHeaderString += "\n\tShipAltitude: " + this.ShipAltitude;
            xtfPingHeaderString += "\n\tShipDepth: " + this.ShipDepth;
            xtfPingHeaderString += "\n\tFixTimeHour: " + this.FixTimeHour;
            xtfPingHeaderString += "\n\tFixTimeMinute: " + this.FixTimeMinute;
            xtfPingHeaderString += "\n\tFixTimeSecond: " + this.FixTimeSecond;
            xtfPingHeaderString += "\n\tFixTimeHsecond: " + this.FixTimeHsecond;
            xtfPingHeaderString += "\n\tSensorSpeed: " + this.SensorSpeed;
            xtfPingHeaderString += "\n\tKP: " + this.KP;
            xtfPingHeaderString += "\n\tSensorYcoordinate: " + this.SensorYcoordinate;
            xtfPingHeaderString += "\n\tSensorXcoordinate: " + this.SensorXcoordinate;
            xtfPingHeaderString += "\n\tSonarStatus: " + this.SonarStatus;
            xtfPingHeaderString += "\n\tRangeToFish: " + this.RangeToFish;
            xtfPingHeaderString += "\n\tBearingToFish: " + this.BearingToFish;
            xtfPingHeaderString += "\n\tCableOut: " + this.CableOut;
            xtfPingHeaderString += "\n\tLayback: " + this.Layback;
            xtfPingHeaderString += "\n\tCableTension: " + this.CableTension;
            xtfPingHeaderString += "\n\tSensorDepth: " + this.SensorDepth;
            xtfPingHeaderString += "\n\tSensorPrimaryAltitude: " + this.SensorPrimaryAltitude;
            xtfPingHeaderString += "\n\tSensorAuxAltitude: " + this.SensorAuxAltitude;
            xtfPingHeaderString += "\n\tSensorPitch: " + this.SensorPitch;
            xtfPingHeaderString += "\n\tSensorRoll: " + this.SensorRoll;
            xtfPingHeaderString += "\n\tSensorHeading: " + this.SensorHeading;
            xtfPingHeaderString += "\n\tHeave: " + this.Heave;
            xtfPingHeaderString += "\n\tYaw: " + this.Yaw;
            xtfPingHeaderString += "\n\tAttitudeTimeTag: " + this.AttitudeTimeTag;
            xtfPingHeaderString += "\n\tDOT: " + this.DOT;
            xtfPingHeaderString += "\n\tNavFixMilliseconds: " + this.NavFixMilliseconds;
            xtfPingHeaderString += "\n\tComputerClockHour: " + this.ComputerClockHour;
            xtfPingHeaderString += "\n\tComputerClockMinute: " + this.ComputerClockMinute;
            xtfPingHeaderString += "\n\tComputerClockSecond: " + this.ComputerClockSecond;
            xtfPingHeaderString += "\n\tComputerClockHsec: " + this.ComputerClockHsec;
            xtfPingHeaderString += "\n\tFishPositionDeltaX: " + this.FishPositionDeltaX;
            xtfPingHeaderString += "\n\tFishPositionDeltaY: " + this.FishPositionDeltaY;
            xtfPingHeaderString += "\n\tFishPositionErrorCode: " + this.FishPositionErrorCode;
            xtfPingHeaderString += "\n\tOptionalOffset: " + this.OptionalOffset;
            xtfPingHeaderString += "\n\tCableOutHundredths: " + this.CableOutHundredths;
            xtfPingHeaderString += "\n\tReservedSpace2: " + this.ReservedSpace2;
            return xtfPingHeaderString;
        }
    }
}