using StudentMemberWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMemberWebAPI.Repositories
{
    public class StudentMemberRepositories
    {
        private static Lazy<List<StudentMember>> container = new Lazy<List<StudentMember>>(() => Initialize(), true);

        private static List<StudentMember> Initialize()
        {
            return new List<StudentMember>
            {
                new StudentMember(1,"Emre KÖKÇÜ",23,"Computer Engineer",4),
                new StudentMember(2,"Mustafa ERKEN",23,"Construction Engineer",1),
                new StudentMember(3,"Osman KURT",23,"Environmental Engineer",2),
                new StudentMember(4,"Özde ACARKAN",23,"Electricity Engineer",4),
                new StudentMember(5,"Sevinç AK",23,"Electronic Engineer",3),
                new StudentMember(6,"Cihan IŞINAY",23,"Computer Engineer",1)

            };
        }

        public static StudentMember Add(string FullName, int Age, string Section, int ClassNumber)
        {
            int lastId = container.Value.Max(sM => sM.Id);
            StudentMember newStudentMember = new StudentMember(lastId + 1, FullName, Age, Section, ClassNumber);
            container.Value.Add(newStudentMember);
            return newStudentMember;
        }

        public static List<StudentMember> Get() {
            return container.Value;
        }

        public static StudentMember Get(int Id)
        {
            return container.Value.SingleOrDefault(sM => sM.Id == Id);
        }

        public static void Remove(int Id) {
            container.Value.Remove(Get(Id));
        }

        public static void Update(StudentMember StudentMember) {
            Remove(StudentMember.Id);
            container.Value.Add(StudentMember);
        }

        public static bool IsExist(int Id)
        {
            return container.Value.Any(sM => sM.Id == Id);
        }

        public static bool IsExist(string FullName)
        {
            return container.Value.Any(sM => sM.FullName == FullName);
        }
    }
}