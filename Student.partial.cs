using System;
using System.Linq;

namespace ExamPerformancePredictor
{
public partial class Student
{
public void AddAssessment(Assessment assessment)
{
Assessments.Add(assessment);
}

    public double PredictFinalGrade()
    {
        if (Assessments.Count == 0) return 0;
        return Assessments.Sum(a => a.GetWeightedScore());
    }
}


}
