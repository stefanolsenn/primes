using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Prime.logic;

namespace primes
{
    class Program
    {
        private IPrimeGenerator _generator = new PrimeGenerator();

        static async Task Main()
        {
            var program = new Program();
            long first = 10;
            long last = 100_000_0;
            Console.Write("Primes seq: ");
            var seq = program.Watch(() => program._generator.GetPrimesSequential(first, last));
            Console.WriteLine();
            

        }

        public T Watch<T>(Func<T> action)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = action.Invoke();
            sw.Stop();
            Console.Write($"{sw.ElapsedMilliseconds / 1000d :F5} sek");
            return result;
        }
        
        
    }
}