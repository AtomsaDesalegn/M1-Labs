using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TmsCore.Exercises
{
    // =========================================================================
    // EXERCISE 7 STEP 1: CUSTOM DOMAIN EXCEPTIONS
    // =========================================================================
    public class TmsDatabaseException : Exception
    {
        public string Operation { get; }
        
        public TmsDatabaseException(string operation, string message) : base(message)
        {
            Operation = operation;
        }
        
        public TmsDatabaseException(string operation, string message, Exception innerException) 
            : base(message, innerException)
        {
            Operation = operation;
        }
    }

    public class CapacityReachedException : InvalidOperationException
    {
        public string CourseCode { get; }

        public CapacityReachedException(string courseCode) 
            : base($"Course {courseCode} has reached maximum capacity.")
        {
            CourseCode = courseCode;
        }

        public CapacityReachedException(string courseCode, Exception innerException) 
            : base($"Course {courseCode} has reached maximum capacity.", innerException)
        {
            CourseCode = courseCode;
        }
    }

    // =========================================================================
    // EXERCISE 7 ENGINE & REPORT EXECUTION
    // =========================================================================
    public class Exercise7Exceptions
    {
        public static void Run()
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("========================================");
            Console.WriteLine("     EXERCISE 7: CUSTOM EXCEPTIONS      ");
            Console.WriteLine("========================================");

            // Mocked parallel loaded data from Session 3 requirements
            var studentList = new List<Student7>
            {
                new() { Id = "S1", Name = "Student-S1", GPA = 3.8m },
                new() { Id = "S2", Name = "Student-S2", GPA = 2.4m },
                new() { Id = "S3", Name = "Student-S3", GPA = 3.5m },
                new() { Id = "S4", Name = "Student-S4", GPA = 1.9m },
                new() { Id = "S5", Name = "Student-S5", GPA = 3.2m }
            };

            var enrollCourse = new Course7 { Code = "CRS-101", Capacity = 2 };
            var failures = new List<string>();
            int successfulEnrollments = 0;

            Console.WriteLine("\nProcessing Registrations...");
            foreach (var student in studentList)
            {
                try
                {
                    if (enrollCourse.EnrolledCount >= enrollCourse.Capacity)
                    {
                        throw new CapacityReachedException(enrollCourse.Code);
                    }

                    enrollCourse.EnrolledCount++;
                    successfulEnrollments++;
                    Console.WriteLine($" Enrolled: {student.Name}");
                }
                catch (CapacityReachedException ex)
                {
                    failures.Add($"{student.Name}: {ex.Message}");
                    Console.WriteLine($" Rejected: {student.Name} [Course: {ex.CourseCode}] {ex.Message}");
                }
            }

            // =========================================================================
            // EXERCISE 7B: THE ENROLLMENT SUMMARY REPORT
            // =========================================================================
            sw.Stop();

            decimal classAverage = studentList.Count > 0 
                ? studentList.Average(s => s.GPA) 
                : 0m;

            Console.WriteLine("\n========== ENROLLMENT SUMMARY ==========");
            Console.WriteLine($"Total students loaded:  {studentList.Count}");
            Console.WriteLine($"Successful enrollments: {successfulEnrollments}");
            Console.WriteLine($"Failed enrollments:     {failures.Count}");
            Console.WriteLine($"Class average GPA:      {classAverage:F2}");
            Console.WriteLine($"Total elapsed time:     {sw.ElapsedMilliseconds}ms");
            
            if (failures.Count > 0)
            {
                Console.WriteLine("\n--- Failure Details---");
                foreach (var failure in failures)
                {
                    Console.WriteLine($" {failure}");
                }
            }
            Console.WriteLine("========================================");
        }
    }

    public class Student7 { public string Id { get; set; } = ""; public string Name { get; set; } = ""; public decimal GPA { get; set; } }
    public class Course7 { public string Code { get; set; } = ""; public int Capacity { get; set; } public int EnrolledCount { get; set; } }
}