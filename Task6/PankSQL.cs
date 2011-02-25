using System.Data.SqlClient;

namespace Task6
{
    sealed class PankSQL:PankDB
    {
        private int i = 0;
        public PankSQL(string url)
            :base(url)
        {
            connection = new SqlConnection(url);
        }

        protected override void ArrangeParameters(ref string sql, PankParameter[] param)
        {
            //здесь ничего не надо делать
        }

        public override PankParameter CreateParameter(string name)
        {
            PankParameter param = new PankParameter(name, "@" + name+i.ToString());
            i++;
            return param;
        }
    }
}
