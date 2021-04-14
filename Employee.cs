using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home0330_8_7
{
    public class Employee
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public string BirthCity { get; set; }
        public Guid UniqueCode { get; set; } = Guid.NewGuid();

        public override string ToString()
        {
            return $"{SurName}{Name}{MiddleName}, {BirthDate.ToShortDateString()}, {BirthCity}";
        }
        public static Employee DeepCopy(Employee employee)
        {
            return new Employee()
            {
                SurName = employee.SurName,
                Name = employee.Name,
                MiddleName = employee.MiddleName,
                BirthCity = employee.BirthCity,
                BirthDate = employee.BirthDate,
                UniqueCode = Guid.NewGuid()
            };
        }

        public static Employee[] employees = new Employee[]
        {
            new Employee()
            {
                SurName = "Petrenko",
                Name = "Petro",
                MiddleName = "Petrovich",
                BirthDate = new DateTime(1985, 02, 02),
                BirthCity = "Petrivka"
            },
            new Employee()
            {
                SurName = "Ignatenko",
                Name = "Ignat",
                MiddleName = "Ignatovich",
                BirthDate = new DateTime(1986, 06, 16),
                BirthCity = "Ignativka"
            },
            new Employee()
            {
                SurName = "Sergienko",
                Name = "Sergiy",
                MiddleName = "Sergiyovich",
                BirthDate = new DateTime(1987, 07, 17),
                BirthCity = "Sergiivka"
            },
            new Employee()
            {
                SurName = "Ivanko",
                Name = "Ivanka",
                MiddleName = "Ivanivna",
                BirthDate = new DateTime(1988, 08, 18),
                BirthCity = "Ivanivka"
            },
            new Employee()
            {
                SurName = "Mirenko",
                Name = "Maria",
                MiddleName = "Sergyivna",
                BirthDate = new DateTime(1989, 09, 19),
                BirthCity = "Petrivka"
            },
            new Employee()
            {
                SurName = "Maksimov",
                Name = "Maksim",
                MiddleName = "Maksimovich",
                BirthDate = new DateTime(1990, 01, 10),
                BirthCity = "Maksimovka"
            },
            new Employee()
            {
                SurName = "Chebotar",
                Name = "Anna",
                MiddleName = "Ivanivna",
                BirthDate = new DateTime(1991, 02, 22),
                BirthCity = "Ivanivka"
            },
            new Employee()
            {
                SurName = "Olesko",
                Name = "Sydir",
                MiddleName = "Sydorovich",
                BirthDate = new DateTime(1992, 03, 23),
                BirthCity = "Sydirivka"
            },
            new Employee()
            {
                SurName = "Marcipan",
                Name = "Marcik",
                MiddleName = "Marcikovich",
                BirthDate = new DateTime(1993, 04, 24),
                BirthCity = "Marcipanivka"
            },
            new Employee()
            {
                SurName = "Golodranko",
                Name = "Volodya",
                MiddleName = "Terendylovich",
                BirthDate = new DateTime(1994, 05, 25),
                BirthCity = "Kryvodumovka"
            }
        };
    }
}
