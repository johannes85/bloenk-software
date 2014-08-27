using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloenkJenkins
{
    [Serializable]
    public class Job
    {
        public string Server { get; set; }
        public string Project { get; set; }
        public int Led { get; set; }

        public string Color { get; set; }

        public Job(int led, string server, string project)
        {
            Led = led;
            Server = server;
            Project = project;
            Color = "";
        }

        public override string ToString()
        {
            return Project;
        }
    }
}
