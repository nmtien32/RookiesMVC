using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookiesMVC.Models
{
    public class Person
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime DoB { get; set; }
        public string? Phone { get; set; }
        public string? BirthPlace { get; set; }
        public bool IsGraduated { get; set; }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DoB.Year;
            }
        }
        public string FullName
        {
            get
            {
                return FirstName + LastName;
            }
        }
        public Person(string firstName, string lastName, string gender, DateTime dob, string number, string birthPlace, bool isGraduated)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DoB = dob;
            Phone = number;
            BirthPlace = birthPlace;
            IsGraduated = isGraduated;
        }
    }
}