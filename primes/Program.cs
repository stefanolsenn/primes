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

            var userInput = program.GetRangeInput();
            Console.Write("Primes seq: ");
            Console.WriteLine();
            var seq = program.StartTask(userInput[0],userInput[1]);
            program.ShowGuiStuff(seq);

            Console.Write("Parallel primes seq: ");
            Console.WriteLine();
            var seq2 = program.StartTaskPrimesParallel(userInput[0], userInput[1]);
            program.ShowGuiStuff(seq2);

            Console.Write("Parallel partitioned primes seq: ");
            Console.WriteLine();
            var seq3 = program.StartTaskPrimesParallelPartitioned(userInput[0], userInput[1]);
            program.ShowGuiStuff(seq3);

            Console.WriteLine();
            Console.ReadLine();   

        }

        void ShowGuiStuff(Task task) {
            var i = 0;
            bool reset = false;
            while (task.Status != TaskStatus.RanToCompletion) {
                switch (i) {
                    case 0:
                        Console.Write("\r|");
                        break;
                    case 1:
                        Console.Write("\r/");
                        break;
                    case 2:
                        Console.Write("\r-");
                        break;
                    case 3:
                        Console.Write("\r\\");
                        break;
                    case 4:
                        Console.Write("\r|");
                        break;
                    case 5:
                        Console.Write("\r/");
                        break;
                    case 6:
                        Console.Write("\r-");
                        break;
                    case 7:
                        Console.Write("\r\\");
                        reset = true;
                        break;
                }
                if (reset) {
                    i = 0;
                    reset = false;
                } else {
                    i++;
                }
                Thread.Sleep(100);
            }
        }

        async Task<List<long>> StartTaskPrimesParallelPartitioned(long first, long last) {
            var seq = await Task.Run(() => Watch(() => _generator.GetPrimesParallelPartitioned(first, last)));

            return seq;
        }

        async Task<List<long>> StartTaskPrimesParallel(long first, long last) {
            var seq = await Task.Run(() => Watch(() => _generator.GetPrimesParallel(first, last)));

            return seq;
        }

        async Task<List<long>> StartTask(long first, long last) {
            var seq = await Task.Run(() => Watch(() => _generator.GetPrimesSequential(first, last)));

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
            Console.SetCursorPosition(35, Console.CursorTop -1);
            Console.WriteLine($"{sw.ElapsedMilliseconds / 1000d :F5} sek");
            return result;
        }
        
        
    }
}