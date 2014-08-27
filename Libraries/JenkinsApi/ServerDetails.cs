using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsApi
{
    public class ServerDetails
    {
        public string Description { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
