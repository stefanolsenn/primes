using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prime.logic
{
    public class PrimeGenerator : IPrimeGenerator
    {
        public Task<List<long>> GetPrimesSequential(long first, long last)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<long>> GetPrimesParallel(long first, long last)
        {
            throw new System.NotImplementedException();
        }
    }
}