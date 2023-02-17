using Dynacoop2023.AlfaPeople.ConsoleApplication.Controllers;
using Dynacoop2023.AlfaPeople.ConsoleApplication.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop2023.AlfaPeople.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();

            ContaController contaController = new ContaController(serviceClient);
            
            ContaMethods(contaController);
            
            ReadMethods(contaController);

            Console.ReadKey();
        }

        private static void ContaMethods(ContaController contaController)
        {
            Console.WriteLine("Nova conta sendo criada");
            Guid accountId = contaController.Create();
            Console.WriteLine("Conta craida com sucesso");

            Console.WriteLine($"https://org90caa978.crm2.dynamics.com/main.aspx?appid=4d306bb3-f4a9-ed11-9885-000d3a888f48&pagetype=entityrecord&etn=account&id={accountId}");


        }

        private static void ContatoMethods(ContaController contaController)
        {
            Console.WriteLine("Você deseja criar um contato para essa conta? (S/N)");
            var answerToUpdate = Console.ReadLine();

            if (answerToUpdate.ToString().ToUpper() == "S")
            {
                ReadMethods(contaController);
            }
            else
            {
                Environment.Exit(0);
            }

        }

        private static void ReadMethods(ContaController contaController)
        {
            Console.WriteLine("1 - pesquisar uma conta por Id");
            Console.WriteLine("2 - pesquisar uma conta por nome");
            Console.WriteLine("3 - pesquisar uma conta por telefone");

            var answer = Console.ReadLine();


            switch (answer)
            {
                case "1":
                    Console.WriteLine("Qual o id da conta que você deseja pesquisar");
                    var accountId = Console.ReadLine();
                    Entity account = contaController.GetAccountById(new Guid(accountId));
                    ShowAccountName(account);
                    break;
                case "2":
                    Console.WriteLine("Qual o nome da conta que você deseja pesquisar");
                    var name = Console.ReadLine();
                    account = contaController.GetAccountByName(name);
                    Console.WriteLine($"O telefone recuperada é {account["telephone1"].ToString()}");
                    break;
                case "3":
                    Console.WriteLine("Qual o telefone da conta que você deseja pesquisar");
                    var telephone = Console.ReadLine();
                    account = contaController.GetAccountByTelephone(telephone);
                    ShowAccountName(account);
                    break;
                default:
                    Console.WriteLine("Opção invalida");
                    break;
            }
            
        }

        private static void ShowAccountName(Entity account)
        {
            Console.WriteLine($"A conta recuperada se chama {account["name"].ToString()}");
        }

        
    }
}