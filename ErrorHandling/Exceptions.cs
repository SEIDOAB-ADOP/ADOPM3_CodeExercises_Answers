using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling
{
    public enum ErrorSeverity { managable, fatal}
    public class ExplosionException : Exception
    {
        public int ButtonPressed { get; init; }
        public ErrorSeverity Severity { get; init; }

        public ExplosionException(string message) : base(message)
        {
        }
    }
}
