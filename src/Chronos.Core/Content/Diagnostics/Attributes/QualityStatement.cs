
namespace Chronos.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class QualityStatement: Attribute
    {
        // This is a positional argument
        public QualityStatement(string user, string dateTime)
        {
        }

    }
}
