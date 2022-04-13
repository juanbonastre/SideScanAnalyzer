using System;
using System.Collections.Generic;

namespace SideScanAnalyzer.Core.xtfreader
{
    public class PingPacketsManager
    {

        private List<List<int[]>> _processedPingData;
        public List<List<int[]>> processedPingData
        {
            get
            {
                return _processedPingData;
            }
            set
            {
                _processedPingData = value;
            }
        }

        private int _maxValue;
        public int maxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public PingPacketsManager()
        {
            _processedPingData = new List<List<int[]>>();
            _maxValue = 0;
        }

        public void AddPing(int channelNumber, byte[] byteArray, int bytesPerSample)
        {
            EnsureSize(channelNumber+1);
            int[] intArray = new int[byteArray.Length/bytesPerSample];
            for (int j = 0; j<byteArray.Length; j+=bytesPerSample)
            {
                int jPos = j/bytesPerSample;
                int value = 0;
                switch (bytesPerSample)
                {
                    case 2:
                        value = BitConverter.ToUInt16(byteArray[j..(j+bytesPerSample)]);
                        break;
                    case 1:
                        value = BitConverter.ToInt16(new byte[] { byteArray[j], 0 });
                        break;
                }
                intArray[jPos] = value;
                if(value > maxValue)
                {
                    maxValue = value;
                }
            }
            this.processedPingData[channelNumber].Add(intArray);
        }

        private void EnsureSize(int requiredSize)
        {
            if (this.processedPingData.Count < requiredSize)
            {
                for (int i = this.processedPingData.Count; i < requiredSize; i++)
                {
                    this.processedPingData.Add(new List<int[]>());
                }
            }
        }

    }
}
