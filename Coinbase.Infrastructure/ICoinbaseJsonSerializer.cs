using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Infrastructure
{
    public interface ICoinbaseJsonSerializer<T>
    {
        bool TrySerialize(T serializable);
    }
}
