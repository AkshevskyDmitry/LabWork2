using System;
using System.IO;


namespace LaboratoryWork2
{
    class Program
    {
        /// <summary>
        /// Точка входа в программу 
        /// </summary>
        /// <param name="args">аргументы командной строки</param>
        static void Main(string[] args)
        {
            string inputFilePath = "C:\\input.txt";
            int age = 45;
            CreateBase(inputFilePath, age);
        }

        /// <summary>
        /// Этот метод создают базу из людей, выводит информацию о тех, кто подходит по указанном возрасту 
        /// </summary>
        /// <param name="filePath">расположение файла</param>
        /// <param name="age">возраст</param>
        public static void CreateBase(string filePath, int age)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);
                int n = Convert.ToInt32(sr.ReadLine());
                Persona[] men = new Persona[n];
                for (int i = 0; i < n; i++)
                {
                    switch (sr.ReadLine())
                    {
                        case "A":
                            men[i] = new Applicant(sr.ReadLine(), Convert.ToDateTime(sr.ReadLine()), sr.ReadLine());
                            break;
                        case "S":
                            men[i] = new Student(sr.ReadLine(), Convert.ToDateTime(sr.ReadLine()), sr.ReadLine(), Convert.ToInt32(sr.ReadLine()));
                            break;
                        case "L":
                            men[i] = new Lecture(sr.ReadLine(), Convert.ToDateTime(sr.ReadLine()), sr.ReadLine(), (JobPosition)Convert.ToInt32(sr.ReadLine()), Convert.ToDateTime(sr.ReadLine()));
                            break;
                    }
                }
                foreach (Persona human in men)
                {
                    if (human.CurrentAge() < age)
                        human.PrintInfo();
                }
            }
            catch
            {
                Console.WriteLine("Некорректные входные данные");
            }
        }

        /// <summary>
        /// абстрактный класс, абстракция персоны 
        /// </summary>
        abstract class Persona
        {
            public abstract void PrintInfo();
            public abstract int CurrentAge();
        }
        /// <summary>
        /// класс описывающий персону Applicant
        /// </summary>
        class Applicant : Persona
        {
            public string LastName { get; set; }
            public DateTime DateOfBith { get; set; }
            public string Faculty { get; set; }

            public Applicant(string _lastName, DateTime _dateOfBirth, string _faculty)
            {
                LastName = _lastName;
                DateOfBith = _dateOfBirth;
                Faculty = _faculty;
            }

            public override void PrintInfo()
            {
                Console.WriteLine("Фамилия: {0}, дата рождения: {1}, факультет: {2}", LastName, DateOfBith.ToShortDateString(), Faculty);
            }
            public override int CurrentAge()
            {
                return (int)DateTime.Now.Subtract(DateOfBith).Days / 365;
            }
        }
        /// <summary>
        /// класс описывающий персону Student
        /// </summary>
        class Student : Persona
        {
            public string LastName { get; set; }
            public DateTime DateOfBith { get; set; }
            public string Faculty { get; set; }
            public int YearOfStudy { get; set; }

            public Student(string _lastName, DateTime _dateOfBirth, string _faculty, int _yearOfStudy)
            {
                LastName = _lastName;
                DateOfBith = _dateOfBirth;
                Faculty = _faculty;
                YearOfStudy = _yearOfStudy;
            }
            public override void PrintInfo()
            {
                Console.WriteLine("Фамилия: {0}, дата рождения: {1}, факультет: {2}, курс: {3}", LastName, DateOfBith.ToShortDateString(), Faculty, YearOfStudy);
            }
            public override int CurrentAge()
            {
                return (int)DateTime.Now.Subtract(DateOfBith).Days / 365;
            }
        }
        /// <summary>
        /// класс описывающий персону Lecture
        /// </summary>
        class Lecture : Persona
        {
            public string LastName { get; set; }
            public DateTime DateOfBith { get; set; }
            public string Faculty { get; set; }
            public JobPosition Position { get; set; }
            public int Expierence { get; set; }

            public Lecture(string _lastName, DateTime _dateOfBirth, string _faculty, JobPosition _position, DateTime _hireDate)
            {
                LastName = _lastName;
                DateOfBith = _dateOfBirth;
                Faculty = _faculty;
                Position = _position;
                Expierence = DateTime.Now.Subtract(_hireDate).Days / 365;
            }
            public override void PrintInfo()
            {
                Console.WriteLine("Фамилия: {0}, дата рождения: {1}, факультет: {2}, должность: {3}, стаж {4}", LastName, DateOfBith.ToShortDateString(), Faculty, Position, Expierence);
            }
            public override int CurrentAge()
            {
                return (int)DateTime.Now.Subtract(DateOfBith).Days / 365;
            }
        }
        /// <summary>
        /// Перечисление всех профессий в университете
        /// </summary>
        enum JobPosition
        {
            Assistant,
            Dean,
            Professor
        }
    }
}
