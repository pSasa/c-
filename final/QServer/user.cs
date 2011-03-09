using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QServer
{
    public enum RequestType
    {
        GetAllPerson,
        GetAllSubject,
        GetAllMark,
        GetPerson,
        SavePerson,
        DeletePerson
    };

    public enum ResponseType
    {
        Ok,
        Fail
    };

    public class User
    {
        public string name;
        public string surname;
        public int cours;
        public Object u;
        public Object u1;
    }

    [Serializable()]
    public class Person
    {
        const int MAX_COURS = 6;
        const int MAX_GROUP = 10;

        public int id;
        public string name;
        public string surname;
        public int cours;
        public int group;

        public string[] ToArray()
        {
            string [] str = new string[5];
            str[0] = id.ToString();
            str[1] = name;
            str[2] = surname;
            str[3] = cours.ToString();
            str[4] = group.ToString();
            return str;
        }

        static public bool ValidateCourse(Object o)
        {
            int cource;
            if (o == null || !Int32.TryParse(o.ToString(), out cource) || cource < 0 || cource > MAX_COURS)
                return false;
            return true;
        }
        static public bool ValidatGroup(Object o)
        {
            int group;
            if (o == null || !Int32.TryParse(o.ToString(), out group) || group < 0 || group > MAX_GROUP)
                return false;
            return true;
        }

        static public bool ValidatName(Object o)
        {
            if (o == null || o.ToString().Length == 0)
                return false;
            return true;
        }

        public bool Validate()
        {
            if (id < 0)
                return false;
            if (name == null || name.Length == 0)
            {
                return false;
            }
            if (surname == null || surname.Length == 0)
            {
                return false;
            }
            if (cours <= 0 || cours > MAX_COURS)
            {
                return false;
            }
            if (group <= 0 || group > MAX_GROUP)
            {
                return false;
            }
            return true;
        }
    }

    [Serializable()]
    public class Subject
    {
        public int id;
        public string name;
        public string teacher;
        public int hour;
    }


    [Serializable()]
    public class Request
    {
        public RequestType type;
        public Object param;
    }

    [Serializable()]
    public class Response
    {
        public ResponseType type;
        public Object param;
    }
}
