using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Klasse, die einen Detaileintrag mit Link auf dem Namen realisiert.
    /// </summary>
    public class TeacherWithDetail : Teacher
    {

        private int _id;

        public  TeacherWithDetail(string name, string tag, string zeit, string stunde, string raum, int detail) : base(name, tag, zeit, stunde, raum)
        {
            this._id = detail;
        }

        public override string GetTeacherHtmlRow()
        {
            return $"<a href=\"?id={_id}\">{Name}</a>";
        }
        
    }
}
