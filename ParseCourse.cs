using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileStream
{
     class ParseCourse
    {
        [Name("Student Id")]
        public string? CourseName { get; set; }


        [Name("Course Id")]
        public int? CourseId { get; set; }

        public ParseCourse(string rowData)
        {
            string[] data = rowData.Split(',');

            // Parse data into properties
            this.CourseName = data[1];
            this.CourseId = Convert.ToInt32(data[0]);


        }

        public override string ToString()
        {
            string str = $"CourseId: {this.CourseId},CourseName: {this.CourseName}";

            return str;
        }

    }
}
