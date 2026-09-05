using System;
using System.Globalization;

namespace EmployeeManagement
{
    [Flags]
    public enum SecurityLevel : byte
    {
        Guest = 1,
        Developer = 2,
        Secretary = 4,
        DBA = 8
    }

    public enum Gender
    {
        M,
        F
    }

    public class HiringDate
    {
        private int day;
        private int month;
        private int year;

        public HiringDate(int day, int month, int year)
        {
            SetDate(day, month, year);
        }

        public int Day
        {
            get => day;
            set
            {
                if (value < 1 || value > 31)
                    day = 1;
                else
                    day = value;
            }
        }

        public int Month
        {
            get => month;
            set
            {
                if (value < 1 || value > 12)
                    month = 1;
                else
                    month = value;
            }
        }

        public int Year
        {
            get => year;
            set
            {
                if (value < 1900 || value > DateTime.Now.Year)
                    year = DateTime.Now.Year;
                else
                    year = value;
            }
        }

        public void SetDate(int day, int month, int year)
        {
            Year = year;
            Month = month;

            int maxDays = DateTime.DaysInMonth(Year, Month);
            if (day < 1 || day > maxDays)
                Day = 1;
            else
                Day = day;
        }

        public override string ToString()
        {
            return $"{Day:D2}/{Month:D2}/{Year}";
        }
    }

    public class Employee
    {
        private int id;
        private string name;
        private decimal salary;

        public int Id
        {
            get => id;
            set => id = value < 0 ? 0 : value;
        }

        public string Name
        {
            get => name;
            set => name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public SecurityLevel SecurityLevel { get; set; }

        public decimal Salary
        {
            get => salary;
            set => salary = value < 0 ? 0 : value;
        }

        public HiringDate HireDate { get; set; }

        public Gender Gender { get; set; }

        public Employee() : this(0, "Unknown", SecurityLevel.Guest, 0, new HiringDate(1, 1, 2000), Gender.M)
        {
        }

        public Employee(int id, string name, SecurityLevel securityLevel, decimal salary, HiringDate hireDate, Gender gender)
        {
            Id = id;
            Name = name;
            SecurityLevel = securityLevel;
            Salary = salary;
            HireDate = hireDate ?? new HiringDate(1, 1, 2000);
            Gender = gender;
        }

        public override string ToString()
        {
            string formattedSalary = string.Format(CultureInfo.CurrentCulture, "{0:C}", Salary);
            return string.Format("ID: {0}, Name: {1}, Gender: {2}, Security Level: {3}, Salary: {4}, Hire Date: {5}",
                Id, Name, Gender, SecurityLevel, formattedSalary, HireDate);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee[] EmpArr = new Employee[3];

            EmpArr[0] = new Employee(
                id: 101,
                name: "Ahmed Mostafa",
                securityLevel: SecurityLevel.DBA,
                salary: 15000,
                hireDate: new HiringDate(15, 5, 2018),
                gender: Gender.M
            );

            EmpArr[1] = new Employee(
                id: 102,
                name: "Mona Hassan",
                securityLevel: SecurityLevel.Guest,
                salary: 4000,
                hireDate: new HiringDate(1, 9, 2022),
                gender: Gender.F
            );

            SecurityLevel fullPermissions = SecurityLevel.Guest | SecurityLevel.Developer | SecurityLevel.Secretary | SecurityLevel.DBA;
            EmpArr[2] = new Employee(
                id: 103,
                name: "Kareem Ibrahim",
                securityLevel: fullPermissions,
                salary: 22000,
                hireDate: new HiringDate(10, 1, 2015),
                gender: Gender.M
            );

            foreach (var emp in EmpArr)
            {
                Console.WriteLine(emp.ToString());
            }
        }
    }
}
