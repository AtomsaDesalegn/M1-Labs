using System;

namespace M1LabSession2
{
    public class EnrollmentService
    {
        public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
        {
            if (student is null) throw new ArgumentNullException(nameof(student));
            if (course is null) throw new ArgumentNullException(nameof(course));
            if (course.Capacity <= 0) throw new InvalidOperationException("Course is full.");

            string standing = student.GPA switch
            {
                >= 3.5m => "Honors",
                >= 2.5m => "Good Standing",
                _ => "Academic Warning"
            };
            Console.WriteLine($"{student.Name} is in {standing}.");

            return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
        }
    }

    // public class Student
    // {
    //     public string Id { get; set; } = string.Empty;
    //     public string Name { get; set; } = string.Empty;
    //     public int Age { get; set; }
    //     public decimal GPA { get; set; }
    // }

    public class Course
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }

    public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);
}