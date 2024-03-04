using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
namespace FileStream
{

    class Program
    {
    static void Main(string[] args)
        {
            
            // Define delegates for parsing student and course records
            Func<string, ParseStudent> parseStudent = line => new ParseStudent(line);
            Func<string, ParseCourse> parseCourse = line => new ParseCourse(line);
            Func<string, ParseStudentCourses> parseStudentCourse = line => new ParseStudentCourses(line);

            // Read students from CSV
            var students = ReadFromCSv("C:\\Users\\Hp\\source\\repos\\FileStream\\students.csv", parseStudent);

            // Display students
            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

            // Read courses from CSV
            var courses = ReadFromCSv("C:\\Users\\Hp\\source\\repos\\FileStream\\Courses.csv", parseCourse);

            // Display courses
            Console.WriteLine("\nCourses:");
            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }
            // Read studentCourses from CSV
            var studentCourses = ReadFromCSv("C:\\Users\\Hp\\source\\repos\\FileStream\\StudentCourses.csv", parseStudentCourse);
            // Display studentCourses
            Console.WriteLine("\nStudentCourses:");
            foreach (var studentCourse in studentCourses)
            {
                Console.WriteLine(studentCourse);
            }
            var results = (from student in students
                           join sc in studentCourses on student.StudentId equals sc.StudentId
                           join c in courses on sc.CourseId equals c.CourseId
                           select new
                           {
                               student.StudentId,
                               student.StudentName,
                               c.CourseId,
                               c.CourseName
                           })
            .ToList();
            //Write the output of the inner join to the JoinResult.csv File.
            QueryAndWriteToCsv(results, result => result.StudentName, "C:\\Users\\Hp\\source\\repos\\FileStream\\JoinResult.csv");

            //Write the sorted students  to the SortedStudents.csv File.
            QueryAndWriteToCsv(students, student => student.StudentName, "C:\\Users\\Hp\\source\\repos\\FileStream\\SortedStudents.csv");
            //Write the  students that are older than 20  to the GreaterThan20Students.csv File.
            var greaterThan20 = students.Where(student => student.StudentAge > 20).ToList();
            QueryAndWriteToCsv(greaterThan20, student => student.StudentName, "C:\\Users\\Hp\\source\\repos\\FileStream\\GreaterThan20Students.csv");
            //Write the  average  Age of students  to the averageAge.csv File.
            double averageAge = students.Average(student => student.StudentAge);
            WriteToCsv(new List<double> { averageAge }, "C:\\Users\\Hp\\source\\repos\\FileStream\\averageAge.csv");

            Console.ReadKey();
        }

        static void QueryAndWriteToCsv<T, TKey>(List<T> records, Func<T, TKey> keySelector, string filePath)
        {
            try
            {
                var QueryRecords = records.OrderBy(keySelector).ToList();

                if (IsExist(filePath))
                {
                    using (var writer = new StreamWriter(filePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(QueryRecords);
                    }
                }
                else
                {
                    Console.WriteLine($"The file {filePath} doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void WriteToCsv<T>(List<T> records, string filePath)
        {
            try
            {
                if (IsExist(filePath))
                {
                    using (var writer = new StreamWriter(filePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(records);
                    }
                }
                else
                {
                    Console.WriteLine($"The file {filePath} doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static List<T> ReadFromCSv<T>(string fileName, Func<string, T> parseFunction)
        {
            List<T> records = new List<T>();

            if (IsExist(fileName))
            {
                string[] csvLines = File.ReadAllLines(fileName);

                // Skip header row
                for (int i = 1; i < csvLines.Length; i++)
                {
                    T record = parseFunction(csvLines[i]);
                    records.Add(record);
                }


            }
            else
            {
                Console.WriteLine($"The file {fileName} doesn't exist.");
            }

            return records;
        }

       static bool IsExist(string fileName)
        {
            return File.Exists(fileName);
        }
    }

}