using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace DAL
{
    public class SQLError : ApplicationException
    {
        public SQLError(string msg)
            : base(msg)
        {
        }

        public SQLError(string msg, Exception ex)
            : base(msg, ex)
        {
        }
    }
}
