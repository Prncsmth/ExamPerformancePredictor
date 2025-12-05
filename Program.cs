using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamPerformancePredictor
{
class Program
{
static void Main(string[] args)
{
List<Student> students = FileManager.Load();
PerformancePredictor predictor = new PerformancePredictor(students);

        bool exit = false;

        Console.Title = "📊 Exam Performance Predictor 📊";

        while (!exit)
        {
            DrawBox("PREDICT EXAM PERFORMANCE", new string[]
            {
                "1. Add Student",
                "2. Add Assessment",
                "3. Show Assessments",
                "4. Predict Final Grade",
                "5. Save & Exit"
            });

            Console.Write("Choice: ");
            string choice = Console.ReadLine()!;
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddStudent(students);
                    break;

                case "2":
                    AddAssessment(students);
                    break;

                case "3":
                    ShowAssessments(students);
                    break;

                case "4":
                    PredictGrades(students, predictor);
                    break;

                case "5":
                    FileManager.Save(students);
                    exit = true;
                    break;

                default:
                    PrintError("❌ Invalid choice. Please select 1-5.");
                    break;
            }

            Pause();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ Data saved. Goodbye!");
        Console.ResetColor();
    }

    static void DrawBox(string title, string[] options)
    {
        Console.Clear();
        int width = Math.Max(title.Length, options.Max(o => o.Length)) + 6;
        string topBottom = "╔" + new string('═', width) + "╗";

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(topBottom);
        Console.WriteLine($"║  {title.PadRight(width - 2)}║");
        Console.WriteLine("╠" + new string('═', width) + "╣");
        Console.ResetColor();

        foreach (var option in options)
        {
            Console.WriteLine($"║  {option.PadRight(width - 2)}║");
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╚" + new string('═', width) + "╝\n");
        Console.ResetColor();
    }

    static void Pause()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.ResetColor();
    }

    static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    static void AddStudent(List<Student> students)
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine()!;
        Console.Write("Enter student ID: ");
        string id = Console.ReadLine()!;

        students.Add(new Student(name, id));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✅ Student added successfully!");
        Console.ResetColor();
    }

    static void AddAssessment(List<Student> students)
    {
        if (students.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️ No students yet. Add student first.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("Select student by index:");
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i}. {students[i].Name} (ID: {students[i].ID})");
        }

        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= students.Count)
        {
            PrintError("❌ Invalid index. Try again:");
        }

        Student selectedStudent = students[index];

        Console.Write("Enter subject: ");
        string subject = Console.ReadLine()!;

        Console.WriteLine("Select assessment type: 1. Quiz 2. Assignment 3. Test");
        string typeChoice = Console.ReadLine()!;

        double score;
        while (!double.TryParse(Console.ReadLine(), out score) || score < 0 || score > 100)
        {
            PrintError("❌ Invalid score. Enter 0-100:");
        }

        Assessment? assessment = typeChoice switch
        {
            "1" => new Quiz(subject, score),
            "2" => new Assignment(subject, score),
            "3" => new Test(subject, score),
            _ => null
        };

        if (assessment != null)
        {
            selectedStudent.AddAssessment(assessment);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Assessment added successfully!");
            Console.ResetColor();
        }
        else
        {
            PrintError("❌ Invalid assessment type.");
        }
    }

    static void ShowAssessments(List<Student> students)
    {
        if (students.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️ No students or assessments to show.");
            Console.ResetColor();
            return;
        }

        foreach (var student in students)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n📘 Student: {student.Name} (ID: {student.ID})");
            Console.ResetColor();

            Console.WriteLine("{0,-15} {1,-12} {2,6}", "Subject", "Type", "Score");
            foreach (var a in student.Assessments)
            {
                Console.WriteLine("{0,-15} {1,-12} {2,6:F2}", a.Subject, a.GetType().Name, a.Score);
            }
        }
    }

    static void PredictGrades(List<Student> students, PerformancePredictor predictor)
    {
        if (students.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️ No students or assessments to predict.");
            Console.ResetColor();
            return;
        }

        predictor.PredictGrades();

        double classAverage = students.Count > 0 ? students.Average(s => s.PredictFinalGrade()) : 0;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n🎯 Class Average: {classAverage:F2}");
        Console.ResetColor();
    }
}


}
