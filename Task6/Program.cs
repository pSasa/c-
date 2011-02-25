using System.Data;
using System.Data.Common;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            PankDB mssql = new PankSQL(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\code\c#_traning\test.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True;");
            mssql.Open();
            try
            {
                mssql.ExecuteNonQuery("create table z(z int, za char(20));");
            }
            catch(PankDBException)
            {
                //таблица уже есть
            }
            PankParameter p1 = mssql.CreateParameter("z");
            p1.Value = 123;
            PankParameter p2 = mssql.CreateParameter("za");
            p2.Value = "23232";
            mssql.ExecuteNonQuery("insert into z (z, za) values (" + p1.PlaceHolder + ", " + p2.PlaceHolder + ")", p1, p2);
            p1.Value = 1;
            DbDataReader mssqldr = mssql.ExecuteReader("select * from z where z > "+p1.PlaceHolder, p1);
            DataTable mssqldt = mssql.ExecuteDataTable("select * from z where z > " + p1.PlaceHolder, p1);

            PankDB odbc = new PankODBC("DSN=PostgreSQL30");
            odbc.Open();
            try
            {
                odbc.ExecuteNonQuery("create table z(z int, za char(20));");
            }
            catch (PankDBException)
            {
                //таблица уже есть
            }
            PankParameter p3 = odbc.CreateParameter("z");
            p3.Value = 123;
            PankParameter p4 = odbc.CreateParameter("za");
            p4.Value = "23232";
            odbc.ExecuteNonQuery("insert into z (z, za) values (" + p3.PlaceHolder + ", " + p4.PlaceHolder + ")", p4, p3);
            p3.Value = 1;
            DbDataReader odbcdr = odbc.ExecuteReader("select * from z where z > " + p3.PlaceHolder, p3);
            DataTable odbcdt = odbc.ExecuteDataTable("select * from z where z > " + p3.PlaceHolder, p3);

        }
    }
}
