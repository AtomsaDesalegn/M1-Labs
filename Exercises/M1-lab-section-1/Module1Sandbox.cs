using System;

namespace TmsCore.Exercises.M1_Lab_Section_1
{
    // Interface Contract (LO 1.3/1.4)
    public interface IStudentModel
    {
        int Id { get; }
        string Name { get; }
        string? Region { get; } // '?' Allows null values safely
        decimal Balance { get; } // 'decimal' fixes the floating-point bug
    }

    public class Student : IStudentModel
    {
        public int Id { get; }
        public string Name { get; }
        public string? Region { get; } 
        public decimal Balance { get; private set; } 

        public Student(int id, string name, string? region, decimal initialBalance)
        {
            Id = id;
            Name = name;
            Region = region;
            Balance = initialBalance;
        }
    }

    public static class Module1Sandbox
    {
        public static void RunExercisses()
        {
            Console.WriteLine("=== Running Module 1 Exercises ===");

            // --- Bug Fix 1: Financial Precision ---
            // The legacy system used 'double', causing a 0.03 Birr error.
            // By using 'decimal' (noted by the 'm' suffix), the math is completely exact.
            decimal balance1 = 0.01m;
            decimal balance2 = 0.02m;
            decimal totalBalance = balance1 + balance2;
            Console.WriteLine($"[Audit Check] Precise Balance: {totalBalance} Birr (Exactly 0.03)");

            // --- Bug Fix 2: Null Safety ---
            // If region is null, using '??' provides a fallback string instead of throwing a NullReferenceException.
            Student sampleStudent = new Student(101, "Atomsa", null, 500.00m);
            string regionDisplay = sampleStudent.Region ?? "Unknown Region";
            Console.WriteLine($"Student Region: {regionDisplay}");
            
            Console.WriteLine("=================================");
        }
    }
}