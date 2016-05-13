using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    public class Employee
    {
        public int IdEmployee { get; set; }
        public string Name { get; set; }
        public int IdHabitat { get; set; }

        public Employee(int idEmployee, string name, int idHabitat)
        {
            this.IdEmployee = idEmployee;
            this.Name = name;
            this.IdHabitat = idHabitat;
        }
    }
}
