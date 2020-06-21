using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VseTut.Web.Host.Models
{
    public class CustomResponse
    {
        public object Result { get; set; }
        public string Error;

        public CustomResponse(object obj, int code)
        {
            if (code == 200)
            {
                Result = obj;
            }
            else
            {
                Error = obj.ToString();
            }
        }
    }
}
