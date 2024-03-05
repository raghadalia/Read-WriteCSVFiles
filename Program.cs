using CsvHelper;
using System;
using System.Collections;
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
            string studentRelativePath = Path.Combine(Directory.GetCurrentDirectory(), "CSVFiles\\students.csv");
            string courseRelativePath = Path.Combine(Directory.GetCurrentDirectory(), "CSVFiles\\Courses.csv");
            string studentCoursesRelativePath = Path.Combine(Directory.GetCurrentDirectory(), "CSVFiles\\StudentCourses.csv");
            string liq1RelativePath = Path.Combine(Directory.GetCurrentDirectory(), "CSVFiles\\SortedStudents.csv");
            string linq2RelativePath = Path.Combine(Directory.GetCurrentDirectory(), "CSVFiles\\GreaterThan20Students.csv");
            string linq3RelativePath = Path.Combine(Directory.GetCurrentDirectory(), "CSVFiles\averageAge.csv");
            string linq4RelativePath = Path.Combine(Directory.GetCurrentDirectory(), "CSVFiles\\JoinResult.csv");
            var students = ReadFromCSv(studentRelativePath, parseStudent);
            // Display students
            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            // Read courses from CSV
            var courses = ReadFromCSv(courseRelativePath, parseCourse);

            // Display courses
            Console.WriteLine("\nCourses:");
            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }
            // Read studentCourses from CSV
            var studentCourses = ReadFromCSv(studentCoursesRelativePath, parseStudentCourse);
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
            WriteToCsv(results, linq4RelativePath);
            var sortedStudents = students.OrderBy(student => student.StudentName).ToList();
            //Write the sorted students  to the SortedStudents.csv File.
            WriteToCsv(sortedStudents, liq1RelativePath);
            //Write the  students that are older than 20  to the GreaterThan20Students.csv File.
            var greaterThan20 = students.Where(student => student.StudentAge > 20).ToList();
            WriteToCsv(greaterThan20,linq2RelativePath);
            //Write the  average  Age of students  to the averageAge.csv File.
            double averageAge = students.Average(student => student.StudentAge);
            WriteToCsv(new List<double> { averageAge }, linq3RelativePath);

            Console.ReadKey();
        }
        static void WriteToCsv<T>(List<T> records, string filePath)
        {
            StreamWriter writer = null;
            CsvWriter csv = null;
            try
            {
                if (IsExist(filePath))
                {
                    writer = new StreamWriter(filePath);
                    csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                    csv.WriteRecords(records);
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
            finally
            {
                // Dispose and close resources in the finally block
                if (csv != null)
                {
                    csv.Dispose();
                }

                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
        static List<T> ReadFromCSv<T>(string fileName, Func<string, T> parseFunction)
        {
            List<T> records = new List<T>();
            try
            {
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
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return records;
        }
        static bool IsExist(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
