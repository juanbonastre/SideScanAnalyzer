using SideScanAnalyzer.JSON_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Models
{
    public class JSONImage : JSONModel
    {
        public string path;
        public JSONImage(string path)
        {
            this.path = path;
        }

        public string ToJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
