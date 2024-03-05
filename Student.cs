using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVReader_Class
{
    public class Student
    {
        [Name("StudentName")]
        public string? StudentName { get; set; }


        [Name("StudentId")]
        public int? StudentId { get; set; }

        [Name("StudentAge")]
        public int? StudentAge { get; set; }

        public Student(string rowData)
        {
            string[] data = rowData.Split(',');

            // Parse data into properties
            this.StudentId = Convert.ToInt32(data[0]);
            this.StudentName = data[1];
            this.StudentAge = Convert.ToInt32(data[2]);
           
        }

        public override string ToString()
        {
            string str = $"StudentId: { this.StudentId}, Name: { this.StudentName} , Age: { this.StudentAge}";
            
            return str;
        }
        

    }

}

