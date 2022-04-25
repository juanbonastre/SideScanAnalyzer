using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Models
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
        private bool test;
        public bool Test
        {
            get
            {
                return test;
            }
            set
            {
                test = value;
            }
        }
    }
}
