using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Exceptions
{
    public class ImageNullException : Exception
    {
        public string Propertname { get; set; }
        public ImageNullException(string propertyName,string? message) : base(message)
        {
            Propertname = propertyName;
        }
    }
}
