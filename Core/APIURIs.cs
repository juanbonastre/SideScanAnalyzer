using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Core
{
    public class APIURIs
    {
        private const string location = "http://localhost:5000";
        public const string PING = location+"/ping";
        public const string ANALYZE = location+"/analyze";
    }
}
