using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            string url = "org90caa978.crm2.dynamics.com/";
            string clientId = "c1fa970e-36ff-4ca3-af66-02523e5b0b79";
            string clientSecret = "iDt8Q~49_eoxOAG2jNxAaz~v9TqFpT4F7ZE~caiy";

            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");

            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão realizada com sucesso");
            else
                Console.WriteLine("Erro na conexão");

            return serviceClient;
        }
    }
}
