using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public sealed class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid employeeId)
            : base($"Employee with Id:{employeeId} does not exist in the database.")
        {

        }
    }
}
