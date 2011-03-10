using System;

namespace QServer
{
    [Serializable()]
    abstract public class Item
    {
        public int id;
        protected RequestType saveType;
        protected RequestType getType;
        protected RequestType deleteType;
        protected RequestType getAllType;
        public RequestType SaveType
        {
            get { return saveType; }
        }
        public RequestType GetType
        {
            get { return getType; }
        }
        public RequestType DeleteType
        {
            get { return deleteType; }
        }
        public RequestType GatAllType
        {
            get { return getAllType; }
        }

        abstract public string[] ToArray();
        abstract public bool Validate();
        static public bool ValidatName(Object o)
        {
            if (o == null || o.ToString().Length == 0)
                return false;
            return true;
        }

    }
    #region студент 
    [Serializable()]
    sealed public class Person : Item
    {
        const int MAX_COURS = 6;
        const int MAX_GROUP = 10;

        public string name;
        public string surname;
        public int cours;
        public int group;


        public Person()
        {
            saveType = RequestType.SavePerson;
            getType = RequestType.GetPerson;
            deleteType = RequestType.DeletePerson;
            getAllType = RequestType.GetAllPerson;
        }

        public override string ToString()
        {
            return name;
        }

        public override string[] ToArray()
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

        public override bool Validate()
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
    #endregion

    #region предмет
    [Serializable()]
    public class Subject : Item
    {
        public string name;
        public string teacher;
        public int hour;

        public Subject()
        {
            saveType = RequestType.SaveSubject;
            getType = RequestType.GetSubject;
            deleteType = RequestType.DeleteSubject;
            getAllType = RequestType.GetAllSubject;
        }

        public override string ToString()
        {
            return name;
        }

        public override string[] ToArray()
        {
            string[] str = new string[4];
            str[0] = id.ToString();
            str[1] = name;
            str[2] = teacher;
            str[3] = hour.ToString();
            return str;
        }

        static public bool ValidateHour(Object o)
        {
            int h;
            if (o == null || !Int32.TryParse(o.ToString(), out h) || h < 0)
                return false;
            return true;
        }

        public override bool Validate()
        {
            if (id < 0)
                return false;
            if (name == null || name.Length == 0)
            {
                return false;
            }
            if (teacher == null || teacher.Length == 0)
            {
                return false;
            }
            if (hour <= 0)
            {
                return false;
            }
            return true;
        }
    }
    #endregion

    #region оценка
    [Serializable()]
    public class Mark : Item
    {
        public int person;
        public int subject;
        public int mark;


        public Mark()
        {
            saveType = RequestType.SaveMark;
            getType = RequestType.GetMark;
            deleteType = RequestType.DeleteMark;
            getAllType = RequestType.GetAllMark;
        }
        
        public override string[] ToArray()
        {
            string[] str = new string[4];
            str[0] = id.ToString();
            str[1] = "";
            str[2] = "";
            str[3] = mark.ToString();
            return str;
        }

        static public bool ValidateMark(Object o)
        {
            int m;
            if (o == null || !Int32.TryParse(o.ToString(), out m) || m < 1 || m > 5)
                return false;
            return true;
        }

        public override bool Validate()
        {
            if (id < 0)
                return false;
/*            if (name == null || name.Length == 0)
            {
                return false;
            }
            if (teacher == null || teacher.Length == 0)
            {
                return false;
            }
 */
            if (mark < 1|| mark > 5)
            {
                return false;
            }
            return true;
        }
    }
    #endregion
    #region запрос - ответ
    public enum RequestType
    {
        GetAllPerson,
        GetPerson,
        SavePerson,
        DeletePerson,
        GetAllSubject,
        GetSubject,
        SaveSubject,
        DeleteSubject,
        GetAllMark,
        GetMark,
        SaveMark,
        DeleteMark
    };

    public enum ResponseType
    {
        Ok,
        Fail
    };

    [Serializable()]
    sealed public class Request
    {
        public RequestType type;
        public Object param;
    }

    [Serializable()]
    sealed public class Response
    {
        public ResponseType type;
        public Object param;
    }
    #endregion
}
