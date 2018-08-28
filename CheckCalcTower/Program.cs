using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CheckCalcTower
{
    class Program
    {
        static void Main(string[] args)
        {

           
            int towersSize = Other.towers.Count();

            CheckCenter calcCenter = new CheckCenter(towers);

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            //Console.WriteLine(float.Parse("3.1488", culture));
            foreach (var tow in Other.towers)
                tow.Print();

            double[] deltas = new double[towersSize];
            StreamReader fs = new StreamReader("C:\\Users\\Xcem\\source\\repos\\CsvParse\\CsvParse\\log8clean.csv");
        }
    }
}
