using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace FileDownload.Controllers
{
    public class FilesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            var byteArray = GenerateCSVFile();

            return new FileResult(byteArray, "text/csv", "mp_employee_list.csv");
        }

        private byte[] GenerateCSVFile()
        {
            var employees = new List<Employee>()
            {
                new Employee { Name = "Thiha", Project = "Merchant Portal"},
                new Employee { Name = "Ei Phyu Zin", Project = "Merchant Portal"},
                new Employee { Name = "Zin Mar", Project = "Merchant Portal"},
                new Employee { Name = "Phyo Thiri Tun", Project = "Merchant Portal"},
            };

            StringBuilder csv = new StringBuilder();

            csv.Append(GetColumnName());
            csv.Append("\r\n");

            if (employees != null && employees.Count() > 0)
            {
                foreach (var employee in employees)
                {
                    csv.Append(GetColumnValue(employee));
                    //Add new line.  
                    csv.Append("\r\n");
                }
                //Download the CSV file.  
            }

            return new UTF8Encoding().GetBytes(csv.ToString());
        }

        private StringBuilder GetColumnName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Name" + ",");
            sb.Append("Project" + ",");
            return sb;
        }

        private StringBuilder GetColumnValue(Employee employee)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((string.IsNullOrEmpty(employee.Name) ? "" : employee.Name.Replace(",", ";").Replace("\n", ";")) + ',');
            sb.Append((string.IsNullOrEmpty(employee.Project) ? "" : employee.Project.Replace(",", ";").Replace("\n", ";")) + ',');
            return sb;
        }

        public class Employee
        {
            public string Name
            {
                get;
                set;
            }

            public string Project
            {
                get;
                set;
            }
        }
    }
}
