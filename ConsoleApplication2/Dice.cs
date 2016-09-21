using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Dice
    {
        private static readonly Random random = new Random();
        public Dice(int v)
        {
            this.v = v;
            Result = random.Next(1, v);
        }
        public int v { get; private set; }
        public int Result { get; set; }
    }
}
