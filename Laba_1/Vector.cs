using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_6
{
    interface Vector : IComparable<Vector>, ICloneable
    {
        double GetNorm();
        int Length { get; }
        double this[int index]
        {
            get; set;
        }
    }
}
