using System;

namespace ExamPerformancePredictor
{
public abstract class Assessment
{
public string Subject { get; set; }
public double Score { get; set; }


    public Assessment(string subject, double score)
    {
        Subject = subject;
        Score = score;
    }

    public abstract double GetWeightedScore();
}

public class Quiz : Assessment
{
    public Quiz(string subject, double score) : base(subject, score) { }

    public override double GetWeightedScore()
    {
        return Score * 0.2; // Quiz weight: 20%
    }
}

public class Assignment : Assessment
{
    public Assignment(string subject, double score) : base(subject, score) { }

    public override double GetWeightedScore()
    {
        return Score * 0.3; // Assignment weight: 30%
    }
}

public class Test : Assessment
{
    public Test(string subject, double score) : base(subject, score) { }

    public override double GetWeightedScore()
    {
        return Score * 0.5; // Test weight: 50%
    }
}


}
