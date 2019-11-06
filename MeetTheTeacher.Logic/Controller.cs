using System;
using System.Collections.Generic;
using System.Text;

namespace MeetTheTeacher.Logic
{
    /// <summary>
    /// Verwaltung der Lehrer (mit und ohne Detailinfos)
    /// </summary>
    public class Controller
    {
        private readonly List<Teacher> _teachers;
        private readonly Dictionary<string, int> _details;

        /// <summary>
        /// Liste für Sprechstunden und Dictionary für Detailseiten anlegen
        /// </summary>
        public Controller(string[] teacherLines, string[] detailsLines)
        {
            _teachers = new List<Teacher>();
            _details = new Dictionary<string, int>();
           
           
            InitDetails(detailsLines);
            InitTeachers(teacherLines);


        }

        public int Count => _teachers.Count;

        public int CountTeachersWithoutDetails => (_teachers.Count - _details.Count);

           
        /// <summary>
        /// Anzahl der Lehrer mit Detailinfos in der Liste
        /// </summary>
        public int CountTeachersWithDetails => _details.Count;
        /// <summary>
        /// Aus dem Text der Sprechstundendatei werden alle Lehrersprechstunden 
        /// eingelesen. Dabei wird für Lehrer, die eine Detailseite haben
        /// ein TeacherWithDetails-Objekt und für andere Lehrer ein Teacher-Objekt angelegt.
        /// </summary>
        /// <returns>Anzahl der eingelesenen Lehrer</returns>
        private void InitTeachers(string[] lines)
        {
            foreach (KeyValuePair<string, int> pair in _details)
            {
                foreach (string line in lines)
                {
                    string[] spL = line.Split(";");

                    if (String.Compare(spL[0], pair.Key)==0)
                    {
                        _teachers.Add(new TeacherWithDetail(spL[0], spL[1], spL[2], spL[3], spL[4], _details[pair.Key]));
                    }
                }
            }
            
            string[] names = new string[_teachers.Count];
            for (int i = 0; i < _teachers.Count; i++)
            {
                names[i] = _teachers[i].Name;
            }
            
            foreach (string line in lines)
            {
                string[] spL = line.Split(";");
                 _teachers.Add(new Teacher(spL[0], spL[1], spL[2], spL[3], spL[4]));
                    
            }  
        }

        /// <summary>
        /// Lehrer, deren Name in der Datei IgnoredTeachers steht werden aus der Liste 
        /// entfernt
        /// </summary>
        public void DeleteIgnoredTeachers(string[] names)
        {
            for(int i = 0; i < _teachers.Count; i++ )
            {
                foreach(string name in names)
                {
                    if(_teachers[i].Name.Equals(name))
                    {
                        _teachers.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Sucht Lehrer in Lehrerliste nach dem Namen
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns>Index oder -1, falls nicht gefunden</returns>
        private int FindIndexForTeacher(string teacherName)
        {
            for(int i = 0; i < _teachers.Count; i++)
            {
                if(teacherName.Equals(_teachers[i].Name))
                {
                    return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// Ids der Detailseiten für Lehrer die eine
        /// derartige Seite haben einlesen.
        /// </summary>
        private void InitDetails(string[] lines)
        {
          
            for(int i = 0; i < lines.Length; i++)
            {
                string[] splittedLine = lines[i].Split(";");
                _details.Add(splittedLine[0],Convert.ToInt32(splittedLine[1]) );
            }

        }

        /// <summary>
        /// HTML-Tabelle der ganzen Lehrer aufbereiten.
        /// </summary>
        /// <returns>Text für die Html-Tabelle</returns>
        public string GetHtmlTable()
        {
            StringBuilder mysb = new StringBuilder();
            mysb.Append("<table id = tabelle>");
            mysb.Append("\n");
            mysb.Append("<tr>\n");
            mysb.Append("<th align =\"center\">Name <\th>\n");
            mysb.Append("<th align =\"center\">Tag <\th>\n");
            mysb.Append("<th align =\"center\">Zeit <\th>\n");
            mysb.Append("<th align =\"center\">Raum <\th>\n");
            mysb.Append("<tr>\n");
            mysb.Append("\n");
            mysb.Append("\n");
            _teachers.Sort();

            for(int i = 0; i < _teachers.Count; i++)
            {
                mysb.Append("<tr>\n");
                mysb.Append(_teachers[i].GetTeacherHtmlRow());
                mysb.Append("\n");
                mysb.Append("</tr>\n");
                mysb.Append("\n");
                mysb.Append("\n");
            }
            return mysb.ToString();
        }
    }
}
 