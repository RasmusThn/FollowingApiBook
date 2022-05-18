using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public sealed class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(Guid companyId)
            : base($"The Company with Id:{companyId} does not exist in the database.")
        {

        }
    }
}
