using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Task6
{
    abstract class PankDB
    {
        protected DbConnection connection = null;
        private DbTransaction transaction = null;

        public PankDB(string url)
        {

        }

        abstract string convert 

        public void ExecuteNonQuery(string sql)
        {
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = sql;
            dc.ExecuteNonQuery();
        }

        public DbDataReader ExecuteReader(string sql)
        {
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = sql;
            DbDataReader res = dc.ExecuteReader();
            return res;
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }


    }
}