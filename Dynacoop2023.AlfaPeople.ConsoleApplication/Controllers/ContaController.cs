using Dynacoop2023.AlfaPeople.ConsoleApplication.Models;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication.Controllers
{
    public class ContaController
    {
        public CrmServiceClient ServiceClient { get; set; }
        public Conta Conta { get; set; }

        public ContaController(CrmServiceClient crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Conta = new Conta(ServiceClient);
        }

        public Guid Create()
        {
            return Conta.Create();
        }

        public Entity GetAccountById(Guid id) 
        {
            return Conta.GetAccountById(id);
        }

        public Entity GetAccountByName(string name) 
        {
            return Conta.GetAccountByName(name);
        }

        public Entity GetAccountByTelephone(string telephone) 
        {
            return Conta.GetAccountByTelephone(telephone);
        }

    }
}
