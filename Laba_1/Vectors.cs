using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_6    
{
    static class Vectors
    {
        public class VectorsNormComparer : IComparer<Vector>
        {
            public int Compare(Vector x, Vector y)
            {
                double xGetNorm = x.GetNorm();
                double yGetNorm = y.GetNorm();

                if (xGetNorm < yGetNorm) return -1;
                if (xGetNorm > yGetNorm) return 1;

                return 0;
            }
        }

        public static Vector Sum(Vector arr1,Vector arr2) {
            if (arr1.Length != arr2.Length) {
                throw new FormatException("Длины суммируемых векторов различны");
            }

            Vector arrN = new ArrayVector(arr2.Length);
            for (int i = 0; i < arr2.Length; i++) {
               
                arrN[i] = arr1[i] + arr2[i];
            }
            return arrN;
        }

        public static double Scalar(Vector arr1, Vector arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                throw new FormatException("Длины перемножаемых векторов различны");
            }
            double sum = 0;
            for (int i = 0; i < arr2.Length; i++)
            {

                sum = sum + arr1[i] * arr2[i];
            }
            return sum;
        }
        public static Vector NumberMul(Vector arr1, double coefficient)
        {
            Vector arrN = new ArrayVector(arr1.Length);
            for (int i = 0; i < arr1.Length; i++)
            {
                arrN[i] = arr1[i] * coefficient;
            }
            return arrN;
        }

        public static double GetNorm(Vector arr1)
        {
            return arr1.GetNorm();
        }
    }
}
