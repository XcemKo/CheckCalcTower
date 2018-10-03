using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;

namespace CheckCalcTower
{

    class NotEqualsOrder : Exception {
        public NotEqualsOrder(string mes) : base(mes) { }
    }

    class Matrix {
        double[,] arr;
        int x, y;
        public Matrix(int n) {
            arr = new double[n, n];
            x = y = n;
        }

        public Matrix(int n, int m)
        {
            arr = new double[n, m];
            x = n;
            y = m;
        }

        public double this[int i, int j]{
            get {return arr[i,j];       }
            set { arr[i, j] = value;    }
        }

        public static Matrix operator +(Matrix rhs, Matrix lhs) {

            if (rhs.x != lhs.x && rhs.y != lhs.y)
                throw new NotEqualsOrder("Разный размер матриц");

            Matrix ret = new Matrix(rhs.x);

            for (int i = 0; i < rhs.x; i++)
                for (int j = 0; j < rhs.y; j++)
                    ret[i, j] = rhs[i, j] + lhs[i, j];
            return ret;
        }

        public static Matrix operator *(Matrix r, Matrix l) {
            if (r.y != l.x)
                throw new NotEqualsOrder("Разный размер матриц");

            Matrix ret = new Matrix(r.x,l.y);
            for (int i = 0; i < r.x; i++)
                for (int j = 0; j < l.y; j++)
                {
                    ret[i, j] = 0;
                    for (int k = 0; k < r.y; k++)
                        ret[i, j] += r[i, k] * l[k, j];
                }
            return ret;
        }

        public static Matrix operator *(Matrix r, double l)
        {
            Matrix ret = new Matrix(r.x,r.y);
            for (int i = 0; i < r.x; i++)
                for (int j = 0; j < r.y; j++)
                    r[i, j] *= l;
            return ret;
        }

        public static Matrix operator *(double r, Matrix l) {
            return (l * r);

        }

    }

    class Vect {
        double[] arr;
        public Vect(int n) {
            arr = new double[n];
        }

        public double this[int i]
        {
            get { return arr[i]; }
            set { arr[i] = value; }
        }
    }


    class NewCenter: CheckCenter
    {
        List<Matrix> A;
        List<Matrix> A_;
        List<Vect> Z;
        public NewCenter(List<Tower> _tower) : base(_tower){

        }


        //public new void GetMetkiFromString(string[] str) {

        //}

        public new void CalcKoef() {
            Matrix tmp = new Matrix(1);
            Vect vec = new Vect(1);
            double parentTime = 0f;
            int count = 0;
            int oldIndex = 0,firstIndex = 0;
            numberOfPacket = metki[0].packet;
            for (int i = 0; i < metki.Count; i++)
            {
                if (metki[i].from == metki[i].to)
                {
                    if (i != 0)
                    {
                        Z.Add(vec);
                        A.Add(tmp);
                    }
                    firstIndex = i;
                    for (int j = i; metki[j].packet != numberOfPacket; j++)
                        oldIndex = j;
                    parentTime = metki[i].time;
                    tmp = new Matrix(towers.Count, oldIndex - i);
                    vec = new Vect(oldIndex - i);
                    continue;
                }
                else
                {
                    tmp[i - firstIndex, metki[i].from] = 1;
                    tmp[i - firstIndex, metki[i].to]   = -1;
                    double c = (Vector3.Distance(towers[metki[i].from].position, towers[metki[i].to].position) / Other.LightSpeed);
                    vec[i - firstIndex] = (metki[i].time - parentTime - c);
                }
            }
        }

        public new double[] Delta() {
            int size = towers.Count;
            double[]  ret    = new double[size];
            double[,] answer = new double[size, size];

            for (int i = 0; i < numberOfPacket; i++) {
                


            }

            int info = 0; alglib.densesolverreport reporter;
            alglib.rmatrixsolve(answer, size - 2, solKoef, out info, out reporter, out ret);

            return ret;
        }
    }
}
