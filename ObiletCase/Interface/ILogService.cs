using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletCase.Interface
{
    public interface ILogService
    {
        void Info(string logMessage);
        void Error(Exception ex, string logMessage);
    }
}
