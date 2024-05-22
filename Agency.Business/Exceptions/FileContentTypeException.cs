using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Exceptions
{
    public class FileContentTypeException : Exception
    {
        public string Propertyname { get; set; }
        public FileContentTypeException(string propertyname,string? message) : base(message)
        {
            Propertyname = propertyname;
        }
    }
}
