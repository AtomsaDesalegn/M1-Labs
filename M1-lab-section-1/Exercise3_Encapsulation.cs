using System;

namespace TmsCore.Exercises.M1_Lab_Section_1
{
    // ==========================================
    // 1. COURSE CLASS (New Exercise)
    // ==========================================
    public class Course
    {
        private int _capacity; // Manual backing field
        public int Capacity
        {
            get => _capacity;
            set
            {
                _capacity = value;
            }
        }
    }

    // ==========================================
    // 2. ENROLLMENT CLASS & RECORD
    // ==========================================
    public class Enrollment
    {
        /* public string StudentId {get; set;} = string.Empty;
        public string CourseCode {get; set;} = string.Empty;
        public DateTime ProcessedAt {get; set;} = DateTime.Now; */
        

        // 2. Public Auto-Implemented Property
        

        // 3. Encapsulated Property (The Gatekeeper)
        public string Status { get; set; } = "Pending"; 

        // 4. Constructor
        public Enrollment(string studentId, string courseCode)
        {
            // Left blank as per your design
        }
    }

    public record EnrollmentRecord(string StudentId, string CourseCode, DateTime ProcessedAt);

    // ==========================================
    // 3. EXERCISE RUNNER
    // ==========================================
    public static class Exercise3
    {
        public static void Run()
        {
            // Enrollment Record Testing
            var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.Now);
            Console.WriteLine(enrollment);

            var corrected = enrollment with { CourseCode = "CS-402" };
            Console.WriteLine(corrected);

            // Value equality testing
            var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.ProcessedAt);
            Console.WriteLine($"Same data? {enrollment == duplicate}"); // True

            // Course Testing
            var course = new Course();
            course.Capacity = 45;
            Console.WriteLine($"Course Capacity Set To: {course.Capacity}");
        }
    }
}