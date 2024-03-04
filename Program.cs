using System;
using CsvHelper;
using CSVReader_Class;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

class Program
{
   static  bool IsExist(string path)
    {
        if (File.Exists(path))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    static void Main(string[] args)
    {
         // Students
            var students = new List<Student>();
        string fileName = "C:\\Users\\Hp\\source\\repos\\FileStream\\students.csv";
        // Read the contents of the CSV files as individual lines
        if (IsExist(fileName))
        {
            string[] csvLines = System.IO.File.ReadAllLines(fileName);
           

            // Split each row into column data
            for (int i = 1; i < csvLines.Length; i++)
            {
                Student st = new Student(csvLines[i]);
                students.Add(st);
            }

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine(students[i]);
            }

        }
        else
        {
           Console.WriteLine($"The file {fileName} doesn't exist.");
        }

        

        var sortedStudents = students.OrderBy(student => student.StudentName);
   
        string path = "C:\\Users\\Hp\\source\\repos\\FileStream\\SortedStudents.csv";
        if (IsExist(path))
        {
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);


            csv.WriteHeader<Student>();
            csv.NextRecord();
            foreach (var student in sortedStudents)
            {
                csv.WriteRecord(student);
                csv.NextRecord();
            }
        }
        else
        {
            Console.WriteLine($"The file {fileName} doesn't exist.");
        }

        var greaterThan20 = students.Where(std => std.StudentAge > 20);
        Console.WriteLine("\nNames of students who are older than 20 years:");
        foreach (var student in greaterThan20)
        {
            Console.WriteLine($"Name: {student.StudentName}, Age: {student.StudentAge}");
        }

        int averageAge = (int)students.Average(student => student.StudentAge);
        Console.WriteLine($"\nAverage age of all students: {averageAge}\n");


        Console.ReadKey();
    }
}