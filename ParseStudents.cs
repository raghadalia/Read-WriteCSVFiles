using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FileStream
{
    public class ParseStudent
    {
        [Name("Student Name")]
        public string StudentName { get; private set; }
        [Name("Student Id")]
        public int StudentId { get; private set; }
        [Name("Student Age")]
        public int StudentAge { get; private set; }
        private static HashSet<int> uniqueCourseIds = new HashSet<int>();
        public ParseStudent(string rowData)
        {
            string[] data = rowData.Split(',');
            try
            {
                if (data.Length != 3)
                {
                    throw new ArgumentException("Invalid number of fields it should be three values separated by commas.");
                }
                // Parse data into properties
                if (int.TryParse(data[0], out int studentId) && studentId > 0)
                {

                    if (uniqueCourseIds.Add(studentId))
                    {
                        this.StudentId = studentId;
                    }
                    else
                    {
                        throw new ArgumentException($"Duplicate CourseId found: {studentId}. CourseId must be unique.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid StudentID. Expeceted an integer value");
                }
                if (!string.IsNullOrWhiteSpace(data[1]))
                {
                    this.StudentName = data[1];
                }
                else
                {
                    throw new ArgumentException("Invalid name. It can't be empty or contain only spaces.");
                }

                if (int.TryParse(data[2], out int studentAge))
                {
                    this.StudentAge = studentAge;
                }
                else
                {
                    throw new ArgumentException("Invalid StudentAge. Expeceted an integer value");
                }
            }
            catch (FormatException ex)
            {
                // Handle FormatException
                Console.WriteLine($"Error parsing data: {ex.Message}");
            }
            catch (OverflowException ex)
            {
                // Handle OverflowException (value is too large or too small)
                Console.WriteLine($"Error parsing data: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
        public override string ToString()
        {
            string str = $"StudentId: {this.StudentId}, Name: {this.StudentName} , Age: {this.StudentAge}";

            return str;
        }
    }
}

