using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Odbc;

namespace Task6
{
    class PankODBC : PankDB
    {
        public PankODBC(string url)
            :base(url)
        {
            connection = new OdbcConnection(url);
        }
    }
}
