using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    class EmployeeStorage : IEmployeeStorage // Works with Employee Controller
    {
        private EmployeeContext _db;// Entity Framework used...

        public EmployeeStorage()
        {
            _db = new EmployeeContext();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if(employee == null)
            {
                return;
            }
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}
