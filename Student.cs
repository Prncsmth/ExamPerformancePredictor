using System;
using System.Collections.Generic;

namespace ExamPerformancePredictor
{
public partial class Student
{
public string Name { get; set; }
public string ID { get; set; }
public List<Assessment> Assessments { get; set; }


    public Student(string name, string id)
    {
        Name = name;
        ID = id;
        Assessments = new List<Assessment>();
    }
}


}
