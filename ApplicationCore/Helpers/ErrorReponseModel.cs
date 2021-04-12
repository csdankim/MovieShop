using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Helpers
{
    public class ErrorReponseModel
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTree { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public string FullName  { get; set; }
        public bool IsAuthorized { get; set; }
        public DateTime ExceptionDateTime { get; set; }
    }
}
