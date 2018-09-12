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

        static double Normal(double mean, double stdDev) {
            double u1 = 1.0 - Other.rnd.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - Other.rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            return (mean + stdDev * randStdNormal);
        }

        static void Main(string[] args)
        {
            int countIter = 50;
            double[] deltaMap = {
                0.000_00,//src31_6_0,

                //0.000_132,//src31_32_0,
                0.000_001,//src31_32_0,
                0.000_002,//src31_32_1,
                0.000_003,//src31_32_2,
                0.000_004,//src31_32_3,

                0.000_005,//src31_40_0,

                0.000_130,//src31_130_0,

                -0.000_1321,//src31_132_0,
                -0.000_1322,//src31_132_1,
                -0.000_1323,//src31_132_2,
                -0.000_1324,//src31_132_3,

                0.000_134,//src31_134_0
            };

            
            //for (int j = 0; j < omegaMap.Length; j++)
            //    omegaMap[j] = Other.rnd.Next(-10, 10);
            for (int j = 0; j < deltaMap.Length; j++)
                Other.towers[j].Delta = deltaMap[j];// + omegaMap[j];

            foreach (var t in Other.towers)
                Console.WriteLine("{0} --> {1}", t.Id,t.Delta);
            
            int towersSize = Other.towers.Count();

            CheckCenter calcCenter = new CheckCenter(Other.towers);

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            //Console.WriteLine(float.Parse("3.1488", culture));
            foreach (var tow in Other.towers)
                tow.Print();
            double[] omegaMap = new double[towersSize];
            double[,] deltas = new double[countIter,towersSize];
            double[,] omegas = new double[countIter+1, towersSize];
            double[] tmpDelta = new double[towersSize];

            StreamReader fs = new StreamReader("C:\\Users\\Xcem\\source\\repos\\CsvParse\\CsvParse\\log8clean.csv");
            fs.ReadLine();
            string tmp;
            int i = 0; int iter = 0;
            while (fs.Peek() != -1)
            {
                tmp = fs.ReadLine();
                string[] array = tmp.Split(',');
                if (CheckLenght(array)){
                    calcCenter.GetMetkiFromString(array); i++;
                    if (i > 100){

                        calcCenter.CalcKoef();
                        tmpDelta = calcCenter.Delta();
                        for (int j=0;j<towersSize;j++)
                            deltas[iter,j] = tmpDelta[j] * 1000_000f;
                        //Console.WriteLine(iter * i);
                        i = 0;
                        calcCenter.reset();
                        
                        for (int j = 1; j < deltaMap.Length; j++){
                            //omegaMap[j] = Other.rnd.Next(0, 15)/1000_000f;
                            omegaMap[j] = Normal(0,10)/ 1000_000f;
                            omegas[iter+1, j] = omegaMap[j] * 1000_000f;
                            Other.towers[j].Delta = deltaMap[j] + omegaMap[j];
                        }
                        iter++;
                        Console.WriteLine();
                        //break;
                    }
                    if (iter >= countIter)
                    {
                        Console.WriteLine(iter);
                        break;
                    }
                      
                }
            }
            Console.WriteLine();
            Console.WriteLine("==OMEGAS==");
            for (i = 0; i < countIter; i++)
            {
                for(int j=0; j<towersSize;j++)
                    Console.Write("{0:00.0} {1:00.0} |", deltas[i, j],omegas[i,j]);
                Console.WriteLine();
            }
            double[] avDelta = new double[towersSize];
            for (i=0;i< countIter; i++) {
                for (int j = 0; j < towersSize; j++)
                    avDelta[j] += deltas[i, j];
            }
            Console.WriteLine();
            Console.WriteLine("==avDelta==");
            for (int j = 0; j < towersSize; j++)
                avDelta[j] = avDelta[j] / countIter;
            for (int j = 0; j < towersSize; j++)
            {
                Console.Write("{0:00.00}\t", avDelta[j]);
                if ((j + 1) % 5 == 0) Console.WriteLine();
            }
            Console.WriteLine();
            double[] sigma = new double[towersSize];
            for (int j = 0; j < towersSize; j++)
            {
                for (i = 0; i < countIter; i++)
                    sigma[j] += Math.Pow(deltas[i, j] - avDelta[j],2);
                sigma[j] = sigma[j] / (countIter - 1);
            }
            Console.WriteLine("==SIGMA==");
            for (int j = 0; j < towersSize; j++)
            {
                Console.Write("{0:.0}\t", sigma[j]/countIter);
                if ((j + 1) % 5 == 0) Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
