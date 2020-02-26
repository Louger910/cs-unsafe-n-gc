using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsafeDemo
{
    class Data
    {
        public string myString = "Data";
        public Data childData;

        public Data()
        {
            Console.WriteLine("new Data");
        }

        ~Data()
        {
            Console.WriteLine("~Data");
            if (childData != null)
            {
                childData.childData = this;
            }
        }
    }
}
