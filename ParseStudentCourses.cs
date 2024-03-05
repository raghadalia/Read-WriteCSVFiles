using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileStream
{
    class ParseStudentCourses
    {
        [Name("StudentId")]
        public int StudentId { get; set; }


        [Name("CourseId")]
        public int CourseId { get; set; }
        public ParseStudentCourses(string rowData)
        {

            string[] data = rowData.Split(',');

            // Parse data into properties
            try
            {
                if (data.Length != 2)
                {
                    throw new ArgumentException("Invalid number of fields .Expected have two values separated by commas.");
                }
                // Parse data into properties
                if (int.TryParse(data[1], out int courseId) && courseId > 0)
                {
                    
                        this.CourseId = courseId;
                    
                    
                }
                else
                {
                    throw new ArgumentException("Invalid Course ID .expected to be a positave integer value");
                }
                if (int.TryParse(data[0], out int studentId) && studentId > 0)
                {

                    
                        this.StudentId = studentId;
                    
                }
                else
                {
                    throw new ArgumentException("Invalid StudentID. Expeceted an integer value");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error parsing data: {ex.Message}");

            }


        }

        public override string ToString()
        {
            string str = $"StudentId: {this.StudentId},CourseId: {this.CourseId}";

            return str;
        }


    }
}

