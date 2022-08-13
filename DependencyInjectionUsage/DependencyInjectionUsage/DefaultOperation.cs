using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionUsage
{
    public class DefaultOperation :
     ITransientOperation,
     IScopedOperation,
     ISingletonOperation
    {
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];

    }
}
