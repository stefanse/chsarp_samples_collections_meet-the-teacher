using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Verwaltet einen Eintrag in der Sprechstundentabelle
    /// Basisklasse für TeacherWithDetail
    /// </summary>
    public class Teacher : IComparable<Teacher>
    {
        private string _day;
        private string _hour;
        private string _room;
        private string _time;


        //"TeacherA", "MO", "10:00-10:50", "3", "1234", 999
        public Teacher(string name, string tag, string zeit, string stunde, string raum)
        {
            this.Name = name;
            this._day = tag;
            this._hour = stunde;
            this._room = raum;
            this._time = zeit;
        }

        public string Name
        {   get;
            set;
        }
         

       public int CompareTo(Teacher other)
        {

            if(other == null) 
            {
                throw new ArgumentException(nameof(other));
            }
            return this.Name.CompareTo(other.Name);
        }


        public virtual string GetHtmlForName()
        {
            return Name;
        }
        
        public virtual string GetTeacherHtmlRow()
        {
            return
            $"<td align=\"left\">{GetHtmlForName()}</td>\n" +
            $"<td align=\"left\">{_day}</td>\n" +
            $"<td align=\"left\">{_hour}</td>\n" +
            $"<td align=\"left\">{_room}</td>";
               
        }


        public String ToSTring()
        {
            return string.Format(Name, _day, _time, _room) ;
        }

    }
 } 

