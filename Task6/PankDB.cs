using System;
using System.Data;
using System.Data.Common;

namespace Task6
{
    #region PankDBException
    /// <summary>
    /// Исключение
    /// </summary>
    sealed class PankDBException : DbException
    {
        public PankDBException(string mess)
            :base(mess)
        {
        }
        public PankDBException(string mess, Exception e)
            : base(mess, e)
        {
        }
    }
    #endregion

    #region PankParameter
    /// <summary>
    /// Класс используемый для организации параметров
    /// </summary>
    sealed class PankParameter:IComparable
    {
        public string PlaceHolder;
        public Object Value;
        public string Name;
        public int Range = 0;
        public PankParameter(string n, string ph)
        {
            Name = n;
            PlaceHolder = ph;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            PankParameter right = (PankParameter)obj;
            if (Range < right.Range)
            {
                return -1;
            }
            else if (Range > right.Range)
            {
                return 1;
            }
            return 0;
        }

        #endregion
    }
    #endregion


    abstract class PankDB:IDisposable
    {
        protected DbConnection connection = null;
        private DbTransaction transaction = null;

        public PankDB(string url)
        {
            if(url.Length == 0)
            {
                throw new PankDBException("Пустой URL");
            }
        }

        public void Open()
        {
            try
            {
                connection.Open();
            }
            catch(DbException e)
            {
                throw new PankDBException("Ошибка при подключении к базе", e);
            }
        }

        public abstract PankParameter CreateParameter(string name);
        protected abstract void ArrangeParameters(ref string sql, PankParameter[] param);

        public void ExecuteNonQuery(string sql, params PankParameter[] param)
        {
            try
            {
                ArrangeParameters(ref sql, param);
                DbCommand dc = connection.CreateCommand();
                dc.CommandText = sql;
                foreach (PankParameter par in param)
                {
                    DbParameter dpar = dc.CreateParameter();
                    dpar.Value = par.Value;
                    dpar.ParameterName = par.PlaceHolder;
                    dc.Parameters.Add(dpar);
                }
                dc.ExecuteNonQuery();
            }
            catch (DbException e)
            {
                if (transaction != null)
                {
                    Rollback();
                }
                throw new PankDBException("Ошибка при выполнении запроса ExecuteNonQuery", e);
            }
        }

        public DbDataReader ExecuteReader(string sql, params PankParameter[] param)
        {
            try
            {
                ArrangeParameters(ref sql, param);
                DbCommand dc = connection.CreateCommand();
                dc.CommandText = sql;
                foreach (PankParameter par in param)
                {
                    DbParameter dpar = dc.CreateParameter();
                    dpar.Value = par.Value;
                    dpar.ParameterName = par.PlaceHolder;
                    dc.Parameters.Add(dpar);
                }
                DbDataReader res = dc.ExecuteReader();
                return res;
            }
            catch(DbException e)
            {
                if (transaction != null)
                {
                    Rollback();
                }
                throw new PankDBException("Ошибка при выполнении запроса ExecuteReader", e);
            }
        }

        public DataTable ExecuteDataTable(string sql, params PankParameter[] param)
        {
            DbDataReader dr = ExecuteReader(sql, param);
            DataTable dt = new DataTable ();
            dt.Load(dr, LoadOption.OverwriteChanges);
            return dt;
        }

        public void BeginTransaction()
        {
            if (transaction != null)
            {
                throw new PankDBException("Транзакция уже начата");
            }
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction != null)
            {
                throw new PankDBException("Нет начатой транзакции");
            }
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        #region IDisposable Members
        public void Dispose()
        {
            //подчищаем за собой
            //закрываем транзакцию, не факт что нужен Комит, может уместным был бы ролбэк
            //и закрываем конект к базе
            //пул не чистим, может кто еще в других нитях работает
            if (transaction != null)
            {
                transaction.Commit();
            }
            connection.Close();
        }
        #endregion
    }
}