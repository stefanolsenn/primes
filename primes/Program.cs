using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
            

            var seq = program.RunTaskInParallel();
            Console.Write("\rPrimes seq: ");

            Console.WriteLine("done");
            var seqRes = seq.Result;
            //for (int j = 0; j < seqRes.Count; j = j + 1000) {
            //    Console.Write(seqRes[j] + " ");
            //}
            //var seq = program.Watch(() => program._generator.GetPrimesSequential(userInput[0], userInput[1]));
            Console.WriteLine();
            Console.ReadLine();   

        }

        Task<List<long>> RunTaskInParallel() {
            long first = 10;
            long last = 100_000_0;
            var userInput = GetRangeInput();

            var seq = StartTask(userInput[0], userInput[1]);
            var i = 0;
            while (seq.Status != TaskStatus.RanToCompletion) {
                switch (i) {
                    case 0:
                        Console.Write("\r|");
                        i++;
                        break;
                    case 1:
                        Console.Write("\r/");
                        i++;
                        break;
                    case 2:
                        Console.Write("\r_");
                        i++;
                        break;
                    case 3:
                        Console.Write("\r\\");
                        i++;
                        break;
                    case 4:
                        Console.Write("\r|");
                        i++;
                        break;
                    case 5:
                        Console.Write("\r/");
                        i++;
                        break;
                    case 6:
                        Console.Write("\r_");
                        i++;
                        break;
                    case 7:
                        Console.Write("\r\\");
                        i = 0;
                        break;
                }
                Thread.Sleep(100);
            }
            return seq;
        }

        async Task<List<long>> StartTask(long first, long last) {
            var seq = await Task.Run(() => Watch(() => _generator.GetPrimesSequential(first, last)));
            //var seq = program.Watch(() => program._generator.GetPrimesSequential(userInput[0], userInput[1]));

            return seq;
        }

        long[] GetRangeInput() {
            long[] set = new long[2];
            var first = long.Parse( Console.ReadLine());
            var last = long.Parse( Console.ReadLine());
            set[0] = first;
            set[1] = last;
            return set;
            
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