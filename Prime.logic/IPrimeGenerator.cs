using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prime.logic
{
    public interface IPrimeGenerator
    {
        List<long> GetPrimesSequential(long first, long last);
        List<long> GetPrimesParallel(long first, long last);

        List<long> GetPrimesParallelPartitioned(long first, long last);
    }
}