using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAWS_SNS
{
    internal class Configuracao
    {
        public BasicAWSCredentials Credentials
        {
            get
            {
                var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddUserSecrets<Configuracao>();

                IConfigurationRoot configuration = builder.Build();
                // necessário criar Secret User no padrão ou adicionar no appsettings
                /*
                   {
                      "AWS": {
                        "AccessKey": "accessKey",
                        "SecretKey": "secret"
                      }
                    }
                 */
                var access = configuration.GetSection("AWS:AccessKey").Value;
                var secret = configuration.GetSection("AWS:SecretKey").Value;

                return new BasicAWSCredentials(access, secret);
            }
        }
    }
}
