using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustSpeak.Common
{
    public class Constants:IConstants
    {
        IConfiguration configuration;

        public Constants(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public Constants()
        { 
        }

        public string EConnectionString()
        {
           return ConfigurationExtensions.GetConnectionString(this.configuration, "JustSpeakContext");
        }
    }
}
