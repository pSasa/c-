using System;
using System.Data.Odbc;

namespace Task6
{
    sealed class PankODBC : PankDB
    {
        int i = 0;

        public PankODBC(string url)
            :base(url)
        {
            connection = new OdbcConnection(url);
        }

        protected override void ArrangeParameters(ref string sql, PankParameter[] param)
        {
            foreach (PankParameter par in param)
            {
                par.Range = sql.IndexOf(par.PlaceHolder);
                if (par.Range == -1)
                {
                    throw new PankDBException("Неверный параметр");
                }
                sql = sql.Replace(par.PlaceHolder, "?");
            }
            Array.Sort(param);
        }

        public override PankParameter CreateParameter(string name)
        {
            //Заворачиваем таким хитрым способос чтобы потом соблюсти порядок
            PankParameter param = new PankParameter(name, "?"+ i.ToString() + name + i.ToString()+"?");
            i++;
            return param;
        }
    }
}
