using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365.ConsoleApp.Models
{
    public class Contact
    {
        public void UpdateEmployeeTotalIncome(string totalIncome, IOrganizationService service)
        {
            var contact = new Entity("contact");
            contact["neu_totalincome"] = totalIncome;

            service.Update(contact);

        }

        public bool IsEmployeeDuplicate(string email)
        {
            /// logic goes here
            return true;
        }
    }
}
