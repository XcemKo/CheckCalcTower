using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Globalization;

namespace CheckCalcTower
{

    class NotEqualsOrder : Exception {
        public NotEqualsOrder(string mes) : base(mes) { }
    }

  //  class Matrix {
  //      double[,] arr;
  //      int x, y;

  //      public int X { get => x; }
  //      public int Y { get => y; }

  //      public Matrix(int n) {
  //          arr = new double[n, n];
  //          x = y = n;
  //      }

  //      public Matrix(int n, int m)
  //      {
  //          arr = new double[n, m];
  //          x = n;
  //          y = m;
  //      }

  //      public double this[int i, int j]{
  //          get {return arr[i,j];       }
  //          set { arr[i, j] = value;    }
  //      }

  //      public Matrix (Matrix rhs) {
  //          arr = new double[rhs.X, rhs.Y];
  //          x = rhs.X;
  //          y = rhs.Y;
  //          for (int i = 0; i < x; i++)
  //              for (int j = 0; j < y; j++)
  //                  arr[i, j] = rhs.arr[i, j];
  //      }

  //      public static Matrix operator +(Matrix rhs, Matrix lhs) {

  //          if (rhs.x != lhs.x && rhs.y != lhs.y)
  //              throw new NotEqualsOrder("Разный размер матриц");

  //          Matrix ret = new Matrix(rhs.x);

  //          for (int i = 0; i < rhs.x; i++)
  //              for (int j = 0; j < rhs.y; j++)
  //                  ret[i, j] = rhs[i, j] + lhs[i, j];
  //          return ret;
  //      }

  //      public static Matrix operator *(Matrix r, Matrix l) {
  //          if (r.y != l.x)
  //              throw new NotEqualsOrder("Разный размер матриц");

  //          Matrix ret = new Matrix(r.x,l.y);
  //          for (int i = 0; i < r.x; i++)
  //              for (int j = 0; j < l.y; j++)
  //              {
  //                  ret[i, j] = 0;
  //                  for (int k = 0; k < r.y; k++)
  //                      ret[i, j] += r[i, k] * l[k, j];
  //              }
  //          return ret;
  //      }

  //      public static Matrix operator *(Matrix r, double l)
  //      {
  //          Matrix ret = new Matrix(r.x,r.y);
  //          for (int i = 0; i < r.x; i++)
  //              for (int j = 0; j < r.y; j++)
  //                  r[i, j] *= l;
  //          return ret;
  //      }

  //      public static Matrix operator *(double r, Matrix l) {
  //          return (l * r);

  //      }

		//public static Matrix operator *(Matrix r, Vect l) {
		//	if (r.y != l.n)
		//		throw new NotEqualsOrder("Разный размер матрицы и вектора");
		//	Matrix ret = new Matrix(r.x,r.y);
		//	for (int i = 0; i < r.x; i++)
		//		for (int j = 0; j < r.y; j++)
		//			r[i, j] *= l[i];
		//	return ret;

		//}

		//public Matrix Transpont() {
  //          Matrix ret = new Matrix(this.y,this.x);
  //          for (int i = 0; i < this.y; i++)
  //              for (int j = 0; j < this.x; j++)
		//			ret[i, j] = this[j, i];
		//	return ret;
  //      }

		//public Matrix Revert() {
		//	Matrix ret = new Matrix(this.y,this.x);


		//	for (int i = 0; i < this.y; i++)
		//		for (int j = 0; j < this.x; j++)
		//			ret[i, j] = this[j, i];
		//	return ret;
		//}

		//public int Determinant(){
			
		//}

  //      public Matrix DelRowCol(int index) {
  //          Matrix ret = new Matrix(x - 1, y - 1);
  //          for (int i = 0; i < x; i++)
  //          {
  //              for (int j = 0; j < y; j++)
  //              {
  //                  if (j < index)
  //                      if (i < index)
  //                          ret[i, j] = this[i, j];
  //                      else
  //                          ret[i - 1, j] = this[i, j];
  //                  else if (j == index)
  //                      ;
  //                  else
  //                  {
  //                      if (i < index)
  //                          ret[i, j - 1] = this[i, j];
  //                      else
  //                          ret[i - 1, j - 1] = this[i, j];
  //                  }


  //              }
  //          }
  //          return ret;
  //      }

  //  }

  //  class Vect {
  //      double[] arr;
		//public int n;
  //      public Vect(int _n) {
		//	n= _n;
  //          arr = new double[n];
  //      }

  //      public double this[int i]
  //      {
  //          get { return arr[i]; }
  //          set { arr[i] = value; }
  //      }
  //  }


    class NewCenter: CheckCenter
    {
        List<Matrix<double>> A;
        List<Vector<double>> Z;
        public NewCenter(List<Tower> _tower) : base(_tower){

			A = new List<Matrix<double>>();
			Z = new List<Vector<double>>();
        }


        //public new void GetMetkiFromString(string[] str) {

        //}

        public new void CalcKoef() {
            Console.WriteLine("{0}", metki.Count);
            Matrix<double> tmp = Matrix<double>.Build.Dense(towers.Count, towers.Count);
            Vector<double> vec = Vector<double>.Build.Dense(towers.Count);
            double parentTime = 0f;
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
                        numberOfPacket++;
                    }
                    firstIndex = i;
                    for (int j = i+1; j < metki.Count && metki[j].packet == numberOfPacket; j++)
                        oldIndex = j;
                    parentTime = metki[i].time;
                    tmp = Matrix<double>.Build.Dense(oldIndex - firstIndex, towers.Count);
                    vec = Vector<double>.Build.Dense(oldIndex - firstIndex);
                    continue;
                }
                else
                {
                    tmp[i - firstIndex-1, metki[i].from] = -1;
                    tmp[i - firstIndex-1, metki[i].to]   = 1;
                    double c = (Vector3.Distance(towers[metki[i].from].position, towers[metki[i].to].position) / Other.LightSpeed);
                    vec[i - firstIndex-1] = (metki[i].time - parentTime - c);
                }
            }
        }

        public new void reset() {
            A = new List<Matrix<double>>();
            Z = new List<Vector<double>>();
            metki = new List<MetkaNew>();
            numberOfPacket = 1;
            time = 0.000_01f;
        }

        public new double[] Delta() {
            int size = towers.Count;
            double[]  ret    = new double[size];
			List<Matrix<double>> l1 = new List<Matrix<double>>();
			List<Vector<double>> l2 = new List<Vector<double>>();

            Vector<double> result = Vector<double>.Build.Dense(towers.Count-1);
            Matrix<double> tmp;
            Matrix<double> first;
            Vector<double> second;
            int i = 0;
            for (i = 0; i < numberOfPacket-1; i++) {
                A[i] = A[i].RemoveColumn(6);
                tmp = (A[i] * A[i].Transpose()).Inverse();
                tmp = A[i].RemoveColumn(0).Transpose() * tmp;
                //tmp = A[i].RemoveColumn(0);

                l1.Add(tmp * A[i].RemoveColumn(0));
				l2.Add(tmp * Z[i]);
            }
            first = l1[0];
            second = l2[0];
            for (i = 0; i < numberOfPacket-1; i++) {
				first += l1[i];
				second += l2[i];
			}
			result = first.Inverse() * second;
            i = 0;
            foreach(var r in result){
                Console.WriteLine("{0:.000_000}", r);
                ret[i++] = r;
            }
            //Console.WriteLine(result.ToString());
            return ret;
        }
    }
}
