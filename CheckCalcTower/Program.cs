﻿using System;
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

                0.000_021,//src31_32_0,
                0.000_122,//src31_32_1,
                0.000_033,//src31_32_2,
                0.000_044,//src31_32_3,

                0.000_065,//src31_40_0,

                0.000_130,//src31_130_0,

                -0.000_1321,//src31_132_0,
                -0.000_1322,//src31_132_1,
                -0.000_1323,//src31_132_2,
                -0.000_1324,//src31_132_3,

                0.000_134,//src31_134_0
            };

            for (int j = 0; j < deltaMap.Length; j++)
                Other.towers[j].Delta = deltaMap[j];// + omegaMap[j];

            foreach (var t in Other.towers)
                Console.WriteLine("{0} --> {1}", t.Id,t.Delta);
            
            int towersSize = Other.towers.Count();

            CheckCenter calcCenter = new CheckCenter(Other.towers);
            NewCenter newCalcCenter = new NewCenter(Other.towers);

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            //Console.WriteLine(float.Parse("3.1488", culture));
            foreach (var tow in Other.towers)
                tow.Print();
            double[] omegaMap = new double[towersSize];
            double[,] deltas = new double[countIter,towersSize];
            double[,] omegas = new double[countIter+1, towersSize];
            double[] tmpDelta = new double[towersSize];

            FileStream file1 = new FileStream("C:\\Users\\Xcem\\source\\repos\\CheckCalcTower\\CheckCalcTower\\deltas.txt", FileMode.Create, FileAccess.ReadWrite);
            FileStream file2 = new FileStream("C:\\Users\\Xcem\\source\\repos\\CheckCalcTower\\CheckCalcTower\\other.txt", FileMode.Create, FileAccess.ReadWrite);
            FileStream file3 = new FileStream("C:\\Users\\Xcem\\source\\repos\\CheckCalcTower\\CheckCalcTower\\resss.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter writerDelta = new StreamWriter(file1);
            StreamWriter writerOther = new StreamWriter(file2);
            StreamWriter writerCmp = new StreamWriter(file3);

            StreamReader fs = new StreamReader("C:\\Users\\Xcem\\source\\repos\\CsvParse\\CsvParse\\log8clean.csv");
            fs.ReadLine();
            string tmp;
            int i = 0; int iter = 0;
            while (fs.Peek() != -1)
            {
                tmp = fs.ReadLine();
                string[] array = tmp.Split(',');
                if (CheckLenght(array)){

                    calcCenter.GetMetkiFromString(array);
                    newCalcCenter.GetMetkiFromString(array);
                    i++;

                    for (int j = 1; j < deltaMap.Length; j++)
                    {
                        //omegaMap[j] = Other.rnd.Next(0, 15)/1000_000f;
                        omegaMap[j] = Normal(0, 100) / 1000_000f;

                        //omegas[iter + 1, j] = omegaMap[j] * 1000_000f;
                        Other.towers[j].Delta = deltaMap[j] + omegaMap[j];
                    }

                    if (i > 1000)
                    {
                        calcCenter.CalcKoef();
                        tmpDelta = calcCenter.Delta();

                        foreach (var t in tmpDelta)
                            writerCmp.Write("{0}\t", t);
                        writerCmp.Write("\t\t");

                        newCalcCenter.CalcKoef();
                        tmpDelta = newCalcCenter.Delta();
                        foreach (var t in tmpDelta)
                            writerCmp.Write("{0}\t", t);
                        writerCmp.WriteLine("\t\t");

                        //break;

                        /*for (int j = 0; j < towersSize; j++)
                        {
                            deltas[iter, j] = tmpDelta[j] * 1000_000f;
                            writerDelta.Write("{0}\t", deltas[iter, j]);
                        }
                        writerDelta.WriteLine("{0}",iter*i);
                        //Console.WriteLine(iter * i);*/
                        i = 0;
                        calcCenter.reset();
                        newCalcCenter.reset();
                        iter++;
                        
                        //break;
                    }
                    if (iter >= countIter)
                    {
                        
                        Console.WriteLine(iter);
                        break;
                    }
                }
            }
            fs.Close();

            Console.WriteLine();
            //Console.WriteLine("==OMEGAS==");
            //writerOther.WriteLine("==OMEGAS==");
            //for (i = 0; i < countIter; i++)
            //{
            //    for (int j = 0; j < towersSize; j++)
            //    {
            //        Console.Write("{0:00.0} {1:00.0} |", deltas[i, j], omegas[i, j]);
            //        writerOther.Write("{0}\t", omegas[i, j]);
            //    }
            //    writerOther.WriteLine();
            //    Console.WriteLine();
            //}
            writerOther.WriteLine();
            double[] avDelta = new double[towersSize];
            for (i=0;i< countIter; i++) {
                for (int j = 0; j < towersSize; j++)
                    avDelta[j] += deltas[i, j];
            }
            Console.WriteLine();
            Console.WriteLine("==avDelta==");
            writerOther.WriteLine("==avDelta==");
            for (int j = 0; j < towersSize; j++)
                avDelta[j] = avDelta[j] / countIter;
            for (int j = 0; j < towersSize; j++)
            {
                Console.Write("{0:00.00}\t", avDelta[j]);
                writerOther.Write("{0}\t", avDelta[j]);
                if ((j + 1) % 5 == 0) Console.WriteLine();
            }
            writerOther.WriteLine();
            writerOther.WriteLine("Sigma^2");
            Console.WriteLine();
            double[] sigma = new double[towersSize];
            for (int j = 0; j < towersSize; j++)
            {
                for (i = 0; i < countIter; i++)
                    sigma[j] += Math.Pow(deltas[i, j] - avDelta[j], 2);
                sigma[j] = sigma[j] / (countIter - 1);
                writerOther.Write("{0}\t", sigma[j]);
            }
            writerOther.WriteLine();
            writerOther.WriteLine("==SIGMA==");
            Console.WriteLine("==SIGMA==");
            for (int j = 0; j < towersSize; j++)
            {
                Console.Write("{0:.0}\t", Math.Sqrt(sigma[j]));
                writerOther.Write("{0}\t", Math.Sqrt(sigma[j]));
                if ((j + 1) % 5 == 0) Console.WriteLine();
            }
            Console.WriteLine();
            writerOther.Close();
            writerDelta.Close();
            writerCmp.Close();
        }

    }
}
