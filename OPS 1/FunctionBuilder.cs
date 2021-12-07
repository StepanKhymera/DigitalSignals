using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;

namespace OPS_1
{

    class FunctionBuilder
    {
        //public List<double> Y = new List<double> { 2.08, 2.94, 3.05, 3.58, 3.63, 3.71 };
        public List<double> Y = new List<double> { 5.01, 4.78, 3.52, 3.12, 3.19, 2.95 };

        public List<double> X = new List<double>();
        public List<double> A = new List<double>();
        int n;
        public double x_step;
        double area;
        public FunctionBuilder()
        {
            n = Y.Count();
            x_step = 2 * Math.PI / n;
            area = 0;

            for (int i = 0; i < n; ++i)
            {
                X.Add((-1) * Math.PI + i * x_step);
                area += Y[i] * x_step;
            }

            Matrix<double> left = DenseMatrix.OfArray(new double[,] {
                { n, X.Sum(), X.Sum(x => x*x) },
                { X.Sum(), X.Sum(x => x*x),  X.Sum(x => x*x*x) },
                { X.Sum(x => x*x),  X.Sum(x => x*x*x), X.Sum(x => x*x*x*x) } });

            Vector<double> input = CreateVector.DenseOfArray(new double[] { Y.Sum(), Y.Sum(y => y*X[Y.IndexOf(y)]) , Y.Sum(y => y * X[Y.IndexOf(y)] * X[Y.IndexOf(y)]) });
            A = left.Solve(input).ToList();
            Console.WriteLine(left);
            Console.WriteLine(input);
            Console.WriteLine(A);
            X.Add(Math.PI);
            Console.WriteLine($"a0 = {A_0()}");
            for(int i = 1; i <= 4; ++i)
            {
                Console.WriteLine($"a{i} = {A_N(i)}, b{i} = {B_N(i)} ");
            }
        }

        private double A_0()
        {
            return area / Math.PI  ;
        }

        private double A_N(int m)
        {
            double sum = 0;
            for (int i = 0; i < n; ++i)
            {
                sum += Y[i] * (Math.Sin(m * X[i + 1]) - Math.Sin(m * X[i]));

            }
            return  sum/(Math.PI * m);
        }

        private double B_N(int m)
        {
            double sum = 0;
            for (int i = 0; i < n; ++i)
            {
                sum += (-1) * Y[i] * (Math.Cos(m * X[i + 1]) - Math.Cos(m * X[i]));

            }

            return sum / (Math.PI * m);
        }

        public double MNK(double x)
        {
            return A[0] + A[1] * x + A[2] * x * x;
        }

        public double Original(double x)
        {
            if (!(x <= Math.PI / 2.0 && x > Math.PI * (-3) / 2))
            {
                x = (x % (2 * Math.PI));
                if (!(x <= Math.PI / 2.0 && x > Math.PI * (-3) / 2))
                {
                    x += (x > 0 ? (-2 * Math.PI) : (2 * Math.PI));
                }
            }
            return x > 0 ? 1 : -1;
        }

        public double Fourier(double x, int N )
        {
            double result = 0, lastIteration = 0;
            int i = 1;
            do
            {
                lastIteration = result;
                result += A_N(i) * Math.Cos(i * x) + B_N(i) * Math.Sin(i * x);

                ++i;
            } while (i < N);
            return A_0() / 2.0 + result;
        }

    }
}
