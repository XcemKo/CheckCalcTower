using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;

namespace CheckCalcTower
{
    struct MetkaNew
    {
        public int packet, from, to;
        public double time;
        public Vector3 pos;

        public MetkaNew(int _packet, int _from, int _to, double _time, Vector3 _pos)
        {
            packet = _packet;
            from = _from;
            to = _to;
            time = _time;
            pos = _pos;
        }

        public void Print()
        {
            if (from == to)
                Console.WriteLine("{0} {1}     -  {2}\t{3}", packet, from, time, pos);
            else
                Console.WriteLine("{0} {1}->{2}  -  {3}\t{4}", packet, from, to, time, pos);
        }
    }

    class CheckCenter
    {
        int numberOfPacket;
        private List<MetkaNew> metki;
        private List<Tower> towers;
        private CultureInfo culture;
        double[,] koef;
        double[] solKoef;
        double[] solveDelta;
        double[] fixedKoef;

        public CheckCenter(List<Tower> _tower)
        {
            numberOfPacket = 1;
            towers = _tower;
            culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";
            metki = new List<MetkaNew>();
            koef = new double[towers.Count, towers.Count];
            solKoef = new double[towers.Count];
            solveDelta = new double[towers.Count];
            fixedKoef = new double[towers.Count];
        }

    }
}
