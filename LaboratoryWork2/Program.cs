using System;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;


namespace LaboratoryWork2
{
    public class Program
    {
        /// <summary>
        /// Точка входа в программу 
        /// </summary>
        /// <param name="args">аргументы командной строки</param>
        public static void Main(string[] args)
        {
            string inputFilePath = "C:\\input.txt";
            int age = 60;
            CreateBase(inputFilePath, age);
            Trace.Unindent();
            Trace.WriteLine("Метод CreateBase выполнен");
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
                Trace.WriteLine("Метод CreateBase запущен");
                Trace.Indent();
                StreamReader sr = new StreamReader(filePath);
                int n = Convert.ToInt32(sr.ReadLine());
                Persona[] men = new Persona[n];
                XmlSerializer fmen = new XmlSerializer(typeof(Persona[]));
                FileStream fs = new FileStream("C:\\Users\\Dmitr\\Desktop\\XMLout.xml", FileMode.Create);
                fmen.Serialize(fs, men);
                for (int i = 0; i < n; i++)
                {
                    switch (sr.ReadLine())
                    {
                        case "A":
                            men[i] = new Applicant(sr.ReadLine(), Convert.ToDateTime(sr.ReadLine()), sr.ReadLine());
                            XmlSerializer fApp = new XmlSerializer(typeof(Applicant));
                            fApp.Serialize(fs, men[i]);
                            Trace.WriteLine("создается объект класса Applicant");
                            break;
                        case "S":
                            men[i] = new Student(sr.ReadLine(), Convert.ToDateTime(sr.ReadLine()), sr.ReadLine(), Convert.ToInt32(sr.ReadLine()));
                            XmlSerializer fStud = new XmlSerializer(typeof(Student));
                            fStud.Serialize(fs, men[i]);
                            Trace.WriteLine("создается объект класса Student");
                            break;
                        case "L":
                            men[i] = new Lecture(sr.ReadLine(), Convert.ToDateTime(sr.ReadLine()), sr.ReadLine(), (JobPosition)Convert.ToInt32(sr.ReadLine()), Convert.ToDateTime(sr.ReadLine()));
                            XmlSerializer fLec = new XmlSerializer(typeof(Lecture));
                            fLec.Serialize(fs, men[i]);
                            Trace.WriteLine("создается объект класса Lecture");
                            break;
                    }
                }
                fs.Close();
                Trace.Indent();
                foreach (Persona human in men)
                {
                    if (human.CurrentAge() < age)
                    {
                        Trace.WriteLine("Возраст посчитан успешно");
                        human.PrintInfo();
                    }
                       
                }
                Trace.Unindent();
            }
            catch
            {
                Trace.WriteLine("Некорректны входные данные");
                Console.WriteLine("Некорректные входные данные");
            }
        }

        /// <summary>
        /// абстрактный класс, абстракция персоны 
        /// </summary>
        [Serializable]
        public abstract class Persona
        {
            public abstract void PrintInfo();
            public abstract int CurrentAge();

            public Persona()
            {

            }
        }
        /// <summary>
        /// класс описывающий персону Applicant
        /// </summary>
        [Serializable]
        public class Applicant : Persona
        {
            public string LastName { get; set; }
            public DateTime DateOfBith { get; set; }
            public string Faculty { get; set; }

            public Applicant()
            {

            }
            public Applicant(string _lastName, DateTime _dateOfBirth, string _faculty)
            {
                LastName = _lastName;
                DateOfBith = _dateOfBirth;
                Faculty = _faculty;
            }

            public override void PrintInfo()
            {
                Trace.Indent();
                Trace.WriteLine("Метод PrintInfo запущен");
                Console.WriteLine("Фамилия: {0}, дата рождения: {1}, факультет: {2}", LastName, DateOfBith.ToShortDateString(), Faculty);
                Trace.WriteLine("Метод PrintInfo выполнен");
                Trace.Unindent();
            }
            public override int CurrentAge()
            {
                return (int)DateTime.Now.Subtract(DateOfBith).Days / 365;      
            }
        }
        /// <summary>
        /// класс описывающий персону Student
        /// </summary>
        [Serializable]
        public class Student : Persona
        {
            public string LastName { get; set; }
            public DateTime DateOfBith { get; set; }
            public string Faculty { get; set; }
            public int YearOfStudy { get; set; }

            public Student()
            {

            }
            public Student(string _lastName, DateTime _dateOfBirth, string _faculty, int _yearOfStudy)
            {
                LastName = _lastName;
                DateOfBith = _dateOfBirth;
                Faculty = _faculty;
                YearOfStudy = _yearOfStudy;
            }
            public override void PrintInfo()
            {
                Trace.Indent();
                Trace.WriteLine("Метод PrintInfo запущен");
                Console.WriteLine("Фамилия: {0}, дата рождения: {1}, факультет: {2}, курс: {3}", LastName, DateOfBith.ToShortDateString(), Faculty, YearOfStudy);
                Trace.WriteLine("Метод PrintInfo выполнен");
                Trace.Unindent();
            }
            public override int CurrentAge()
            {
                return (int)DateTime.Now.Subtract(DateOfBith).Days / 365;
            }
        }
        /// <summary>
        /// класс описывающий персону Lecture
        /// </summary>
        [Serializable]
        public class Lecture : Persona
        {
            public string LastName { get; set; }
            public DateTime DateOfBith { get; set; }
            public string Faculty { get; set; }
            public JobPosition Position { get; set; }
            public int Expierence { get; set; }

            public Lecture()
            {

            }
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
                Trace.Indent();
                Trace.WriteLine("Метод PrintInfo запущен");
                Console.WriteLine("Фамилия: {0}, дата рождения: {1}, факультет: {2}, должность: {3}, стаж {4}", LastName, DateOfBith.ToShortDateString(), Faculty, Position, Expierence);
                Trace.WriteLine("Метод PrintInfo выполнен");
                Trace.Unindent();
            }
            public override int CurrentAge()
            {
                return (int)DateTime.Now.Subtract(DateOfBith).Days / 365;
            }
        }
        /// <summary>
        /// Перечисление всех профессий в университете
        /// </summary>
        public enum JobPosition
        {
            Assistant,
            Dean,
            Professor
        }
    }
}
