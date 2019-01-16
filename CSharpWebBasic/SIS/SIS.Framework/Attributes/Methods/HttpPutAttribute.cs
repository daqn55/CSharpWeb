using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Framework.Attributes.Methods
{
    class HttpPutAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToUpper() == "PUT")
            {
                return true;
            }

            return false;
        }
    }
}
