using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Exceptions
{
    public class FileSizeException : Exception
    {
        public string Propertyname { get; set; }
        public FileSizeException(string propertyName,string? message) : base(message)
        {
            Propertyname = propertyName;
        }
    }
}
