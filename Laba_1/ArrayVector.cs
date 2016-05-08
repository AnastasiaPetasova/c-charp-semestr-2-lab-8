using System;
using System.Text;

namespace Laba_7
{
    [Serializable()]
    class ArrayVector : AbstractVector
    {
        private double[] coordinates;

        public override double this[int i]
        {
            get { return GetElement(i); }
            set { SetElement(i, value); }
        }

        public override int Length
        {
            get { return coordinates.Length; }
        }

        public ArrayVector(double[] coordinates)
        {
            this.coordinates = coordinates;
        }
         
        public ArrayVector(int n)
            : this(new double[n])
        { }

        public ArrayVector() : this(5)
        { }

        protected void SetElement(int index, double value) {
            AssertIndex(index, "set");
            coordinates[index] = value;
        }

        protected double GetElement(int index)
        {
            AssertIndex(index, "get");
            return coordinates[index];
        }

        public override object Clone()
        {
            Vector arrayVector = new ArrayVector(Length);
            for (int i = 0; i < Length; ++i)
            {
                arrayVector[i] = this[i];
            }

            return arrayVector;
        }
    }
}
	