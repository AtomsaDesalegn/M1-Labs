using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TmsCore.Exercises
{
    public class Exercise6Async
    {
        // The main entry method called by the root Program.cs
        public static async Task Run()
        {
            // ==========================================
            // STEP 1: See Thread Starvation in Numbers
            // ==========================================
            Console.WriteLine("--- STEP 1: Thread Starvation in Numbers ---");

            // THE WRONG WAY: Blocking with Thread.Sleep
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(300); // Thread is HELD for 300ms cannot serve anyone else
            }
            Console.WriteLine($"Blocking sequential: {sw.ElapsedMilliseconds}ms");

            // ASYNC BUT STILL SEQUENTIAL: Thread released, but calls are one-at-a-time
            sw.Restart();
            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(300); // Thread released while waiting but still sequential
            }
            Console.WriteLine($"Async sequential: {sw.ElapsedMilliseconds}ms");

            // THE RIGHT WAY: Async parallel all 5 start simultaneously
            sw.Restart();
            var tasks = Enumerable.Range(0, 5).Select(_ => Task.Delay(300));
            await Task.WhenAll(tasks);
            Console.WriteLine($"Async parallel: {sw.ElapsedMilliseconds}ms\n");


            // ==========================================
            // STEP 3: Load Data in Parallel (Part A)
            // ==========================================
            Console.WriteLine("--- STEP 3: Loading Data in Parallel ---");
            sw.Restart();

            string[] studentIds = ["S1", "S2", "S3", "S4", "S5"];
            string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"];

            // Start all fetches simultaneously
            var studentTasks = studentIds.Select(id => FetchStudentAsync(id));
            var courseTasks = courseCodes.Select(code => FetchCourseAsync(code));

            // Both arrays load concurrently
            Student[] students = await Task.WhenAll(studentTasks);
            Course[] courses = await Task.WhenAll(courseTasks);

            Console.WriteLine($"\nLoaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms");
            foreach (var s in students)
            {
                Console.WriteLine($" {s.Name} GPA: {s.GPA}");
            }
            Console.WriteLine();


            // ==========================================
            // EXERCISE 6 PART B: The TMS Enrollment Engine
            // ==========================================
            Console.WriteLine("--- PART B: TMS Enrollment Engine ---");
            var enrollCourse = new Course { Code = "CRS-101", Title = "C# Mastery", Capacity = 2 };
            var enrollService = new EnrollmentService();
            var enrollments = new List<EnrollmentRecord>();
            var failures = new List<string>();

            sw.Restart();
            foreach (var student in students)
            {
                try
                {
                    var record = enrollService.ProcessRegistration(student, enrollCourse);
                    enrollCourse.EnrolledCount++;
                    enrollments.Add(record);
                    Console.WriteLine($" Enrolled: {student.Name}");
                    
                    // Exercise 6B: Safe Fire-and-Forget Notification (Optional)
                    _ = SendConfirmationAsync(student);
                }
                catch (InvalidOperationException ex)
                {
                    failures.Add($"{student.Name}: {ex.Message}");
                    Console.WriteLine($" Rejected: {student.Name} {ex.Message}");
                }
            }

            // Give Fire-and-Forget a brief moment to log emails before exiting method
            await Task.Delay(150); 
        }

        // ==========================================
        // STEP 2 & 6B: Async Helper Methods
        // ==========================================
        private static async Task<Student> FetchStudentAsync(string id)
        {
            Console.WriteLine($" Fetching {id}...");
            await Task.Delay(300); // Simulate database latency
            return new Student
            {
                Id = id,
                Name = $"Student-{id}",
                Age = 20,
                GPA = id switch
                {
                    "S1" => 3.8m,
                    "S2" => 2.4m,
                    "S3" => 3.5m,
                    "S4" => 1.9m,
                    "S5" => 3.2m,
                    _ => 2.5m
                }
            };
        }

        private static async Task<Course> FetchCourseAsync(string code)
        {
            Console.WriteLine($" Fetching course {code}...");
            await Task.Delay(200); // Simulate database latency
            return new Course
            {
                Code = code,
                Title = $"Course-{code}",
                Capacity = code switch
                {
                    "CRS-101" => 2,
                    "CRS-201" => 30,
                    "CRS-301" => 15,
                    _ => 25
                }
            };
        }

        private static async Task SendConfirmationAsync(Student student)
        {
            try
            {
                await Task.Delay(100); // Simulate sending email
                Console.WriteLine($" Email sent to {student.Name}");
            }
            catch (Exception ex)
            {
                // Log the failure do NOT re-throw.
                Console.WriteLine($" Email failed for {student.Name}: {ex.Message}");
            }
        }
    }

    // ==========================================
    // DOMAIN MODELS & SERVICES 
    // ==========================================
    public class Student
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal GPA { get; set; }
    }

    public class Course
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int EnrolledCount { get; set; } = 0;
    }

    public class EnrollmentRecord
    {
        public string StudentId { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
    }

    public class EnrollmentService
    {
        public EnrollmentRecord ProcessRegistration(Student student, Course course)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (course == null) throw new ArgumentNullException(nameof(course));

            if (course.EnrolledCount >= course.Capacity)
            {
                throw new InvalidOperationException("Course is full.");
            }

            return new EnrollmentRecord
            {
                StudentId = student.Id,
                CourseCode = course.Code,
                EnrollmentDate = DateTime.Now
            };
        }
    }
}