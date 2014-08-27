using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BloenkJenkins
{
    public class Settings : ApplicationSettingsBase {
        
        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        [DefaultSettingValue("")]
        public String JenkinsLastServer
        {
            get
            {
                return ((string)this["JenkinsLastServer"]);
            }
            set
            {
                this["JenkinsLastServer"] = (string)value;
            }
        }

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        public List<Job> Jobs
        {
            get
            {
                if (this["Jobs"] == null)
                {
                    this["Jobs"] = new List<Job>();
                }
                return ((List<Job>)this["Jobs"]);
            }
            set
            {
                this["Jobs"] = (List<Job>)value;
            }
        }

    }
}
