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

            WriteToCsv(results, "C:\\Users\\Hp\\source\\repos\\FileStream\\JoinResult.csv");
            var sortedStudents = students.OrderBy(student => student.StudentName).ToList();
            //Write the sorted students  to the SortedStudents.csv File.
            WriteToCsv(sortedStudents, "C:\\Users\\Hp\\source\\repos\\FileStream\\SortedStudents.csv");
            //Write the  students that are older than 20  to the GreaterThan20Students.csv File.
            var greaterThan20 = students.Where(student => student.StudentAge > 20).ToList();
            WriteToCsv(greaterThan20, "C:\\Users\\Hp\\source\\repos\\FileStream\\GreaterThan20Students.csv");
            //Write the  average  Age of students  to the averageAge.csv File.
            double averageAge = students.Average(student => student.StudentAge);
            WriteToCsv(new List<double> { averageAge }, "C:\\Users\\Hp\\source\\repos\\FileStream\\averageAge.csv");

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