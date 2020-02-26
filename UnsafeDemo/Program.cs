using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsafeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //unsafe
            //{
            //    // 1
            //    int[] array = new int[] { 1, 0, 10, 23, 6, -7 };
            //    fixed (int* p = array) {
            //        // Console.WriteLine(*p);
            //        int* currentP = p;
            //        for (int i = 0; i < array.Length; i++)
            //        {
            //            Console.WriteLine(*(currentP++));
            //        }
            //    }

            //    // 2
            //    long a = 15000;
            //    long b = 1000000000000;

            //    DateTime t1 = DateTime.Now;
            //    Console.WriteLine(a * b);
            //    Console.WriteLine(DateTime.Now - t1);

            //    // 1100100
            //    // a << 3 + a << 2 + a << 0
            //    // Console.WriteLine(x << 2);

            //    DateTime t2 = DateTime.Now;
            //    int result = 0;
            //    while (b != 0)
            //    {
            //        if ((b & 1) == 1)
            //            result += a;
            //        b >>= 1;
            //        a <<= 1;
            //    }
            //    Console.WriteLine(result);
            //    Console.WriteLine(DateTime.Now - t2);

            //    a = 15000;
            //    b = 1000000000000;

            //   DateTime t3 = DateTime.Now;
            //    Console.WriteLine(a * b);
            //    Console.WriteLine(DateTime.Now - t3);
            //}

            // 100

            long startMem = GC.GetTotalMemory(false); // x
            Data data = new Data();
            Data data2 = new Data();

            data.childData = data2;

            data = null;

            Console.ReadKey();
            // x1 - x > 0
            Console.WriteLine(GC.GetTotalMemory(true) - startMem);

            // 16
        }
    }
}
