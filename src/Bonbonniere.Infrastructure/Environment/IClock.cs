using System;

namespace Bonbonniere.Infrastructure.Environment
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
