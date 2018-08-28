using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CheckCalcTower
{
    class Tower
    {
        static private int id = 1;
        private int privId;
        private string name;
        public Vector3 position;

        public Tower(Vector3 _pos, string _name)
        {
            privId = id++;
            position = _pos;
            name = _name;
        }

        public void Print()
        {
            Console.WriteLine("Tower #{0}\tpos - {1}", privId, position.ToString());
        }

    }
}


