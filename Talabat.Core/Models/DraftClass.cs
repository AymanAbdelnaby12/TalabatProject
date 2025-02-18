using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models
{
    public class Employee : BaseModel
    {
        public string Name { set; get; }
        public decimal Salay { set; get; }
        public int Age { set; get; }
        public int DepartmentID { set; get; }
        public Department Department { set; get; }
    }
    public class Department :BaseModel
    {
        public string Name { set; get; }
        public ICollection<Employee> employees { set; get; }
    }
}

