using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    public class Animal
    {
        public int IdHabitat { get; set; }
        public string Name { get; set; }
        public string CommonName { get; set; }

        public Animal(int idHabitat, string name, string commonName)
        {
            this.IdHabitat = idHabitat;
            this.Name = name;
            this.CommonName = commonName;
        }
    }
}
