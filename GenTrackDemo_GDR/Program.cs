using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenTrackDemo.Interfaces;
using GenTrackDemo.Helpers;

namespace GenTrackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataHelper helper = new DataHelper();
            helper.LoadFile(@"testfile.xml");

            Console.ReadLine();

        }
    }
}
