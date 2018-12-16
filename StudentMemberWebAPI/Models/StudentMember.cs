using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMemberWebAPI.Models
{
    public class StudentMember
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Section { get; set; }
        public int ClassNumber { get; set; }


        //Constructor
        public StudentMember(int Id, string FullName, int Age, string Section, int ClassNumber) {
            this.Id = Id;
            this.FullName = FullName;
            this.Age = Age;
            this.Section = Section;
            this.ClassNumber = ClassNumber;
        }
       
    }
}