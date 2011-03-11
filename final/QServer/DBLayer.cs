using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;

namespace QServer
{
    sealed class DBLayer
    {
        private OdbcConnection connection;
        private static string Dns = null;

        #region базовые функции класса работы с базой
        public static void SetDns(string dns)
        {
            Dns = dns;
        }

        public DBLayer()
        {
            if (Dns == null || Dns.Length == 0)
            {
                throw new QServerException("Dns не задан");
            }
            connection = new OdbcConnection(Dns);
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public static DBLayer Create()
        {
            return new DBLayer();
        }

        public static bool Test()
        {
            try
            {
                DBLayer db = DBLayer.Create();
                db.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region обработка студентов
        public Person[] GetAllPersons()
        {
            List<Person> persons = new List<Person>();
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "select id, name, surname, cours, grp from person order by surname, name;";
            using (DbDataReader dr = dc.ExecuteReader())
            {
                while (dr.Read())
                {
                    Person p = new Person();
                    p.id = dr.GetInt32(0);
                    p.name = dr.GetString(1);
                    p.surname = dr.GetString(2);
                    p.cours = dr.GetInt32(3);
                    p.group = dr.GetInt32(4);
                    persons.Add(p);
                };
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
                p.id = (int)dc.ExecuteScalar();
            }
            return p;
        }

        public Object DeletePerson(int id)
        {
            if (id <= 0)
            {
                throw new InvalidRequestFormat("id должен быть > 0");
            }
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "delete from person where id = ?;";
            DbParameter par = dc.CreateParameter();
            par = dc.CreateParameter();
            par.DbType = DbType.Int32;
            par.Value = id;
            dc.Parameters.Add(par);
            dc.ExecuteNonQuery();
            return null;
        }
        #endregion

        #region обработка предметов
        public Subject[] GetAllSubject()
        {
            List<Subject> subjects = new List<Subject>();
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "select id, name, teacher, hour from subject order by name;";
            using (DbDataReader dr = dc.ExecuteReader())
            {
                while (dr.Read())
                {
                    subjects.Add(ParseSubject(dr));
                };
            }
            return subjects.ToArray();
        }

        public Subject GetSubject(int id)
        {
            Subject s = null;
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "select id, name, teacher, hour from subject where id = ?;";
            DbParameter par = dc.CreateParameter();
            par.DbType = DbType.Int32;
            par.Value = id;
            dc.Parameters.Add(par);
            using (DbDataReader dr = dc.ExecuteReader())
            {
                if (dr.Read())
                {
                    s = ParseSubject(dr);
                }
            }
            return s;
        }

        private Subject ParseSubject(DbDataReader dr)
        {
            Subject s = new Subject();
            s.id = dr.GetInt32(0);
            s.name = dr.GetString(1);
            s.teacher = dr.GetString(2);
            s.hour = dr.GetInt32(3);
            return s;
        }

        public Object SaveSubject(Subject s)
        {
            if (!s.Validate())
            {
                throw new InvalidRequestFormat("Структура Subject заполена некорректно");
            }
            DbCommand dc = connection.CreateCommand();
            if (s.id > 0)
            {
                dc.CommandText = "update subject set name = ?, teacher = ?, hour = ? where id = ?;";
                DbParameter par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = s.name;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = s.teacher;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = s.hour;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = s.id;
                dc.Parameters.Add(par);
                dc.ExecuteNonQuery();
            }
            else
            {
                dc.CommandText = "insert into subject (name, teacher, hour) values(?, ?, ?) returning id;";
                DbParameter par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = s.name;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par = dc.CreateParameter();
                par.DbType = DbType.String;
                par.Value = s.teacher;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = s.hour;
                dc.Parameters.Add(par);
                s.id = (int)dc.ExecuteScalar();
            }
            return s;
        }

        public Object DeleteSubject(int id)
        {
            if (id <= 0)
            {
                throw new InvalidRequestFormat("id должен быть > 0");
            }
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "delete from subject where id = ?;";
            DbParameter par = dc.CreateParameter();
            par = dc.CreateParameter();
            par.DbType = DbType.Int32;
            par.Value = id;
            dc.Parameters.Add(par);
            dc.ExecuteNonQuery();
            return null;
        }
        #endregion

        #region обработка оценок
        public Mark[] GetAllMark()
        {
            List<Mark> marks = new List<Mark>();
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "select id, person_id, subject_id, mark from mark;";
            using (DbDataReader dr = dc.ExecuteReader())
            {
                while (dr.Read())
                {
                    marks.Add(ParseMark(dr));
                };
            }
            return marks.ToArray();
        }

        public Mark GetMark(int id)
        {
            Mark m = null;
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "select id, person_id, subject_id, mark from mark where id = ?;";
            DbParameter par = dc.CreateParameter();
            par.DbType = DbType.Int32;
            par.Value = id;
            dc.Parameters.Add(par);
            using (DbDataReader dr = dc.ExecuteReader())
            {
                if (dr.Read())
                {
                    m = ParseMark(dr);
                }
            }
            return m;
        }

        private Mark ParseMark(DbDataReader dr)
        {
            Mark m = new Mark();
            m.id = dr.GetInt32(0);
            m.person = dr.GetInt32(1);
            m.subject = dr.GetInt32(2);
            m.mark = dr.GetInt32(3);
            return m;
        }

        public Object SaveMark(Mark m)
        {
            if (!m.Validate())
            {
                throw new InvalidRequestFormat("Структура Mark заполена некорректно");
            }
            DbCommand dc = connection.CreateCommand();
            if (m.id > 0)
            {
                dc.CommandText = "update mark set person_id = ?, subject_id = ?, mark = ? where id = ?;";
                DbParameter par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = m.person;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = m.subject;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = m.mark;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = m.id;
                dc.Parameters.Add(par);
                dc.ExecuteNonQuery();
            }
            else
            {
                dc.CommandText = "insert into mark (person_id, subject_id, mark) values(?, ?, ?) returning id;";
                DbParameter par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = m.person;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = m.subject;
                dc.Parameters.Add(par);
                par = dc.CreateParameter();
                par.DbType = DbType.Int32;
                par.Value = m.mark;
                dc.Parameters.Add(par);
                m.id = (int)dc.ExecuteScalar();
            }
            return m;
        }

        public Object DeleteMark(int id)
        {
            if (id <= 0)
            {
                throw new InvalidRequestFormat("id должен быть > 0");
            }
            DbCommand dc = connection.CreateCommand();
            dc.CommandText = "delete from mark where id = ?;";
            DbParameter par = dc.CreateParameter();
            par = dc.CreateParameter();
            par.DbType = DbType.Int32;
            par.Value = id;
            dc.Parameters.Add(par);
            dc.ExecuteNonQuery();
            return null;
        }
        #endregion
    }
}
