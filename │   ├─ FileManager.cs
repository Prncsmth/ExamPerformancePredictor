using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamPerformancePredictor
{
public static class FileManager
{
private static string filePath = "students.txt";


    public static void Save(List<Student> students)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.ID}|{student.Name}");
                foreach (var a in student.Assessments)
                {
                    writer.WriteLine($"{a.GetType().Name}|{a.Subject}|{a.Score}");
                }
                writer.WriteLine("---");
            }
        }
    }

    public static List<Student> Load()
    {
        List<Student> students = new List<Student>();
        if (!File.Exists(filePath)) return students;

        var lines = File.ReadAllLines(filePath);
        Student? currentStudent = null;

        foreach (var line in lines)
        {
            if (line == "---")
            {
                currentStudent = null;
                continue;
            }

            if (line.Contains("|"))
            {
                var parts = line.Split('|');
                if (currentStudent == null)
                {
                    currentStudent = new Student(parts[1]!, parts[0]!);
                    students.Add(currentStudent);
                }
                else
                {
                    Assessment? assessment = parts[0] switch
                    {
                        "Quiz" => new Quiz(parts[1]!, double.Parse(parts[2]!)),
                        "Assignment" => new Assignment(parts[1]!, double.Parse(parts[2]!)),
                        "Test" => new Test(parts[1]!, double.Parse(parts[2]!)),
                        _ => null
                    };
                    if (assessment != null)
                        currentStudent.AddAssessment(assessment);
                }
            }
        }

        return students;
    }
}


}
