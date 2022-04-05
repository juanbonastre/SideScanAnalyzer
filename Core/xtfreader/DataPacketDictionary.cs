using SideScanAnalyzer.Core.xtfreader.DataPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core.xtfreader
{
    internal static class DataPacketDictionary
    {
        private static Dictionary<int, Type> dataPacketDict = new Dictionary<int, Type>();

        public static Type getDataPacketType(int headerType)
        {
            if (!dataPacketDict.ContainsKey(headerType))
            {
                if(!Enum.IsDefined(typeof(PacketTypes), headerType)){
                    return null;
                }
                else
                {
                    switch (headerType)
                    {
                        case (int)PacketTypes.XTF_HEADER_NOTES:
                            dataPacketDict.Add(headerType, typeof(XTFHeaderNotes));
                            break;
                        case (int)PacketTypes.XTF_HEADER_SONAR:
                            dataPacketDict.Add(headerType, typeof(XTFPingHeader));
                            break;
                    }
                }
            }
            return dataPacketDict[headerType];
        }
    }
}
