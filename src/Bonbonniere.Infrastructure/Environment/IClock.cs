using System;
using System.Collections.Generic;
using System.Text;

namespace Bonbonniere.Infrastructure.Environment
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
