using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ICustomLogger
    {
        void LogInfo(string message);
        void LogException(Exception ex);
        void LogObject(object _object);
    }
}
