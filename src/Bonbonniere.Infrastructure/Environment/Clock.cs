using System;

namespace Bonbonniere.Infrastructure.Environment
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}
