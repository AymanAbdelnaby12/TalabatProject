using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class EmployeeWithDepartmentSpec:Specifications<Employee>
    {
        public EmployeeWithDepartmentSpec():base()
        {
            Includes.Add(E => E.Department);
        }
        public EmployeeWithDepartmentSpec(int id):base(E=>E.Id==id)
        {
            Includes.Add(E => E.Department);
        }
    }
}
