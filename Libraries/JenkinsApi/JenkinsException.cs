using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace JenkinsApi
{
    public class JenkinsException : Exception
    {
        public JenkinsException(string message)
            : base(message)
        {

        }
        public JenkinsException(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}
