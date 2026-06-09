using System;

// =================================================================
// BLOCK 1: Exercise 3 Part 3 Namespace (For the Student class)
// =================================================================
namespace TmsCore.Exercises.M1_Lab_Section_1.Exercise3Part3
{
    public class Student
    {
        public required string Id { get; init; }

        public required string Name
        {
            get;
            set => field = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));
        }

        public int Age
        {
            get;
            set => field = value is >= 16 and <= 100
                ? value
                : throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 16 and 100.");
        }

        public decimal GPA
        {
            get;
            set => field = value is >= 0.0m and <= 4.0m
                ? value
                : throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0 and 4.0.");
        }
    }
} // <--- Closes Block 1 perfectly!

// =================================================================
// BLOCK 2: Main Lab Namespace (For the IGradable Interface & Types)
// =================================================================
namespace TmsCore.Exercises.M1_Lab_Section_1
{
    // 1. Define the Contract Interface
    public interface IGradable
    {
        string Title { get; }
        decimal CalculateGrade();
    }

    // 2. First implementation: Quiz
    public class Quiz : IGradable
    {
        public required string Title { get; init; }
        public required int CorrectAnswers { get; init; }
        public required int TotalQuestions { get; init; }

        public decimal CalculateGrade()
        {
            if (TotalQuestions == 0) return 0m;
            return (decimal)CorrectAnswers / TotalQuestions * 100m;
        }
    }

    // 3. Second implementation: LabAssignment
    public class LabAssignment : IGradable
    {
        public required string Title { get; init; }
        public required decimal FunctionalityScore { get; init; }
        public required decimal CodeQualityScore { get; init; }

        public decimal CalculateGrade()
        {
            // 70% functionality, 30% code quality
            return (FunctionalityScore * 0.7m) + (CodeQualityScore * 0.3m);
        }
    }
} // <--- Closes Block 2 perfectly!