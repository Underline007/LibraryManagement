using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Enums
{
    public enum BorrowingRequestStatus
    {
       Waitting = 0,
       Approved = 1, 
       Rejected = 2,
       Returned = 3,
       Expired =4 ,
    }
}
