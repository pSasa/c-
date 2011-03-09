using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Odbc;
using System.Data;

namespace QServer
{
    class DBLayer
    {
        private OdbcConnection connection;
        private static string Dns = null;
        public static void SetDns(string dns)
        {
            Dns = dns;
        }

        public DBLayer()
        {
            if (Dns == null || Dns.Length == 0)
            {
                throw new DBException("Dns не задан");
            }
            connection = new OdbcConnection(Dns);
            connection.Open();
        }

        public static DBLayer Create()
        {
            return new DBLayer();
        }

        public Person[] GetAllPersons()
        {
            List<Person> persons = new List<Person>();
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "select id, name, surname, cours, grp from person;";
            using (DbDataReader dr = dc.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    do
                    {
                        Person p = new Person();
                        p.id = dr.GetInt32(0);
                        p.name = dr.GetString(1);
                        p.surname = dr.GetString(2);
                        p.cours = dr.GetInt32(3);
                        p.group = dr.GetInt32(4);
                        persons.Add(p);
                    } while (dr.Read());
                }
            }
            return persons.ToArray();
        }

        public Person GetPerson(int id)
        {
            Person p = null;
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "select id, name, surname, cours, grp from person where id = ?;";
            DbParameter par = dc.CreateParameter();
            par.DbType = DbType.Int32;
            par.Value = id;
            dc.Parameters.Add(par);
            using (DbDataReader dr = dc.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    p = new Person();
                    p.id = dr.GetInt32(0);
                    p.name = dr.GetString(1);
                    p.surname = dr.GetString(2);
                    p.cours = dr.GetInt32(3);
                    p.group = dr.GetInt32(4);
                }
            }
            return p;
        }

        public Object SavePerson(Person p)
        {
            if (!p.Validate())
            {
                throw new InvalidRequestFormat("Структура Person заполена некорректно");
            }
            DbCommand dc = connection.CreateCommand();
            if (p.id > 0)
            {
                dc.CommandText = "update person set name = ?, surname = ?, cours = ?, grp = ? where id = ?;";
                DbParameter par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = p.name;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = p.surname;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = p.cours;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = p.group;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = p.id;
                dc.Parameters.Add(par);
                dc.ExecuteNonQuery();
            }
            else
            {
                dc.CommandText = "insert into person (name, surname, cours, grp) values(?, ?, ?, ?) returning id;";
                DbParameter par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = p.name;
                dc.Parameters.Add(par);
                 par = dc.CreateParameter();
                par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = p.surname;
                dc.Parameters.Add(par);
                 par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = p.cours;
                dc.Parameters.Add(par);
                 par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = p.group;
                dc.Parameters.Add(par);
                p.id = (int) dc.ExecuteScalar();
            }
            return p;
        }

        public Object DeletePerson(Person p)
        {
            if (!p.Validate())
            {
                throw new InvalidRequestFormat("Структура Person заполена некорректно");
            }
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "delete from person where id = ?;";
            DbParameter par = dc.CreateParameter();
            par = dc.CreateParameter();
            par.DbType = DbType.Int32;
            par.Value = p.id;
            dc.Parameters.Add(par);
            dc.ExecuteNonQuery();
            return null;
        }
    }
}
