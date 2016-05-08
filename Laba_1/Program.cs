using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_8
{
    class Program
    {
        static int ReadInt(string message, int minValue, int maxValue)
        {
            while (true)
                try {
                    int value = int.Parse(ReadLine(message));
                    if (value >= minValue && value <= maxValue)
                    {
                        return value;
                    }
                    else
                    {
                        Print("Введено некорректное значение. Повторите ввод");
                    }
                }
                catch { }
        }

        static double ReadDouble(string message)
        {
            while (true)
                try
                {
                    return double.Parse(ReadLine(message));
                }
                catch { }
        }

        static string ReadLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static void Print(string message) {
            Console.WriteLine(message);
        }

        enum VectorType
        {
            ARRAY, LINKED
        }

        static Vector ReadVector(VectorType type) {
            Vector vector = null;

            while (vector == null)
            {
                int size = ReadInt("Введите 0, если хотите создать массив(Vector) фиксированной размерности 5, в ином случае введите размерность массива", 0, Int32.MaxValue);
                if (size == 0)
                {
                    switch (type)
                    {
                        case VectorType.ARRAY: vector = new ArrayVector(); break;
                        case VectorType.LINKED: vector = new LinkedListVector(); break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case VectorType.ARRAY: vector = new ArrayVector(size); break;
                        case VectorType.LINKED: vector = new LinkedListVector(size); break;
                    }
                }
            }
            
            SetCoordinates(vector);

            return vector;
        }

        static void SetCoordinates(Vector vector)
        {
            for (int i = 1; i <= vector.Length; i++)
            {
                vector[i - 1] = ReadDouble("Введите " + i + " элемент массива");
            }
        }

        delegate void MenuItem();

        static List<Tuple<string, MenuItem>> menuItems;

        static List<Vector> vectors;

        static Program()
        {
            vectors = new List<Vector>();

            menuItems = new List<Tuple<string, MenuItem>>();
            menuItems.Add(new Tuple<string, MenuItem>("Добавить вектор", AddVector));
            menuItems.Add(new Tuple<string, MenuItem>("Вывести вектор", PrintVector));
            menuItems.Add(new Tuple<string, MenuItem>("Удалить вектор", RemoveVector));
            menuItems.Add(new Tuple<string, MenuItem>("Вывести все вектора", PrintAllVectors));
        }

        static void AddVector()
        {
            int vectorType = ReadInt("Введите 1, если хотите добавить ArrayVector. Введите 2, если хотите добавить LinkedListVector.", 1, 2);

            Vector vector = null;
            if (vectorType == 1)
            {
                vector = ReadVector(VectorType.ARRAY);
            }
            else
            {
                vector = ReadVector(VectorType.LINKED);
            }

            vectors.Add(vector);
        }

        static void PrintVector()
        {
            int vectorIndex = ReadInt("Введите индекс вектора (от 1 до " + vectors.Count + "), элементы которого надо отобразить.", 1, vectors.Count);
            PrintVector(vectorIndex - 1);
        }

        static void PrintAllVectors()
        {
            for (int index = 0; index < vectors.Count; ++index)
            {
                PrintVector(index);
            }
        }

        static void PrintVector(int index)
        {
            Console.WriteLine("Размер и элементы вектора с индексом {0}: {1}", index + 1, vectors[index]);
        }

        static void RemoveVector()
        {
            int vectorIndex = ReadInt("Введите индекс вектора (от 1 до " + vectors.Count + "), который надо удалить.", 1, vectors.Count);
            vectors.RemoveAt(vectorIndex - 1);
            Console.WriteLine("Вектор удален");
        }

        static void PrintMenuItems()
        {
            for (int index = 0; index < menuItems.Count; ++index)
            {
                Console.WriteLine("{0}: {1}", index + 1, menuItems[index].Item1);
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                PrintMenuItems();

                int numberOfOperations = ReadInt("Введите количество операций, которые вы хотите провести. Для завершения работы введите 0.", 0, Int32.MaxValue);
                if (numberOfOperations == 0)
                {
                    break;
                }
                else
                {
                    MenuItem[] items = new MenuItem[numberOfOperations];
                    for (int i = 0; i < numberOfOperations; ++i)
                    {
                        PrintMenuItems();

                        int index = ReadInt("Введите индекс операции (от 1 до " + menuItems.Count + ")", 1, menuItems.Count);
                        items[i] = menuItems[index - 1].Item2;
                    }

                    foreach (MenuItem menuItem in items)
                    {
                        if (menuItem != AddVector && vectors.Count == 0)
                        {
                            Print("Список векторов пуст.");
                        }
                        else
                        {
                            menuItem();
                        }
                    }
                }
            }
        }

        static void TestStreams() {
            String str = "SlavaLalka";
            String fileName = "PandaLavanda";

            FileInfo byteFile = new FileInfo(fileName + ".bin");
            FileInfo charFile = new FileInfo(fileName + ".txt");
            FileInfo serializeFile = new FileInfo(fileName + ".data");
            
            Vector v = new ArrayVector(
                new double[] {
                    10, 9, 8, 7, 32, 11, 44, 3, 18
                }
            );

            Console.WriteLine(v);

            using (FileStream byteFileStream = new FileStream(byteFile.FullName, FileMode.OpenOrCreate))
            {
                Vectors.OutputVector(v, byteFileStream);
            }

            using (FileStream byteFileReadStream = new FileStream(byteFile.FullName, FileMode.Open))
            {
                Vector readedV = Vectors.InputVector(byteFileReadStream);
                Console.WriteLine(readedV);
            }

            using (TextWriter textWriteStream = new StreamWriter(new FileStream(charFile.FullName, FileMode.OpenOrCreate)))
            {
                Vectors.WriteVector(v, textWriteStream);
            }

            using (TextReader textReadStream = new StreamReader(new FileStream(charFile.FullName, FileMode.Open)))
            {
                Vector readedV = Vectors.ReadVector(textReadStream);
                Console.WriteLine(readedV);
            }

            using (FileStream serializeFileStream = new FileStream(serializeFile.FullName, FileMode.OpenOrCreate))
            {
                Vectors.SerializeVector(v, serializeFileStream);
            }

            using (FileStream deserializeFileStream = new FileStream(serializeFile.FullName, FileMode.Open))
            {
                Vector readedV = Vectors.DeserializeVector(deserializeFileStream);
                Console.WriteLine(readedV);
                Console.WriteLine("outputVector == inputVector: " + (v == readedV));
                Console.WriteLine("outputVector.equals(inputVector): " + v.Equals(readedV));
            }

            Console.ReadKey();

            //byteFileStream.Write(str)
        }

        static int GetMaximumLength(Vector[] vectors) {
            int max = vectors[0].Length;
            Vector maxV = vectors[0];
            for (int i = 1; i < vectors.Length; i++)
            {
                if (maxV.CompareTo(vectors[i]) < 0)
                {
                    max = vectors[i].Length;
                }
            }
            return max;
        }
        static int GetMinimumLength(Vector[] vectors)
        {
            int min = vectors[0].Length;
            Vector minV = vectors[0];
            for (int i = 0; i < vectors.Length; i++)
            {
                if (minV.CompareTo(vectors[i]) > 0)
                {
                    min = vectors[i].Length;
                }
            }
            return min;
        }
        static void PrintMinMax(Vector[] vectors)
        {
            int max = GetMaximumLength(vectors);
            int min = GetMinimumLength(vectors);
            for (int i = 1; i < vectors.Length; i++)
            {
                if (vectors[i].Length == max) {
                    Console.WriteLine("Maximum " + vectors[i]);
                }
                if (vectors[i].Length == min)
                {
                    Console.WriteLine("Minimum " + vectors[i]);
                }

            }
          
        }


        static Vector GetMaximum(Vector[] vectors)
        {
            Vector max = vectors[0];
            for (int i = 1; i < vectors.Length; i++)
            {
                if (max.CompareTo(vectors[i]) < 0)
                {
                    max = vectors[i];
                }
            }
            return max;
        }

        static Vector GetMinimum(Vector[] vectors)
        {
            Vector max = vectors[0];
            for (int i = 1; i < vectors.Length; i++)
            {
                if (max.CompareTo(vectors[i]) > 0)
                {
                    max = vectors[i];
                }
            }
            return max;
        }

        static void TestArrayOfVectors()
        {
            Print("Программа выполняет различные действия с векторами");
            int vectorsCount = ReadInt("Введите длину массива массивов; тип вектора будет выбран случайно", 1, Int32.MaxValue);

            Random random = new Random();

            Vector[] vectors = new Vector[vectorsCount];
            for (int i = 0; i < vectorsCount; ++i)
            {
                VectorType type = (random.Next(2) == 0 ? VectorType.ARRAY : VectorType.LINKED);
                Console.WriteLine(type);
                vectors[i] = ReadVector(type);
                
            }

            // Vector min = GetMinimum(vectors);
            // Vector max = GetMaximum(vectors);

            // Console.WriteLine("Minimum " + min);
            // Console.WriteLine("Maximum " + max);
            PrintMinMax(vectors);
            Array.Sort(vectors);

            Console.WriteLine("Сортированный по количеству координат массив векторов");
            foreach (Vector vector in vectors)
            {
                Console.WriteLine(vector);
            }

            Array.Sort(vectors, new Vectors.VectorsNormComparer());

            Console.WriteLine("Сортированный по длине массив векторов");
            foreach (Vector vector in vectors)
            {
                Console.WriteLine(vector);
            }

            Vector cloned = vectors[0];
            Vector clone = (Vector)cloned.Clone();

            Console.WriteLine("До изменения: Вектор, который клонировали: " + cloned);
            Console.WriteLine("До изменения: Клон: " + clone);

            clone[0] += 5;

            Console.WriteLine("После изменения: Вектор, который клонировали: " + cloned);
            Console.WriteLine("После изменения: Клон: " + clone);

            Console.ReadKey();
        }
    }
}
