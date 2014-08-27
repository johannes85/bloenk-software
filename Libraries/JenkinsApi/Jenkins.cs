using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace JenkinsApi
{
    public class Jenkins
    {
        private string url;

        public Jenkins(string url)
        {
            this.url = url;
        }

        public ServerDetails GetServerDetails()
        {
            RestClient client = new RestClient(this.url);
            RestRequest request = new RestRequest("api/json");
            IRestResponse<ServerDetails> response = client.Execute<ServerDetails>(request);

            if (response.ErrorException != null)
            {
                throw new JenkinsException("Couldn't get data from the Jenkins server", response.ErrorException);
            }

            if (response.ErrorMessage != null)
            {
                throw new JenkinsException("Couldn't get data from the Jenkins server", new JenkinsException(response.ErrorMessage));
            }

            if (response.Data == null)
            {
                throw new JenkinsException("Empty response from Jenkins server", response.ErrorException);
            }

            return (response.Data);
        }

        public Job GetJob(String jobName)
        {
            RestClient client = new RestClient(this.url);
            RestRequest request = new RestRequest("job/" + jobName + "/api/json");
            IRestResponse<Job> response = client.Execute<Job>(request);

            if (response.ErrorException != null)
            {
                throw new JenkinsException("Couldn't get data from the Jenkins server", response.ErrorException);
            }

            if (response.ErrorMessage != null)
            {
                throw new JenkinsException("Couldn't get data from the Jenkins server", new JenkinsException(response.ErrorMessage));
            }

            if (response.Data == null)
            {
                throw new JenkinsException("Empty response from Jenkins server", response.ErrorException);
            }

            return (response.Data);
        }
    }
}
