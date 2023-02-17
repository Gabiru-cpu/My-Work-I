using Dynacoop2023.AlfaPeople.ConsoleApplication.Controllers;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication.Models
{
    public class Conta
    {
        public CrmServiceClient ServiceClient { get; set; }

        public string Logicalname { get; set; }

        public Conta(CrmServiceClient crmServiceClient) 
        {
            this.ServiceClient = crmServiceClient;
            this.Logicalname= "account";
        }
        public Guid Create()
        {
            Entity conta = new Entity(this.Logicalname);
            
            Console.WriteLine("Por favor informe o nome da Conta");
            var nameConta = Console.ReadLine();
            conta["name"] = nameConta;
            
            Console.WriteLine("Por favor informe o telefone");
            var telephoneConta = Console.ReadLine();
            conta["telephone1"] = telephoneConta;

            Console.WriteLine("Por favor informe o numero total de oportunidades (inteiro)");         
            var nmrTotaloppConta = int.Parse(Console.ReadLine());
            conta["dcp_nmr_total_opp"] = nmrTotaloppConta;

            Console.WriteLine("Por favor informe o numero total de oportunidades (option) dica: Cliente =775050000 Fornecedor =775050001 Revendedor =775050002");
            //se der tempo melhorar usando string para q o usuario digite o nome da opção e assim a variavel receba o numero representante da opção
            var tipodeRelacaoConta = int.Parse(Console.ReadLine());
            conta["dcp_tipoderelacao"] = new OptionSetValue(tipodeRelacaoConta);

            Console.WriteLine("Por favor informe o valor total de oportunidades (Decimal/Moeda)");
            var vlrTotaloppConta = decimal.Parse(Console.ReadLine());
            conta["dcp_valor_total_opp"] = new Money(vlrTotaloppConta);

            Console.WriteLine("Por favor informe o id do contato primario (Lookup) dica: 79ae8582-84bb-ea11-a812-000d3a8b3ec6");
            //se der tempo melhorar usando string para q o usuario digite o nome do contato e assim puxar o id para cá
            var primaryContactConta = Console.ReadLine();
            conta["primarycontactid"] = new EntityReference("contact", new Guid(primaryContactConta));
       
            Guid accountId = this.ServiceClient.Create(conta);
            return accountId;


        }



        public Entity GetAccountByName(string name)
        {
            QueryExpression queryAccount = new QueryExpression(this.Logicalname);
            queryAccount.ColumnSet.AddColumns("telephone1", "primarycontactid");
            queryAccount.Criteria.AddCondition("name", ConditionOperator.Equal, name);
            return RetrieveOneAccount(queryAccount);
        }

        private Entity RetrieveOneAccount(QueryExpression queryAccount)
        {
            EntityCollection accounts = this.ServiceClient.RetrieveMultiple(queryAccount);

            if (accounts.Entities.Count() > 0)
                return accounts.Entities.FirstOrDefault();
            else
                Console.WriteLine("Nenhuma conta econtrada com esse nome");

            return null;
        }



        public Entity GetAccountById(Guid id) 
        {
            var context = new OrganizationServiceContext(this.ServiceClient);

            return (from a in context.CreateQuery("account") 
                           where (Guid)a["accountid"] == id
                           select a).ToList().FirstOrDefault();
        }
       

        private static void AddUpsertRequest(OrganizationRequestCollection requestCollection, Entity entity)
        {
            UpsertRequest upsertRequest = new UpsertRequest()
            {
                Target = entity
            };

            requestCollection.Add(upsertRequest);
        }

        public Entity GetAccountByTelephone(string telephone1)
        {
            string fetchXML = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                  <entity name='account'>
                                    <attribute name='name' />
                                    <attribute name='primarycontactid' />
                                    <attribute name='telephone1' />
                                    <attribute name='accountid' />
                                    <order attribute='name' descending='false' />
                                    <filter type='and'>
                                      <condition attribute='telephone1' operator='eq' value='" + telephone1 + @"' />
                                    </filter>
                                  </entity>
                                </fetch>";

            return this.ServiceClient.RetrieveMultiple(
                new FetchExpression(fetchXML)
            ).Entities.FirstOrDefault();
        }

    }
}
