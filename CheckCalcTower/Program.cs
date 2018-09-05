using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace CheckCalcTower
{
    class Program
    {
        static bool CheckLenght(string[] array)
        {
            int countLenght = 0;
            for (int i = 2; i < 14; i++)
                if (array[i].Length > 0)
                    countLenght++;
            return countLenght > 1 ? true : false;
        }

        static void Main(string[] args)
        {
            double[] deltaMap = {
                0.000_00,//src31_6_0,

                0.000_132,//src31_32_0,
                0.000_232,//src31_32_1,
                0.000_332,//src31_32_2,
                0.000_432,//src31_32_3,

                0.000_030,//src31_40_0,

                0.000_130,//src31_130_0,

                -0.000_1321,//src31_132_0,
                -0.000_1322,//src31_132_1,
                -0.000_1323,//src31_132_2,
                -0.000_1324,//src31_132_3,

                0.000_134,//src31_134_0
            };

            double[] omegaMap = deltaMap;
            for (int j = 0; j < omegaMap.Length; j++)
                omegaMap[j] = Other.rnd.Next(-10, 10);

            for (int j = 0; j < deltaMap.Length; j++)
                Other.towers[j].Delta = deltaMap[j] + omegaMap[j];

            foreach (var t in Other.towers)
                Console.WriteLine("{0} --> {1}", t.Id,t.Delta);

            int towersSize = Other.towers.Count();

            CheckCenter calcCenter = new CheckCenter(Other.towers);

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            //Console.WriteLine(float.Parse("3.1488", culture));
            foreach (var tow in Other.towers)
                tow.Print();

            double[] deltas = new double[towersSize];
            StreamReader fs = new StreamReader("C:\\Users\\Xcem\\source\\repos\\CsvParse\\CsvParse\\log8clean.csv");

            fs.ReadLine();
            string tmp;
            int i = 0; int iter = 0;
            while (fs.Peek() != -1 )
            {
                tmp = fs.ReadLine();
                string[] array = tmp.Split(',');
                if (CheckLenght(array)){
                    calcCenter.GetMetkiFromString(array); i++;
                    if (i > 100){

                        calcCenter.CalcKoef();
                        deltas = calcCenter.Delta();

                        iter++;
                        i = 0;
                        calcCenter.reset();

                        for (int j = 0; j < deltaMap.Length; j++){
                            omegaMap[j] = Other.rnd.Next(-10, 10);
                            Other.towers[j].Delta = deltaMap[j] + omegaMap[j];
                        }

                        break;
                    }
                    if (iter == 20)
                        break;
                }
            }

            
            for (int j = 0; j < towersSize; j++)
            {
                Console.Write("{0:.000_000}\t", deltas[j]);
                if ((j+1) % 5 == 0) Console.WriteLine();
            }
            Console.WriteLine();

        }

    }
}
