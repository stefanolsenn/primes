using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prime.logic
{
    public interface IPrimeGenerator
    {
        Task<List<long>> GetPrimesSequential(long first, long last);
        Task<List<long>> GetPrimesParallel(long first, long last);
    }
}