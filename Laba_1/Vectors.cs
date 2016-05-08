using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

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

        public static void OutputVector(Vector v, Stream output) {
            using (BinaryWriter bw = new BinaryWriter(output))
            {
                bw.Write(v.Length);
                for (int i = 0; i < v.Length; i++)
                {
                    bw.Write(v[i]);
                }
            }
        }
        public static Vector InputVector(Stream input) {
            Vector v = null;

            using (BinaryReader br = new BinaryReader(input))
            {
                int len = br.ReadInt32();

                double[] coordinates = new double[len];
                for (int i = 0; i < len; i++)
                {
                    coordinates[i] = br.ReadDouble();
                }

                v = new ArrayVector(coordinates);
            }   
              
            return v;
        }
        public static void WriteVector(Vector v, TextWriter output)
        {
            output.Write(v.ToString());
        }

        public static Vector ReadVector(TextReader input)
        {
            String[] strings = input.ReadLine().Split(' ');

            int length = Int32.Parse(strings[0]);

            double[] coordinates = new double[length];
            for (int i = 1; i <= length; ++i)
            {
                coordinates[i - 1] = Double.Parse(strings[i]);
            }

            Vector v = new LinkedListVector(coordinates);
            return v;
        }

        public static void SerializeVector(Vector v, Stream output)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(output, v);
        }

        public static Vector DeserializeVector(Stream input)
        {
            BinaryFormatter deserializer = new BinaryFormatter();
            return (Vector)deserializer.Deserialize(input);
        }
    }
}

