using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_6
{
    [Serializable()]
    abstract class AbstractVector : Vector
    {
        public abstract double this[int index] { get; set; }

        public abstract int Length { get; }

        public double GetNorm()
        {
            double norm = 0;
            for(int i = 0; i < Length; i++)
            {
                norm += this[i] * this[i];
            }
            norm = Math.Sqrt(norm);
            return norm;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Length);
            builder.Append(' ');

            for (int i = 0; i < Length; i++)
            {
                builder.Append(this[i]);
                builder.Append(' ');
            }

            return builder.ToString();
        }
        protected void AssertIndex(int index, string methodName)
        {
            if (index < 0 || Length <= index)
                throw new IndexOutOfRangeException("Некорректный индекс в " + methodName);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Vector other = obj as Vector;
            if (other == null) return false;

            if (this.GetHashCode() != other.GetHashCode()) return false;

            if(this.Length != other.Length) return false;
            
            for(int i = 0; i < Length; i++)
            {
                if(this[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public int CompareTo(Vector other)
        {
            return this.Length - other.Length;
        }

        public abstract object Clone();
    }
}
