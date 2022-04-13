using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScanAnalyzer.Models
{
    public class Prediction
    {
        public double prediction;
        public double x1;
        public double x2;
        public double y1;
        public double y2;

        public Prediction(double prediction, double x1, double x2, double y1, double y2)
        {
            this.prediction = prediction;
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

        public override string ToString()
        {
            return "Result: "+prediction+"\nCoordinates: ["+x1+"-"+x2+"] ["+y1+"-"+y2+"]";
        }

    }
}
