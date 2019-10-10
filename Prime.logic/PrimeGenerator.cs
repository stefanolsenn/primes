using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prime.logic
{
    public class PrimeGenerator : IPrimeGenerator
    {
        public List<long> GetPrimesSequential(long first, long last)
        {
            var primes = new List<long>();
            for (long i = first; i < last; i++)
            {
                if (IsPrime(i)) primes.Add(i);
            }

            return primes;
        }

        public List<long> GetPrimesParallel(long first, long last)
        {
            var primes = new List<long>();
            Parallel.For(first, last, ctr =>
            {
                if (IsPrime(ctr)) primes.Add(ctr);
            });
            return primes;
        }

        public List<long> GetPrimesParallelPartitioned(long first, long last)
        {

            var src = CreateRange(first, last);
            var primes = new List<long>();
          
            var customPartitioner = Partitioner.Create(0, src.ToList().Count);
            Parallel.ForEach(customPartitioner, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                    if (IsPrime(i)) primes.Add(i);
            });             return primes;
        }

        private IEnumerable<long> CreateRange(long start, long count)
        {
            var limit = start + count;

            while (start < limit)
            {
                yield return start;
                start++;
            }
        }

        private bool IsPrime(long number)
        {
            if (number <= 1)
                return false;
            else if (number % 2 == 0)
                return number == 2;

            long N = (long)(Math.Sqrt(number) + 0.5);

            for (int i = 3; i <= N; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}