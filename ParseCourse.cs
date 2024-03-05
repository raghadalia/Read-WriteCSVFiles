using CsvHelper.Configuration.Attributes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileStream
{
    class ParseCourse
    {
        [Name("CourseName")]
        public string CourseName { get; private set; }
        [Name("CourseId")]
        public int CourseId { get; private set; }
        private static HashSet<int> uniqueCourseIds = new HashSet<int>();
        public ParseCourse(string rowData)
        {
            string[] data = rowData.Split(',');
            try
            {
                if (data.Length != 2)
                {
                    throw new ArgumentException("Invalid number of fields .Expected have two values separated by commas.");
                }
                // Parse data into properties
                if (int.TryParse(data[0], out int courseId) && courseId > 0)
                {
                    if (uniqueCourseIds.Add(courseId))
                    {
                        this.CourseId = courseId;
                    }
                    else
                    {
                        throw new ArgumentException($"Duplicate CourseId found: {courseId}. CourseId must be unique.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid Course ID .expected to be a positave integer value");
                }
                if (!string.IsNullOrWhiteSpace(data[1]))
                {
                    this.CourseName = data[1];
                }
                else
                {
                    throw new ArgumentException("Course Name must be a string not empty or spaces");
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine ($"Error parsing data: {ex.Message}");

            }
        }
        public override string ToString()
        {
            string str = $"CourseId: {this.CourseId},CourseName: {this.CourseName}";

            return str;
        }
    }
}
