//using CsvHelper.Configuration.Attributes;
//using CsvHelper;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StudentCourses
//{
//    class WriteToCSV
//    {

//        class Employee
//        {
//            [Name("Student Name")]
//            public string? StudentName { get; set; }


//            [Name("Student Id")]
//            public int? StudentId { get; set; }

//            [Name("Student Age")]
//            public int? StudentAge { get; set; }


//        }

//        class Program
//        {
//            static void Main(string[] args)
//            {
//                var sortedStudents = students.OrderBy(student => student.StudentName);
               
//                //        var employees = new List<Employee> {
//                //   new Employee {
//                //      FirstName = "John", LastName = "Doe", JoinedDate = new DateOnly(2020, 1, 1), Salary = 50000.00M, Active = "Yes"
//                //   },
//                //   new Employee {
//                //      FirstName = "Jane", LastName = "Doe", JoinedDate = new DateOnly(2021, 6, 15), Salary = 60000.00M, Active = "Yes"
//                //   },
//                //   new Employee {
//                //      FirstName = "Bob", LastName = "Smith", JoinedDate = new DateOnly(2019, 3, 10), Salary = 70000.00M, Active = "No"
//                //   }
//                //};


//                using var writer = new StreamWriter("C:\\Users\\Hp\\source\\repos\\StudentCourses.csproj\\SLinq.csv");
//                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);


//                csv.WriteHeader<Employee>();
//                csv.NextRecord();
//                foreach (var employee in students)
//                {
//                    csv.WriteRecord(employee);
//                    csv.NextRecord();
//                }

//            }
//        }
//    }
//}
