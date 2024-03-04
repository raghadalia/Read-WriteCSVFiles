using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileStream
{
    class ParseStudentCourses
    {
        [Name("Student Id")]
        public int? StudentId { get; set; }


        [Name("Course Id")]
        public int? CourseId { get; set; }

        public ParseStudentCourses(string rowData)
        {
            string[] data = rowData.Split(',');

            // Parse data into properties
            this.StudentId = Convert.ToInt32(data[0]);
            this.CourseId = Convert.ToInt32(data[1]);


        }

        public override string ToString()
        {
            string str = $"StudentId: {this.StudentId},CourseId: {this.CourseId}";

            return str;
        }


    }
}

