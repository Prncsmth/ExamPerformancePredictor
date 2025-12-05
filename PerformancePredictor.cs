using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamPerformancePredictor
{
public class PerformancePredictor
{
private List<Student> Students;


    public PerformancePredictor(List<Student> students)
    {
        Students = students;
    }

    public void ShowAssessments()
    {
        foreach (var student in Students)
        {
            Console.WriteLine($"\nStudent: {student.Name} (ID: {student.ID})");
            Console.WriteLine("{0,-15} {1,-12} {2,6}", "Subject", "Type", "Score");
            foreach (var a in student.Assessments)
            {
                Console.WriteLine("{0,-15} {1,-12} {2,6:F2}", a.Subject, a.GetType().Name, a.Score);
            }
        }
    }

    public void PredictGrades()
    {
        foreach (var student in Students)
        {
            double finalGrade = student.PredictFinalGrade();
            Console.WriteLine($"\nStudent: {student.Name} (ID: {student.ID})");

            if (finalGrade >= 90)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Predicted Final Grade: {finalGrade:F2} (Excellent!)");
            }
            else if (finalGrade >= 75)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Predicted Final Grade: {finalGrade:F2} (Good, but can improve)");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Predicted Final Grade: {finalGrade:F2} (Needs Improvement!)");
            }
            Console.ResetColor();

            // Improvement suggestions
            Console.WriteLine("Improvement Suggestions:");
            foreach (var a in student.Assessments.Where(a => a.Score < 75))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"- Improve on {a.TypeName()} in {a.Subject}, scored {a.Score:F2}");
                Console.ResetColor();
            }
        }
    }
}

public static class AssessmentExtensions
{
    public static string TypeName(this Assessment a)
    {
        return a.GetType().Name;
    }
}


}
