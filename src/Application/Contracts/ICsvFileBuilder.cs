using System.Collections.Generic;

namespace CleanArchitectureBase.Application.Contracts
{
    public interface ICsvFileBuilder
    {
        byte[] WriteItems<T>(IEnumerable<T> records);
    }
}
