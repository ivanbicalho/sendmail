using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var lista = new ConcurrentBag<int>();
            var lista2 = new List<string>() { "a", "b", "c" };

            Parallel.ForEach(lista2, (item, loopState, index) =>
            {
                Console.WriteLine(Convert.ToInt32(index));
            });

            //Parallel.For(0, 100, (i) =>
            //{
            //    lista.Add(new Random(9500 + i).Next(0, 10001));
            //});
        }
    }
}
