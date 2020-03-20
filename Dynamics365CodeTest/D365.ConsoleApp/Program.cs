using D365.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365.ConsoleApp
{
    class Program
    {
        static IOrganizationService crmService;
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;
            CrmServiceClient crmConn = new CrmServiceClient(connectionString);
            if (crmConn.IsReady)
            {
                Console.WriteLine("You are connected to:" + crmConn.ConnectedOrgFriendlyName);
                crmService = crmConn.OrganizationServiceProxy;
                Console.WriteLine("Which operation would you like to perform?\n");
                Console.WriteLine("Enter 1 for updating total income of contact and 2 checking if contact is a duplicate. Please press Enter key after input.");

                var input = Console.ReadLine();
                var contact = new Contact();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Please enter GUID of contact to update:\n");
                        var contactId = Console.ReadLine();

                        Console.WriteLine("Please enter total income to update\n");
                        var totalIncome = Console.ReadLine();

                        contact.UpdateEmployeeTotalIncome(totalIncome, crmService);
                        break;
                    case "2":
                        Console.WriteLine("Please enter email of contact to check:\n");
                        var email = Console.ReadLine();

                        var isDuplicate = IsEmployeeDuplicate(email);
                        Console.WriteLine("An employee with same email already exists");
                        break;
                    default:
                        Console.WriteLine("Invalid Input. Please run application again.");
                        Console.ReadLine();
                        break;
                }

            }
        }
    }
}
